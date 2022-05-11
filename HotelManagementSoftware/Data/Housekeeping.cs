using System;

namespace HotelManagementSoftware.Data
{
    public class HousekeepingRequest
    {
        public int HousekeepingRequestId { get; set; }

        public int? OpenEmployeeId { get; set; }
        public Employee? OpenEmployee { get; set; }

        public int? CloseEmployeeId { get; set; }
        public Employee? CloseEmployee { get; set; }

        public Room? Room { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime? CloseTime { get; set; }

        public string? Note { get; set; }

        public HousekeepingRequestStatus Status { get; set; }

        public HousekeepingRequest(DateTime startTime,
                                   DateTime endTime,
                                   HousekeepingRequestStatus status)
        {
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }
    }

    public enum HousekeepingRequestStatus
    {
        OPENED,
        CLOSED
    }
}
