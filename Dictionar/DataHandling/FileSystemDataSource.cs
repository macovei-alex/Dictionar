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
			try
			{
				string path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);
				if (File.Exists(path) == false)
				{
					throw new Exception($"File {path} does not exist");
				}

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
			try
			{
				return ReadEntry(Activator.CreateInstance(typeof(T), key) as T);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void UpdateEntry(T entry)
		{
			try
			{
				var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);
				if (File.Exists(path) == false)
				{
					throw new Exception($"File {path} does not exist");
				}

				File.Delete(path);
				CreateEntry(entry);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void DeleteEntry(T entry)
		{
			try
			{
				var path = Path.Combine(DirectoryPath, entry.CollectionKey, entry.FileName);
				if (File.Exists(path) == false)
				{
					throw new Exception($"File {path} does not exist");
				}

				File.Delete(path);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public void DeleteEntry(string key)
		{
			try
			{
				DeleteEntry(Activator.CreateInstance(typeof(T), key) as T);
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
	}
}
