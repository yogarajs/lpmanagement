using System;

namespace LPManagement.Common.Enums
{
    [Flags]
    public enum Location
    {
        None = 0,
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
        All = CHN | HYD | BEN | PUN
    }
}
