using System;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Menu
    {
        public void Run()
        {
            string userInput = "";
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine("\nWelcome to the menu:");
                Console.WriteLine("1. Member");
                Console.WriteLine("2. Non-member");
                Console.WriteLine("3. Staff");
                Console.WriteLine("q. Quit program");
                Console.Write("Enter your choice: ");
                userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "1":
                        choiceMember();
                        break;
                    case "2":
                        choiceNonMember();
                        break;
                    case "3":
                        choiceStaff();
                        break;
                    case "4":
                        choiceService();
                        break;
                    case "q":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("\nInvalid option");
                        Run();
                        break;
                }
            }
        }

        // Fundera på om funktionerna ska returnera data för att bli testbar
        private void choiceMember()
        {
            Console.WriteLine("\n--- Member ---");
            Console.WriteLine("1. View schedule");       // Samla view schedule, equipment, PT(?) och space här
            Console.WriteLine("2. Sign Up to activity");  // Skriv upp sig på en aktivitet, eller skapa en???
            Console.WriteLine("3. Make a reservation");  // Kom vidare till att se vad som går att reservera
            Console.WriteLine("4. Edit reservation"); // Cancel resevation or make a new!!!
            Console.WriteLine("5. Cancel Activity");
            Console.WriteLine("q. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // TODO Code to view schedule
                    break;
                case "2":
                    // TODO Code to signup to activity
                    break;
                case "3":
                    // TODO Code to make reservation                    // implement Method "make reservation" method for space,equimpent and trainer!!
                    // Behöver skriva ut alternativ till användaren
                    break;
                case "4":
                    // TODO Implemetnera metoden
                    break;
                case "5":
                    // TODO Implementera remove activity
                    break;
                case "q":
                    // Go back to main menu
                    Run();
                    break;
                default:
                    Console.WriteLine("\nInvalid option");
                    choiceMember();
                    break;
            }
        }

        private void choiceNonMember()
        {
            Console.WriteLine("\n--- Non-member ---");
            Console.WriteLine("1. Purchase subscription"); // Registrera ny användare och tidslängd på sub.
            Console.WriteLine("q. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // TODO Code to purchase subscription
                    // Make a temporary member
                    break;
                case "q":
                    Run();
                    break;
                default:
                    Console.WriteLine("\nInvalid option");
                    choiceNonMember();
                    break;
            }
        }

        private void choiceStaff()
        {
            Console.WriteLine("\n--- Staff ---");
            Console.WriteLine("1. View schedule");             // Se alla pass, bokningsbart eller fullbokat
            Console.WriteLine("2. View equipment");            // Se vilka equipments som går att boka
            Console.WriteLine("3. View space");                // Se lediga lokaler
            Console.WriteLine("4. Add activity");              // Reservera PT, gruppass, equipments, lokal
            Console.WriteLine("5. Modify activity");
            Console.WriteLine("6. Remove activity");
            Console.WriteLine("7. Make reservation");
            Console.WriteLine("8. Edit reservation");          // Redigera reservation
            Console.WriteLine("9. View log");                  // Se de anställdas loggar
            Console.WriteLine("10. Edit schedule");             // Uppdatera schemat för de anställda
            Console.WriteLine("11. Restrict equipment");        // Registrera avstängda maskiner/lokaler
            Console.WriteLine("12. User management");           // Användarhantering, purchase subscription to members
            Console.WriteLine("q. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // TODO Code to view schedule
                    break;
                case "2":
                    // TODO Code to view equipment
                    break;
                case "3":
                    // TODO Code to view space
                    break;
                case "4":
                    // TODO Code to Add activity
                    break;
                case "5":
                    // TODO Code to Modify activity
                    break;
                case "6":
                    // TODO Code to Remove activity
                    break;
                case "7":
                    // TODO Code to Make reservation
                    break;
                case "8":
                    // TODO Code to Edit reservation
                    break;
                case "9":
                    // TODO Code to view log
                    break;
                case "10":
                    // TODO Code to edit schedule
                    break;
                case "11":
                    // TODO Code to restrict equipment
                    // flytta från space list to restricted list!
                    break;
                case "12":
                    // TODO Code to user management
                    break;
                case "q":
                    Run();
                    break;
                default:
                    Console.WriteLine("\nInvalid option");
                    choiceStaff();
                    break;
            }
        }
        private void choiceService()
        {
            Console.WriteLine("--- Service ---");
            Console.WriteLine("1. View Restrictions");
            Console.WriteLine("2. Drop Restrictions");
            Console.WriteLine("q. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // View Restrictions
                    break;
                case "2":
                    // Code to Drop Restrictions
                    break;
                case "q":
                    Run();
                    break;
                default:
                    Console.WriteLine("\nInvalid option");
                    choiceService();
                    break;
            }
        }
    }
}

