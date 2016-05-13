using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjectDeployer
{
	class Program
	{
		static void Main(string[] args)
		{

			XElement xe = XElement.Load("../../DeployProfile.xml");

			var projects = xe.Elements("Project");

			foreach (XElement project in projects)
			{
				//
				string sourceDir = project.Element("source").Value.Trim();
				

				var domains = project.Elements("Domain");
				foreach (XElement domain in domains)
				{
					string domainName = domain.Attribute("name").Value;
					Console.WriteLine("domainName:"+domainName);

					// target directory
					string targetDir = domain.Element("Target").Value.Trim();
					string dbName = domain.Element("DBConnectionString").Value.Trim();
					string showRevision = domain.Element("ShowRevision").Value.Trim();
					string UpdateReleaseDate = domain.Element("UpdateReleaseDate").Value.Trim();

					targetDir.StrDump();
					dbName.StrDump();
					
					CopyFile(sourceDir, targetDir);
					ChangeConfig(targetDir, dbName, showRevision, UpdateReleaseDate );
				}

			}
			

		}

		public static void CopyFile(string strDir, string targetDir)
		{
			string[] files = Directory.GetFiles(strDir);

			if (!Directory.Exists(targetDir))
				Directory.CreateDirectory(targetDir);

			foreach (string file in files)
			{
				string newPath = Path.Combine(targetDir, Path.GetFileName(file));
				newPath.StrDump();
				File.Copy(file, newPath, true);

			}
		}

		public static void ChangeConfig(string targetDir, string newDBConnValue, string isUpdateEffectiveDate, string showRevision)
		{
			string configFile = Directory.GetFiles(targetDir).Where(a => a.EndsWith(".config")).FirstOrDefault();
			configFile.StrDump();

			XElement root = XElement.Load(configFile);

			// db connection string
			XElement dbConnElement =
				root
					.Element("appSettings")
					.Elements().Where(a => a.Name == "add" && a.Attribute("key").Value == "DBConnectionString")
					.FirstOrDefault();
			
			dbConnElement.SetAttributeValue("value", newDBConnValue);


			// effective date
			if (isUpdateEffectiveDate.ToLower() == "true")
			{
				XElement effectiveDateElement =
					root
						.Element("appSettings")
						.Elements().Where(a => a.Name == "add" && a.Attribute("key").Value == "EffectiveDate")
						.FirstOrDefault();

				effectiveDateElement.SetAttributeValue("value", DateTime.Now.ToString("MMMM dd yyyy")); 
			}

			// effective date
			XElement showRevisionElement =
				root
					.Element("appSettings")
					.Elements().Where(a => a.Name == "add" && a.Attribute("key").Value == "ShowRevision")
					.FirstOrDefault(); 

			if(showRevision.ToLower() == "true")
				showRevisionElement.SetAttributeValue("value", "true");
			else if (showRevision.ToLower() == "false")
				showRevisionElement.SetAttributeValue("value", "false"); 

			root.Save(configFile);
		}

	}
}
