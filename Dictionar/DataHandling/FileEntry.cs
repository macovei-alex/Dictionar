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
		[JsonIgnore]
		public abstract char FirstLetter { get; }

		[JsonIgnore]
		public abstract string Name { get; }

		[JsonIgnore]
		public abstract string FileName { get; }


		public abstract string Serialize();

		public static FileEntry Deserialize(string data)
		{
			throw new NotImplementedException();
		}
	}
}
