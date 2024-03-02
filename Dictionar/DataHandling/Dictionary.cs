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

		public bool CreateEntryNoThrow(DictionaryEntry entry)
		{
			try
			{
				DataSource.CreateEntry(entry);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public DictionaryEntry ReadEntry(DictionaryEntry entry)
		{
			return DataSource.ReadEntry(entry);
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

		public DictionaryEntry ReadEntry(string word)
		{
			return DataSource.ReadEntry(word);
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

		public void UpdateEntry(DictionaryEntry entry)
		{
			DataSource.UpdateEntry(entry);
		}

		public bool UpdateEntryNoThrow(DictionaryEntry entry)
		{
			try
			{
				DataSource.UpdateEntry(entry);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void DeleteEntry(DictionaryEntry entry)
		{
			DataSource.DeleteEntry(entry);
		}

		public bool DeleteEntryNoThrow(DictionaryEntry entry)
		{
			try
			{
				DataSource.DeleteEntry(entry);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public void DeleteEntry(string word)
		{
			DataSource.DeleteEntry(word);
		}

		public bool DeleteEntryNoThrow(string word)
		{
			try
			{
				DataSource.DeleteEntry(word);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
