using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Dictionar.DataHandling
{
	internal class FileSystemDataSource<T> : IDataSource<T> where T : FileEntry
	{
		public string DirectoryPath { get; set; }

		public FileSystemDataSource(string directoryPath)
		{
			DirectoryPath = directoryPath;
		}

		public void CreateEntry(T entry)
		{
			var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);

			if (File.Exists(path))
			{
				throw new Exception("File already exists");
			}

			try
			{
				using (FileStream fileStream = File.Create(path))
				{
					byte[] entryBytes = entry.GetBytes();
					fileStream.Write(entryBytes, 0, entryBytes.Length);
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public T ReadEntry(T entry)
		{
			var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);

			if (File.Exists(path) == false)
			{
				throw new Exception("File does not exist");
			}

			try
			{
				var ret = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
				if (ret == null)
				{
					throw new Exception("Bad entry format");
				}
				return ret;
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public T ReadEntry(string key)
		{
			FileEntry entry = (FileEntry)Activator.CreateInstance(typeof(T), key);

			var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);
			var ret = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

			if (ret == null)
			{
				throw new Exception("Bad entry format");
			}

			return ret;
		}

		public void UpdateEntry(T entry)
		{
			throw new NotImplementedException();
		}

		public void DeleteEntry(T entry)
		{
			throw new NotImplementedException();
		}
	}
}
