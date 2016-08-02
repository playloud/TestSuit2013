using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Algo_Impl_EqualStacks
{
	class Solution
	{
		static void Main(string[] args)
		{
			string[] tokens_n1 = Console.ReadLine().Split(' ');
			int n1 = Convert.ToInt32(tokens_n1[0]);
			int n2 = Convert.ToInt32(tokens_n1[1]);
			int n3 = Convert.ToInt32(tokens_n1[2]);
			string[] h1_temp = Console.ReadLine().Split(' ');
			int[] elements1 = Array.ConvertAll(h1_temp, Int32.Parse);
			string[] h2_temp = Console.ReadLine().Split(' ');
			int[] elements2 = Array.ConvertAll(h2_temp, Int32.Parse);
			string[] h3_temp = Console.ReadLine().Split(' ');
			int[] elements3 = Array.ConvertAll(h3_temp, Int32.Parse);

			int total1 = elements1.Sum();
			int total2 = elements2.Sum();
			int total3 = elements3.Sum();

			int[] sumArr1 = new int[elements1.Length];
			int[] sumArr2 = new int[elements2.Length];
			int[] sumArr3 = new int[elements3.Length];

			int temp = 0;
			int index = 0;

			foreach (int e in elements1.Reverse())
			{
				temp += e;
				sumArr1[index] = temp;
				index++;
			}

			temp = 0;
			index = 0;
			foreach (int e in elements2.Reverse())
			{
				temp += e;
				sumArr2[index] = temp;
				index++;
			}

			temp = 0;
			index = 0;
			foreach (int e in elements3.Reverse())
			{
				temp += e;
				sumArr3[index] = temp;
				index++;
			}

			var query =
				from a in sumArr1
				from b in sumArr2
				from c in sumArr3
				where
					a == b && b == c && c == a
				select a;
			if(query.Any())
				Console.WriteLine(query.First());
		}
	}
}
