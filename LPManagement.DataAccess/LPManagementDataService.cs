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
            DataTable dataTable = CreateDataTable(launchPadDetails);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("[dbo].[usp_employee_persist]", connection);
                command.CommandType = CommandType.StoredProcedure;

                var sqlParameter = command.Parameters.AddWithValue("@employee_data", dataTable);
                sqlParameter.SqlDbType = SqlDbType.Structured;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private static DataTable CreateDataTable(List<LaunchPadDetail> launchPadDetails)
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

            foreach (var launchPadDetail in launchPadDetails)
            {
                dataTable.Rows.Add(launchPadDetail.LaunchPadCode, launchPadDetail.Location, launchPadDetail.FinancialYear,
                    launchPadDetail.EmployeeId, launchPadDetail.EmployeeName, launchPadDetail.EmployeeMailId, launchPadDetail.BusinessLayer,
                    launchPadDetail.Practice, launchPadDetail.LpCategory, launchPadDetail.TrainingLocation, launchPadDetail.WorkLocation);
            }

            return dataTable;
        }
    }
}