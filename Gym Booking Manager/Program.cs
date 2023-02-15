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
            Database data1 = new Database();
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
                Console.WriteLine("----------------------------");

                switch (userInput)
                {
                    case "1":
                        ReservingEntity member = data1.userObjects[0];
                        Console.WriteLine("--- Member ---");
                        Console.WriteLine("1. View schedules that you are signed up to");       // Samla view schedule, equipment, PT(?) och space här
                        Console.WriteLine("2. View and sign Up to activity");  // Kom vidare till att se vad som går att boka för aktivitet
                        Console.WriteLine("3. Make an individual reservation");  // Kom vidare till att se vad som går att reservera
                        Console.WriteLine("4. Edit individual reservation"); // Cancel resevation or make a new!!!
                        Console.WriteLine("5. Cancel Activity");
                        Console.WriteLine("e. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();
                        Console.WriteLine("----------------------------");


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
                                data1.MakeRes(member);
                                Console.WriteLine();
                                // Implement Method "make reservation" method for space,equimpent and trainer!!
                                //Behöver skriva ut alternativ till användaren
                                break;
                            case "4":
                                // Implemetnera metoden 
                            case "5":
                                //implementera remove activity
                                data1.schedule.RemoveActivity(member, data1);
                                break;
                            case "e":
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
                        Console.WriteLine("e. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();
                        Console.WriteLine("----------------------------");

                        switch (userInput)
                        {
                            case "1":
                                // Code to purchase subscription
                                data1.user.UserManagement(nonMember, data1);
                                break;
                            case "e":
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
                        Console.WriteLine("4. View trainer");
                        Console.WriteLine("5. Add activity");              // Reservera PT, gruppass, equipments, lokal
                        Console.WriteLine("6. Modify activity");            
                        Console.WriteLine("7. Remove acticity");
                        Console.WriteLine("8. make reservation");
                        Console.WriteLine("9. Edit reservation");          // Redigera reservation
                        Console.WriteLine("10. View log");                  // Se de anställdas loggar
                        Console.WriteLine("11. Restrict equipment");        // Registrera avstängda maskiner/lokaler
                        Console.WriteLine("12. User management");           // Användarhantering, purchase subscription to members
                        Console.WriteLine("13. Add template");
                        Console.WriteLine("e. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();
                        Console.WriteLine("----------------------------");

                        switch (userInput)
                        {
                            case "1":
                                // Code to view schedule
                                data1.schedule.ViewSchedule(staff);
                                break;
                            case "2":
                                data1.ViewEquipments();
                                // Code to view equipment
                                break;
                            case "3":
                                // Code to view space
                                data1.ViewSpaces();
                                break;
                            case "4":
                                // Code to view trainer
                                data1.ViewTrainer();
                                break;
                            case "5":
                                data1.schedule.AddActivity(staff, data1);
                                break;
                            case "6":
                                // Code to Modify activity
                                data1.schedule.ModifyActivity(data1, staff);
                                break;
                            case "7":
                                // Code to Remove activity
                                data1.schedule.RemoveActivity(staff, data1);
                                break;
                            case "8":
                                // make a reservation for a member by a staff
                                break;
                            case "9":
                                // edit a reservation for a member by a staff
                                break;
                            case "10":
                                // View logfile
                                break;
                            case "11":
                                data1.restricted.SetRestrictedStatus(data1);
                                break;
                            case "12":
                                data1.user.UserManagement(data1.userObjects[1],data1);
                                break;
                            case "13":
                                data1.schedule.AddTemplateActivity(staff, data1);
                                break;
                            case "e":
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
                        Console.WriteLine("e. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();
                        Console.WriteLine("----------------------------");

                        switch (userInput)
                        {
                            case "1":
                                // View Restrictions
                                data1.ViewRestrictedObject();
                                break;
                            case "2":
                                // Code to Drop Restrictions
                                data1.restricted.DropRestrictedObjects(data1);
                                break;
                            case "e":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                }
                data1.SaveToDataBase();
            }
        }
        //Add method below if needed!!!
    }
}