using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Dictionar.DataHandling
{
	public class FileSystemDataSource<T> : IDataSource<T> where T : FileEntry
	{
		public string DirectoryPath { get; set; }

		private bool DirectoriesCleaned { get; set; }
		private Random RNG { get; }

		public FileSystemDataSource(string directoryPath)
		{
			DirectoryPath = directoryPath;

			CleanDirectories();
			DirectoriesCleaned = true;
			RNG = new Random();
		}

		public void CreateEntry(T entry)
		{
			var directoryPath = Path.Combine(DirectoryPath, entry.CollectionKey);
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			var path = Path.Combine(directoryPath, entry.FileName);

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

				DirectoriesCleaned = false;
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

				DirectoriesCleaned = false;
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		public T RandomEntry()
		{
			if (DirectoriesCleaned == false)
			{
				CleanDirectories();
			}

			try
			{
				string[] collections = Directory.GetDirectories(DirectoryPath);
				string collection = collections[RNG.Next(0, collections.Length)];

				string[] files = Directory.GetFiles(collection);
				string file = files[RNG.Next(0, files.Length)];

				string entryPath = Path.Combine(DirectoryPath, collection, file);

				return JsonConvert.DeserializeObject<T>(File.ReadAllText(entryPath));
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}

		private void CleanDirectories()
		{
			foreach (var collectionDirectory in Directory.GetDirectories(DirectoryPath))
			{
				foreach (var filePath in Directory.GetFiles(collectionDirectory))
				{
					try
					{
						var fileName = Path.GetFileNameWithoutExtension(filePath);
						ReadEntry(fileName);
					}
					catch (Exception)
					{
						File.Delete(filePath);
					}
				}

				if (Directory.GetFiles(collectionDirectory).Length == 0)
				{
					Directory.Delete(collectionDirectory);
				}
			}
			DirectoriesCleaned = true;
		}
	}
}
