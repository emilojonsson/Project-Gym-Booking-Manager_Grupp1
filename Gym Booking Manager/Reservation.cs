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
        public ReservingEntity owner;
        [DataMember]
        public DateTime timeSlot;
        public Reservation (ReservingEntity owner, DateTime timeSlot)
        {
            this.owner = owner;
            this.timeSlot = timeSlot;
        }
        public override string ToString()
        {
            return $"{owner} {timeSlot}";
        }
    }
}
