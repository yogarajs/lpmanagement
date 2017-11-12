using LPManagement.Common.Enums;

namespace LPManagement.Common
{
    /// <summary>
    /// LaunchPad details business object class.
    /// </summary>
    public class LaunchPadDetail
    {
        /// <summary>
        /// Gets or sets launchpad code.
        /// </summary>
        public string LaunchPadCode { get; set; }

        /// <summary>
        /// Gets or sets location.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets financial year.
        /// </summary>
        public int FinancialYear { get; set; }

        /// <summary>
        /// Gets or sets employee ID
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets employee name
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets employee e-mail
        /// </summary>
        public string EmployeeMailId { get; set; }

        /// <summary>
        /// Gets or sets businesslayer
        /// </summary>
        public string BusinessLayer { get; set; }

        /// <summary>
        /// Gets or sets practice
        /// </summary>
        public string Practice { get; set; }

        /// <summary>
        /// Gets or sets lp category
        /// </summary>
        public string LpCategory { get; set; }

        /// <summary>
        /// Gets or sets training location
        /// </summary>
        public string TrainingLocation { get; set; }

        /// <summary>
        /// Gets or sets work location
        /// </summary>
        public string WorkLocation { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets utilization
        /// </summary>
        public string Utilization { get; set; }
    }
}
