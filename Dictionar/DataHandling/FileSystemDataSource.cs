using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Dictionar.DataHandling
{
	internal class FileSystemDataSource : IDataSource
	{
		public string DirectoryPath { get; set; }

		public FileSystemDataSource(string directoryPath)
		{
			DirectoryPath = directoryPath;
		}

		public void CreateEntry(IEntry entry)
		{
			CreateEntry(entry as FileEntry);
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

		public IEntry ReadEntry(IEntry entry)
		{
			return ReadEntry<FileEntry>(entry as FileEntry);
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

		public void UpdateEntry(IEntry entry)
		{
			UpdateEntry(entry as FileEntry);
		}

		public void UpdateEntry(FileEntry entry)
		{

		}

		public void DeleteEntry(IEntry entry)
		{
			throw new NotImplementedException();
		}

		public void DeleteEntry(string name)
		{
			throw new NotImplementedException();
		}
	}
}
