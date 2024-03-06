using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	public class DictionaryEntry : FileEntry
	{
		public static string DefaultImageString { get => "default"; }
		public static DictionaryEntry Empty { get; } = new DictionaryEntry();

		public override string Key
		{
			get
			{
				return Word;
			}
		}

		public override string FileExtension
		{
			get
			{
				return "json";
			}
		}

		public string Word { get; set; }
		public string Definition { get; set; }
		public string Image { get; set; }

		[JsonConstructor]
		public DictionaryEntry(string word, string description, string image)
		{
			Word = word;
			Definition = description;
			Image = image;
		}

		public DictionaryEntry(FileEntry entry)
		{
			Word = entry.Key;
			Definition = string.Empty;
			Image = string.Empty;
		}

		public DictionaryEntry(string word)
		{
			Word = word;
			Definition = string.Empty;
			Image = string.Empty;
		}

		public DictionaryEntry()
		{
			Word = string.Empty;
			Definition = string.Empty;
			Image = string.Empty;
		}

		public override string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}

		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(Serialize());
		}

		public override string ToString()
		{
			return $"Word: {Word}, Description: {Definition}, Image: {Image}";
		}
	}
}
