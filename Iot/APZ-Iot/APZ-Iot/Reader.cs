using APZ_Iot.Configuration;
using APZ_Iot.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Flurl.Http;
using System.Linq;
using Flurl;
using System.Threading.Tasks;
using System.Threading;

namespace APZ_Iot
{
	public class Reader
	{
		private int ReaderId { get; set; }
		private string SecretKey { get; set; }
		private string ConnectionString { get; set; }
		private List<ItemDto> BindedReaderItems { get; set; }
		private List<string> ActualReaderItems { get; set; }

		public void LoadSettings(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new ArgumentException($"File '{fileName}' wasn't found");
			}

			var rs = JsonConvert.DeserializeObject<ReaderSettings>(File.ReadAllText(fileName));
			ReaderId = rs.ReaderId;
			SecretKey = rs.SecretKey;
			ConnectionString = rs.ConnectionString;
		}

		public void Start()
		{
			Console.WriteLine("INIT  | Starting reader...");
			GetReaderItems().Wait();
			CheckItemsAvailability();
			LaunchUI();
		}

		private async Task GetReaderItems()
		{
			try
			{
				Console.WriteLine("INIT  | Loading binded items from backend");

				BindedReaderItems = await ConnectionString
					.AppendPathSegments("api", "Readers", "reader-items")
					.PostJsonAsync(new { readerId = ReaderId, secret = SecretKey }).ReceiveJson<List<ItemDto>>();
			}
			catch (FlurlHttpException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		private async Task<dynamic> ReturnItem(string rfid)
		{
			try
			{
				var res = await ConnectionString
					.AppendPathSegments("api", "Readers", "return-item")
					.AllowHttpStatus("400-404")
					.PostJsonAsync(new { itemRfid = rfid, secretKey = SecretKey, readerId = ReaderId }).ReceiveJson();
				if (res == null)
					ActualReaderItems.Add(rfid);
				return res;
			}
			catch (FlurlHttpException ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
				return null;
			}
		}

		private async Task<dynamic> TakeItem(string itemRfId, string userRfid)
		{
			try
			{
				var result = await ConnectionString
					.AppendPathSegments("api", "Readers", "take-item")
					.AllowHttpStatus("400-404")
					.PostJsonAsync(new { itemRfid = itemRfId, userRfid = userRfid, secretKey = SecretKey, readerId = ReaderId }).ReceiveJson();
				if (result == null)
					ActualReaderItems.Remove(itemRfId);
				return result;
			}
			catch (FlurlHttpException ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
				return null;
			}
		}

		private void CheckItemsAvailability()
		{
			if (BindedReaderItems.Count == 0)
			{
				Console.WriteLine("INIT  | The items were not binded to reader. Turning on.");
				Environment.Exit(1);
			}

			ActualReaderItems = ItemsRfidSensor.ReadActualItems(BindedReaderItems);

			BindedReaderItems.ForEach(i =>
			{
				bool isAllItemsBinded = true;
				if (!ActualReaderItems.Contains(i.RfId))
				{
					Console.WriteLine($"ERROR | Item '{i.Id} - {i.Name}' isn't on the reader");
					isAllItemsBinded = false;
				}
				if (!isAllItemsBinded) Environment.Exit(1); 
			});

			Console.WriteLine("INIT  | All items are available. Reader is ready");
		}

		private void LaunchUI()
		{
			Console.WriteLine("\nPress any key to start");
			Console.ReadLine();
			Console.Clear();

			LoadMainMenu();
		}

		private void LoadMainMenu()
		{
			var mainMenuItems = new List<string> { "View items", "Return item", "Take item" };
			//ConsoleMenu.DrawMenu(mainMenuItems);

			string input = "";
			int selector = 0;
			bool isNumber = false;

			while (true)
			{
				Console.Clear();
				ConsoleMenu.DrawMenu(mainMenuItems);

				input = Console.ReadLine();
				if (input == "off")
					Environment.Exit(1);

				isNumber = int.TryParse(input, out selector);
				if (isNumber && selector > 0 && selector <= mainMenuItems.Count)
				{
					switch(selector)
					{
						case 1:
							LoadViewItemsMenu();
							Console.Clear();
							ConsoleMenu.DrawMenu(mainMenuItems);
							break;
						case 2:
							LoadReturnItemMenu();
							Console.Clear();
							ConsoleMenu.DrawMenu(mainMenuItems);
							break;
						case 3:
							LoadTakeItemMenu();
							Console.Clear();
							ConsoleMenu.DrawMenu(mainMenuItems);
							break;
					}
				}
				else
				{
					Console.Clear();
					Console.WriteLine("ERROR | Wrong input");
					Console.WriteLine("Press any key to continue...");
					Console.ReadLine();
					ConsoleMenu.DrawMenu(mainMenuItems);
				}
			}
		}

		private void LoadViewItemsMenu()
		{
			Console.Clear();
			string items = "";
			BindedReaderItems
				.Where(bi => ActualReaderItems.Contains(bi.RfId))
				.ToList()
				.ForEach(bi => items += $"\tName: {bi.Name}\n\tDescription: {bi.Description}\n\n");

			string input = "";
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Actual items are: ");
				Console.WriteLine(items);
				Console.WriteLine("0. Return to previous menu");
				Console.WriteLine("\nType 'off' to turn off reader");
				input = Console.ReadLine();
				if (input == "0")
					return;
				else if (input == "off")
					Environment.Exit(1);
				else
					ConsoleMenu.DrawError("Wrong input");
			}
		}

		private void LoadReturnItemMenu()
		{
			string input = "";
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Put item on reader (enter item rfid number):");
				Console.WriteLine("\n\n\n\n0. Return to previous menu");
				Console.WriteLine("\nType 'off' to turn off reader");
				Console.SetCursorPosition(0, 1);

				input = Console.ReadLine();

				if (input == "0")
					return;
				else if (input == "off")
					Environment.Exit(1);
				else if (input.Length != 20)
				{
					ConsoleMenu.DrawError("RFID length is 20 chars");
				}
				else if (input.Length == 20)
				{
					LoadReturnItemMenu(input);
					return;
				}
				else
					ConsoleMenu.DrawError("Wrong input");
			}
		}

