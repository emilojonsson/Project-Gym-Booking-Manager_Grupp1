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
            Space space1 = new Space(Space.Category.Hall, "Jympaområdet");
            Space space2 = new Space(Space.Category.Hall, "Uppvärmingsdelen");
            Space space3 = new Space(Space.Category.Lane, "Tennisområdet");
            Space space4 = new Space(Space.Category.Lane, "Paddelytan");
            Space space5 = new Space(Space.Category.Studio, "Dansscenen");
            Space space6 = new Space(Space.Category.Studio, "Gymmet");
            Trainer trainer1 = new Trainer(Trainer.Category.Trainer, "Dansledare");
            Trainer trainer2 = new Trainer(Trainer.Category.Trainer, "Passledare");
            Trainer trainer3 = new Trainer(Trainer.Category.Consultation, "Racketcoach");
            Trainer trainer4 = new Trainer(Trainer.Category.Consultation, "PT");
            Equipment equipment1 = new Equipment(Equipment.Category.Small, "Tennisracket");
            Equipment equipment2 = new Equipment(Equipment.Category.Small, "Musikanläggning");
            Equipment equipment3 = new Equipment(Equipment.Category.Small, "Hopprep");
            Equipment equipment4 = new Equipment(Equipment.Category.Large, "Löparband");
            Equipment equipment5 = new Equipment(Equipment.Category.Large, "Paddelracket");
            Equipment equipment6 = new Equipment(Equipment.Category.Large, "Bänkpress");
            ReservingEntity user1 = new ReservingEntity("Anders Andersson", "1000", "100", "a.a@gmail.com", "Member");
            ReservingEntity user2 = new ReservingEntity("Berit Birgersson", "1001", "101", "b.b@gmail.com", "Member");
            ReservingEntity user3 = new ReservingEntity("Cedric Cederkvist", "1002", "102", "c.c@gmail.com", "Member");
            ReservingEntity user4 = new ReservingEntity("Daniel Damp", "1003", "103", "d.d@gmail.com", "Staff");
            ReservingEntity user5 = new ReservingEntity("Erik Erdogan", "1004", "104", "e.e@gmail.com", "Staff");
            ReservingEntity user6 = new ReservingEntity("Fredrik Fransson", "1005", "105", "f.f@gmail.com", "Service");
            DatabaseTemp data1 = new DatabaseTemp();
            data1.equipmentObjects.Add(equipment1);
            data1.equipmentObjects.Add(equipment2);
            data1.equipmentObjects.Add(equipment3);
            data1.equipmentObjects.Add(equipment4);
            data1.equipmentObjects.Add(equipment5);
            data1.equipmentObjects.Add(equipment6);
            data1.spaceObjects.Add(space1);
            data1.spaceObjects.Add(space2);
            data1.spaceObjects.Add(space3);
            data1.spaceObjects.Add(space4);
            data1.spaceObjects.Add(space5);
            data1.spaceObjects.Add(space6);
            data1.trainerObjects.Add(trainer1);
            data1.trainerObjects.Add(trainer2);
            data1.trainerObjects.Add(trainer3);
            data1.trainerObjects.Add(trainer4);
            data1.userObjects.Add(user1);
            data1.userObjects.Add(user2);
            data1.userObjects.Add(user3);
            data1.userObjects.Add(user4);
            data1.userObjects.Add(user5);
            data1.userObjects.Add(user6);
            DateTime testTime = DateTime.Now;
            GroupSchedule schedule1 = new GroupSchedule();
            Activity activity1 = new Activity("Danspass", "a01", 20, testTime, user1, space5, trainer1, equipment2);
            Activity activity2 = new Activity("Jympapass", "a02", 20, testTime, user2, space1, trainer2, equipment3);
            Activity activity3 = new Activity("Tennisträning", "a03", 20, testTime, user3, space3, trainer3, equipment1);
            Activity activity4 = new Activity("Löparträning", "a04", 20, testTime, user3, space2, trainer4, equipment4);
            Activity activity5 = new Activity("Gymträning", "a05", 20, testTime, user1, space6, trainer4, equipment6);
            schedule1.activities.Add(activity1);
            schedule1.activities.Add(activity2);
            schedule1.activities.Add(activity3);
            schedule1.activities.Add(activity4);
            schedule1.activities.Add(activity5);
            data1.scheduleObjects.Add(schedule1);

            schedule1.AddActivity(user1, data1);
            schedule1.activities[5].SignUp(user1);
            Console.WriteLine(data1.equipmentObjects[0].calendar.reservations[0]);

            Menu m1 = new Menu();
            m1.Run();

            string userInput = "";
            while (userInput != "q")
            {
                // TODO - skapa en säkerhetsåtgärd för inloggning på Customer och Staff
                Console.WriteLine("Welcome to the menu:");
                Console.WriteLine("1. Member");
                Console.WriteLine("2. Non-member");
                Console.WriteLine("3. Staff");
                Console.WriteLine("q. Quit program");
                Console.Write("Enter your choice: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
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
                                schedule1.ViewSchedule(user4);
                                break;
                            case "2":
                                // Make a reservation
                                activity3.SignUp(user5);
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