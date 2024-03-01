using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionar
{
	internal interface IEntry
	{
		string CollectionKey { get; }
		string Key { get; }
	}
}
