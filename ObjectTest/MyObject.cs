using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectTest
{
	public class MyObject : IDisposable
	{
		private bool disposed = false;

		public MyObject()
		{
			Console.WriteLine("constructor");
		}

		~MyObject()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					Console.WriteLine("Dispose called");
				}
				else
				{
					Console.WriteLine("Finalizer called");
				}
				disposed = true;
			}
		}

	}
}
