using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Gym_Booking_Manager
{
    internal interface IReservable
    {
        void MakeReservation(ReservingEntity owner, DateTime timeSlot);
        void CancelReservation(ReservingEntity owner);
        void ViewTimeTable(ReservingEntity owner); // start and end as arguments?
    }
}
