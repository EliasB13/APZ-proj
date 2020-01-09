using APZ_BACKEND.Core.Dtos.Readers;
using APZ_BACKEND.Core.Dtos.SharedItems;
using APZ_BACKEND.Core.Dtos.Users;
using APZ_BACKEND.Core.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APZ_BACKEND.Core.Services.Readers
{
	public interface IReaderService
	{
		Task<IEnumerable<ReaderItemDto>> GetActualReaderItems(int readerId, string secret);
		Task<IEnumerable<ReaderItemDto>> GetReaderItems(int businessUserId, int readerId);
		Task<IEnumerable<ReaderDto>> GetReaders(int businessUserId);
		Task<GenericServiceResponse<SharedItemDto>> AddItemToReader(int businessUserId, AddItemToReaderRequest request);
		Task<GenericServiceResponse<ReaderDto>> OrderReader(int businessUserId);
		Task<GenericServiceResponse<PrivateUserAccountData>> OrderCard(int privateUserId);
		Task<GenericServiceResponse<SharedItemDto>> TakeItem(TakeItemRequest request);
		Task<GenericServiceResponse<SharedItemDto>> ReturnItem(ReturnItemRequest request);
		Task<GenericServiceResponse<SharedItemDto>> RemoveReaderItem(int businessUserId, int itemId);

	}
}
