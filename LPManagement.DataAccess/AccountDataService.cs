using LPManagement.Common;
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
    /// <summary>
    /// Account data service class.
    /// </summary>
    public class AccountDataService
    {
        /// <summary>
        /// Gets list of users.
        /// </summary>
        /// <returns>List of users.</returns>
        public List<User> GetUserDetails()
        {
            var users = new List<User>();
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
                DateTime previousDate = DateTime.MinValue;
                application = new Excel.Application();
                workBook = application.Workbooks.Open(fileName, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                var sheets = workBook.Worksheets;
                workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);
                range = workSheet.UsedRange;

                for (int rows = 2; rows <= range.Rows.Count; rows++)
                {
                    try
                    {
                        dynamic userName = (range.Cells[rows, 1] as Excel.Range).Value2;
                        dynamic password = (range.Cells[rows, 2] as Excel.Range).Value2;

                        users.Add(new User()
                        {
                            UserName = userName.ToString(),
                            Password = password.ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("--------------------------------");
                        Debug.WriteLine(users.Last().UserName.ToString());
                        Debug.WriteLine(e.Message.ToString());
                        Debug.WriteLine("--------------------------------");
                    }
                }
            }
            catch (COMException ex)
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
            return users;
        }
    }
}