using LPManagement.Common;
using LPManagement.Common.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace LPManagement.DataAccess
{
    public class LPManagementDataService
    {
        public IEnumerable<LaunchPadDetails> GetLaunchPadDetails()
        {
            var launchPadDetails = new List<LaunchPadDetails>();
            Excel.Application application = null;
            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;
            Excel.Range range = null;
            var fileName = ConfigurationManager.AppSettings["DataFilePath"];
            if (fileName == null)
            {
                throw new Exception("DataFilePath: config entry is missing");
            }
            try
            {                
                application = new Excel.Application();
                workBook = application.Workbooks.Open(fileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                var sheets = workBook.Worksheets;
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(2);
                range = workSheet.UsedRange;

                for (int rows = 2; rows <= range.Rows.Count; rows++)
                {
                    try
                    {
                        dynamic launchPadCode = (range.Cells[rows, 1] as Excel.Range).Value2;
                        dynamic location = (range.Cells[rows, 3] as Excel.Range).Value2;
                        dynamic status = (range.Cells[rows, 4] as Excel.Range).Value2;
                        dynamic quarter = (range.Cells[rows, 5] as Excel.Range).Value2;
                        dynamic financialYear = (range.Cells[rows, 6] as Excel.Range).Value2;
                        dynamic technology = (range.Cells[rows, 7] as Excel.Range).Value2;
                        dynamic noOfTrainees = (range.Cells[rows, 8] as Excel.Range).Value2;
                        dynamic noOfAllocation = (range.Cells[rows, 9] as Excel.Range).Value2;

                        launchPadDetails.Add(new LaunchPadDetails()
                        {
                            LaunchPadCode = launchPadCode.ToString(),
                            Location = Enum.Parse(typeof(Location), location.ToString()),
                            Status = Enum.Parse(typeof(Status), status.ToString()),
                            Quarter = Enum.Parse(typeof(Quarter), quarter.ToString()),
                            FinancialYear = int.Parse(financialYear.ToString()),
                            Technology = technology.ToString(),
                            NoOfTrainees = int.Parse(noOfTrainees.ToString()),
                            NoOfAllocation = int.Parse(noOfAllocation.ToString()),
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
                throw new FileNotFoundException();
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
            return launchPadDetails;
        }
    }
}