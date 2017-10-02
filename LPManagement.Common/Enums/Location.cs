using System;

namespace LPManagement.Common.Enums
{
    /// <summary>
    /// Location enum.
    /// </summary>
    [Flags]
    public enum Location
    {
        /// <summary>
        /// Chennai
        /// </summary>
        CHN = 1,
        /// <summary>
        /// Hyderabad 
        /// </summary>
        HYD = 2,
        /// <summary>
        /// Bengaluru 
        /// </summary>
        BEN = 4,
        /// <summary>
        /// Pune
        /// </summary>
        PUN = 8,
        /// <summary>
        /// All locations
        /// </summary>
        All = 16
    }
}
