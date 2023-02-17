using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class Space : IReservable,IComparable<Space> 
    {
        //private static readonly List<Tuple<Category, int>> hourlyCosts = InitializeHourlyCosts(); // Costs may not be relevant for the prototype. Let's see what the time allows.
        [DataMember]
        public Category category { get; set; }
        [DataMember]
        public String name { get; set; }
        [DataMember]
        public Calendar calendar { get; set; }

        public Space(Category category, string name)
        {
            this.category = category;
            this.name = name;
            this.calendar = new Calendar();
        }
        public int CompareTo(Space? other)
        {
            // If other is not a valid object reference, this instance is greater.
            if (other == null) return 1;
            // Sort primarily on category.
            if (this.category != other.category) return this.category.CompareTo(other.category);
            // When category is the same, sort on name.
            return this.name.CompareTo(other.name);
        }

        public override string ToString()
        {
            return $"{nameof(category)}:{category.ToString()},{nameof(name)}:{name}";
        }
        public enum Category
        {
            Hall,
            Lane,
            Studio
        }
        public bool MakeReservation(ReservingEntity owner, DateTime timeSlot, double durationMinutes)
        {
            //calendar.reservations.Add(new Reservation(owner, timeSlot));
            return calendar.BookReservation(owner, timeSlot, durationMinutes);
        }
        public void ViewReservations(Space space, ReservingEntity user)
        {
            if (user.status == "Member")
            {
                foreach (Reservation rs in calendar.reservations)
                {
                    if (rs.owner.name == user.name)
                    {
                        Console.WriteLine($"{rs.owner.name} {space} {rs.startTime}");
                    }

                }
            }
            if (user.status == "Staff")
            {
                foreach (Reservation rs in calendar.reservations)
                {
                    Console.WriteLine($"{rs.owner.name} {space} {rs.startTime}");
                }
            }
        }
        public void CancelReservation(ReservingEntity owner, Space space)
        {
            if (owner.status == "Member")
            {
                foreach (Reservation rs in calendar.reservations.ToList())
                {
                    if (rs.owner.name == owner.name)
                    {
                        space.calendar.reservations.Remove(rs);
                    }
                }
            }
            if (owner.status == "Staff")
            {
                foreach (Reservation rs in calendar.reservations.ToList())
                {
                    space.calendar.reservations.Remove(rs);

                }
            }
        }

    }
}
