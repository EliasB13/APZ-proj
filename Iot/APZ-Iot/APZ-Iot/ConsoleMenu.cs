using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_Iot
{
	public static class ConsoleMenu
	{
		public static void DrawMenu(List<string> menuItems)
		{
			for (int menuNumber = 1; menuNumber <= menuItems.Count; ++menuNumber)
				Console.WriteLine($"{menuNumber}. {menuItems[menuNumber - 1]}");
			Console.WriteLine("\nType 'off' to turn off reader");
		}

		public static void DrawError(string error)
		{
			Console.Clear();
			Console.WriteLine($"ERROR | {error}");
			Console.WriteLine("\nPress any key to continue...");
			Console.ReadLine();
		}
	}
}
