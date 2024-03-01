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

		public Dictionary(IDataSource dataSource)
		{
			DataSource = dataSource;
		}

		public void CreateEntry(IEntry entry)
		{
			DataSource.CreateEntry(entry);
		}

		public IEntry ReadEntry(IEntry entry)
		{
			return DataSource.ReadEntry(entry);
		}
	}
}
