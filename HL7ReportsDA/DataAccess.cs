using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL7ReportsDA.DataEntities;
using System.Data.SqlClient;

namespace HL7ReportsDA
{
    public class DataAccess
    {
        public static void SaveNewReport(Report report)
        {
            FieldName fieldName = new FieldName();
            List<SqlParameter> prms = new List<SqlParameter>();
            prms.Add(new SqlParameter("@XML", Common.CommonFunctions.ConvertEntityToXML(report)));
            DatabaseHelper.ExecuteNonQuery("SaveReport", prms);
        }

        static List<FieldName> fieldNames;

        public static List<DataEntities.FieldName> GetAllFieldNames(bool Refresh = false)
        {

            if (Refresh || (fieldNames == null || fieldNames.Count < 1))
            {
                fieldNames = new List<FieldName>();
                SqlDataReader sdr = DatabaseHelper.ExecuteSQLReader("GetAllFieldNames");
                while (sdr.Read())
                {
                    fieldNames.Add(new FieldName()
                    {
                        FieldNameID = sdr["FieldNameID"].ToString(),
                        FieldNameDes = sdr["FieldNameDes"].ToString(),
                        ParentFieldNameID = sdr["ParentFieldNameID"].ToString()
                    });
                }

                sdr.Close();

                //find parents
                foreach (FieldName fName in fieldNames)
                {
                    //check cyclic ref
                    /// TODO: check cyclic ref, yet to implement 

                    fName.ParentFieldName = fieldNames.Where(x => x.FieldNameID == fName.ParentFieldNameID).FirstOrDefault();
                }


            }


            return fieldNames;
        }



        /// <summary>
        /// this will return Field Name object.
        /// this will not fill in parent field name. then it might craete recurring calls
        /// whereever is required to get parent fieldName object, it must be called/codded saperatly. 
        /// </summary>
        /// <returns></returns>
        public static FieldName GetFieldNameByID(String FieldNameID)
        {
            FieldName fieldName = new FieldName();
            List<SqlParameter> prms = new List<SqlParameter>();
            prms.Add(new SqlParameter() { ParameterName = "@FieldNameID", Value = FieldNameID });
            SqlDataReader sdr = DatabaseHelper.ExecuteSQLReader("GetFieldNameByID");
            sdr.Read();
            fieldName = new FieldName()
            {
                FieldNameID = sdr["FieldNameID"].ToString(),
                FieldNameDes = sdr["FieldNameDes"].ToString(),
                ParentFieldNameID = sdr["ParentFieldNameID"].ToString()
            };
            return fieldName;
        }


        public static List<Report> GetReports(String PatientName, String AccessionNumber)
        {
            List<Report> Reports = new List<Report>();
            Reports = new List<Report>();

            List<SqlParameter> prms = new List<SqlParameter>();
            prms.Add(new SqlParameter() { ParameterName = "@PatientName", Value = PatientName });
            prms.Add(new SqlParameter() { ParameterName = "@AccessionNumber", Value = AccessionNumber });
            SqlDataReader sdr = DatabaseHelper.ExecuteSQLReader("GetReport",prms);
            while (sdr.Read())
            {
                Reports.Add(new Report()
                {
                    AccessionNumber = sdr["AccessionNumber"].ToString(),
                    ImportedOn = (DateTime)sdr["ImportedOn"],
                    PatientID = sdr["PatientID"].ToString(),
                    Patientname = sdr["Patientname"].ToString(),
                    ReportDateTime = (DateTime)sdr["ReportDateTime"],
                    ReportText = sdr["ReportXML"].ToString(),
                    
                });
            }
            sdr.Close();

            return Reports;
        }
    }
}
