using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_amazon_FibonacciFactors
{
	class Solution
	{
		static void Main(string[] args)
		{
			int numberOfTestCase = 0;
			List<int> elements = new List<int>();
			numberOfTestCase = int.Parse(Console.ReadLine());
			for (int i = 0; i < numberOfTestCase; i++)
				elements.Add(int.Parse(Console.ReadLine()));

			elements.ForEach(a => PrintX(a));
		}

		public static void PrintX(int input)
		{
			long fibo = 0;
			long prevValue = 0;
			Stack<long> history = new Stack<long>(4);

			long smallestFibo = 0;
			long smallestD = 0;

			bool hasFoundSmallestFibo = false;

			// finding common divide smallkestFibo and input
			for (long i = 2; i < long.MaxValue; i++)
			{
				if (input % i == 0)
				{
					smallestD = i;
					break;
				}
			}

			// finding smallest fibo numbr to divide
			while (fibo < long.MaxValue && !hasFoundSmallestFibo)
			{
				if (fibo > 1 && fibo % smallestD == 0)
				{
					smallestFibo = fibo;
					hasFoundSmallestFibo = true;
				}

				if (history.Count > 0)
					prevValue = history.Peek();
				else
					prevValue = 1;
				history.Push(fibo);
				fibo = fibo + prevValue;
			}


			Console.WriteLine("{0} {1}", smallestFibo, smallestD);
		}
	}
}
