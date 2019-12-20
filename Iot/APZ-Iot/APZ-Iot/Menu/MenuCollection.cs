using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APZ_Iot.Menu
{
	public class MenuCollection
	{
		public MenuCollection()
		{
			Menus = new List<Menu>();
		}

		public virtual List<Menu> Menus { get; set; }

		public virtual void AddMenu(Menu menu)
		{
			Menus.Add(menu);
		}

		public virtual void ShowMenu(int id)
		{
			var currentMenu = Menus.Where(m => m.MenuId == id).Single();
			currentMenu.PrintToConsole();

			string choice = Console.ReadLine();

			if (!int.TryParse(choice, out int choiceIndex) || choiceIndex < 0 || choiceIndex >= currentMenu.MenuItems.Count)
			{
				Console.Clear();

				Console.WriteLine("Invalid selection - try again");
				ShowMenu(id);
			}
			else
			{
				var menuItemSelected = currentMenu.MenuItems[choiceIndex];

				if (menuItemSelected.SubMenuId.HasValue)
				{
					Console.Clear();
					ShowMenu(menuItemSelected.SubMenuId.Value);
				}
				else
				{
					menuItemSelected.Action();
				}
			}
		}
	}
}
