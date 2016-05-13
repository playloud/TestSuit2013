using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;



namespace MIMRadiologyDataCopier
{
	class Program
	{
		static void Main(string[] args)
		{

			string code = "590269";

			SqlConnection scon = new SqlConnection(@"Data Source=PA-IMI-VAL-01\MSSQLSRVR_2008R2;initial catalog=DWJM_MIM1_C25001_Visceral;User ID=ecrfuser;PWD=3crfus3r;");
			string query = null;

			// get visit Pages		
			query = @"
select 
	* 
from 
	VisitPages
where 
	Code = {0}
	and ReaderGroup = 'Testing'	
	and IsActive = 1
".F(code);
			DataTable dtVisitPages = AppConfig.LoadTableAsync(scon, query);
			string newSummaryPageID = null;

			foreach (DataRow rowVP in dtVisitPages.Rows)
			{
				string ecRFPageID = rowVP["eCRFPageID"].ToString();
				string userName = rowVP["UserName"].ToString();

				Console.WriteLine(ecRFPageID);
				Console.WriteLine(userName);

				// create visit page
				query = GetInsertQueryNewVisitPage(ecRFPageID);
				RunQuery(scon, query);
				string newPageID = GetNewVisitPageID(scon, code);

				// copy modality
				query = GetInsertQueryNewModality(ecRFPageID, newPageID);
				RunQuery(scon, query);

				// copy LesionRow
				query = GetNewLesionQuery(ecRFPageID, newPageID);
				RunQuery(scon, query);

				// copy visitSeriesRow
				query = GetInsertQueryVisitSeries(ecRFPageID, newPageID);
				RunQuery(scon, query);

				//// copy SummaryPages
				//if (newSummaryPageID == null)
				//{
				//	query = GetInsertQuerySummaryPage(code, userName);
				//	RunQuery(scon, query);
				//	newSummaryPageID = GetNewSummaryPageID(scon, code); 
				//}

				//// copy SummaryVisitRows
				//query = GetInsertQuerySummaryVisitRows(code, userName, ecRFPageID, newPageID, newSummaryPageID);
			}


			// adjudication page


			scon.Close();

		}

		private static string GetNewVisitPageID(SqlConnection scon, string code )
		{
			string query = @"
select 
	top 1 eCRFPageID 
from 
	VisitPages 
where 
	code = {0} 
	and ReaderGroup='Development3'
order by 
	eCRFPageID desc
".F(code);

			DataTable dtVisitPages = AppConfig.LoadTableAsync(scon, query);
			if(dtVisitPages.Rows.Count > 0)
				return dtVisitPages.Rows[0]["eCRFPageID"].ToString();

			return null;
		}

		private static string GetInsertQuerySummaryPage(string code, string userName)
		{
			string query = @"

".F();

			return query;
		}

		
		private static string GetNewSummaryPageID(SqlConnection scon, string code)
		{
			string query = @"
select 
	top 1 eCRFPageID 
from 
	VisitPages 
where 
	code = {0} 
	and ReaderGroup='Development3'
order by 
	eCRFPageID desc
".F(code);


			DataTable dtVisitPages = AppConfig.LoadTableAsync(scon, query);
			if (dtVisitPages.Rows.Count > 0)
				return dtVisitPages.Rows[0]["eCRFPageID"].ToString();

			return null;
		}

		private static string GetInsertQuerySummaryVisitRows(string code, string userName, string ecRFPageID, string newPageID, string newSummaryPageID)
		{
			string query = @"
".F();

			return query;
		}

		private static string GetInsertQueryNewVisitPage(string pageID)
		{
			string query = @"
insert into VisitPage
(
[ProtocolCode] ,[ReaderGroup] ,[ReadType] ,[Job] ,[SiteSubject] ,[Code] ,[VisitName] ,[PassNum] ,[UserName] ,[FullName] ,[RecordTime] ,[eCRFPageStatus] ,[Reason] ,[ScanDate] ,[IsActive] ,[VisitDate] ,[TimepointName_VisitDate] ,[CodeMatch] ,[NoLesionSeen] ,[TimepointName] ,[FirstTimepointName] ,[FirstVisitDate] ,[IsBL] ,[TargetLesionResponse] ,[VisitResponseComment] ,[NewLesions] ,[NewLesionsSinceBL_BeforeCurrentTimepoint] ,[BLAllNotReadable] ,[SpleenEnlargement] ,[LiverEnlargement] ,[PriorEvaluableTPSpleenEnlargement] ,[PriorEvaluableTPLiverEnlargement] ,[SpleenStatus] ,[LiverStatus] ,[TargetVisceralSPD] ,[pcChangeBLVisceral] ,[pcChangeNadirVisceral] ,[BLVisceralSPD] ,[NadirVisceralSPD] ,[TargetSpleenNoduleSPD] ,[DerivedVisitResponse] ,[ReviewerVisitResponse] ,[MaxModalityNumber] ,[PriorMaxModalityNumber] ,[ResetModalityNumbersOnce] ,[VersionLabel] ,[LesionBiopsyAvailable] ,[LesionBiopsyReviewed] ,[BLNoLesionSeen] ,[NoLesionSeenSetOnce] ,[CanBeLossOfResponse] ,[BLSpleenEnlargement] ,[BLLiverEnlargement] ,[UnequivocalProgression] ,[PDForPriorTimepoint] ,[PRForPriorTimepoint] ,[PRorCRForPriorTimepoint]
)
select
[ProtocolCode] ,'Development3' ,[ReadType] ,[Job] ,[SiteSubject] ,[Code] ,[VisitName] ,[PassNum] ,[UserName] ,[FullName] ,[RecordTime] ,[eCRFPageStatus] ,[Reason] ,[ScanDate] ,[IsActive] ,[VisitDate] ,[TimepointName_VisitDate] ,[CodeMatch] ,[NoLesionSeen] ,[TimepointName] ,[FirstTimepointName] ,[FirstVisitDate] ,[IsBL] ,[TargetLesionResponse] ,[VisitResponseComment] ,[NewLesions] ,[NewLesionsSinceBL_BeforeCurrentTimepoint] ,[BLAllNotReadable] ,[SpleenEnlargement] ,[LiverEnlargement] ,[PriorEvaluableTPSpleenEnlargement] ,[PriorEvaluableTPLiverEnlargement] ,[SpleenStatus] ,[LiverStatus] ,[TargetVisceralSPD] ,[pcChangeBLVisceral] ,[pcChangeNadirVisceral] ,[BLVisceralSPD] ,[NadirVisceralSPD] ,[TargetSpleenNoduleSPD] ,[DerivedVisitResponse] ,[ReviewerVisitResponse] ,[MaxModalityNumber] ,[PriorMaxModalityNumber] ,[ResetModalityNumbersOnce] ,[VersionLabel] ,[LesionBiopsyAvailable] ,[LesionBiopsyReviewed] ,[BLNoLesionSeen] ,[NoLesionSeenSetOnce] ,[CanBeLossOfResponse] ,[BLSpleenEnlargement] ,[BLLiverEnlargement] ,[UnequivocalProgression] ,[PDForPriorTimepoint] ,[PRForPriorTimepoint] ,[PRorCRForPriorTimepoint]
from VisitPages
where eCRFPageID = {0}
".F(pageID);

			return query;
		}

