using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Gym_Booking_Manager.Space;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    //
    internal abstract class User
    {
        public string uniqueID { get; set; }
        public string name { get; set; } // Here the "field" is private, but properties (access of the field) public here - this constellation being purely declarative without change in functionality
        public string phone { get; set; }
        public string email { get; set; }
        public string status { get; set; }  

        protected User(string name, string uniqueID, string phone, string email, string status)
        {
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.uniqueID = uniqueID;
            this.status = status;
        }
        public override string ToString()
        {
            return this.CSVify(); // TODO: Don't use CSVify. Make it more readable.
        }

        // Every class C to be used for DbSet<C> should have the ICSVable interface and the following implementation.
        public string CSVify()
        {
            return $"Status : {status}\nID : {uniqueID}\nName : {name}\nPhone : {phone}\nEmail : {email}";
        }
    }
}