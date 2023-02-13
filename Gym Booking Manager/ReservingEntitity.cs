using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    [DataContract]
    internal class ReservingEntity : User, ICSVable
    {
        public ReservingEntity(string name, string uniqueID, string phone, string email, string status) : base(name, uniqueID, phone, email, status)
        {

        }
    }
}