		private static string GetInsertQueryNewModality(string ecRFPageID, string newPageID)
		{
			string query = @"
insert into ModalityRows
(
[eCRFPageID], [IsActive] ,[MRow] ,[Modality] ,[Quality] ,[QualityComment] ,[OtherQualityComment] ,[Anatomy] ,[AnatomyComment]
)
select
{1} ,[IsActive] ,[MRow] ,[Modality] ,[Quality] ,[QualityComment] ,[OtherQualityComment] ,[Anatomy] ,[AnatomyComment]
from ModalityRows
where eCRFPageID = {0}
".F(ecRFPageID, newPageID);


			return query;
		}

		private static string GetInsertQueryVisitSeries(string ecRFPageID, string newPageID)
		{
			string query = @"
insert into VisitSeries
(
[eCRFPageID] ,[SeriesID] ,[TaskSeriesID] ,[ForRead] ,[SeriesDateTime] ,[Status] ,[LogDateTime] ,[VisitName] ,[VisitLabel] ,[IsActive]
)
select 
{1} ,[SeriesID] ,[TaskSeriesID] ,[ForRead] ,[SeriesDateTime] ,[Status] ,[LogDateTime] ,[VisitName] ,[VisitLabel] ,[IsActive]
from VisitSeriesRows
where eCRFPageID = {0}
".F(ecRFPageID, newPageID);
			return query;
		}

		private static string GetNewLesionQuery(string ecRFPageID, string newPageID)
		{
			string query = @"
insert into LesionRows
(
[eCRFPageID] ,[IsActive] ,[LRow] ,[LocationCommentRequired] ,[IsTargetLesion] ,[StatusCommentRequired] ,[OverlayID] ,[MIRAOverlayRequired] ,[LesionLabel] ,[LesionType] ,[LesionSite] ,[LocationComment] ,[LesionStatus] ,[LesionStatusComment] ,[LesionModality] ,[LongestDiameter] ,[ShortAxis] ,[LesionScanDate] ,[AssociatedLesion] ,[Length1] ,[Length2] ,[InitialLesionIdentification] ,[OverlayType] ,[MeasurementRequired] ,[ModalityNumber] ,[EffectiveSliceThickness] ,[LocationType] ,[BLGTD] ,[NadirGTD] ,[ProductOfDiameters] ,[BLGTDPercentChange] ,[NadirGTDPercentChange] ,[NadirSA]
)
select 
[LesionRowID] ,{1} ,[IsActive] ,[LRow] ,[LocationCommentRequired] ,[IsTargetLesion] ,[StatusCommentRequired] ,[OverlayID] ,[MIRAOverlayRequired] ,[LesionLabel] ,[LesionType] ,[LesionSite] ,[LocationComment] ,[LesionStatus] ,[LesionStatusComment] ,[LesionModality] ,[LongestDiameter] ,[ShortAxis] ,[LesionScanDate] ,[AssociatedLesion] ,[Length1] ,[Length2] ,[InitialLesionIdentification] ,[OverlayType] ,[MeasurementRequired] ,[ModalityNumber] ,[EffectiveSliceThickness] ,[LocationType] ,[BLGTD] ,[NadirGTD] ,[ProductOfDiameters] ,[BLGTDPercentChange] ,[NadirGTDPercentChange] ,[NadirSA]
from LesionRows
where eCRFPageID = {0}
".F(ecRFPageID, newPageID);
			return query;
		}

		private static string RunQuery(SqlConnection scon, string query)
		{
			StringBuilder sbuf = new StringBuilder();

			sbuf.AppendLine("EXEC dbo.SetPendingAction 'SePark','Seho Park','Test'");
			sbuf.AppendLine(query);
			sbuf.AppendLine("EXEC dbo.ClearPendingAction");

			SqlCommand com = new SqlCommand(sbuf.ToString(), scon);
			//com.ExecuteNonQuery();
			
			return null;
		}
	}
}
