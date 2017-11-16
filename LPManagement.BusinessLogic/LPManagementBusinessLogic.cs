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

            try
            {
                if (launchPadDetails.Any())
                {
                    var lpManagementDataService = new LPManagementDataService();
                    lpManagementDataService.PersistLaunchPadDetails(launchPadDetails);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                        // LP details
                        dynamic employeeIdDynamic = (range.Cells[rows, 3] as Excel.Range).Value2;
                        int employeeId = int.Parse(employeeIdDynamic.ToString());
                        dynamic employeeName = (range.Cells[rows, 4] as Excel.Range).Value2;
                        dynamic employeeMail = (range.Cells[rows, 5] as Excel.Range).Value2;
                        dynamic businessLayer = (range.Cells[rows, 6] as Excel.Range).Value2;
                        dynamic practice = (range.Cells[rows, 7] as Excel.Range).Value2;
                        dynamic lpCategory = (range.Cells[rows, 8] as Excel.Range).Value2;
                        dynamic trainingLocation = (range.Cells[rows, 9] as Excel.Range).Value2;
                        dynamic workLoation = (range.Cells[rows, 10] as Excel.Range).Value2;
                        dynamic status = (range.Cells[rows, 37] as Excel.Range).Value2;
                        dynamic utilization = (range.Cells[rows, 38] as Excel.Range).Value2;

                        // LP score details
                        dynamic faculty = (range.Cells[rows, 11] as Excel.Range).Value2;
                        dynamic unix = (range.Cells[rows, 12] as Excel.Range).Value2;
                        dynamic softwareEngineeringAndUML = (range.Cells[rows, 13] as Excel.Range).Value2;
                        dynamic oracleSQLAndPLSQL = (range.Cells[rows, 14] as Excel.Range).Value2;
                        dynamic dotNet = (range.Cells[rows, 15] as Excel.Range).Value2;
                        dynamic coreJava = (range.Cells[rows, 16] as Excel.Range).Value2;
                        dynamic advanceJava = (range.Cells[rows, 17] as Excel.Range).Value2;
                        dynamic J2EE = (range.Cells[rows, 18] as Excel.Range).Value2;
                        dynamic hibernate = (range.Cells[rows, 19] as Excel.Range).Value2;
                        dynamic spring = (range.Cells[rows, 20] as Excel.Range).Value2;
                        dynamic html = (range.Cells[rows, 21] as Excel.Range).Value2;
                        dynamic javaScript = (range.Cells[rows, 22] as Excel.Range).Value2;
                        dynamic informatica = (range.Cells[rows, 23] as Excel.Range).Value2;
                        dynamic talentDevelopment = (range.Cells[rows, 24] as Excel.Range).Value2;
                        dynamic manualTesting = (range.Cells[rows, 25] as Excel.Range).Value2;
                        dynamic UFT = (range.Cells[rows, 26] as Excel.Range).Value2;
                        dynamic selenium = (range.Cells[rows, 27] as Excel.Range).Value2;
                        dynamic OOPS = (range.Cells[rows, 28] as Excel.Range).Value2;
                        dynamic RDBMS = (range.Cells[rows, 29] as Excel.Range).Value2;
                        dynamic finalExam = (range.Cells[rows, 30] as Excel.Range).Value2;
                        dynamic overallScore = (range.Cells[rows, 31] as Excel.Range).Value2;
                        dynamic projectScore = (range.Cells[rows, 32] as Excel.Range).Value2;
                        dynamic projectReviewer = (range.Cells[rows, 33] as Excel.Range).Value2;
                        dynamic overallFeedback = (range.Cells[rows, 34] as Excel.Range).Value2;
                        dynamic facultyFeedback = (range.Cells[rows, 35] as Excel.Range).Value2;
                        dynamic remarks = (range.Cells[rows, 36] as Excel.Range).Value2;

                        var employeeScoreDetail = new EmployeeScoreDetail()
                        {
                            EmployeeId = employeeId,
                            Faculty = faculty.ToString(),
                            Unix = float.Parse(unix.ToString()),
                            SEAndUML = float.Parse(softwareEngineeringAndUML.ToString()),
                            SQL = float.Parse(oracleSQLAndPLSQL.ToString()),
                            DotNet = float.Parse(dotNet.ToString()),
                            CoreJava = float.Parse(coreJava.ToString()),
                            AdvanceJava = float.Parse(advanceJava.ToString()),
                            J2EE = float.Parse(J2EE.ToString()),
                            Hibernate = float.Parse(hibernate.ToString()),
                            Spring = float.Parse(spring.ToString()),
                            HTML = float.Parse(html.ToString()),
                            JavaScript = float.Parse(javaScript.ToString()),
                            Informatica = float.Parse(informatica.ToString()),
                            TalentDevelopment = float.Parse(talentDevelopment.ToString()),
                            ManualTesting = float.Parse(manualTesting.ToString()),
                            UFT = float.Parse(UFT.ToString()),
                            Selenium = float.Parse(selenium.ToString()),
                            OOPS = float.Parse(OOPS.ToString()),
                            RDBMS = float.Parse(RDBMS.ToString()),
                            FinalExam = float.Parse(finalExam.ToString()),
                            OverallScore = float.Parse(overallScore.ToString()),
                            ProjectScore = float.Parse(projectScore.ToString()),
                            ProjectReviewer = float.Parse(projectReviewer.ToString()),
                            OverallFeedback = float.Parse(overallFeedback.ToString()),
                            FacultyFeedback = float.Parse(facultyFeedback.ToString()),
                            Remarks = float.Parse(remarks.ToString())
                        };

                        launchPadDetails.Add(new LaunchPadDetail()
                        {
                            LaunchPadCode = cellData.ToString(),
                            Location = location,
                            FinancialYear = financialYear,
                            EmployeeId = employeeId,
                            EmployeeName = employeeName.ToString(),
                            EmployeeMailId = employeeMail.ToString(),
                            BusinessLayer = businessLayer.ToString(),
                            Practice = practice.ToString(),
                            LpCategory = lpCategory.ToString(),
                            TrainingLocation = trainingLocation.ToString(),
                            WorkLocation = workLoation?.ToString(),
                            Status = status.ToString(),
                            Utilization = utilization.ToString(),
                            EmployeeScoreDetail = employeeScoreDetail
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