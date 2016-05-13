using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPNDBInput
{
	class Program
	{
		static void Main(string[] args)
		{
			//PutTargetLesionForPostBaseline();
			PutNonTargetLesionSTatusPostBaseline();
		}

		public static void PutTargetLesionForPostBaseline()
		{
			PSH_IPN_8_55_58102_003_DEVEntities entities = new PSH_IPN_8_55_58102_003_DEVEntities();

			// parsing lesion location selection
			List<string> lines = File.ReadAllLines(@"..\..\Lesion_Target_PostBaseline.txt").ToList();
			foreach (string line in lines)
			{
				string[] e = line.Split(new char[] { '\t' });

				LU_LesionStatusAll lesionStatus = new LU_LesionStatusAll();

				lesionStatus.LesionStatus = e[0];
				lesionStatus.LesionCommentFlag = e[1];
				lesionStatus.MIRAOverlayRequired = e[2];
				lesionStatus.ForBL = 0;
				lesionStatus.ForFU = 1;
				lesionStatus.ForNonTarget = 0;
				lesionStatus.ForTarget = 1;
				lesionStatus.InitialCLD = 0;

				entities.LU_LesionStatusAll.Add(lesionStatus);
			}
			entities.SaveChanges();
		}

		public static void PutNonTargetLesionSTatusPostBaseline()
		{
			PSH_IPN_8_55_58102_003_DEVEntities entities = new PSH_IPN_8_55_58102_003_DEVEntities();

			// parsing lesion location selection
			List<string> lines = File.ReadAllLines(@"..\..\LesionStatus_NonTarget_PostBaseline.txt").ToList();
			foreach (string line in lines)
			{
				string[] elems = line.Split(new char[] { '\t' });
				string name = elems[0];
				var query = entities.LU_LesionStatusAll.Where(a => a.LesionStatus == name);
				if (query.Any())
				{
					query.FirstOrDefault().ForNonTarget = 1;
				}
				else
				{
					LU_LesionStatusAll lesionStatus = new LU_LesionStatusAll();

					lesionStatus.LesionStatus = elems[0];
					lesionStatus.LesionCommentFlag = elems[1];
					lesionStatus.MIRAOverlayRequired = elems[2];
					lesionStatus.ForBL = 0;
					lesionStatus.ForFU = 1;
					lesionStatus.ForNonTarget = 1;
					lesionStatus.ForTarget = 0;
					lesionStatus.InitialCLD = 0;

					entities.LU_LesionStatusAll.Add(lesionStatus);
				}
			}
			entities.SaveChanges();
		}

		public static void PutLesionLocation()
		{
			PSH_IPN_8_55_58102_003_DEVEntities entities = new PSH_IPN_8_55_58102_003_DEVEntities();

			// parsing lesion location selection
			List<string> lines = File.ReadAllLines(@"..\..\LesionLocationSelections.txt").ToList();
			foreach (string line in lines)
			{
				string[] elems = line.Split(new char[] { '\t' });
				LU_LesionSiteOrgan data = new LU_LesionSiteOrgan();
				data.LesionSite = elems[0];
				data.LocationCommentFlag = elems[1];
				data.LateralityRequired = elems[2];
				data.Organ = elems[3];

				entities.LU_LesionSiteOrgan.Add(data);
			}

			entities.SaveChanges();

			var queryLesionLocation =
				from row in entities.LU_LesionSiteOrgan
				select row;

			foreach (LU_LesionSiteOrgan lso in queryLesionLocation)
			{
				Console.WriteLine(lso.LesionSite);
			}
		}
	}
}
