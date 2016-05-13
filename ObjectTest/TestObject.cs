using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTest
{
	public class TestObject : IDisposable
	{
		public int ID { get; set; }
		public TestObject(int id)
		{
			ID = id;
			Console.WriteLine("Constructor of {0}", ID);
		}

		~TestObject()
		{
			Console.WriteLine("TestObject {0} finalize called", ID);
		}

		public void DoSomething()
		{
			Console.WriteLine("TestObject {0} do something called", ID);
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Console.WriteLine("TestObject {0} Dispose called", ID);
		}

	}
}
