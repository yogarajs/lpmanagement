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
        #region Public methods
        /// <summary>
        /// Get launch pad details for the given filter criteria.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="quarter">Quarter</param>
        /// <param name="status">Status</param>
        /// <param name="financialYear">Financial year</param>
        /// <param name="practice">Practice</param>
        /// <returns>List of LaunchPadDetails.</returns>
        public IEnumerable<LaunchPadDetail> GetLauchPadDetails(Location location, Quarter quarter, Status status, int financialYear, string practice)
        {
            var lastFourYears = financialYear - 4;
            var lpManagementDataService = new LPManagementDataService();
            var launchPadDetails = lpManagementDataService.GetLaunchPadDetails()
                                    .ToList()
                                    .Where(x => x.FinancialYear >= lastFourYears && x.FinancialYear <= financialYear);

            if (location != Location.All)
            {
                launchPadDetails = launchPadDetails.Where(x => x.Location == location).ToList();
            }

            if (practice != "All")
            {
                launchPadDetails = launchPadDetails.Where(x => x.Practice == practice).ToList();
            }

            return launchPadDetails;
        }

        /// <summary>
        /// Gets launch pad details by launch pad code.
        /// </summary>
        /// <param name="launchPadCode">Launch pad code.</param>
        /// <returns>List of launch pad details.</returns>
        public LaunchPadSummary GetLaunchPadDetailsByLaunchPadCode(string launchPadCode)
        {
            var lpManagementDataService = new LPManagementDataService();
            var launchPadDetails = lpManagementDataService.GetLaunchPadDetailsByLaunchPadCode(launchPadCode).ToList();
            var totalNoOfLPCount = launchPadDetails.Count;
            var overallScoreAbove70PercentCount = launchPadDetails.Where(x => x.EmployeeScoreDetail.OverallScore >= 70).ToList().Count;
            var overallScore60To70PercentCount = launchPadDetails.Where(x => x.EmployeeScoreDetail.OverallScore >= 69 && x.EmployeeScoreDetail.OverallScore <= 60).ToList().Count;
            var overallScoreBelow60PercentCount = launchPadDetails.Where(x => x.EmployeeScoreDetail.OverallScore < 60).ToList().Count;

            var launchPadSummary = new LaunchPadSummary()
            {
                LaunchPadCode = launchPadCode,
                Status = launchPadDetails[0].Status,
                TotalNoOfLPCount = totalNoOfLPCount,
                OverallScoreAbove70PercentCount = overallScore60To70PercentCount,
                OverallScore60To70PercentCount = overallScore60To70PercentCount,
                OverallScoreBelow60PercentCount = overallScoreBelow60PercentCount
            };
            return launchPadSummary;
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
        #endregion

        #region Private methods
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
                int financialYear = int.Parse(cellDataArray[0].Substring(2)) + 2000;

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
                            Unix = unix != null ? float.Parse(unix.ToString()) : null,
                            SEAndUML = softwareEngineeringAndUML != null ? float.Parse(softwareEngineeringAndUML.ToString()) : null,
                            SQL = oracleSQLAndPLSQL != null ? float.Parse(oracleSQLAndPLSQL.ToString()) : null,
                            DotNet = dotNet != null ? float.Parse(dotNet.ToString()) : null,
                            CoreJava = coreJava != null ? float.Parse(coreJava.ToString()) : null,
                            AdvanceJava = advanceJava != null ? float.Parse(advanceJava.ToString()) : null,
                            J2EE = J2EE != null ? float.Parse(J2EE.ToString()) : null,
                            Hibernate = hibernate != null ? float.Parse(hibernate.ToString()) : null,
                            Spring = spring != null ? float.Parse(spring.ToString()) : null,
                            HTML = html != null ? float.Parse(html.ToString()) : null,
                            JavaScript = javaScript != null ? float.Parse(javaScript.ToString()) : null,
                            Informatica = informatica != null ? float.Parse(informatica.ToString()) : null,
                            TalentDevelopment = talentDevelopment != null ? float.Parse(talentDevelopment.ToString()) : null,
                            ManualTesting = manualTesting != null ? float.Parse(manualTesting.ToString()) : null,
                            UFT = UFT != null ? float.Parse(UFT.ToString()) : null,
                            Selenium = selenium != null ? float.Parse(selenium.ToString()) : null,
                            OOPS = OOPS != null ? float.Parse(OOPS.ToString()) : null,
                            RDBMS = RDBMS != null ? float.Parse(RDBMS.ToString()) : null,
                            FinalExam = finalExam != null ? float.Parse(finalExam.ToString()) : null,
                            OverallScore = overallScore != null ? float.Parse(overallScore.ToString()) : null,
                            ProjectScore = projectScore != null ? float.Parse(projectScore.ToString()) : null,
                            ProjectReviewer = projectReviewer,
                            OverallFeedback = overallFeedback != null ? float.Parse(overallFeedback.ToString()) : null,
                            FacultyFeedback = facultyFeedback != null ? float.Parse(facultyFeedback.ToString()) : null,
                            Remarks = remarks
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
                            Utilization = utilization?.ToString(),
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
        #endregion
    }
}
