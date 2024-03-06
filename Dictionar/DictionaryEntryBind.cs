using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionar.DataHandling;

namespace Dictionar
{
	public class DictionaryEntryBind : DictionaryEntry, INotifyPropertyChanged
	{
		public string WordNotify
		{
			get
			{
				return Word;
			}
			set
			{
				if (Word != value)
				{
					Word = value;
					OnPropertyChanged(nameof(WordNotify));
				}
			}
		}

		public string DefinitionNotify
		{
			get
			{
				return Definition;
			}
			set
			{
				if (Definition != value)
				{
					Definition = value;
					OnPropertyChanged(nameof(DefinitionNotify));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public DictionaryEntryBind(DictionaryEntry entry)
		{
			if (entry == null)
			{
				WordNotify = string.Empty;
				DefinitionNotify = string.Empty;

				Word = string.Empty;
				Definition = string.Empty;
				Image = string.Empty;

				return;
			}

			WordNotify = entry.Word;
			DefinitionNotify = entry.Definition;

			Word = entry.Word;
			Definition = entry.Definition;
			Image = entry.Image;
		}

		public DictionaryEntryBind(string word)
		{
			WordNotify = word;
			DefinitionNotify = string.Empty;

			Word = word;
			Definition = string.Empty;
			Image = string.Empty;
		}

		public DictionaryEntryBind()
		{
			WordNotify = string.Empty;
			DefinitionNotify = string.Empty;

			Word = string.Empty;
			Definition = string.Empty;
			Image = string.Empty;
		}
	}
}
