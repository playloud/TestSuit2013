using System;

namespace HR_Algo_Implementation
{
	public static class StrangeCounter
	{
		public static void DoJob()
		{
			PrlongInput(999999766777);
			
		}

		public static void PrlongInput(double input)
		{
			double startIndex = 1;
			double endIndex = 0;
			double length = 0;

			for (int i = 0; i < 1000; i++)
			{
				startIndex = endIndex + 1;
				length = 3 << i;
				endIndex += length;

				Console.WriteLine(endIndex);
				
				if (startIndex <= input && input <= endIndex)
				{
					Console.WriteLine(endIndex + 1 - input);
					return;
				}
			}
		}
	}
}