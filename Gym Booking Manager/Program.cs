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
            RunMenu();
        }

        private static void RunMenu()
        {
            Database data1 = new Database();
            data1.LoadDataBase();
            string userInput = "";

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
                    choiceMember(data1);
                    break;
                case "2":
                    choiceNonMember(data1);
                    break;
                case "3":
                    choiceStaff(data1);
                    break;
                case "4":
                    choiceService(data1);
                    break;
                case "q":
                    break;
            }
            data1.SaveToDataBase();
            
        }

        private static string choiceService(Database data1)
        {
            string userInput;
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
                    data1.ViewRestrictedObject();
                    break;
                case "2":
                    data1.restricted.DropRestrictedObjects(data1);
                    break;
                case "e":
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }

        private static string choiceNonMember(Database data1)
        {
            ReservingEntity nonMember = data1.userObjects[3];
            Console.WriteLine("--- Non-member ---");
            Console.WriteLine("1. Purchase subscription");
            Console.WriteLine("e. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("----------------------------");

            switch (userInput)
            {
                case "1":
                    data1.user.UserManagement(nonMember, data1);
                    break;
                case "e":
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }

        private static string choiceStaff(Database data1)
        {
            ReservingEntity staff = data1.userObjects[1];
            Console.WriteLine("--- Staff ---");
            Console.WriteLine("1. View group activities");
            Console.WriteLine("2. View equipment");
            Console.WriteLine("3. View space");
            Console.WriteLine("4. View trainer");
            Console.WriteLine("5. Add activity");
            Console.WriteLine("6. Modify activity");
            Console.WriteLine("7. Remove activity");
            Console.WriteLine("8. Make reservation");
            Console.WriteLine("9. Edit reservation");
            Console.WriteLine("10. View log");
            Console.WriteLine("11. Restrict equipment");
            Console.WriteLine("12. User management");
            Console.WriteLine("13. Add template");
            Console.WriteLine("14. View template");
            Console.WriteLine("15. Delete template");
            Console.WriteLine("16. Member registrations");
            Console.WriteLine("e. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("----------------------------");

            switch (userInput)
            {
                case "1":
                    data1.schedule.ViewSchedule(data1, staff);
                    break;
                case "2":
                    data1.ViewEquipments();
                    break;
                case "3":
                    data1.ViewSpaces();
                    break;
                case "4":
                    data1.ViewTrainer();
                    break;
                case "5":
                    data1.schedule.AddActivity(staff, data1);
                    break;
                case "6":
                    data1.schedule.ModifyActivity(data1, staff);
                    break;
                case "7":
                    data1.schedule.RemoveActivity(staff, data1, editInsted: false);
                    break;
                case "8":
                    data1.MakeResStaff();
                    break;
                case "9":
                    // Insert method here
                    break;
                case "10":
                    // View logfile
                    break;
                case "11":
                    data1.restricted.SetRestrictedStatus(data1);
                    break;
                case "12":
                    data1.user.UserManagement(data1.userObjects[1], data1);
                    break;
                case "13":
                    data1.schedule.AddTemplateActivity(staff, data1);
                    break;
                case "14":
                    data1.schedule.ViewTemplate(data1);
                    break;
                case "15":
                    data1.schedule.DeleteTemplate(data1);
                    break;
                case "16":
                    data1.ViewReservations(staff);
                    break;
                case "e":
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }

        private static string choiceMember(Database data1)
        {
            ReservingEntity member = data1.userObjects[0];
            Console.WriteLine("--- Member ---");
            Console.WriteLine("1. View schedules that you are signed up to");
            Console.WriteLine("2. View and sign up to activity");
            Console.WriteLine("3. Make an individual reservation");
            Console.WriteLine("4. Edit individual reservation");
            Console.WriteLine("5. Cancel Activity");
            Console.WriteLine("e. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("----------------------------");


            switch (userInput)
            {
                case "1":
                    data1.schedule.ViewSchedule(data1, member);
                    break;
                case "2":
                    data1.schedule.SignUp(member, data1);
                    break;
                case "3":
                    data1.MakeRes(member);
                    Console.WriteLine();
                    break;
                case "4":                    // Insert method here                    break;
                case "5":
                    data1.schedule.RemoveActivity(member, data1, editInsted: false);
                    break;
                case "e":
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }
    }
}