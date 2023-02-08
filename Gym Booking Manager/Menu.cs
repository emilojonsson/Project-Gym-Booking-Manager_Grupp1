using System;

namespace Gym_Booking_Manager
{    public class Menu    {        public Menu()        {            string userInput = "";
            while (userInput != "q")
            {
                Console.WriteLine("Welcome to the menu:");
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Non-member");
                Console.WriteLine("3. Staff");
                Console.WriteLine("q. Quit");
                Console.Write("Enter your choice: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("--- Customer ---");
                        Console.WriteLine("1. View group schedule");
                        Console.WriteLine("2. View equipment");
                        Console.WriteLine("3. View space");
                        Console.WriteLine("4. Make reservation");
                        Console.WriteLine("5. Edit reservation");
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "1":
                                // Code to view group schedule
                                break;
                            case "2":
                                // Code to view equipment
                                break;
                            case "3":
                                // Code to view space
                                break;
                            case "4":
                                // Code to make reservation
                                break;
                            case "5":
                                // Code to edit reservation
                                break;
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine("--- Non-member ---");
                        Console.WriteLine("1. Purchase subscription");
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "1":
                                // Code to purchase subscription
                                break;
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;                    case "3":                        Console.WriteLine("--- Staff ---");
                        Console.WriteLine("1. View group schedule");
                        Console.WriteLine("2. View equipment");
                        Console.WriteLine("3. View space");
                        Console.WriteLine("4. Make reservation");
                        Console.WriteLine("5. Edit reservation");
                        Console.WriteLine("6. View log");
                        Console.WriteLine("7. Update group schedule");
                        Console.WriteLine("8. Restrict equipment");
                        Console.WriteLine("9. User management");
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();                        switch (userInput)
                        {
                            case "1":
                                // Code to view group schedule
                                break;
                            case "2":
                                // Code to view equipment
                                break;
                            case "3":
                                // Code to view space
                                break;
                            case "4":
                                // Code to make reservation
                                break;
                            case "5":
                                // Code to edit reservation
                                break;
                            case "6":
                                // Code to view log
                                break;
                            case "7":
                                // Code to update group schedule
                                break;
                            case "8":
                                // Code to restrict equipment
                                break;
                            case "9":
                                // Code to user management
                                break;
                            case "q":
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;                }            }        }
    }
}

