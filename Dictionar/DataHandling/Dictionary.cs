using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal class Dictionary
	{
		private IDataSource<DictionaryEntry> DataSource { get; set; }

		public Dictionary(IDataSource<DictionaryEntry> dataSource)
		{
			DataSource = dataSource;
		}

		public void CreateEntry(DictionaryEntry entry)
		{
			DataSource.CreateEntry(entry);
		}

		public void CreateEntryNoThrow(DictionaryEntry entry)
		{
			try
			{
				DataSource.CreateEntry(entry);
			}
			catch (Exception)
			{
			}
		}

		public DictionaryEntry ReadEntry(DictionaryEntry entry)
		{
			return DataSource.ReadEntry(entry);
		}

		public DictionaryEntry ReadEntry(string word)
		{
			return DataSource.ReadEntry(word);
		}

		public DictionaryEntry ReadEntryOrNull(DictionaryEntry entry)
		{
			try
			{
				return DataSource.ReadEntry(entry);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public DictionaryEntry ReadEntryOrNull(string word)
		{
			try
			{
				return DataSource.ReadEntry(word);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
