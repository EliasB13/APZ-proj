﻿using System;
using System.Collections.Generic;
using System.Text;

namespace APZ_Iot.Menu
{
	public class MenuItem
	{
		public virtual string Text { get; set; }

		public virtual int? SubMenuId { get; set; }

		public virtual Action Action { get; set; }

		public static MenuItem CreateWithAction(string title, Action action)
		{
			return new MenuItem()
			{
				Text = title,
				Action = action
			};
		}

		public static MenuItem CreateWithSubMenu(string title, int subMenuId)
		{
			return new MenuItem()
			{
				Text = title,
				SubMenuId = subMenuId
			};
		}
	}
}