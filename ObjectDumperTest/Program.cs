using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDumperTest
{
	class Program
	{
		static void Main(string[] args)
		{
			
			TempClass t = new TempClass();
			t.Name = "Seho";
			t.age = 123;
			t.LastName = "Park";

			string result = ObjectDumper.WriteToString(t);
			Console.WriteLine(result);
		}
	}
}