		private void LoadTakeItemMenu()
		{
			string input = "";
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Put card on reader (enter user rfid number):");
				Console.WriteLine("\n\n\n\n0. Return to previous menu");
				Console.WriteLine("\nType 'off' to turn off reader");
				Console.SetCursorPosition(0, 1);

				input = Console.ReadLine();

				if (input == "0")
					return;
				else if (input == "off")
					Environment.Exit(1);
				else if (input.Length != 20)
				{
					ConsoleMenu.DrawError("RFID length is 20 chars");
				}
				else if (input.Length == 20)
				{
					LoadSelectItemMenu(input);
					return;
				}
				else
					ConsoleMenu.DrawError("Wrong input");
			}
		}

		private void LoadSelectItemMenu(string userRfid)
		{
			string input = "";
			string itemsString = "";
			var items = BindedReaderItems
				.Where(bi => ActualReaderItems.Contains(bi.RfId))
				.ToList();

			for (int itemNumber = 1; itemNumber <= items.Count(); ++itemNumber)
				itemsString += $"\t{itemNumber}) {items[itemNumber - 1].Name}\n";

			int selector = 0;
			bool isNumber = false;
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Select item from list (type item number):");
				Console.WriteLine(itemsString);
				Console.WriteLine("\n\n\n\n0. Return to previous menu");
				Console.WriteLine("\nType 'off' to turn off reader");
				Console.SetCursorPosition(0, items.Count + 1);

				input = Console.ReadLine();
				isNumber = int.TryParse(input, out selector);

				if (input == "0")
					return;
				else if (input == "off")
					Environment.Exit(1);
				else if (!isNumber || selector < 0 || selector > items.Count)
					ConsoleMenu.DrawError("Wrong input");
				else
				{
					LoadTakeItemMenu(items[selector - 1].RfId, userRfid);
					return;
				}
			}
		}

		private void LoadReturnItemMenu(string itemRfid)
		{
			Console.Clear();
			Console.WriteLine("Sending request");
			var res = ReturnItem(itemRfid).Result;
			if (res == null)
			{
				Console.WriteLine("Success");
				LaunchUI();
			}
			else
			{
				Console.WriteLine("ERROR | " + res.message);
				Console.WriteLine("\nPress any key to continue...");
				Console.ReadLine();
			}
			Console.ReadLine();
		}

		private void LoadTakeItemMenu(string itemRfid, string userRfid)
		{
			Console.Clear();
			Console.WriteLine("Sending request");
			var res = TakeItem(itemRfid, userRfid).Result;
			if (res == null)
			{
				Console.WriteLine("Success");
				LaunchUI();
			}
			else
			{
				Console.WriteLine("ERROR | " + res.message);
				Console.WriteLine("\nPress any key to continue...");
				Console.ReadLine();
			}
		}
	}
}
