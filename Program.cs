using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HtmltoPdf
{
    class Program
    {
        static void Main()
        {
            var path = ConfigurationSettings.AppSettings["Output_path"].ToString();
            var prefix = ConfigurationSettings.AppSettings["Output_file_prefix"].ToString();
           // var url = ConfigurationSettings.AppSettings["Url"].ToString();
            var url = "https://www.ttbonline.gov/colasonline/viewColaDetails.do?action=publicFormDisplay&ttbid=10363001000111";
            var Exepath = ConfigurationSettings.AppSettings["Exe_path"].ToString();
            String[] Options = null;

           // string AppDomainManagerInitializationOptions =
          PdfGenerator.HtmlToPdf( path,prefix,url,Options,Exepath);
        }
    }
}
