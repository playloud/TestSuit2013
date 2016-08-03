using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Algo_Impl_CutTheSticks
{
	class Solution
	{

		/// <summary>
		/// sticks-length        length-of-cut   sticks-cut
		//  5 4 4 2 2 8             2               6
		//  3 2 2 _ _ 6             2               4
		//  1 _ _ _ _ 4             1               2
		//  _ _ _ _ _ 3             3               1
		//  _ _ _ _ _ _           DONE DONE
		/// </summary>
		/// <param name="args"></param>

		static void Main(string[] args)
		{
			int n = Convert.ToInt32(Console.ReadLine());
			string[] arr_temp = Console.ReadLine().Split();
			int[] sticks = Array.ConvertAll(arr_temp, Int32.Parse);


			while (sticks.Sum() > 0)
			{
				int minimum = sticks.Where(a => a > 0).Min();
				int sticksCutCount = 0;

				for (int i = 0; i < sticks.Length; i++)
				{
					if(sticks[i]==0)
						continue;
					sticks[i] = sticks[i] - minimum;
					sticksCutCount++;
				}
				Console.WriteLine(sticksCutCount);
			}

		}
	}
}
