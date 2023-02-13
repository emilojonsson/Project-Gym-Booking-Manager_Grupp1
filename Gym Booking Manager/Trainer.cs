using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;


#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Trainer : IReservable, ICSVable, IComparable<Trainer>
    {
        //private static readonly List<Tuple<Category, int>> hourlyCosts = InitializeHourlyCosts(); // Costs may not be relevant for the prototype. Let's see what the time allows.
        private Category category;
        public String name;
        public Calendar calendar;
        //private readonly Calendar calendar;

        public string? NewInstructor { get; } // Becca skrev in tillfälligt

        public Trainer(Category category, string name)
        {
            this.category = category;
            this.name = name;
            this.calendar = new Calendar();
        }

        // Every class T to be used for DbSet<T> needs a constructor with this parameter signature. Make sure the object is properly initialized.
        public Trainer(Dictionary<String, String> constructionArgs)
        {
            this.name = constructionArgs[nameof(name)];
            if (!Category.TryParse(constructionArgs[nameof(category)], out this.category))
            {
                throw new ArgumentException("Couldn't parse a valid Trainer.Category value.", nameof(category));
            }

            this.calendar = new Calendar();
        }

        public Trainer(string? newInstructor)
        {
            NewInstructor = newInstructor;
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
            return this.CSVify(); // TODO: Don't use CSVify. Make it more readable.
        }

        // Every class C to be used for DbSet<C> should have the ICSVable interface and the following implementation.
        public string CSVify()
        {
            return $"{nameof(category)}:{category.ToString()},{nameof(name)}:{name}";
        }
        public enum Category
        {
            Trainer,
            Consultation
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

        public void MakeReservation(ReservingEntity owner, DateTime timeSlot)
        {
            calendar.reservations.Add(new Reservation(owner, timeSlot));
            //supervised training session and consultation
            //string input = "";
            //string passname = "";
            //Console.WriteLine("[1] Supervised training session");
            //Console.WriteLine("[2] Consultation");
            //Console.Write("Enter your choice: ");
            //input = Console.ReadLine();
            //switch (input)
            //{
            //    case "1":
            //        Category trainer = Category.Trainer;
            //        passname = Console.ReadLine();

            //        break;
            //    case "2":
            //        Category consultation = Category.Consultation;
            //        passname = Console.ReadLine();

            //        break;
            //}

        }

        public void CancelReservation(ReservingEntity owner)
        {
            ViewTimeTable(owner);
            int del;
            Console.Write("\nCansel reservation (number): ");
            del = Int32.Parse(Console.ReadLine());
            calendar.reservations.Remove(calendar.reservations[del]);
        }

        // Consider how and when to add a new Trainer to the database.
        // Maybe define a method to persist it? Any other reasonable schemes?

        //private static List<Tuple<Category, int>> InitializeHourlyCosts()
        //{
        //    // TODO: fetch from "database"
        //    var hourlyCosts = new List<Tuple<Category, int>>
        //    {
        //        Tuple.Create(Category.Trainer, Eric),
        //        Tuple.Create(Category.Consultation, Adam),
        //    };
        //    return hourlyCosts;
        //}

    }
}
