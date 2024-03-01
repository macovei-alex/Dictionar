using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal abstract class FileEntry : IEntry
	{
		public abstract string Name { get; }

		public abstract string FileName { get; }

		public string CollectionKey
		{
			get
			{
				return Name[0].ToString().ToLower();
			}
		}

		public string Key
		{
			get
			{
				return Name;
			}
		}

		public abstract string Serialize();

		public static FileEntry Deserialize(string data)
		{
			throw new NotImplementedException();
		}
	}
}
