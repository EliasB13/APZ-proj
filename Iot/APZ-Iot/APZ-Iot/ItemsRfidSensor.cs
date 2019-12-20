using APZ_Iot.Dto;
using System.Collections.Generic;
using System.Linq;

namespace APZ_Iot
{
	public static class ItemsRfidSensor
	{
		public static List<string> ReadActualItems()
		{
			return new List<string>();
		}
		public static List<string> ReadActualItems(List<ItemDto> seedData)
		{
			return seedData.Select(i => i.RfId ).ToList();
		}

	}
}
