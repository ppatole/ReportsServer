using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCCIReports
{
    public class PCCIReports
    {
        private AxISITELib.AxiSiteNonVisual axiSiteNonVisual;

        string accessionNo = string.Empty, patientID = string.Empty, org = string.Empty;

        public string AccessionNo
        {
            get
            {
                return accessionNo;
            }

            set
            {
                accessionNo = value;
            }
        }

        public string PatientID
        {
            get
            {
                return patientID;
            }

            set
            {
                patientID = value;
            }
        }

        public string Org
        {
            get
            {
                return org;
            }          
        }

        protected bool InitializeNonVisualCtrl()
        {
            bool bRet = false;

            if (axiSiteNonVisual.Initialized)
                bRet = true;
            else
            {
                axiSiteNonVisual.iSyntaxServerIP = Properties.Settings.Default.ServerIP;// serverIP.Text;
                axiSiteNonVisual.ImageSuiteURL = "http://" + Properties.Settings.Default.ServerIP + "/iSiteWeb/WorkList/PrimaryWorkList.ashx/";
                axiSiteNonVisual.ImageSuiteDSN = "iSite";
                axiSiteNonVisual.Options = "StentorBackEnd,HideLoginWindow";                 //DisableISSA

                bRet = axiSiteNonVisual.Initialize();

                if (!bRet)
                {
                    RecordLog("Error Initializing NonVisual Control : " + axiSiteNonVisual.GetLastErrorCode());
                }
            }

            return bRet;

        }

        protected bool Login()
        {
            bool bRet = false;

            if (axiSiteNonVisual.GetCurrentUser() == "")
            {
                bRet = axiSiteNonVisual.Login(Properties.Settings.Default.ServerUserName, Properties.Settings.Default.ServerPassword, "iSite", "", "");

                if (!bRet)
                    RecordLog("Error logging in " + axiSiteNonVisual.GetLastErrorCode());
            }
            else
                bRet = true;

            return bRet;
        }

        public string getReport(string accessionNo, string patientID)
        {
            this.accessionNo = accessionNo;
            this.patientID = patientID;
            this.org = Properties.Settings.Default.OrgName;

            if (InitializeNonVisualCtrl())
            {
                if (Login())
                {
                    String ExamID;
                    ExamID = axiSiteNonVisual.FindExam(AccessionNo, PatientID, org);
                    System.GC.Collect();
                    return axiSiteNonVisual.GetReportData(PatientID, ExamID);
                }
                else
                {
                    return "Intellispace server Login Failed";
                }
            }
            else
            {
                return "Connection with Intellispace Server couldnot be established";
            }
        }

        private void RecordLog(String log)
        {
            Common.Log.WriteFilelog("PCCI Reports", "Reports pull", log);

        }  
    }
}
