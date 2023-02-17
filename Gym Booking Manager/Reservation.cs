using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    [DataContract]
    internal class Reservation
    {
        [DataMember]
        public ReservingEntity owner { get; set; }
        [DataMember]
        public DateTime startTime { get; set; }
        public double durationMinutes { get; set; }

        public Reservation (ReservingEntity owner, DateTime startTime, double durationMinutes)
        {
            this.owner = owner;
            this.startTime = startTime;
            this.durationMinutes = durationMinutes;
        }
        public override string ToString()
        {
            return $"{owner} {startTime.ToString("yyyy/MM/dd")}, mellan: {startTime.ToString("HH:mm")}-{startTime.AddMinutes(durationMinutes).ToString("HH:mm")}";
        }
    }
}
