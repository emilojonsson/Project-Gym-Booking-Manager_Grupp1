﻿using Gym_Booking_Manager;
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
                        Console.WriteLine("2. Make a reservation");  // Kom vidare till att se vad som går att reservera
                        Console.WriteLine("3. Edit reservation");
                        Console.WriteLine("q. Go back");
                        Console.Write("Enter your choice: ");
                        userInput = Console.ReadLine();


                        switch (userInput)
                        {
                            case "1":
                                // Code to view schedule
                                //schedule1.ViewSchedule(user4);
                                break;
                            case "2":
                                // Make a reservation
                                //activity3.SignUp(user5);
                                break;
                            case "3":
                                // Edit reservation
                                break;
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
                        Console.WriteLine("4. Make a reservation");        // Reservera PT, gruppass, equipments, lokal
                        Console.WriteLine("5. Edit reservation");          // Redigera reservation
                        Console.WriteLine("6. View log");                  // Se de anställdas loggar
                        Console.WriteLine("7. Edit schedule");             // Uppdatera schemat för de anställda
                        Console.WriteLine("8. Restrict equipment");        // Registrera avstängda maskiner/lokaler
                        Console.WriteLine("9. User management");           // Användarhantering
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
                                // Code to view space
                                break;
                            case "4":
                                data1.schedule.AddActivity(staff, data1);
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
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case "4":
                        User service = data1.userObjects[2]; //denna meny ska fixas till enligt Usercase
                        Console.WriteLine("--- Service ---");
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
                        break;
                }
            }
        }
    }
}