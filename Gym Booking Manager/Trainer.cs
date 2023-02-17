using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;


#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    [DataContract]
    internal class Trainer : IReservable, IComparable<Trainer>
    {
        List<Trainer> test = new List<Trainer>();
        //private static readonly List<Tuple<Category, int>> hourlyCosts = InitializeHourlyCosts(); // Costs may not be relevant for the prototype. Let's see what the time allows.
        [DataMember]
        public Category category;
        [DataMember]
        public String name;
        [DataMember]
        public Calendar calendar;

        public Trainer(Category category, string name)
        {
            this.category = category;
            this.name = name;
            this.calendar = new Calendar();
        }

        public int CompareTo(Trainer? other)
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
            return $"{nameof(category)}:{category.ToString()},{nameof(name)}:{name}"; ; // TODO: Don't use CSVify. Make it more readable.
        }
        public enum Category
        {
            Trainer,
            Consultation
        }
        public bool MakeReservation(ReservingEntity owner, DateTime timeSlot, double durationMinutes)
        {
            return calendar.BookReservation(owner, timeSlot, durationMinutes);
        }
        public void ViewReservations(Trainer trainer, ReservingEntity user)
        {
            if (user.status == "Member")
            {
                foreach (Reservation rs in calendar.reservations)
                {
                    if (rs.owner.name == user.name)
                    {
                        Console.WriteLine($"{rs.owner.name} {trainer} {rs.startTime}");
                    }
                }
            }
            if (user.status == "Staff")
            {
                foreach (Reservation rs in calendar.reservations)
                {
                    Console.WriteLine($"{rs.owner.name} {trainer} {rs.startTime}");
                }
            }
        }
        public void CancelReservation(ReservingEntity owner, Trainer trainer)
        {
            if (owner.status == "Member")
            {
                foreach (Reservation rs in calendar.reservations.ToList())
                {
                    if (rs.owner.name == owner.name)
                    {
                        trainer.calendar.reservations.Remove(rs);
                    }
                }
            }
            if (owner.status == "Staff")
            {
                foreach (Reservation rs in calendar.reservations.ToList())
                {
                    trainer.calendar.reservations.Remove(rs);
                }
            }
        }
    }
}
