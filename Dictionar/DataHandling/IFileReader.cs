using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar.DataHandling
{
	internal interface IFileReader : IDataSource
	{
		string Path { get; }

		void LoadContents();
		void UnloadContents();
	}
}
