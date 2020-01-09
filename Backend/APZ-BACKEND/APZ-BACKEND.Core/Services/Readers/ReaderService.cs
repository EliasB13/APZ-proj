using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APZ_BACKEND.Core.Dtos.Readers;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Entities;
using APZ_BACKEND.Core.Helpers;
using APZ_BACKEND.Core.Interfaces;
using APZ_BACKEND.Core.Mappers;
using APZ_BACKEND.Core.Services.Communication;
using APZ_BACKEND.Core.Services.Users;

namespace APZ_BACKEND.Core.Services.Readers
{
	public class ReaderService : IReaderService
	{
		private readonly ISharedItemsRepository sharedItemsRepository;
		private readonly IItemsTakingsRepository itemTakingsRepository;
		private readonly IAsyncRepository<ItemTakingLine> itemTakingLinesRepository;
		private readonly IAsyncRepository<PrivateUser> privateUsersRepository;
		private readonly IAsyncRepository<Reader> readersRepository;
		private readonly IAsyncRepository<BusinessUser> businessUsersRepository;

		public ReaderService(ISharedItemsRepository sharedItemsRepository,
			IAsyncRepository<ItemTakingLine> itemTakingLinesRepository,
			IAsyncRepository<PrivateUser> privateUsersRepository,
			IItemsTakingsRepository itemTakingsRepository,
			IAsyncRepository<Reader> readersRepository,
			IAsyncRepository<BusinessUser> businessUsersRepository)
		{
			this.sharedItemsRepository = sharedItemsRepository;
			this.itemTakingLinesRepository = itemTakingLinesRepository;
			this.privateUsersRepository = privateUsersRepository;
			this.itemTakingsRepository = itemTakingsRepository;
			this.readersRepository = readersRepository;
			this.businessUsersRepository = businessUsersRepository;
		}

		public async Task<GenericServiceResponse<SharedItemDto>> AddItemToReader(int businessUser, AddItemToReaderRequest request)
		{
			try
			{
				var item = await sharedItemsRepository.SingleOrDefaultAsync(si => si.Id == request.SharedItemId, si => si.Reader);
				if (item == null)
					return new GenericServiceResponse<SharedItemDto>($"Item with id {request.SharedItemId} wasn't found", ErrorCode.ITEM_NOT_FOUND);

				var isItemTaken = await itemTakingLinesRepository.AnyAsync(itl => itl.SharedItemId == item.Id && itl.IsTaken);
				if (isItemTaken)
					return new GenericServiceResponse<SharedItemDto>($"Item is taken", ErrorCode.ITEM_IS_TAKEN);

				if (item.Reader != null)
				{
					if (item.Reader.Id == request.ReaderId)
						return new GenericServiceResponse<SharedItemDto>($"Item with id {request.SharedItemId} already binded to reader with id: {request.ReaderId}", ErrorCode.ITEM_ALREADY_IN_READER);

					return new GenericServiceResponse<SharedItemDto>($"Item already added to another reader", ErrorCode.ITEM_IN_ANOTHER_READER);
				}

				var reader = await readersRepository.GetByIdAsync(request.ReaderId);
				if (reader == null)
					return new GenericServiceResponse<SharedItemDto>($"Reader with id {request.ReaderId} wasn't found", ErrorCode.READER_NOT_FOUND);

				item.Reader = reader;

				await sharedItemsRepository.UpdateAsync(item);
				return new GenericServiceResponse<SharedItemDto>(item.ToDto(false));
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItemDto>("Error | Adding item to reader: " + ex.Message, ErrorCode.COMMON_ERROR);
			}
		}

		public async Task<IEnumerable<ReaderItemDto>> GetReaderItems(int readerId, string secret)
		{
			var reader = await readersRepository.GetByIdAsync(readerId);
			if (reader == null)
				return new List<ReaderItemDto>();

			if (!UsersExtensions.VerifyHash(secret, reader.SecretHash, reader.SecretSalt))
				return new List<ReaderItemDto>();

			var items = await sharedItemsRepository.ListAllAsync(si => si.Reader.Id == readerId, si => si.Reader);
			var dtos = items.Select(i => i.ToDto());
			return dtos;
		}

