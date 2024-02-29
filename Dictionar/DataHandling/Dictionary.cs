using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal class Dictionary
	{
		private IDataSource DataSource { get; set; }
		private IFileReader FileReader { get; set; }

		public Dictionary(IDataSource dataSource)
		{
			this.DataSource = dataSource;
		}

		public Dictionary(string path)
		{
			FileReader = new XMLReader(path);
		}
	}
}
