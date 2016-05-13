using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMover_prod;
using DataMover;

namespace DataMover
{
	class Program
	{
		static void Main(string[] args)
		{
			MoveROR();
			MoveNDIS();
		}

		static void MoveNDIS()
		{
			PSH_ProdDataContext dcSource = new PSH_ProdDataContext();
			DataClasses1DataContext dcTarget = new DataClasses1DataContext();

			var queryNDIS = dcSource.CD_NDIs;
			foreach (DataMover_prod.CD_NDI source in queryNDIS)
			{
				CD_NDI target = new CD_NDI();

				target.CENTRE = source.CENTRE;
				target.ISVALID = source.ISVALID;
				target.NKDISC = source.NKDISC;
				target.PT = source.PT;
				target.RECORDID = source.RECORDID;
				target.STUDY = source.STUDY;
				target.SURGNM = source.SURGNM;
				target.SURYMD = source.SURYMD;
				target.VDATE = source.VDATE;

				dcTarget.CD_NDIs.InsertOnSubmit(target);

				Console.Write(".");
			}
			dcTarget.SubmitChanges();

			
		}

		static void MoveROR()
		{
			PSH_ProdDataContext dcSource = new PSH_ProdDataContext();
			DataClasses1DataContext dcTarget = new DataClasses1DataContext();

			var queryROR = dcSource.CD_RORs;
			foreach (DataMover_prod.CD_ROR s in queryROR)
			{
				CD_ROR target = new CD_ROR();

				target.CENTRE = s.CENTRE;
				target.ISVALID = s.ISVALID;
				target.PT = s.PT;
				target.RECORDID = s.RECORDID;
				target.STUDY = s.STUDY;
				target.SURGNM = s.SURGNM;
				target.SURYMD = s.SURYMD;
				target.TMRESD = s.TMRESD;
				target.VDATE = s.VDATE;

				dcTarget.CD_RORs.InsertOnSubmit(target);

				Console.Write(".");
			}
			dcTarget.SubmitChanges();
		}
	}
}
