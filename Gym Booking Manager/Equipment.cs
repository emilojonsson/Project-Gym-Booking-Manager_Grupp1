﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Equipment : IReservable, ICSVable, IComparable<Equipment>
    {
        //private static readonly List<Tuple<Category, int>> hourlyCosts = InitializeHourlyCosts(); // Costs may not be relevant for the prototype. Let's see what the time allows.
        private Category category;
        public String name;
        public Calendar calendar;        //private readonly Calendar calendar;        public string? NewEquipment { get; }        public Equipment(Category category, string name)
        {
            this.category = category;
            this.name = name;
            this.calendar = new Calendar();
        }

        // Every class T to be used for DbSet<T> needs a constructor with this parameter signature. Make sure the object is properly initialized.
        public Equipment(Dictionary<String, String> constructionArgs)
        {
            this.name = constructionArgs[nameof(name)];
            if (!Category.TryParse(constructionArgs[nameof(category)], out this.category))
            {
                throw new ArgumentException("Couldn't parse a valid Equipment.Category value.", nameof(category));
            }

            this.calendar = new Calendar();
        }        public Equipment(string? newEquipment)        {            NewEquipment = newEquipment;        }        public int CompareTo(Equipment? other)
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
            Small,
            Large
        }

        public void ViewTimeTable()
        {
            // Fetch
            List<Reservation> tableSlice = this.calendar.GetSlice();
            // Show?
            foreach (Reservation reservation in tableSlice)
            {
                // Do something?
            }

        }

        public void MakeReservation(ReservingEntity owner, DateTime dateTime)        {            calendar.reservations.Add(new Reservation(owner, dateTime));

        }

        public void CancelReservation()
        {

        }

        // Consider how and when to add a new Space to the database.
        // Maybe define a method to persist it? Any other reasonable schemes?

        //private static List<Tuple<Category, int>> InitializeHourlyCosts()
        //{
        //    // TODO: fetch from "database"
        //    var hourlyCosts = new List<Tuple<Category, int>>
        //    {
        //        Tuple.Create(Category.Small, Boxing gloves),
        //        Tuple.Create(Category.Large, Bench press)
        //    };
        //    return hourlyCosts;
        //}

    }
}
