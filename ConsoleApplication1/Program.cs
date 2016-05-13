using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{

			int[] tempArr = Enumerable.Range(0, 100).ToArray();

			var query =
				from a in tempArr
				from b in tempArr
				where a == (b * 2-45) && a > b
				select a;

			foreach (int i in query)
				Console.WriteLine(i);

			System.Console.ReadLine();
		}
	}


	public class Parent
	{
		public int data = 321;
	}

	public class Child : Parent
	{
		// shadowing example new is needed. 
		public new int data()
		{
			return 123;
		}
	}
}
