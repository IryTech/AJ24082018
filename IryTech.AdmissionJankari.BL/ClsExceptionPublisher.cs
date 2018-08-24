using System;
using IryTech.AdmissionJankari.DAL;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IryTech.AdmissionJankari.BL
{
/// <summary>
/// Summary description for ClsExceptionPublisher
/// </summary>
public class ClsExceptionPublisher
{
	public ClsExceptionPublisher()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public  void WriteLogFile(string str)
    {
           
            // Open the log file for append and write the log
            string strLogFile = Common.StrBaseDirectory + "Error.log";
            TextWriter tw = new StreamWriter(strLogFile, true);
            tw.WriteLine("Exception at :- " + DateTime.Now.ToLongTimeString() + " :: Exception Info  : - " + str + "\n\r");
            tw.Close();
           

            //string logFile = "~/App_Data/ErrorLog.txt";
            //logFile = HttpContext.Current.Server.MapPath(logFile);

            //// Open the log file for append and write the log
            //StreamWriter sw = new StreamWriter(logFile, true);
            //sw.WriteLine("********** {0} **********", DateTime.Now);
            //if (exc.InnerException != null)
            //{
            //    sw.Write("Inner Exception Type: ");
            //    sw.WriteLine(exc.InnerException.GetType().ToString());
            //    sw.Write("Inner Exception: ");
            //    sw.WriteLine(exc.InnerException.Message);
            //    sw.Write("Inner Source: ");
            //    sw.WriteLine(exc.InnerException.Source);
            //    if (exc.InnerException.StackTrace != null)
            //    {
            //        sw.WriteLine("Inner Stack Trace: ");
            //        sw.WriteLine(exc.InnerException.StackTrace);
            //    }
            //}
            //sw.Write("Exception Type: ");
            //sw.WriteLine(exc.GetType().ToString());
            //sw.WriteLine("Exception: " + exc.Message);
            //sw.WriteLine("Source: " + source);
            //sw.WriteLine("Stack Trace: ");
            //if (exc.StackTrace != null)
            //{
            //    sw.WriteLine(exc.StackTrace);
            //    sw.WriteLine();
            //}
            //sw.Close();
        

    }

    public void Publish(string exceptionInfo, string additionalInfo)
    {
        string connectString = System.Configuration.ConfigurationManager.AppSettings["DB_CON_STRING"].ToString();
        DbWrapper objDataWrapper = new DbWrapper(System.Configuration.ConfigurationManager.AppSettings["DB_CON_STRING"].ToString(), CommandType.StoredProcedure);


        try
        {
            objDataWrapper.AddParameter("@exceptionInfo", exceptionInfo);
            objDataWrapper.AddParameter("@additionalInfo", additionalInfo);
            int i = objDataWrapper.ExecuteNonQuery("Aj_Proc_InsertException");
            
        }
        catch (Exception ex)
        {
            WriteLogFile(exceptionInfo + "," + additionalInfo + ex.ToString());
        }
    }
}
}