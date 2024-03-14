using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Dictionar.DataHandling;

namespace Dictionar
{
	public static class Utils
	{
		public enum Pages
		{
			Invalid,
			MainPage,
			DictionaryModePage,
			AdministratorPage,
			GamePage
		};

		public enum Windows
		{
			Invalid,
			MainWindow,
			TextInputWindow
		}

		public enum Debug
		{
			Invalid,
			Good,
			Bad
		}

		public static string GoodAnswer { get; } = "\u2714";
		public static string BadAnswer { get; } = "\u2716";

		public static Uri DefaultImageUri { get; } = new Uri(Path.GetFullPath(Properties.Settings.Default.DefaultImage));

		public static string FullWordsDirectory { get; } = Path.GetFullPath(Properties.Settings.Default.WordsDirectory);

		public static Dictionary<Pages, string> PageMap { get; } = new Dictionary<Pages, string>
		{
			{ Pages.MainPage, "MainPage" },
			{ Pages.DictionaryModePage, "DictionaryModePage" },
			{ Pages.AdministratorPage, "AdministratorPage" },
			{ Pages.GamePage, "GamePage" }
		};

		public static Dictionary<Windows, string> WindowMap { get; } = new Dictionary<Windows, string>
		{
			{ Windows.MainWindow, "MainWindow" },
			{ Windows.TextInputWindow, "TextInputWindow" },
		};

		public static string GetPageName(Pages page)
		{
			return PageMap[page];
		}

		public static string GetWindow(Windows window)
		{
			return WindowMap[window];
		}

		public static BitmapImage GetImageFromBase64(string base64String)
		{
			try
			{
				byte[] imageBytes = Convert.FromBase64String(base64String);

				using (MemoryStream ms = new MemoryStream(imageBytes))
				{
					BitmapImage bitmapImage = new BitmapImage();
					bitmapImage.BeginInit();
					bitmapImage.StreamSource = ms;
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapImage.EndInit();

					return bitmapImage;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public static string GetBase64FromImage(BitmapImage bitmapImage)
		{
			if (bitmapImage == null)
			{
				return DictionaryEntry.DefaultImageString;
			}

			try
			{
				var writeableBitmap = new WriteableBitmap(bitmapImage);
				using (MemoryStream ms = new MemoryStream())
				{
					BitmapEncoder encoder = new PngBitmapEncoder();
					encoder.Frames.Add(BitmapFrame.Create(writeableBitmap));
					encoder.Save(ms);

					string base64String = Convert.ToBase64String(ms.ToArray());
					return base64String;
				}
			}
			catch (Exception exception)
			{
				throw new Exception("Error converting BitmapImage to Base64 string.", exception);
			}
		}
	}
}
