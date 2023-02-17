using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Gym_Booking_Manager
{
    [DataContract]
    internal class ReservingEntity 
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
        public object NonMemberRegistration(Database data, ReservingEntity user)
        {
            //A payment method should be implemented here
            Random rand = new Random();
            string random = Convert.ToString(rand.Next(100, 500));
            Console.WriteLine("Enter name");
            string name = ReturnString();
            Console.WriteLine($"Enter Email");
            string email = ReturnString();
            user.name = name;
            user.email = email;
            user.status = "Member";
            user.uniqueID = random;
            Console.WriteLine($"You UniqeID is {user.uniqueID}");
            Console.ReadKey();
            return user;
        }
        public void UserManagement(ReservingEntity user, Database data)
        {
            if (user.status == "Non-member")
            {
                NonMemberRegistration(data, user);
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
                    NonMemberRegistration(data, user);
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
                else if(input == "4")
                {
                    Console.Write("Type space,equipment or trainer to add:> ");
                    string choose = Console.ReadLine();
                    if(choose.ToLower() == "space")
                    {
                        Console.WriteLine("[1] Hall [2] Lane [3] Studio");
                        Console.WriteLine("Enter number:> ");
                        int Catagory = Convert.ToInt32(Console.ReadLine());
                        if(Catagory == 1)
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Space space = new Space(category:Space.Category.Hall, name); 
                            data.spaceObjects.Add(space);
                        }
                        else if(Catagory == 2)
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Space space = new Space(category: Space.Category.Lane, name);
                            data.spaceObjects.Add(space);
                        }
                        else if(Catagory == 3 )
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Space space = new Space(category: Space.Category.Studio, name);
                            data.spaceObjects.Add(space);
                        }
                    }
                    else if(choose.ToLower() == "equipment") 
                    {
                        Console.WriteLine("[1] Small [2] Large");
                        Console.WriteLine("Enter number:> ");
                        int Catagory = Convert.ToInt32(Console.ReadLine());
                        if (Catagory == 1)
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Equipment equipment = new Equipment(category: Equipment.Category.Small, name);
                            data.equipmentObjects.Add(equipment);
                        }
                        else if(Catagory == 2)
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Equipment equipment = new Equipment(category: Equipment.Category.Large, name);
                            data.equipmentObjects.Add(equipment);
                        }
                    }
                    else if (choose.ToLower() == "trainer")
                    {
                        Console.WriteLine("[1] PT [2] Consultation");
                        Console.WriteLine("Enter number:> ");
                        int Catagory = Convert.ToInt32(Console.ReadLine());
                        if(Catagory == 1)
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Trainer trainer = new Trainer(Trainer.Category.Trainer, name);
                            data.trainerObjects.Add(trainer);
                        }
                        else if(Catagory==2)
                        {
                            Console.WriteLine("Enter a name:> ");
                            string name = Console.ReadLine();
                            Trainer trainer = new Trainer(Trainer.Category.Consultation, name);
                            data.trainerObjects.Add(trainer);
                        }
                    }
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
            return $"Status : {status}\nID : {uniqueID}\nName : {name}\nPhone : {phone}\nEmail : {email}";
        }
    }
}
