using System;
using System.Linq;
using NHapi.Base.Util;
using NHapi.Base.Model;
using System.Globalization;
using HL7ReportsDA.DataEntities;
using System.Xml;
using System.IO;

namespace HL7ReportsDA
{
    public static class HL7ReportsBL
    {
        public static void SaveNewReport(Report report)
        {
            DataAccess.SaveNewReport(report);
        }
        public static DataEntities.Report GetReportObject_231(NHapi.Model.V231.Message.ORU_R01 orur01)
        {
            DataEntities.Report report = new DataEntities.Report();
            try
            {

                //WriteToHL7LogFile(orur01.GetRESPONSE().PATIENT.PID.AlternatePatientID.Me)

                //MsgBox(orur01.GetRESPONSE().GetORDER_OBSERVATION().GetOBSERVATION.OBX.GetField(10))
                //Terser T = new Terser(orur01);
                //ISegment iSeg = null;
                //int j = 0;

                //int numOBX = orur01. ; //orur01.GetRESPONSE().GetORDER_OBSERVATION().OBSERVATIONRepetitionsUsed;
                //string rep = null;
                ////WriteToHL7LogFile_ClearAll()

                //DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                //myDTFI.ShortDatePattern = "yyyyMMddhhmmss";

                //string AccessionNum = orur01. ;// orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.FillerOrderNumber[0].ToString();
                //string StudyDesc = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.UniversalServiceIdentifier[1].ToString();
                //string StudyDate = string.Empty;
                //StudyDate = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.ObservationDateTime[0].ToString();
                //if (!(StudyDate == null || StudyDate == String.Empty))
                //{
                //    StudyDate = DateTime.ParseExact(StudyDate, "d", myDTFI).ToString();
                //}
                //else
                //{
                //    StudyDate = string.Empty;
                //}
                //string PatientID = orur01.GetRESPONSE().PATIENT.PID.PatientAccountNumber[0].ToString();
                //string PatientName = orur01.GetRESPONSE().PATIENT.PID.PatientName[0].ToString() + " " + orur01.GetRESPONSE().PATIENT.PID.PatientName[1].ToString() + " " + orur01.GetRESPONSE().PATIENT.PID.PatientName[2].ToString();



                ////rep = "{\rtf1\par"
                //rep = "{\\rtf1";

                ////rep = rep & " \par " ' Blank space is needed before and after \par else the word before or after gets clipped
                //rep = rep + " \\b ";
                //rep = rep + "Accession Num.: " + AccessionNum;
                //rep = rep + " \\b0 ";
                //rep = rep + " \\par ";
                //// Blank space is needed before and after \par else the word before or after gets clipped
                //rep = rep + " \\b ";
                //rep = rep + "Patient ID: " + PatientID;
                //rep = rep + " \\b0 ";
                //rep = rep + " \\par ";
                //// Blank space is needed before and after \par else the word before or after gets clipped
                //rep = rep + " \\b ";
                //rep = rep + "Patient Name: " + PatientName;
                //rep = rep + " \\b0 ";
                //rep = rep + " \\par ";
                //// Blank space is needed before and after \par else the word before or after gets clipped
                //rep = rep + " \\b ";
                //rep = rep + "Study Description: " + StudyDesc;
                //rep = rep + " \\b0 ";
                //rep = rep + " \\par ";
                //// Blank space is needed before and after \par else the word before or after gets clipped
                //rep = rep + " \\b ";
                //rep = rep + "Study Date: " + StudyDate;
                //rep = rep + " \\b0 ";
                //rep = rep + " \\par ";
                //// Blank space is needed before and after \par else the word before or after gets clipped


                //for (j = 0; j <= numOBX; j++)
                //{
                //    iSeg = orur01.GetRESPONSE().GetORDER_OBSERVATION().GetOBSERVATION(j).OBX;
                //    //rep = rep & Terser.Get(iSeg, 5, 0, 1, 1) & "\par"
                //    rep = rep + Terser.Get(iSeg, 5, 0, 1, 1);
                //    rep = rep + " \\par ";
                //    // Blank space is needed before and after \par else the word before or after gets clipped
                //}

                //rep = rep + "}";
            }
            catch (Exception e)
            {
                throw e;
            }

            return report;
        }
        public static DataEntities.Report GetReportObject_23(NHapi.Model.V23.Message.ORU_R01 orur01)
        {
            DataEntities.Report report = new DataEntities.Report();
            try
            {


                Terser T = new Terser(orur01);
                ISegment iSeg = null;
                int j = 0;

                int numOBX = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBSERVATIONRepetitionsUsed;
                string rep = null;


                DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
                myDTFI.ShortDatePattern = "yyyyMMddhhmmss";

                string AccessionNum = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.FillerOrderNumber[0].ToString();
                string StudyDesc = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.UniversalServiceIdentifier[1].ToString();
                string StudyDate = string.Empty;
                StudyDate = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.ObservationDateTime[0].ToString();
                if (!(StudyDate == null || StudyDate == String.Empty))
                {
                    StudyDate = DateTime.ParseExact(StudyDate, "d", myDTFI).ToString();
                    report.ReportDateTime = DateTime.ParseExact(StudyDate, "d", myDTFI);
                }
                else
                {
                    StudyDate = string.Empty;
                }
                string PatientID = orur01.GetRESPONSE().PATIENT.PID.PatientAccountNumber[0].ToString();
                string PatientName = orur01.GetRESPONSE().PATIENT.PID.PatientName[0].ToString() + " " + orur01.GetRESPONSE().PATIENT.PID.PatientName[1].ToString() + " " + orur01.GetRESPONSE().PATIENT.PID.PatientName[2].ToString();

                report.ImportedOn = DateTime.Now;
                report.PatientID = PatientID;
                report.Patientname = PatientName;
                report.AccessionNumber = AccessionNum;

                int Seq = 0;
                Seq = AddReportField(report, Constant.ACCESSION_NUMBER, AccessionNum, Seq);
                Seq = AddReportField(report, Constant.PATIENT_ID, PatientID, Seq);
                Seq = AddReportField(report, Constant.PATIENT_NAME, PatientName, Seq);
                Seq = AddReportField(report, Constant.STUDY_DATE, StudyDate, Seq);
                Seq = AddReportField(report, Constant.STUDY_DESCRIPTION, StudyDesc, Seq);
                
                for (j = 0; j <= numOBX; j++)
                {
                    iSeg = orur01.GetRESPONSE().GetORDER_OBSERVATION().GetOBSERVATION(j).OBX;
                    string obs = string.Empty;
                    obs = Terser.Get(iSeg, 5, 0, 1, 1);
                    Seq = AddReportField(report, Constant.OBSERVATION, obs, Seq);
                    rep = rep + obs;
                }
                                
                XmlDocument xformat = getReportFormat();
                xformat.SelectSingleNode("//AccessionNumber").InnerText = AccessionNum;
                xformat.SelectSingleNode("//PatientID").InnerText = PatientID;
                xformat.SelectSingleNode("//PatientName").InnerText = PatientName;
                xformat.SelectSingleNode("//ReportDate").InnerText = StudyDate;
                xformat.SelectSingleNode("//StudyDescription").InnerText = StudyDesc;
                rep = xformat.InnerXml;
                report.ReportText = rep;

                Seq = AddReportField(report, Constant.REPORT_TEXT, rep, Seq);
            }
            catch (Exception e)
            {
                throw e;
            }
            return report;
        }

