// Placeholder name for file until we get a more complete grasp of classes in the system
// and the organisation thereof.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager 
{
    [DataContract]
    internal class Calendar
    {
        [DataMember]
        public List<Reservation> reservations { get; set; }
        [DataMember]
        public DateTime dateTime { get; set; }

        public Calendar() //this is to be used when creating an item for the first time (space, trainer, equipment)
        {
            this.reservations = new List<Reservation>();
        }

        public Calendar(DateTime startTime, double durationMinutes, ReservingEntity Owner)
        {
            this.reservations = new List<Reservation> {new Reservation(Owner, startTime, durationMinutes)};
        }

        public bool BookReservation(ReservingEntity owner, DateTime startTime, double durationMinutes)
        {
            foreach (Reservation reservation in reservations)
            {
                if (reservation.startTime < startTime.AddMinutes(durationMinutes) && reservation.startTime.AddMinutes(reservation.durationMinutes) > startTime)
                {
                    Console.WriteLine($"The item is not possible to book at this time ({startTime}), it's allready booked.");
                    return false;
                }
            }
            reservations.Add(new Reservation(owner, startTime, durationMinutes));
            return true;
        }
    }
}