using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal interface IReservable
    {
        bool MakeReservation(ReservingEntity owner, DateTime timeSlot, double durationMinutes);
    }
}
