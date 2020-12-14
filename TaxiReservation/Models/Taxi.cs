using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiReservation.Models
{
    public class Taxi
    {
        public int Id { get; set; }
        public string Car { get; set; }
        public string Colour { get; set; }
        public string CarNumber { get; set; }
        public bool IsReserved { get; set; }
        public double DriverPhoneNumber { get; set; }
        public double? ClientPhoneNumber { get; set; }
        public DateTime? TimeReservation { get; set; }
    }
}
