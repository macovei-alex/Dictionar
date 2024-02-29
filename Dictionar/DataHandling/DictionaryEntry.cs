using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal class DictionaryEntry : IEntry
	{
		public string Word { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

		public DictionaryEntry(string word, string description, string image)
		{
			Word = word;
			Description = description;
			Image = image;
		}
	}
}
