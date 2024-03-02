using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	internal class Utils
	{
		public enum PageName
		{
			Invalid,
			MainPage,
			DictionaryModePage
		};

		public static readonly Dictionary<PageName, string> pageMap = new Dictionary<PageName, string>
		{
			{ PageName.Invalid, "Invalid" },
			{ PageName.MainPage, "MainPage" },
			{ PageName.DictionaryModePage, "DictionaryModePage" }
		};

		public static string GetPage(PageName pageName)
		{
			return pageMap[pageName];
		}
	}
}
