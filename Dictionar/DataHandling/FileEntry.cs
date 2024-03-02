using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal abstract class FileEntry : IEntry
	{
		public abstract string Key { get; }

		public string CollectionKey
		{
			get
			{
				return Key[0].ToString().ToLower();
			}
		}

		public abstract string FileExtension { get; }

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
