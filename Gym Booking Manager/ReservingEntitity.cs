using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    [DataContract]
    internal class ReservingEntity : ICSVable
    {
        [DataMember]
        public string uniqueID { get; set; }
        [DataMember]
        public string name { get; set; } // Here the "field" is private, but properties (access of the field) public here - this constellation being purely declarative without change in functionality
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string status { get; set; }
        public ReservingEntity() { }

        public ReservingEntity(string name, string uniqueID, string phone, string email, string status)
        {
            this.name = name;
            this.phone = phone;
            this.email = email;
            this.uniqueID = uniqueID;
            this.status = status;
        }
        public void UserManagement(ReservingEntity user, Database data)
        {
            if (user.status == "Non-member")
            {
                //A payment method should be implemented here
                List<ReservingEntity> dayPass = new List<ReservingEntity>();
                Console.WriteLine("Enter name:> ");
                string name = ReturnString();
                ReservingEntity tempMember = new ReservingEntity(name, "333", "", "", "Member");
                dayPass.Add(tempMember);
            }
            else if (user.status == "Staff")
            {
                //Do Something
                Console.WriteLine("[1] Purchase daypass");
                Console.WriteLine("[2] Add new user");
                Console.WriteLine("[3] Remove user");
                Console.WriteLine("[Press any other key] Go back");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    data.userObjects[3].status = "Member";
                }
                else if (input == "2")
                {
                    Console.WriteLine("Name: ");
                    string name = ReturnString();
                    Console.WriteLine("UniqueID: ");
                    string uniqueID = ReturnString();
                    Console.WriteLine("Phone: ");
                    string phone = ReturnString();
                    Console.WriteLine("Email: ");
                    string email = ReturnString();
                    Console.WriteLine("Status: ");
                    string status = ReturnString();
                    ReservingEntity newMember = new ReservingEntity(name, uniqueID, phone, email, status);
                    data.userObjects.Add(newMember);

                }
                else if (input == "3")
                {
                    int n = 1;
                    foreach (ReservingEntity u in data.userObjects)
                    {
                        Console.WriteLine($"{n} : {u.name}");
                        n++;
                    }
                    Console.WriteLine("Type number corresponding with user to remove:> ");
                    int number = Convert.ToInt32(Console.ReadLine());
                    data.userObjects.RemoveAt(number - 1);
                }
                else
                    Console.WriteLine("Input Error");
            }
        }
        public string ReturnString()
        {
            Console.Write(":> ");
            string input = Console.ReadLine();
            return input;
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
