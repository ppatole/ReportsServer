using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IntelliSpaceISE
{
    public partial class ImageDlg : Form
    {

        public ImageDlg()
        {
            InitializeComponent();
        }

        public bool InitializeEnterpriseCtrl(AxISITELib.AxiSiteNonVisual iSiteNonVisual)
        {
            bool bRet = false;

            axiSiteEnterprise1.Options = "StentorBackEnd,DisableAutoLogout,DisableTipOfTheDay,DisablePreferences,HideCloseTabButton,HideFolder,DisableClinicalExamNotes,HideLogoutButton,HideReportButton";

            bRet = axiSiteEnterprise1.Initialize(iSiteNonVisual);

            if (!bRet)
                MessageBox.Show("Error Initializing Enterprise Control");

            return bRet;
        }

        public void OpenCanvasPage(string ExamID)
        {
            string canvasPageID = "";

            if (ExamID.Length > 0)
            {
                canvasPageID = axiSiteEnterprise1.OpenCanvasPage(ExamID, "", true, false, true);

                if (canvasPageID == "")
                {
                    MessageBox.Show("Error Opening CanvasPage");
                }
            }

        }


    }
}
