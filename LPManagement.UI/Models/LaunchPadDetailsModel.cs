﻿using LPManagement.Common.Enums;

namespace LPManagement.UI.Models
{
    /// <summary>
    /// LaunchPad model class.
    /// </summary>
    public class LaunchPadDetailsModel
    {
        /// <summary>
        /// Gets or sets launchpad code.
        /// </summary>
        public string LaunchPadCode { get; set; }

        /// <summary>
        /// Gets or sets IsLaunchPad.
        /// <value>true, if it is a launchpad else false.</value>
        /// </summary>
        public bool IsLaunchPad { get; set; }

        /// <summary>
        /// Gets or sets location.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets Status.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets quarter.
        /// </summary>
        public Quarter Quarter { get; set; }

        /// <summary>
        /// Gets or sets financial year.
        /// </summary>
        public int FinancialYear { get; set; }

        /// <summary>
        /// Gets or sets technology.
        /// </summary>
        public string Technology { get; set; }
    }
}