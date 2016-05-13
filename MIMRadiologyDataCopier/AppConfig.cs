using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;


namespace MIMRadiologyDataCopier
{
    /// <summary>
    /// Helper Application Configuration/State class.
    /// It contains application wise and configuration params, connections, etc.
    /// This class provides raw SQL access methods, wrapper methods for the stored procedures.
    /// Relates to all modules.
    /// </summary>
    internal class AppConfig
    {
        public static string DLLPath = null;
        public static int ApplicationID;
        public static int RoleID;
        public static string SecurityDBConnectionString;

        public static string DBConnectionString = null;

        public static string eCRFCallsLogFile = null;

        /// <summary>
        /// 12/23/09, OVK: Dedicated flag to disable log into DB.
        /// </summary>
        public static bool DisableDBeCRFCallsLog = false;

        public static string EventLogFilePath = null;

        public static string ApplicationDB_ConnectionString;

        /// <summary>
        /// SystemID for local caching.
        /// </summary>
        public static string SystemID
        {
            get
            {
                return System.Net.Dns.GetHostName();
            }
        }

        public static SqlConnection _ActiveDB_Connection;
        public static SqlConnection ActiveDB_Connection
        {
            get
            {
                lock (typeof(AppConfig))
                {
                    //TraceCalls(); // Greate place for tracking the calls...

                    if (_ActiveDB_Connection == null)
                        return null;
                    else
                        return _ActiveDB_Connection;
                }
            }
            set
            {
                lock (typeof(AppConfig))
                {
                    _ActiveDB_Connection = value;
                }

            }
        }

        public static SqlConnection _SecuritySQLConnection;
        public static SqlConnection SecuritySQLConnection
        {
            get
            {
                if (_SecuritySQLConnection == null)
                {
                    try
                    {
                        _SecuritySQLConnection = new SqlConnection(AppConfig.SecurityDBConnectionString);
                        _SecuritySQLConnection.Open();
                    }
                    catch
                    {
                        _SecuritySQLConnection = null;
                    }
                }

                return _SecuritySQLConnection;
            }

            set
            {
                if (value == null && _SecuritySQLConnection != null)
                {
                    try
                    {
                        if (_SecuritySQLConnection.State != ConnectionState.Closed &&
                            _SecuritySQLConnection.State != ConnectionState.Broken)
                        {
                            _SecuritySQLConnection.Close();
                        }
                        _SecuritySQLConnection.Dispose();
                    }
                    catch
                    {
                    }
                    _SecuritySQLConnection = null;
                }
            }
        }

        /// <summary>
        /// Added on 06/18/2008. Helper method for eSignature control
        /// </summary>
        /// <returns></returns>
        public static bool ReconnectSecurityIfClosedOrBroken()
        {
            if (_SecuritySQLConnection == null)
            {
                return ReconnectSecurity();
            }
            else
            {
                try
                {
                    if (_SecuritySQLConnection.State == ConnectionState.Closed ||
                            _SecuritySQLConnection.State == ConnectionState.Broken)
                    {
                        return ReconnectSecurity();
                    }
                    else
                    {
                        SqlCommand sq = new SqlCommand("SELECT @@SPID;", _SecuritySQLConnection);
                        int n = sq.ExecuteNonQuery();
                        return true; // not closed or broken...
                    }
                }
                catch (SqlException)
                {
                    return ReconnectSecurity();
                }
                catch
                {
                    return ReconnectSecurity();
                }
            }
        }

        public static bool ReconnectSecurity()
        {
            if (_SecuritySQLConnection != null)
            {
                try
                {
                    if (_SecuritySQLConnection.State != ConnectionState.Closed &&
                        _SecuritySQLConnection.State != ConnectionState.Broken)
                    {
                        _SecuritySQLConnection.Close();
                    }
                    _SecuritySQLConnection.Dispose();
                }
                catch
                {
                }
                _SecuritySQLConnection = null;
            }

            try
            {
                _SecuritySQLConnection = new SqlConnection(AppConfig.SecurityDBConnectionString);
                _SecuritySQLConnection.Open();
                return true;
            }
            catch
            {
                _SecuritySQLConnection = null;
                return false;
            }
        }