		public async Task<GenericServiceResponse<PrivateUserAccountData>> OrderCard(int privateUserId)
		{
			try
			{
				var pUser = await privateUsersRepository.GetByIdAsync(privateUserId);
				if (pUser == null)
					return new GenericServiceResponse<PrivateUserAccountData>($"Private user with id: {privateUserId}",
						ErrorCode.CONTEXT_USER_NOT_FOUND);

				string rfid = Guid.NewGuid().ToString().Substring(0, 20);

				pUser.RfidNumber = rfid;
				await privateUsersRepository.UpdateAsync(pUser);
				return new GenericServiceResponse<PrivateUserAccountData>(pUser.ToAccountData());
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<PrivateUserAccountData>("Error | Ordering card: " + ex.Message, ErrorCode.COMMON_ERROR);
			}
		}

		public async Task<GenericServiceResponse<ReaderDto>> OrderReader(int businessUserId)
		{
			try
			{
				var bUser = await businessUsersRepository.GetByIdAsync(businessUserId);
				if (bUser == null)
					return new GenericServiceResponse<ReaderDto>($"Business user with id: {businessUserId}", 
						ErrorCode.CONTEXT_USER_NOT_FOUND);

				//Guid secret = Guid.NewGuid();
				string secret = "reader-secret-key";

				byte[] secretHash, secretSalt;
				UsersExtensions.CreateHash(secret.ToString(), out secretHash, out secretSalt);

				var reader = new Reader()
				{
					BusinessUser = bUser,
					SecretHash = secretHash,
					SecretSalt = secretSalt,
				};

				var dbReader = await readersRepository.AddAsync(reader);
				return new GenericServiceResponse<ReaderDto>(dbReader.ToDto());
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<ReaderDto>("Error | Ordering reader: " + ex.Message, ErrorCode.COMMON_ERROR);
			}
		}

		public async Task<GenericServiceResponse<SharedItemDto>> TakeItem(TakeItemRequest request)
		{
			try
			{
				var user = await privateUsersRepository
					.SingleOrDefaultAsync(pu => pu.RfidNumber == request.UserRfid);
				if (user == null)
					return new GenericServiceResponse<SharedItemDto>("User wasn't found", ErrorCode.USER_NOT_FOUND);

				var item = await sharedItemsRepository
					.SingleOrDefaultAsync(si => si.RfidNumber == request.ItemRfid, si => si.Reader);
				if (item == null)
					return new GenericServiceResponse<SharedItemDto>("Item wasn't found", ErrorCode.USER_NOT_FOUND);

				if (item.Reader == null)
					return new GenericServiceResponse<SharedItemDto>("Item with item doesn't belong to reader", 
						ErrorCode.ITEM_NOT_IN_READER);

				if (item.Reader != null)
				{
					if (item.Reader.Id != request.ReaderId)
						return new GenericServiceResponse<SharedItemDto>("Item with item doesn't belong to reader", 
							ErrorCode.ITEM_NOT_IN_READER);
				}

				var isItemTaken = await itemTakingLinesRepository.AnyAsync(itl => itl.SharedItemId == item.Id && itl.IsTaken);
				if (isItemTaken)
					return new GenericServiceResponse<SharedItemDto>("Item already taken", ErrorCode.ITEM_ALREADY_TAKEN);

				var isItemAvailiableForUser = await sharedItemsRepository.IsItemAvailableForUser(item.Id, user.Id);
				if (!isItemAvailiableForUser)
					return new GenericServiceResponse<SharedItemDto>("You don't have permissions", ErrorCode.NO_ACCESS);

				var reader = await readersRepository.GetByIdAsync(request.ReaderId);
				if (reader == null)
					return new GenericServiceResponse<SharedItemDto>("Reader wasn't found", ErrorCode.READER_NOT_FOUND);

				if (!UsersExtensions.VerifyHash(request.SecretKey, reader.SecretHash, reader.SecretSalt))
					return new GenericServiceResponse<SharedItemDto>("Wrong credentials", ErrorCode.NO_ACCESS);

				var itemTaking = new ItemTaking
				{
					TakingTime = DateTime.Now,
					PrivateUser = user,
					ItemTakingLines = new List<ItemTakingLine>
					{
						new ItemTakingLine()
						{
							IsTaken = true,
							SharedItem = item
						}
					}
				};

				await itemTakingsRepository.AddAsync(itemTaking);
				return new GenericServiceResponse<SharedItemDto>(item.ToDto(true));
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItemDto>("Error | Taking item: " + ex.Message, ErrorCode.COMMON_ERROR);
			}
		}

