using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	internal interface IDataSource
	{
		IEntry GetRandomEntry();
		IEntry GetEntry(string name);
		IEntry GetByPreffix(string preffix);
	}
}
