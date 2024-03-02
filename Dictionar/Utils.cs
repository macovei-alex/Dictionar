using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Dictionar
{
	internal class Utils
	{
		public enum Pages
		{
			Invalid,
			MainPage,
			DictionaryModePage
		};

		public static readonly Dictionary<Pages, string> pageMap = new Dictionary<Pages, string>
		{
			{ Pages.MainPage, "MainPage" },
			{ Pages.DictionaryModePage, "DictionaryModePage" }
		};

		public static void GetPageName(Pages page)
		{
			return pageMap[page];
		}
	}
}
