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

		public IEntry ReadEntry(IEntry entry)
		{
			// TODO: template this function

			return ReadEntry(new DictionaryEntry(entry));
		}

		public IEntry ReadEntry(FileEntry entry)
		{
			// TODO: template this function

			return JsonConvert.DeserializeObject<DictionaryEntry>(
				File.ReadAllText(
					Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName)));
		}

		public void CreateEntry(IEntry entry)
		{
			CreateEntry(entry as FileEntry);
		}

		public void CreateEntry(FileEntry entry)
		{
			byte[] entryBytes = Encoding.UTF8.GetBytes(entry.Serialize());
			FileStream fileStream = File.Create(
				Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName));
			fileStream.Write(entryBytes, 0, entryBytes.Length);
			fileStream.Close();
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
