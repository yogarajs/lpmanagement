namespace LPManagement.Common
{
    /// <summary>
    /// The launch pad summary class
    /// </summary>
    public class LaunchPadSummary
    {
        /// <summary>
        /// Gets or sets launchpad code.
        /// </summary>
        public string LaunchPadCode { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets total no. of launch pad
        /// </summary>
        public int TotalNoOfLPCount { get; set; }

        /// <summary>
        /// Gets or sets above 70 percent count.
        /// </summary>
        public int OverallScoreAbove70PercentCount { get; set; }

        /// <summary>
        /// Gets or sets 60 to 70 percent count.
        /// </summary>
        public int OverallScore60To70PercentCount { get; set; }

        /// <summary>
        /// Gets or sets below 60 percent count.
        /// </summary>
        public int OverallScoreBelow60PercentCount { get; set; }
    }
}