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
    internal class Equipment : IReservable, IComparable<Equipment>
    {
        //private static readonly List<Tuple<Category, int>> hourlyCosts = InitializeHourlyCosts(); // Costs may not be relevant for the prototype. Let's see what the time allows.
        [DataMember]
        public Category category;
        [DataMember]
        public String name;
        [DataMember]
        public Calendar calendar;

        public string? NewEquipment { get; }

        public Equipment(Category category, string name)
        {
            this.category = category;
            this.name = name;
            this.calendar = new Calendar();
        }
        public int CompareTo(Equipment? other)
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
            Small,
            Large
        }
        public bool MakeReservation(ReservingEntity owner, DateTime timeSlot, double durationMinutes)
        {
            return calendar.BookReservation(owner, timeSlot, durationMinutes);

        }
        public void ViewReservations(Equipment equipment, ReservingEntity user)
        {
            if (user.status == "Member")
            {
                foreach (Reservation rs in calendar.reservations)
                {
                    if (rs.owner.name == user.name)
                    {
                        Console.WriteLine($"{rs.owner.name} {equipment} {rs.startTime}");
                    }
                    Console.ReadKey();
                }
            }
            if (user.status == "Staff")
            {
                foreach (Reservation rs in calendar.reservations)
                {
                    Console.WriteLine($"{rs.owner.name} {equipment} {rs.startTime}");
                }
                Console.ReadKey();
            }
>>>>>>> 096a836ee6637e341cee225e3c53047de5524db8
        }
        public void CancelReservation(ReservingEntity owner, Equipment equipment)
        {
            if (owner.status == "Member")
            {
                foreach (Reservation rs in calendar.reservations.ToList())
                {
                    if (rs.owner.name == owner.name)
                    {
                        equipment.calendar.reservations.Remove(rs);
                    }
                }
            }
            if (owner.status == "Staff")
            {
                foreach (Reservation rs in calendar.reservations.ToList())
                {
                    equipment.calendar.reservations.Remove(rs);
                }
            }
        }
    }
}
