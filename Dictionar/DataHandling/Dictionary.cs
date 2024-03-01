using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal class Dictionary
	{
		private IDataSource<FileEntry> DataSource { get; set; }

		public Dictionary(IDataSource<FileEntry> dataSource)
		{
			DataSource = dataSource;
		}

		public void CreateEntry(DictionaryEntry entry)
		{
			DataSource.CreateEntry(entry);
		}

		public DictionaryEntry ReadEntry(DictionaryEntry entry)
		{
			return DataSource.ReadEntry(entry) as DictionaryEntry;
		}

		public DictionaryEntry CreateEntryOrNull(DictionaryEntry entry)
		{
			try
			{
				return DataSource.ReadEntry(entry) as DictionaryEntry;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
