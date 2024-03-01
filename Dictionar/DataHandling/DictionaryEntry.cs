using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal class DictionaryEntry : FileEntry
	{
		public string Word { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

		public override string Name
		{
			get
			{
				return Word;
			}
		}

		public override string FileName
		{
			get
			{
				return $"{Name}.json";
			}
		}

		public DictionaryEntry(string word, string description, string image)
		{
			Word = word;
			Description = description;
			Image = image;
		}

		public override string Serialize()
		{
			return $"{{" +
				$"\"Word\": {Word},\n" +
				$"\"Description: \"{Description},\n" +
				$"\"Image\": {Image}\n" +
				$"}}";
		}
	}
}
