using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (TestObject to = new TestObject(111))
				to.DoSomething();

			TestObject to2 = new TestObject(222);
			to2.DoSomething();
			
			TestObject to3 = new TestObject(333);
			to3.DoSomething();
			to3.Dispose();

			TestObject to4 = new TestObject(444);
			to4.DoSomething();
			to4.Dispose();
			to4.TDump();

			GC.Collect(0);
		}
	}
}
