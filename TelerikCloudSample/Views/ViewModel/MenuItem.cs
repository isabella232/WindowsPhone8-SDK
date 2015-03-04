﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Telerik.Windows.Controls.Cloud.Sample.ViewModel
{
	public class MenuItem
	{

		#region Properties

		public string Label { get; set; }

		public string Description { get; set; }

		public string PageUrl { get; set; }

		public object Value { get; set; }

		public string IconPath { get; set; }

		#endregion

		#region Constructors and Initialization

		public MenuItem()
		{

		}

		public MenuItem(string label, string description, string pageUrl, string iconPath)
			: this(label, description, pageUrl, iconPath, null)
		{
			
		}

		public MenuItem(string label, string description, string pageUrl, string iconPath, object value)
		{
			this.Label = label;
			this.Description = description;
			this.PageUrl = pageUrl;
			this.IconPath = iconPath;
			this.Value = value;
		}

		#endregion

		public ImageSource Icon
		{
			get
			{
				return new BitmapImage(new Uri(this.IconPath, UriKind.Relative));
			}
		}
	}
}
