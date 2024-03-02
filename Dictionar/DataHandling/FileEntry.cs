using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	public abstract class FileEntry : IEntry
	{
		[JsonIgnore]
		public abstract string Key { get; }

		[JsonIgnore]
		public string CollectionKey
		{
			get
			{
				return Key[0].ToString().ToLower();
			}
		}

		[JsonIgnore]
		public abstract string FileExtension { get; }

		[JsonIgnore]
		public string FileName
		{
			get
			{
				return $"{Key}.{FileExtension}";
			}
		}

		public abstract string Serialize();
		public abstract byte[] GetBytes();
	}
}
