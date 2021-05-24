using AssignmentWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace AssignmentWebApi
{
    public class Logger
    {
        //create a Logs.txt file for creating logs of all the actions and creating error log in case of any error.
        public static void CreateLog(Exception ex)
        {
            
            try
            {
                string path = "~/Error/" + DateTime.Today.ToString("dd-MMM-yy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("Location :" + ex.StackTrace);
                    w.WriteLine("Log Message : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    
                    w.WriteLine(ex.Message);
                    w.WriteLine("==============================================================================================================================");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception e)
            {
                CreateLog(e);
            }
            return;
            }
                               
    }
}