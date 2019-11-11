using System;

namespace MyVet.Common.Models
{
    public class HistoryResponse
    {
        public int Id { get; set; }

        public string ServiceType { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal => this.Date.ToLocalTime();

        public string Remarks { get; set; }
    }
}
