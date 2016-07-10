using System;
using HL7ReportsDA.DataEntities;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace HL7ReportsDA
{
    public static class ReportWebServicesBL
    {
        public static string GetReports(String PatientName, String AccessionNumber, string PatientID = "")
        {
            string reportResult = string.Empty;
            try
            {
                List<Report> reports = DataAccess.GetReports(PatientName, AccessionNumber);
                if (reports.Count < 1)
                {///if no reports found in DB then query PCCI server
                    if (PatientID != string.Empty)
                    {
                        FetchReportFromPCCI(AccessionNumber, PatientID);
                    }
                    else
                    {
                       // log;
                    }
                }
                else
                {
                    if (reports != null && reports.Count > 0)
                    {
                        reportResult += "<Reports> ";
                        foreach (Report r in reports)
                        {
                            reportResult += r.ReportText += "\r\n";
                        }
                        reportResult += " </Reports> ";
                    }
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
            return reportResult;
        }

        private static void FetchReportFromPCCI(string AccessionNumber, string PatientID)
        {
            String ReportText = string.Empty;
            PCCIReports.PCCIReports pr = new PCCIReports.PCCIReports();
            ReportText = pr.getReport(AccessionNumber, PatientID);
            if (ReportText != string.Empty)
            {
                
            }

        }
    }
}
