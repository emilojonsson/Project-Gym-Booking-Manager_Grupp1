using Gym_Booking_Manager;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseTemp data1 = new DatabaseTemp();
            data1.LoadDataBase();

            //Menu m1 = new Menu();
            //m1.Run();


            string userInput = "";
            while (userInput != "q")
            {
                // TODO - skapa en säkerhetsåtgärd för inloggning på Customer och Staff
                Console.WriteLine("Welcome to the menu:");
                Console.WriteLine("1. Member");
                Console.WriteLine("2. Non-member");
                Console.WriteLine("3. Staff");
                Console.WriteLine("4. Service");
                Console.WriteLine("q. Quit program");
                Console.Write("Enter your choice: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        ReservingEntity member = data1.userObjects[0];
                        Console.WriteLine("--- Member ---");
                        Console.WriteLine("1. View schedule");       // Samla view schedule, equipment, PT(?) och space här
                        Console.WriteLine("2. Sign Up to activity");  // Kom vidare till att se vad som går att boka för aktivitet
                        Console.WriteLine("3. Make a reservation");  // Kom vidare till att se vad som går att reservera
                        Console.WriteLine("4. Edit reservation"); // Cancel resevation or make a new!!!
                        Console.WriteLine("5. Cancel Activity");
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();


                        switch (userInput)
                        {
                            case "1":
                                // Code to view schedule
                                data1.schedule.ViewSchedule(member);
                                break;
                            case "2":
                                // Signup to activity
                                data1.schedule.SignUp(member, data1);
                                break;
                            case "3":
                                // Implement Method "make reservation" method for space,equimpent and trainer!!
                                //Behöver skriva ut alternativ till användaren
                                break;
                            case "4":
                                // Implemetnera metoden 
                            case "5":
                                //implementera remove activity
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case "2":
                        ReservingEntity nonMember = data1.userObjects[3];
                        Console.WriteLine("--- Non-member ---");
                        Console.WriteLine("1. Purchase subscription"); // Registrera ny användare och tidslängd på sub.
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "1":
                                // Code to purchase subscription
                                data1.UserManagement(data1.userObjects[0]);
                                break;
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case "3":
                        ReservingEntity staff = data1.userObjects[1];
                        Console.WriteLine("--- Staff ---");
                        Console.WriteLine("1. View schedule");             // Se alla pass, bokningsbart eller fullbokat
                        Console.WriteLine("2. View equipment");            // Se vilka equipments som går att boka
                        Console.WriteLine("3. View space");                // Se lediga lokaler
                        Console.WriteLine("4. Add activity");              // Reservera PT, gruppass, equipments, lokal
                        Console.WriteLine("5. Modify activity");            
                        Console.WriteLine("6. Remove acticity");
                        Console.WriteLine("7. make reservation");
                        Console.WriteLine("8. Edit reservation");          // Redigera reservation
                        Console.WriteLine("9. View log");                  // Se de anställdas loggar
                        Console.WriteLine("10. Edit schedule");             // Uppdatera schemat för de anställda
                        Console.WriteLine("11. Restrict equipment");        // Registrera avstängda maskiner/lokaler
                        Console.WriteLine("12. User management");           // Användarhantering, purchase subscription to members
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "1":
                                // Code to view schedule
                                break;
                            case "2":
                                // Code to view equipment
                                break;
                            case "3":
                                data1.schedule.SignUp(staff, data1);
                                break;
                            case "4":
                                data1.schedule.AddActivity(staff, data1);
                                break;
                            case "5":
                                // Code to Modify activity
                                break;
                            case "6":
                                // Code to Modify activity
                                break;
                            case "7":
                                // Code to Modify activity
                                break;
                            case "8":
                                // Code to view log
                                break;
                            case "9":
                                // Code to edit schedule
                                break;
                            case "10":
                                // Code to restrict equipment
                                //flytta från space list
                                break;
                            case "11":
                                data1.SetRestrictedStatus();
                                break;
                            case "12":
                                data1.UserManagement(data1.userObjects[1]);
                                break;
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case "4":
                        ReservingEntity service = data1.userObjects[2]; //denna meny ska fixas till enligt Usercase
                        Console.WriteLine("--- Service ---");
                        Console.WriteLine("1. View Restrictions");
                        Console.WriteLine("2. Drop Restrictions");
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "1":
                                // View Restrictions
                                data1.ViewRestrictedObject();
                                break;
                            case "2":
                                // Code to Drop Restrictions
                                data1.DropRestrictedObjects();
                                break;
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                }
            }
        }
        //Add method below if needed!!!
    }
}