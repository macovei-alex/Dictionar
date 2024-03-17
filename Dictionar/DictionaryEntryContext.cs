using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionar.DataHandling;

namespace Dictionar
{
	public class DictionaryEntryContext : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private DictionaryEntry _dictionaryEntry;

		public DictionaryEntry DictionaryEntry
		{
			get
			{
				return _dictionaryEntry;
			}
			set
			{
				if (_dictionaryEntry != value)
				{
					_dictionaryEntry = value;
					RaisePropertyChanged(nameof(DictionaryEntry));
				}
			}
		}

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public DictionaryEntryContext(DictionaryEntry entry)
		{
			DictionaryEntry = entry;
		}

		public DictionaryEntryContext()
		{
			DictionaryEntry = DictionaryEntry.Empty;
		}
	}
}
