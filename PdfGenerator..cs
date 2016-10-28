using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Diagnostics;

namespace HtmltoPdf
{
    class PdfGenerator
    {
        public static string HtmlToPdf(string pdfOutputLocation, string outputFilenamePrefix, string urls,
            string[] options = null,
            string pdfHtmlToPdfExePath = "'C:\\Program Files (x86)\\wkhtmltopdf\\wkhtmltopdf.exe'")
        {             
            string urlsSeparatedBySpaces = string.Empty;
            try
            {
                 
                //Determine inputs
                if ((urls == null) || (urls.Length == 0))
                    throw new Exception("No input URLs provided for HtmlToPdf");
                else
                    urlsSeparatedBySpaces = String.Join(" ", urls); //Concatenate URLs

                string outputFolder = pdfOutputLocation;

               // Console.WriteLine(String.IsNullOrWhiteSpace( pdfHtmlToPdfExePath));
                string outputFilename = outputFilenamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".pdf"; // assemble destination PDF file name
                Console.WriteLine(((options == null) ? "" : String.Join(" ", options)) + " " + urlsSeparatedBySpaces + " " + outputFilename);
                var p = new System.Diagnostics.Process();               
                    p.StartInfo.FileName =pdfHtmlToPdfExePath;
                    p.StartInfo.Arguments = urlsSeparatedBySpaces + " " + outputFilename;
                    p.StartInfo.UseShellExecute = false; // needs to be false in order to redirect output
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.RedirectStandardInput = true; // redirect all 3, as it should be all 3 or none
                    p.StartInfo.WorkingDirectory = outputFolder;                                 
                    p.Start();

                // read the output here...
                var output = p.StandardOutput.ReadToEnd();
                var errorOutput = p.StandardError.ReadToEnd();

                // ...then wait n milliseconds for exit (as after exit, it can't read the output)
                p.WaitForExit(60000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                // if 0 or 2, it worked so return path of pdf
                if ((returnCode == 0) || (returnCode == 2))
                    return outputFolder + outputFilename;
                else
                    throw new Exception(errorOutput);
            }
           
            catch (Exception ex)
            {
                throw new Exception("Processor Usage" + ex.Message);
            }

        }
    }
}