        private static int AddReportField(DataEntities.Report report, string fieldName, string Value, int Seq)
        {
            DataEntities.Field f = new DataEntities.Field();
            DataEntities.FieldName fn = new FieldName();
            fn = DataAccess.GetAllFieldNames().Where(x => x.FieldNameDes == fieldName).FirstOrDefault();
            f = new DataEntities.Field()
            {
                FieldName = fn,
                Value = Value,
                Sequence = Seq++
            };
            report.ReportFields.Add(f);
            return Seq;
        }

        private static void WriteToHL7LogFile(string v)
        {
            Common.Log.WriteFilelog(HL7ReportsDA.Constant.APP_NAME, HL7ReportsDA.Constant.HL7_Listener_ModuleName, v);
        }


        static XmlDocument getReportFormat()
        {
            XmlDocument reportFormat = new XmlDocument();
            try
            {
                reportFormat.Load(Properties.Settings.Default.ReportsSetting);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            return reportFormat;
        }
    }
}







//////MsgBox(orur01.GetRESPONSE().GetORDER_OBSERVATION().GetOBSERVATION.OBX.GetField(10))
////Terser T = new Terser(orur01);
////ISegment iSeg = null;
////int j = 0;

////int numOBX = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBSERVATIONRepetitionsUsed;
////string rep = null;
//////WriteToHL7LogFile_ClearAll()

////DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
////myDTFI.ShortDatePattern = "yyyyMMddhhmmss";

////                string AccessionNum = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.FillerOrderNumber[0].ToString();
////string StudyDesc = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.UniversalServiceIdentifier[1].ToString();
////string StudyDate = string.Empty;
////StudyDate = orur01.GetRESPONSE().GetORDER_OBSERVATION().OBR.ObservationDateTime[0].ToString();
////                if (!(StudyDate == null || StudyDate == String.Empty))
////                {
////                    StudyDate = DateTime.ParseExact(StudyDate, "d", myDTFI).ToString();
////                }
////                else
////                {
////                    StudyDate = string.Empty;
////                }
////                string PatientID = orur01.GetRESPONSE().PATIENT.PID.PatientAccountNumber[0].ToString();
////string PatientName = orur01.GetRESPONSE().PATIENT.PID.PatientName[0].ToString() + " " + orur01.GetRESPONSE().PATIENT.PID.PatientName[1].ToString() + " " + orur01.GetRESPONSE().PATIENT.PID.PatientName[2].ToString();



//////rep = "{\rtf1\par"
////rep = "{\\rtf1";

////                //rep = rep & " \par " ' Blank space is needed before and after \par else the word before or after gets clipped
////                rep = rep + " \\b ";
////                rep = rep + "Accession Num.: " + AccessionNum;
////                rep = rep + " \\b0 ";
////                rep = rep + " \\par ";
////                // Blank space is needed before and after \par else the word before or after gets clipped
////                rep = rep + " \\b ";
////                rep = rep + "Patient ID: " + PatientID;
////                rep = rep + " \\b0 ";
////                rep = rep + " \\par ";
////                // Blank space is needed before and after \par else the word before or after gets clipped
////                rep = rep + " \\b ";
////                rep = rep + "Patient Name: " + PatientName;
////                rep = rep + " \\b0 ";
////                rep = rep + " \\par ";
////                // Blank space is needed before and after \par else the word before or after gets clipped
////                rep = rep + " \\b ";
////                rep = rep + "Study Description: " + StudyDesc;
////                rep = rep + " \\b0 ";
////                rep = rep + " \\par ";
////                // Blank space is needed before and after \par else the word before or after gets clipped
////                rep = rep + " \\b ";
////                rep = rep + "Study Date: " + StudyDate;
////                rep = rep + " \\b0 ";
////                rep = rep + " \\par ";
////                // Blank space is needed before and after \par else the word before or after gets clipped


////                for (j = 0; j <= numOBX; j++)
////                {
////                    iSeg = orur01.GetRESPONSE().GetORDER_OBSERVATION().GetOBSERVATION(j).OBX;
////                    //rep = rep & Terser.Get(iSeg, 5, 0, 1, 1) & "\par"
////                    rep = rep + Terser.Get(iSeg, 5, 0, 1, 1);
////                    rep = rep + " \\par ";
////                    // Blank space is needed before and after \par else the word before or after gets clipped
////                }

////                rep = rep + "}";