		public async Task<GenericServiceResponse<SharedItemDto>> ReturnItem(ReturnItemRequest request)
		{
			try
			{
				//var user = await privateUsersRepository
				//	.SingleOrDefaultAsync(pu => pu.RfidNumber == request.UserRfid);
				//if (user == null)
				//	return new GenericServiceResponse<SharedItemDto>("User wasn't found", ErrorCode.USER_NOT_FOUND);

				var item = await sharedItemsRepository
					.SingleOrDefaultAsync(si => si.RfidNumber == request.ItemRfid, si => si.Reader);
				if (item == null)
					return new GenericServiceResponse<SharedItemDto>("Item wasn't found", ErrorCode.ITEM_NOT_FOUND);


				if (item.Reader == null)
					return new GenericServiceResponse<SharedItemDto>("Item with item doesn't belong to reader", 
						ErrorCode.ITEM_NOT_IN_READER);

				if (item.Reader != null)
				{
					if (item.Reader.Id != request.ReaderId)
						return new GenericServiceResponse<SharedItemDto>("Item with item doesn't belong to reader", 
							ErrorCode.ITEM_NOT_IN_READER);
				}

				var itemTakingLine = await itemTakingLinesRepository.SingleOrDefaultAsync(itl => itl.SharedItem.Id == item.Id && !itl.IsReturned);
				if (itemTakingLine == null)
					return new GenericServiceResponse<SharedItemDto>("Item wasn't taken", ErrorCode.ITEM_NOT_TAKEN_BY_CURRENT_USER);

				//var isItemAvailiableForUser = await sharedItemsRepository.IsItemAvailableForUser(item.Id, user.Id);
				//if (!isItemAvailiableForUser)
				//	return new GenericServiceResponse<SharedItemDto>("You don't have permissions", ErrorCode.NO_ACCESS);

				//var itemTaking = await itemTakingsRepository.GetItemTakingByUserAndItem(user.Id, item.Id);
				//if (itemTaking == null)
				//	return new GenericServiceResponse<SharedItemDto>("This item wasn't taken by this user", 
				//		ErrorCode.ITEM_NOT_TAKEN_BY_CURRENT_USER);

				var reader = await readersRepository.GetByIdAsync(request.ReaderId);
				if (reader == null)
					return new GenericServiceResponse<SharedItemDto>("Reader wasn't found", ErrorCode.READER_NOT_FOUND);

				if (!UsersExtensions.VerifyHash(request.SecretKey, reader.SecretHash, reader.SecretSalt))
					return new GenericServiceResponse<SharedItemDto>("Wrong credentials", ErrorCode.NO_ACCESS);

				//var itemTakingLine = itemTaking.ItemTakingLines.SingleOrDefault(itl => itl.SharedItemId == item.Id);
				itemTakingLine.IsReturned = true;
				itemTakingLine.IsTaken = false;
				itemTakingLine.ReturningTime = DateTime.Now;

				await itemTakingLinesRepository.UpdateAsync(itemTakingLine);
				return new GenericServiceResponse<SharedItemDto>(item.ToDto(false));
			}
			catch (Exception ex)
			{
				return new GenericServiceResponse<SharedItemDto>("Error | Taking item: " + ex.Message, ErrorCode.COMMON_ERROR);
			}
		}

		public async Task<IEnumerable<ReaderDto>> GetReaders(int businessUserId)
		{
			var readers = await readersRepository.ListAllAsync(r => r.BusinessUser.Id == businessUserId, r => r.BusinessUser);
			var dtos = readers.Select(r => r.ToDto());		
			
			return dtos;
		}

		public async Task<GenericServiceResponse<SharedItemDto>> RemoveReaderItem(int businessUserId, int itemId)
		{
			var user = await businessUsersRepository.GetByIdAsync(businessUserId);
			if (user == null)
				return new GenericServiceResponse<SharedItemDto>("Business user wasn't found", ErrorCode.CONTEXT_USER_NOT_FOUND);

			var item = await sharedItemsRepository.SingleOrDefaultAsync(si => si.Id == itemId, si => si.Reader);
			if (item == null)
				return new GenericServiceResponse<SharedItemDto>("Item wasn't found", ErrorCode.ITEM_NOT_FOUND);

			item.Reader = null;
			await sharedItemsRepository.UpdateAsync(item);

			return new GenericServiceResponse<SharedItemDto>(item.ToDto(false));
		}
	}
}
