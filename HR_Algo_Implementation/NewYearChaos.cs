using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Algo_Implementation
{
public static class NewYearChaos
{

	public static void DoJob()
	{
		List<PeopleLine> caseList = new List<PeopleLine>();
		int numberOfCases = int.Parse(Console.ReadLine());
		for (int i = 0; i < numberOfCases; i++)
		{
			PeopleLine line = new PeopleLine();
			line.NumberOfPeople = int.Parse(Console.ReadLine());
			line.SetLine(Console.ReadLine());
			caseList.Add(line);
		}

		foreach (PeopleLine pl in caseList)
		{
			pl.Calculate();
		}
	}

}

public class PeopleLine
{
	private int[] line = null;
	public int NumberOfPeople { get; set; }

	public void SetLine(string readLine)
	{
		line = readLine.Split().Select(a => int.Parse(a)).ToArray();
	}

	public void Calculate()
	{
		var query = line.Select((a, i) => new
		{
			value = a,
			index = i+1
		});

		if (query.Any())
		{
			int sum = 0;

			foreach (var result in query)
			{
				if (result.value - result.index > 2)
				{
					Console.WriteLine("Too chaotic");
					return;
				} else if (result.value > result.index)
				{
					sum += (result.value - result.index);
				}
			}
			Console.WriteLine(sum);
		}

	}
}


}
