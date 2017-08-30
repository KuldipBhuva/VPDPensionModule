using PensionModel;
using System;
using System.Xml;




namespace Pension.Models.ViewModel
{
    public class ErrorLogClass
    {
        VPDPensionEntities DbContext = new VPDPensionEntities();
        public int SaveLog(string msg)
        {
            ErrorMsgMaster objerrr = new ErrorMsgMaster();
            objerrr.UserId = 1;
            //objerrr.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
            objerrr.ErrorMsg = msg;
            objerrr.ExceptiionMsg = msg;
            DbContext.ErrorMsgMasters.Add(objerrr);
            DbContext.SaveChanges();
            return objerrr.EId;
        }

        public static void WriteErrorLog(string Module, string PageName, string Function, Exception objex)
        {
            try
            {
                string temp = objex.Message;
                if (!temp.Contains("Thread was being aborted"))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    XmlElement error = xmldoc.CreateElement("Error");

                    XmlElement dt = xmldoc.CreateElement("DateTime");
                    dt.InnerText = DateTime.Now.ToString();
                    error.AppendChild(dt);

                    XmlElement tagname = xmldoc.CreateElement("ModuleName");
                    tagname.InnerText = Module;
                    error.AppendChild(tagname);
                    tagname = null;

                    tagname = xmldoc.CreateElement("PageName");
                    tagname.InnerText = PageName;
                    error.AppendChild(tagname);
                    tagname = null;

                    tagname = xmldoc.CreateElement("FunctionName");
                    tagname.InnerText = PageName;
                    error.AppendChild(tagname);
                    tagname = null;

                    tagname = xmldoc.CreateElement("ErrorMessage");
                    tagname.InnerText = PageName;
                    error.AppendChild(tagname);
                    tagname = null;


                    tagname = xmldoc.CreateElement("ErrorStackTrace");
                    tagname.InnerText = PageName;
                    error.AppendChild(tagname);
                    tagname = null;

                    ErrorLogClass objerror = new ErrorLogClass();
                    objerror.SaveLog("<ErrorLog>" + error.OuterXml.Replace("'","'") + "</ErrorLog>");



                }

            }

            catch (Exception ex)
            {
               throw ex;
            }

        }


    }
}