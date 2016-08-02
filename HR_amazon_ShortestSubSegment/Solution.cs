using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_amazon_ShortestSubSegment
{
	/// <summary>
	/// working but not passed, because of the performance
	/// </summary>
	class Solution
	{
		static void Main(string[] args)
		{
			string mainStr = null;
			string[] mainElements = null;
			List<string> elements = new List<string>();
			int numberOfElements = 0;

			// get main String
			mainStr = Console.ReadLine();
			mainElements = mainStr.Split();

			// get number of sublines
			numberOfElements = int.Parse(Console.ReadLine());

			for (int i = 0; i < numberOfElements; i++)
				elements.Add(Console.ReadLine());
			//////////////////////////////////////////////// end of input


			///////////////////////////////////////////////////////////
			List<string> founds = new List<string>();
			int smallestLength = int.MaxValue;

			foreach (string elem in elements)
			{
				int[] elementsIndexes = GetIndexes(mainElements, elem);

				for (int i = 0; i < elementsIndexes.Length; i++)
				{
					int startIndex = elementsIndexes[i];

					for (int j = startIndex; j < mainElements.Length; j++)
					{
						string subStr = GetString(startIndex, j, mainElements);

						if (subStr.Length < smallestLength && IsContainsElements(subStr, elements))
						{
							if (subStr.Length < smallestLength && !founds.Contains(subStr))
							{
								string found = RemoveSpecialChars(subStr);
								smallestLength = found.Length;
								founds.Add(found);
								break;
							}
						}
					}
				}
			} // end of logic

			if (founds.Count == 0)
			{
				Console.WriteLine("NO SUBSEGMENT FOUND");
				return;
			}

			//dump of founds
			Console.WriteLine(founds.OrderBy(a => a.Length).ToList()[0]);

		}


		private static string RemoveSpecialChars(string str)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				if ((str[i] >= 'A' && str[i] <= 'z' || (str[i] == ' ')))
				{
					sb.Append(str[i]);
				}
			}

			return sb.ToString();
		}

		private static bool IsContainsElements(string subStr, List<string> elements)
		{
			string[] subStrArr = subStr.Split();
			for (int i = 0; i < elements.Count; i++)
			{
				if (!IsContainsInArr(subStrArr, elements[i]))
					return false;
			}
			return true;
		}

		private static bool IsContainsInArr(string[] subStrArr, string p)
		{
			var query = subStrArr.Where(a => a.ToLower().Replace(".", "").Trim() == p.ToLower());
			if (query.Any())
				return true;
			return false;
		}

		private static string GetString(int startIndex, int j, string[] mainElements)
		{
			StringBuilder sbuf = new StringBuilder();

			for (int i = startIndex; i < j; i++)
				sbuf.Append(mainElements[i] + " ");

			return sbuf.ToString().Trim();
		}

		private static int[] GetIndexes(string[] mainElements, string elem)
		{
			List<int> indexes = new List<int>();
			for (int i = 0; i < mainElements.Length; i++)
				if (mainElements[i].ToLower().Replace(".", "").Trim() == elem.ToLower())
					indexes.Add(i);
			return indexes.ToArray();
		}
	}
}
