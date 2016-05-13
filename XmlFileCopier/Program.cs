using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlFileCopier
{
	class Program
	{
		static void Main(string[] args)
		{
			string sourceFile = @"C:\AppDev\eCRF\eCRF.MIM1.C25001\GRS\MIM1_C25001_GRS.dll.xml";
			string targetFile = @"C:\AppDev\eCRF\eCRF.MIM1.C25001\GRS\bin\Debug\MIM1_C25001_GRS.dll.xml";

			Console.WriteLine(sourceFile);
			Console.WriteLine("To");
			Console.WriteLine(targetFile);
			File.Copy(sourceFile, targetFile, true);
			Console.WriteLine("done");
			Console.ReadLine();
			
		}
	}
}
