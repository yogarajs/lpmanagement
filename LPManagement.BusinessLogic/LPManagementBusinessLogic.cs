using LPManagement.Common;
using LPManagement.Common.Enums;
using LPManagement.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace LPManagement.BusinessLogic
{
    /// <summary>
    /// LaunchPad management business logic class.
    /// </summary>
    public class LPManagementBusinessLogic
    {
        /// <summary>
        /// Get launch pad details for the given filter criteria.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="quarter">Quarter</param>
        /// <param name="status">Status</param>
        /// <param name="financialYear">Financial year</param>
        /// <returns>List of LaunchPadDetails.</returns>
        public IEnumerable<LaunchPadDetail> GetLauchPadDetails(Location location, Quarter quarter, Status status, int financialYear)
        {
            var lpManagementDataService = new LPManagementDataService();
            var launchPadDetails = lpManagementDataService.GetLaunchPadDetails()
                                    .ToList()
                                    .Where(x => x.FinancialYear == financialYear);

            if (location != Location.All)
            {
                launchPadDetails = launchPadDetails.Where(x => x.Location == location).ToList();
            }

            return launchPadDetails;
        }

        /// <summary>
        /// Process Upload files - Read data from excel and converts to business objects.
        /// Persists data to the database.
        /// </summary>
        /// <param name="filePath">File path</param>
        public void ProcessFile(string filePath)
        {
            var launchPadDetails = new List<LaunchPadDetail>();
            ExtractDataFromExcel(filePath, launchPadDetails);

            if (launchPadDetails.Any())
            {
                var lpManagementDataService = new LPManagementDataService();
                lpManagementDataService.PersistLaunchPadDetails(launchPadDetails);
            }
        }

        private void ExtractDataFromExcel(string filePath, List<LaunchPadDetail> launchPadDetails)
        {
            Excel.Application application = null;
            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;
            Excel.Range range = null;
            try
            {
                application = new Excel.Application();
                workBook = application.Workbooks.Open(filePath, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                var sheets = workBook.Worksheets;
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                range = workSheet.UsedRange;

                dynamic cellData = (range.Cells[3, 2] as Excel.Range).Value2;
                var cellDataArray = cellData.ToString().Split('_');
                Location location = Enum.Parse(typeof(Location), cellDataArray[1].ToString());
                int financialYear = int.Parse(cellDataArray[0].Substring(2));

                for (int rows = 3; rows <= range.Rows.Count; rows++)
                {
                    try
                    {
                        dynamic employeeId = (range.Cells[rows, 3] as Excel.Range).Value2;
                        dynamic employeeName = (range.Cells[rows, 4] as Excel.Range).Value2;
                        dynamic employeeMail = (range.Cells[rows, 5] as Excel.Range).Value2;
                        dynamic businessLayer = (range.Cells[rows, 6] as Excel.Range).Value2;
                        dynamic practice = (range.Cells[rows, 7] as Excel.Range).Value2;
                        dynamic lpCategory = (range.Cells[rows, 8] as Excel.Range).Value2;
                        dynamic trainingLocation = (range.Cells[rows, 9] as Excel.Range).Value2;
                        dynamic workLoation = (range.Cells[rows, 10] as Excel.Range).Value2;

                        launchPadDetails.Add(new LaunchPadDetail()
                        {
                            LaunchPadCode = cellData.ToString(),
                            Location = location,
                            FinancialYear = financialYear,
                            EmployeeId = int.Parse(employeeId.ToString()),
                            EmployeeName = employeeName.ToString(),
                            EmployeeMailId = employeeMail.ToString(),
                            BusinessLayer = businessLayer.ToString(),
                            Practice = practice.ToString(),
                            LpCategory = lpCategory.ToString(),
                            TrainingLocation = trainingLocation.ToString(),
                            WorkLocation = workLoation?.ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("--------------------------------");
                        Debug.WriteLine(launchPadDetails.Last().LaunchPadCode.ToString());
                        Debug.WriteLine(e.Message.ToString());
                        Debug.WriteLine("--------------------------------");
                    }
                }
            }
            catch (COMException)
            {
                throw new FileNotFoundException("Data file not");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                }
                if (workSheet != null)
                {
                    Marshal.ReleaseComObject(workSheet);
                }
                if (workBook != null)
                {
                    workBook.Close();
                    Marshal.ReleaseComObject(workBook);
                }
                if (application != null)
                {
                    application.Quit();
                    Marshal.ReleaseComObject(application);
                }
            }
        }
    }
}