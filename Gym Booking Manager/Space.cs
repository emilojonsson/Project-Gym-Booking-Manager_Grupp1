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
    // IComparable<T> interface requires implementing the CompareTo(T other) method.
    // This interface/method is used by for instance SortedSet<T>.Add(T) (and other sorted collections).
    // There is also the non-generic IComparable interface for a CompareTo(Object obj) implementation.
    //
    // The current database class implementation uses SortedSet, and thus classes and objects that we want to store
    // in it should inherit the IComparable<T> interface.
    //
    // As alluded to from previous paragraphs, implementing IComparable<T> is not exhaustive to cover all "comparisons".
    // Refer to official C# documentation to determine what interface to implement to allow use with
    // the class/method/operator that you want.
    [DataContract]
    internal class Space : IReservable, ICSVable, IComparable<Space> 
    {
        //private static readonly List<Tuple<Category, int>> hourlyCosts = InitializeHourlyCosts(); // Costs may not be relevant for the prototype. Let's see what the time allows.
        [DataMember]
        public Category category;
        [DataMember]
        public String name;
        [DataMember]
        public Calendar calendar;

        public Space(Category category, string name)
        {
            this.category = category;
            this.name = name;
            this.calendar = new Calendar();
        }

        // Every class T to be used for DbSet<T> needs a constructor with this parameter signature. Make sure the object is properly initialized.
        public Space(Dictionary<String, String> constructionArgs)
        {
            this.name = constructionArgs[nameof(name)];
            if (!Category.TryParse(constructionArgs[nameof(category)], out this.category))
            {
                throw new ArgumentException("Couldn't parse a valid Space.Category value.", nameof(category));
            }

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
            return $"{name}"; // TODO: Don't use CSVify. Make it more readable.
        }

        // Every class C to be used for DbSet<C> should have the ICSVable interface and the following implementation.
        public string CSVify()
        {
            return $"{nameof(category)}:{category.ToString()},{nameof(name)}:{name}";
        }
        public enum Category
        {
            Hall,
            Lane,
            Studio
        }

        public void ViewTimeTable(ReservingEntity owner)
        {
            // Fetch
            List<Reservation> tableSlice = this.calendar.GetSlice();
            // Show?
            foreach (Reservation reservation in tableSlice)
            {
                Console.WriteLine($"----[{calendar.reservations.IndexOf(reservation)}]----\n{reservation}");
            }

        }

        public bool MakeReservation(ReservingEntity owner, DateTime timeSlot, double durationMinutes)
        {
            //calendar.reservations.Add(new Reservation(owner, timeSlot));
            return calendar.BookReservation(owner, timeSlot, durationMinutes);
        }

        public void CancelReservation(ReservingEntity owner)
        {
            ViewTimeTable(owner);
            int del;
            Console.Write("\nCansel reservation (number): ");
            del = Int32.Parse(Console.ReadLine());
            calendar.reservations.Remove(calendar.reservations[del]);
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

    }
}
