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
	internal class Utils
	{
		public enum Pages
		{
			Invalid,
			MainPage,
			DictionaryModePage,
			AdministratorPage
		};

		public static readonly Dictionary<Pages, string> pageMap = new Dictionary<Pages, string>
		{
			{ Pages.MainPage, "MainPage" },
			{ Pages.DictionaryModePage, "DictionaryModePage" },
			{ Pages.AdministratorPage, "AdministratorPage" }
		};

		public static string GetPageName(Pages page)
		{
			return pageMap[page];
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
			if (bitmapImage.StreamSource == null)
			{
				return null;
			}

			using (var ms = new MemoryStream())
			{
				bitmapImage.StreamSource.CopyTo(ms);
				return Convert.ToBase64String(ms.ToArray());
			}
		}

		public static Uri DefaultImageUri => new Uri(Path.GetFullPath(Properties.Settings.Default.DefaultImage));

		public static string FullWordsDirectory => Path.GetFullPath(Properties.Settings.Default.WordsDirectory);
	}
}