	  //  public static string GetAppSettingOrNull(string settingName)
	  //  {
	  //	  try
	  //	  {
	  //		  ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
	  //		  configFile.ExeConfigFilename = AppConfig.DLLPath + ".config";
	  //		  System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(
	  //			  configFile,ConfigurationUserLevel.None);

                
	  //		  // Display raw xml.
	  ////          GUIHelper.ShowError("XML:{0}", config.AppSettings.SectionInformation.GetRawXml());

                
	  //		  string[] allKeys = config.AppSettings.Settings.AllKeys;

	  //		  foreach (string key in allKeys)
	  //		  {
	  //			  if (key == settingName)
	  //			  {
	  //				  return config.AppSettings.Settings[key].Value;
	  //			  }
	  //		  }
                
	  //		  return null;
	  //	  }
	  //	  catch (Exception ex)
	  //	  {
	  //		  GUIHelper.ShowError(ex.Message);
	  //		  return null;
	  //	  }    
	  //  }
       
        public static int CSAUserID = -1;

		//private static bool GetAppSettingAsBool(string paramName)
		//{
		//	string strValue = GetAppSettingOrNull(paramName);
		//	if (strValue != null)
		//	{
		//		if (strValue.ToUpper() == "TRUE") return true;
		//		else return false;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}

        private static bool ConfigLoaded = false;
        
		//public static bool LoadApplicationIDOnce()
		//{
		//	if (ConfigLoaded == true) return true;
		//	ConfigLoaded = true;
		//	return LoadApplicationID();
		//}

		//public static bool LoadApplicationID()
		//{ 
		//	try
		//	{
		//		ApplicationID = Convert.ToInt32(GetAppSettingOrNull("SecurityApplicationID"));
		//		RoleID = Convert.ToInt32(GetAppSettingOrNull("SecurityRoleID"));
		//		SecurityDBConnectionString = GetAppSettingOrNull("SecurityDBConnectionString");
		//		DBConnectionString = GetAppSettingOrNull("DBConnectionString");
		//		AppConfig.EventLogFilePath = AppConfig.GetAppSettingOrNull("EventLogFilePath");
		//		//GUIHelper.ShowError("VEM1001: SecurityDBConnection String: {0}, ApplicationID: {1}", SecurityDBConnectionString, ApplicationID);
		//	}
		//	catch
		//	{ // FormatException
		//		return false;
		//	}

		//	if (SecurityDBConnectionString == null || SecurityDBConnectionString.Length == 0)
		//		return false;
		//	else
		//		return true;
		//}
      
        public static bool EstablishConnection()
        {
            return EstablishConnection(false);
        }

        public static bool EstablishConnection(bool suppressMsg)
        {
            // Check if connection was already established earlier
            if (ActiveDB_Connection != null)
            {
                return true;
            }

            if (ApplicationDB_ConnectionString == null || ApplicationDB_ConnectionString.Length == 0)
            {
                return false;
            }
            try
            {
                ActiveDB_Connection = new SqlConnection(ApplicationDB_ConnectionString);
                ActiveDB_Connection.Open();
                return true;
            }
            catch (SqlException e)
            {
                if (suppressMsg == false)
                {
                    MessageBox.Show(e.Message, "EstablishConnection error");
                }
                return false;
            }
        }

        public static void CloseConnection()
        {
            if (ActiveDB_Connection != null)
            {
                try
                {
                    ActiveDB_Connection.Close();
                }
                catch { }
            }
            ActiveDB_Connection = null;
        }


        /// <summary>
        /// 03/06/09, OVK
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="formatString"></param>
        /// <param name="pArr"></param>
        /// <returns></returns>
        public static DataRow LoadFirstRowAsync(SqlConnection connection, string formatString, params object[] pArr)
        {
            string sel_qry = String.Format(formatString, pArr);
            DataTable dt = AppConfig.LoadTableAsync(connection, sel_qry);
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0) return null;
            else return dt.Rows[0];
        }



