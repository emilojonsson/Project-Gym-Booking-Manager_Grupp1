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
            string userInput = "";            Console.WriteLine("\nWelcome to the menu:");            Console.WriteLine("1. Member");            Console.WriteLine("2. Non-member");            Console.WriteLine("3. Staff");            Console.WriteLine("q. Quit program");            Console.Write("Enter your choice: ");            userInput = Console.ReadLine();            switch (userInput)            {                case "1":                    choiceMember();                    break;                case "2":                    choiceNonMember();                    break;                case "3":                    choiceStaff();                    break;                case "q":                    break;                default:                    Console.WriteLine("\nInvalid option");                    Run();                    break;            }
        }


        // Fundera på om funktionerna ska returnera data för att bli testbar
        private void choiceMember()
        {
            Console.WriteLine("\n--- Member ---");
            Console.WriteLine("1. View schedule");       // Samla view schedule, equipment, PT(?) och space här
            Console.WriteLine("2. Make a reservation");  // Kom vidare till att se vad som går att reservera
            Console.WriteLine("3. Edit reservation");
            Console.WriteLine("q. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Code to view schedule
                    break;
                case "2":
                    // Code to view equipment
                    break;
                case "3":
                    // Code to view space
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
                    // Code to purchase subscription
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
            Console.WriteLine("4. Make a reservation");        // Reservera PT, gruppass, equipments, lokal
            Console.WriteLine("5. Edit reservation");          // Redigera reservation
            Console.WriteLine("6. View log");                  // Se de anställdas loggar
            Console.WriteLine("7. Edit schedule");             // Uppdatera schemat för de anställda
            Console.WriteLine("8. Restrict equipment");        // Registrera avstängda maskiner/lokaler
            Console.WriteLine("9. User management");           // Användarhantering
            Console.WriteLine("q. Go back");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Code to view schedule
                    break;
                case "2":
                    // Code to view equipment
                    break;
                case "3":
                    // Code to view space
                    break;
                case "4":
                    // Code to make a reservation
                    break;
                case "5":
                    // Code to edit reservation
                    break;
                case "6":
                    // Code to view log
                    break;
                case "7":
                    // Code to edit schedule
                    break;
                case "8":
                    // Code to restrict equipment
                    break;
                case "9":
                    // Code to user management
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
    }
}

