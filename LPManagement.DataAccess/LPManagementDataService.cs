using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LPManagement.Common;

namespace LPManagement.DataAccess
{
    /// <summary>
    /// LaunchPad management data service.
    /// </summary>
    public class LPManagementDataService
    {
        /// <summary>
        /// Gets list of launch pad details.
        /// </summary>
        /// <returns>List of launch pad details.</returns>
        public IEnumerable<LaunchPadDetail> GetLaunchPadDetails()
        {

            return null;
        }

        public void PersistLaunchPadDetails(List<LaunchPadDetail> launchPadDetails)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["lpMGMTConnectionString"].ToString();
            DataTable lpDetailDataTable = CreateLPDetailDataTable(launchPadDetails);
            DataTable lpScoreDetailDataTable = CreateLPScoreDetailDataTable(launchPadDetails);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("[dbo].[usp_lp_detail_persist]", connection);
                command.CommandType = CommandType.StoredProcedure;

                var sqlParameter = command.Parameters.AddWithValue("@employee_data", lpDetailDataTable);
                sqlParameter.SqlDbType = SqlDbType.Structured;
                command.ExecuteNonQuery();

                command = new SqlCommand("[dbo].[usp_lp_score_persist]", connection);
                command.CommandType = CommandType.StoredProcedure;

                sqlParameter = command.Parameters.AddWithValue("@employee_score_data", lpScoreDetailDataTable);
                sqlParameter.SqlDbType = SqlDbType.Structured;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private DataTable CreateLPScoreDetailDataTable(List<LaunchPadDetail> launchPadDetails)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("employee_id", typeof(Int32));
            dataTable.Columns.Add("faculty", typeof(string));
            dataTable.Columns.Add("unix", typeof(float));
            dataTable.Columns.Add("software_engineering_and_uml", typeof(float));
            dataTable.Columns.Add("oracle_SQL_and_PL_SQL", typeof(float));
            dataTable.Columns.Add("dot_net", typeof(float));
            dataTable.Columns.Add("core_java", typeof(float));
            dataTable.Columns.Add("advance_java", typeof(float));
            dataTable.Columns.Add("J2EE", typeof(float));
            dataTable.Columns.Add("hibernate", typeof(float));
            dataTable.Columns.Add("spring", typeof(float));
            dataTable.Columns.Add("html", typeof(float));
            dataTable.Columns.Add("java_script", typeof(float));
            dataTable.Columns.Add("informatica", typeof(float));
            dataTable.Columns.Add("talent_development", typeof(float));
            dataTable.Columns.Add("manual_testing", typeof(float));
            dataTable.Columns.Add("UFT", typeof(float));
            dataTable.Columns.Add("selenium", typeof(float));
            dataTable.Columns.Add("OOPS", typeof(float));
            dataTable.Columns.Add("RDBMS", typeof(float));
            dataTable.Columns.Add("final_exam", typeof(float));
            dataTable.Columns.Add("overall_score", typeof(float));
            dataTable.Columns.Add("project_score", typeof(float));
            dataTable.Columns.Add("project_reviewer", typeof(string));
            dataTable.Columns.Add("overall_feedback", typeof(string));
            dataTable.Columns.Add("faculty_feedback", typeof(string));
            dataTable.Columns.Add("remarks", typeof(string));

            foreach (var launchPadDetail in launchPadDetails)
            {
                var lpScoreDetail = launchPadDetail.EmployeeScoreDetail;
                dataTable.Rows.Add(launchPadDetail.EmployeeId, lpScoreDetail.Faculty, lpScoreDetail.Unix, lpScoreDetail.SEAndUML, lpScoreDetail.SQL,
                    lpScoreDetail.DotNet, lpScoreDetail.CoreJava, lpScoreDetail.AdvanceJava, lpScoreDetail.J2EE, lpScoreDetail.Hibernate, lpScoreDetail.Spring,
                    lpScoreDetail.HTML, lpScoreDetail.JavaScript, lpScoreDetail.Informatica, lpScoreDetail.TalentDevelopment, lpScoreDetail.ManualTesting,
                    lpScoreDetail.UFT, lpScoreDetail.Selenium, lpScoreDetail.OOPS, lpScoreDetail.RDBMS, lpScoreDetail.FinalExam, lpScoreDetail.OverallScore,
                    lpScoreDetail.ProjectScore, lpScoreDetail.ProjectReviewer, lpScoreDetail.OverallFeedback, lpScoreDetail.FacultyFeedback, lpScoreDetail.Remarks);
            }

            return dataTable;
        }

        private static DataTable CreateLPDetailDataTable(List<LaunchPadDetail> launchPadDetails)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("launch_pad_code", typeof(string));
            dataTable.Columns.Add("location", typeof(string));
            dataTable.Columns.Add("financial_year", typeof(Int32));
            dataTable.Columns.Add("employee_id", typeof(Int32));
            dataTable.Columns.Add("employee_name", typeof(string));
            dataTable.Columns.Add("employee_e_mail", typeof(string));
            dataTable.Columns.Add("business_layer", typeof(string));
            dataTable.Columns.Add("practice", typeof(string));
            dataTable.Columns.Add("lp_category", typeof(string));
            dataTable.Columns.Add("training_location", typeof(string));
            dataTable.Columns.Add("work_location", typeof(string)); 
            dataTable.Columns.Add("status", typeof(string));
            dataTable.Columns.Add("utilization", typeof(string));

            foreach (var launchPadDetail in launchPadDetails)
            {
                dataTable.Rows.Add(launchPadDetail.LaunchPadCode, launchPadDetail.Location, launchPadDetail.FinancialYear,
                    launchPadDetail.EmployeeId, launchPadDetail.EmployeeName, launchPadDetail.EmployeeMailId, launchPadDetail.BusinessLayer,
                    launchPadDetail.Practice, launchPadDetail.LpCategory, launchPadDetail.TrainingLocation, launchPadDetail.WorkLocation, launchPadDetail.Status, launchPadDetail.Utilization);
            }

            return dataTable;
        }
    }
}