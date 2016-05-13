using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTest
{
	public static class Extentions
	{
		public static void TDump(this TestObject to)
		{
			Console.WriteLine();
			Console.WriteLine("---------start of TDump");
			Console.WriteLine(to.ID);
			Console.WriteLine("---------end of TDump");
			Console.WriteLine();
		}

	}
}
