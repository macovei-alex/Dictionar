using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Dictionar.DataHandling
{
	internal class FileSystemDataSource : IDataSource<FileEntry>
	{
		public string DirectoryPath { get; set; }

		public FileSystemDataSource(string directoryPath)
		{
			DirectoryPath = directoryPath;
		}

		public void CreateEntry(FileEntry entry)
		{
			var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);
			if (File.Exists(path))
			{
				throw new Exception("File already exists");
			}

			byte[] entryBytes = Encoding.UTF8.GetBytes(entry.Serialize());

			FileStream fileStream = File.Create(path);

			fileStream.Write(entryBytes, 0, entryBytes.Length);
			fileStream.Close();
		}

		public T ReadEntry<T>(FileEntry entry)
		{
			var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);
			var ret = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
			if (ret == null)
			{
				throw new Exception("Bad entry format");
			}

			return ret;
		}

		public FileEntry ReadEntry(FileEntry entry)
		{
			return ReadEntry<FileEntry>(entry);
		}

		public void UpdateEntry(FileEntry entry)
		{
			throw new NotImplementedException();
		}

		public void DeleteEntry(FileEntry entry)
		{
			throw new NotImplementedException();
		}
	}
}
