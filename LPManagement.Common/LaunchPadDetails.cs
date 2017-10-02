using LPManagement.Common.Enums;

namespace LPManagement.Common
{
    public class LaunchPadDetails
    {
        public string LaunchPadCode { get; set; }

        public bool IsLaunchPad { get; set; }

        public Location Location { get; set; }

        public Status Status { get; set; }

        public Quarter Quarter { get; set; }

        public int FinancialYear { get; set; }

        public string Technology { get; set; }

        public int NoOfTrainees { get; set; }

        public int NoOfAllocation { get; set; }
    }
}