        public static DataTable LoadTableAsync(SqlConnection connection, string sel_qry)
        {
            try
            {
                SqlCommand myCommand = new SqlCommand(sel_qry);
                myCommand.Connection = connection;
                myCommand.CommandTimeout = 120; // 120 seconds = 2 minutes 17-May-2006.
                SqlDataAdapter custDA = new SqlDataAdapter(myCommand);

                DataTable sTab = new DataTable();
                custDA.Fill(sTab);
                return sTab;
            }
            catch (SqlException e)
            {
                //				if(RestoreConnection_Or_RetryAfterLocks(ex)==true) 
                //				{ // OOP style...
                //					return LoadTable(sel_qry);
                //				}
                //				else 
                //				{
                MessageBox.Show(e.Message, "LoadTableAsync exception");
                return null;
                //				}
            }
        }


        /// <summary>
        /// Added on 03/06/09
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="qry"></param>
        /// <returns></returns>
        public static int PerformSQLCommandAsync(SqlConnection connection, string qry)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry);
                cmd.Connection = connection;
                int nRows = cmd.ExecuteNonQuery();
                return nRows;//nRows>0; 07/08/08
                // may be just true? because some Updates will update 0 rows, and that's OK.
                // may be I should return INT.
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message + String.Format(@"

SQL Query:
{0}
", qry), "PerformSQLCommandAsync exception");
                return -1;

            }

        }
 

        /// <summary>
        /// Compile SQL Statement from template.
        /// SQL Template contains %PARAMETER1%,etc. which will
        /// be replaced with provided parameterValues 
        /// </summary>
        /// <param name="sqlTemplate">Example: "SELECT * FROM %PARAMETER1% WHERE %PARAMETER2%"</param>
        /// <param name="parameterValues">Example: {"Clients","ClientID=1"}. Intepreting Clients as %PARAMETER1% and "ClientID=1" as "%PARAMETER%2"</param>
        /// <returns>Compiled SQL string generated from template. Example: "SELECT * FROM Clients WHERE ClientID=1"</returns>
        public static string CompileSQLString(string sqlTemplate, string[] parameterValues)
        {
            string compiledString = sqlTemplate;

            for (int i = 0; i < parameterValues.Length; i++)
            {
                string paramName = String.Format("%PARAMETER{0}%", i + 1);
                compiledString = compiledString.Replace(paramName, parameterValues[i]);
            }
            return compiledString;
        }


        public static string GetExpectedSingleValueAsync(SqlConnection connection,
            string formatString, params object[] pArr)
        {
            string qry = null;
			if (pArr != null && pArr.Length > 0)
			{
				try
				{
					qry = String.Format(formatString, pArr);
				}
				catch (Exception fEx)
				{
					Console.WriteLine(fEx.Message);
					return null;
				} 
			}
			else
			{
				qry = formatString;
			}
            string retVal = null;

            DataTable dt = LoadTableAsync(connection, qry);
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
            {
                retVal = null;
            }
            else
            {
                retVal = dt.Rows[0][0].ToString();
            }

            return retVal;
        }
        
        #region Application Forms Monitoring

        public static ArrayList appForms = new ArrayList();

        public static void AddForm(Form xForm)
        {
            lock (appForms)
            {
                if (appForms.Contains(xForm) == false) appForms.Add(xForm);
            }
            
        }

        public static void RemoveForm(Form xForm)
        {
            lock (appForms)
            {
                if (appForms.Contains(xForm) == true) appForms.Remove(xForm);
            }
        }
        

        #endregion Application Forms Monitoring

		//public static string GetReason(string actionName)
		//{
		//	return GetReason(actionName, 3); // 9/26/13, OVK: old default
		//}

        /// <summary>
        /// 9/26/13, OVK: Extending with min characters parameter
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="minCharacters"></param>
        /// <returns></returns>
		//public static string GetReason(string actionName, int minCharacters)
		//{
		//	ActionReasonDialog dlg = new ActionReasonDialog(actionName, minCharacters);

		//	if (dlg.ShowDialog() == DialogResult.OK)
		//	{
		//		return dlg.Reason;
		//	}
		//	else
		//	{
		//		return null;
		//	}
		//}

   

        public static string[] LoadTableOfIDsAsync(SqlConnection connection, string xQry)
        {
            DataTable dt = AppConfig.LoadTableAsync(connection, xQry);

            if (dt == null || dt.Rows == null || dt.Rows.Count == 0) return new string[0];

            string[] ids = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ids[i] = dt.Rows[i][0].ToString();
            }

            return ids;
        }

        /// <summary>
        /// Added on 03/06/09
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="formatString"></param>
        /// <param name="pArr"></param>
        /// <returns></returns>
        public static string[] LoadTableOfIDsAsync(SqlConnection connection, string formatString, params object[] pArr)
        {
            string qry = String.Format(formatString, pArr);
            return LoadTableOfIDsAsync(connection, qry);
        }



        

        /// <summary>
        /// Retrive list of distinct values in desired column of give DataTable
        /// </summary>
        /// <param name="dt">DataTable to search in</param>
        /// <param name="column_name">Column Name to search for distinct values</param>
        /// <returns>ArrayList of distinct values in the specified column</returns>
        public static ArrayList TableColumnDistinctValues(DataTable dt, string condition, string column_name)
        {
            if (condition == null) condition = "";

            if (dt == null || dt.Rows.Count == 0)
            {
                return new ArrayList(0);
            }

            DataRow[] sortedRows = dt.Select(condition, column_name + " ASC");
            object prev_value = null;

            ArrayList retA = new ArrayList();

            for (int i = 0; i < sortedRows.Length; i++)
            {
                object current_value = sortedRows[i][column_name].ToString();
                if (prev_value == null || !current_value.Equals(prev_value))
                {
                    retA.Add(current_value);
                }

                prev_value = current_value;
            }

            return retA;
        }

        public static string[] GetDistinctValues(DataTable dt, string condition, string columnName)
        {
            if (condition == null) condition = "";
            ArrayList aList = TableColumnDistinctValues(dt, condition, columnName);

            // 06/17/2008 - Filter empty values from aList
            aList.Remove("");

            string[] dvals = new string[aList.Count];
            for (int i = 0; i < aList.Count; i++)
            {
                dvals[i] = (string)aList[i];
            }
            return dvals;
        }


    

        public static bool ExitRequested = false;
        public static void RequestExit()
        {
            ExitRequested = true;
        }



        /// <summary>
        /// 03/06/09, OVK.
        /// Helper method for establishing alternative connection to MIRA DB.
        /// For use with different threads.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static SqlConnection EstablishAsyncConnection(out string errorMessage)
        {
            errorMessage = null;
            if (AppConfig.ApplicationDB_ConnectionString == null || AppConfig.ApplicationDB_ConnectionString.Length == 0)
            {
                errorMessage = "Connection String is not defined";
                return null;
            }

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(AppConfig.ApplicationDB_ConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                // no msg...
                errorMessage = ex.Message;
                return null;
            }
        }

        public static void CloseAsyncConnection(SqlConnection connection)
        {
            try
            {
                if (connection == null) return;
                connection.Close();
            }
            catch
            {
            }
        }


        public static DateTime GetServerTime()
        {
            return DateTime.Now;


            //DateTime dt = DateTime.Parse("01/01/1900");
            //string strDateTime = AppConfig.GetExpectedSingleValue("SELECT GetDate();");
            //if (strDateTime != null && strDateTime != "")
            //{
            //    DateTime.TryParse(strDateTime, out dt);
            //}
            //return dt;
        }

        public static void LogEvent(string formatString, params object[] args)
        {
            if (AppConfig.EventLogFilePath == null) return;

            try
            {
                string userString = String.Format(formatString, args);

                string finalString = String.Format("{0}: {1}",
                    DateTime.Now.ToString("yyyyMMdd hhmmss.fff tt"), userString);

                FileInfo outfi = new FileInfo(AppConfig.EventLogFilePath);
                using (StreamWriter sw = outfi.AppendText())
                {
                    sw.WriteLine(finalString);
                }
            }
            catch
            {
            }
        }
    }
}
