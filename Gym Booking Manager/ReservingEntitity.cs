using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class ReservingEntitity : User, IReservingEntity, ICSVable
    {
        
        public ReservingEntitity(string name, string uniqueID, string phone, string email, string status) : base(name, uniqueID, phone, email,status)
        {
        }
        public bool AbleToMakeReservation()
        {
            if (status == "Member") 
            {
                return true;
            }
            else
            {
                throw new Exception("ReservingEnity aint eligible for reservations");
            }
        }
    }
}
