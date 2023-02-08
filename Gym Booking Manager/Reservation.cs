using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class Reservation
    {
        private readonly ReservingEntity owner;
        private readonly DateTime timeSlot;
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
