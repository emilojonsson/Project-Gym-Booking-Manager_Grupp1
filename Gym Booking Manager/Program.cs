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
            Console.Clear();
            Database data1 = new Database();
            data1.LoadDataBase();
            Console.WriteLine("\r\n   ___              ___           _   _             __  __                             \r\n  / __|_  _ _ __   | _ ) ___  ___| |_(_)_ _  __ _  |  \\/  |__ _ _ _  __ _ __ _ ___ _ _ \r\n | (_ | || | '  \\  | _ \\/ _ \\/ _ \\ / / | ' \\/ _` | | |\\/| / _` | ' \\/ _` / _` / -_) '_|\r\n  \\___|\\_, |_|_|_| |___/\\___/\\___/_\\_\\_|_||_\\__, | |_|  |_\\__,_|_||_\\__,_\\__, \\___|_|  \r\n       |__/                                 |___/                        |___/         \r\n");
            ReservingEntity user = new ReservingEntity();
            Console.WriteLine("Enter s for SIGNUP");
            Console.WriteLine("Enter q to EXIT");
            Console.WriteLine("");
            Console.Write("Enter UniqeID:> ");
            string input = Console.ReadLine();
            Console.WriteLine("");
            foreach (ReservingEntity rs in data1.userObjects)
            {
                if (rs.uniqueID == input)
                {
                    user = rs;
                }
            }
            if(input == "q")
            {
                data1.SaveToDataBase();
                Environment.Exit(0);
            }
            else if(input == "s")
            {
                Random rand = new Random();
                string random = Convert.ToString(rand.Next(100, 500));
                Console.WriteLine("Enter name");
                string name = Console.ReadLine();
                Console.WriteLine($"Enter Email");
                string email = Console.ReadLine();
                user.name = name;
                user.email = email;
                user.status = "Member";
                user.uniqueID = random;
                Console.WriteLine($"You UniqeID is {user.uniqueID}");
                Console.ReadKey();

            }
            bool quit = false;

            while (!quit)
            {
                switch (user.status)
                {
                    case "Member":
                        choiceMember(data1, user);
                        break;
                    case "Staff":
                        choiceStaff(data1,user);
                        break;
                    case "Service":
                        choiceService(data1,user);
                        break;
                    default:
                        Console.WriteLine("\nInvalid option");
                        RunMenu();
                        break;
                }
                data1.SaveToDataBase();            
            }
        }

        private static string choiceService(Database data1, ReservingEntity user)
        {
            Console.Clear();
            Console.WriteLine("\r\n  _              _        ___              _        \r\n | |   ___  __ _(_)_ _   / __| ___ _ ___ _(_)__ ___ \r\n | |__/ _ \\/ _` | | ' \\  \\__ \\/ -_) '_\\ V / / _/ -_)\r\n |____\\___/\\__, |_|_||_| |___/\\___|_|  \\_/|_\\__\\___|\r\n           |___/                                    \r\n");
            string userInput;
            ReservingEntity service = user;
            Console.WriteLine("--- Service ---");
            Console.WriteLine("1. View Restrictions");
            Console.WriteLine("2. Drop Restrictions");
            Console.WriteLine("e. Go back");
            Console.Write("Enter your choice: ");
            userInput = Console.ReadLine();
            Console.WriteLine("----------------------------");
            Console.Clear();

            switch (userInput)
            {
                case "1":
                    data1.ViewRestrictedObject();
                    Console.ReadLine();
                    break;
                case "2":
                    data1.restricted.DropRestrictedObjects(data1);
                    break;
                case "e":
                    data1.SaveToDataBase();
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }

        private static string choiceNonMember(Database data1, ReservingEntity user)
        {
            Console.Clear();
            Console.WriteLine("\r\n  _  _            __  __           _              \r\n | \\| |___ _ _   |  \\/  |___ _ __ | |__  ___ _ _  \r\n | .` / _ \\ ' \\  | |\\/| / -_) '  \\| '_ \\/ -_) '_| \r\n |_|\\_\\___/_||_| |_|  |_\\___|_|_|_|_.__/\\___|_|   \r\n                                                  \r\n");
            ReservingEntity nonMember = user;
            Console.WriteLine("--- Non-member ---");
            Console.WriteLine("1. Purchase subscription");
            Console.WriteLine("e. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("----------------------------");
            Console.Clear();

            switch (userInput)
            {
                case "1":
                    data1.user.UserManagement(nonMember, data1);
                    break;
                case "e":
                    data1.SaveToDataBase();
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }

        private static string choiceStaff(Database data1, ReservingEntity user)
        {
            Console.Clear();
            Console.WriteLine("\r\n  _              _        ___ _         __  __ \r\n | |   ___  __ _(_)_ _   / __| |_ __ _ / _|/ _|\r\n | |__/ _ \\/ _` | | ' \\  \\__ \\  _/ _` |  _|  _|\r\n |____\\___/\\__, |_|_||_| |___/\\__\\__,_|_| |_|  \r\n           |___/                               \r\n");
            ReservingEntity staff = user;
            Console.WriteLine("");
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
            Console.Clear();

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
                    data1.CancelReservation(staff);
                    break;
                case "10":
                    data1.ViewLogfile();
                    break;
                case "11":
                    data1.restricted.SetRestrictedStatus(data1);
                    break;
                case "12":
                    data1.user.UserManagement(staff, data1);
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
                    data1.SaveToDataBase();
                    RunMenu();
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            return userInput;
        }

        private static string choiceMember(Database data1, ReservingEntity user)
        {
            Console.Clear();
            Console.WriteLine("\r\n  _              _        __  __           _             \r\n | |   ___  __ _(_)_ _   |  \\/  |___ _ __ | |__  ___ _ _ \r\n | |__/ _ \\/ _` | | ' \\  | |\\/| / -_) '  \\| '_ \\/ -_) '_|\r\n |____\\___/\\__, |_|_||_| |_|  |_\\___|_|_|_|_.__/\\___|_|  \r\n           |___/                                         \r\n");
            ReservingEntity member = user;
            Console.WriteLine("");
            Console.WriteLine("1. View schedules that you are signed up to");
            Console.WriteLine("2. View and sign up to activity");
            Console.WriteLine("3. Make an individual reservation");
            Console.WriteLine("4. Cancel all reservations");
            Console.WriteLine("5. Cancel Activity");
            Console.WriteLine("e. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();
            Console.WriteLine("----------------------------");
            Console.Clear();


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
                case "4":                    data1.CancelReservation(member);                    break;
                case "5":
                    data1.schedule.RemoveActivity(member, data1, editInsted: false);
                    break;
                case "e":
                    data1.SaveToDataBase();
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