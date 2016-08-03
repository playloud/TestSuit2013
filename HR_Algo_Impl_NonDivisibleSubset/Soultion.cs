using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Algo_Impl_NonDivisibleSubset
{
	class Soultion
	{
		/// <summary>
		/// // PSH 08/03/16 :
		/// Not a completed solution, 
		/// pass 4/15
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{

			int[] nk = Array.ConvertAll(Console.ReadLine().Split(), Int32.Parse);
			int n = nk[0];
			int k = nk[1];
			int[] elements = Array.ConvertAll(Console.ReadLine().Split(), Int32.Parse);


			int[] remainderCounts = new int[k];
			for (int i = 0; i < elements.Length; i++)
			{
				int remainder = elements[i]%k;
				remainderCounts[remainder]++;
			}

			int total = 0;
			for (int i = 1; i < k; i++)
			{
				total += Math.Max(remainderCounts[i], remainderCounts[k - i]);
				
				if(i == k-i)
					break;

				if (i+i+1 == k)
					break;
			}

			Console.WriteLine(total);
		}
	}
}
