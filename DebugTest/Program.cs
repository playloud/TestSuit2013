using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTest.PIERService;

namespace DebugTest
{
	class Program
	{
		static void Main(string[] args)
		{
			PIERServiceClient client = new PIERServiceClient();
			bool mustChangePassword = false;
			bool isLockecd = false;
			string errMsg = null;

			string key = client.StartSession("SePark", "ghkdanwl4R", "WARR-4MLPFX1", out errMsg, out mustChangePassword, out isLockecd);
			Console.WriteLine("key:"+key);
			Console.WriteLine(errMsg);
			
		}

		static bool SubFunction()
		{
			return false;
		}
	}
}
