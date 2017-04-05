// This file has been autogenerated from a class added in the UI designer.

using System;

using Foundation;
using OfflineFirstReferenceArch.Models;
using UIKit;

namespace OfflineFirstReferenceArch.IOS
{


	public partial class WidgetCell : UITableViewCell
	{
		public static string Key = "WidgetCell";

		public WidgetCell (IntPtr handle) : base (handle)
		{
		}

		public void ConfigureCell(Widget widget)
		{
			widgetNameLabel.Text = widget.Name;
		}
}
}
