using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class GroupSchedule
    {
        //Do we need thos list? we use this list in DataTemp....? Consult Team!!!
        public List<Activity> activities = new List<Activity>();
        public void ViewSchedule(ReservingEntity user)
        {
            //Todo Here we need to think about a way for the owner (staff) to also be able to view his schedule. Right now we visualize the participants
            if (user.status == "Member")
            {
                foreach (Activity activity in activities)
                {
                    if (activity.participants.Contains(user))
                    {
                        Console.WriteLine(activity);
                    }
                }
            }
            if (user.status == "Staff")
            {
                foreach (Activity activity in activities)
                {
                    Console.WriteLine(activity);
                }
            }
        }
        public void AddActivity(ReservingEntity owner, Database data)
        {
            Console.WriteLine("Ange information om aktiviteten:");
            string? activityDetails = Console.ReadLine();
            
            DateTime uniqueTimeToID = DateTime.Now;
            string activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm"); //choose this for now. it is at least unique
            
            Console.WriteLine("Ange hur många deltagare det maximalt kan vara på aktiviteten:");
            int participantLimit = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Ange när aktiviteten ska starta:");
            DateTime timeSlot = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Ange hur många minuter aktiviteten ska hålla på:");
            double durationMinutes = double.Parse(Console.ReadLine());

            Console.WriteLine("Ange siffra för vilken yta som ska reserveras för aktiviteten:");
            foreach(Space space in data.spaceObjects)
            {
                Console.WriteLine($"{data.spaceObjects.IndexOf(space)} - {space.name}");
            }
            int addedSpace = int.Parse(Console.ReadLine());

            Console.WriteLine("Ange siffra för vilken träningsprofil som ska reserveras för aktiviteten:");
            foreach (Trainer trainer in data.trainerObjects)
            {
                Console.WriteLine($"{data.trainerObjects.IndexOf(trainer)} - {trainer.name}");
            }
            int addedTrainer = int.Parse(Console.ReadLine());

            Console.WriteLine("Select number for which equipment you want to make a reservation:");
            foreach (Equipment equipment in data.equipmentObjects)
            {
                Console.WriteLine($"{data.equipmentObjects.IndexOf(equipment)} - {equipment.name}");
            }
            int addedEquipment = int.Parse(Console.ReadLine());

            activities.Add(new Activity(activityID, activityDetails, participantLimit, timeSlot, durationMinutes, owner, data.spaceObjects[addedSpace], data.trainerObjects[addedTrainer], data.equipmentObjects[addedEquipment]));
            data.spaceObjects[addedSpace].MakeReservation(owner, timeSlot, durationMinutes);
            data.trainerObjects[addedTrainer].MakeReservation(owner, timeSlot, durationMinutes);
            data.equipmentObjects[addedEquipment].MakeReservation(owner, timeSlot, durationMinutes);
        }
        public void RemoveActivity(ReservingEntity user, Database data1)
        {
            if (user.status == "Member")
            {
                foreach (Activity activity in activities)
                {
                    Console.WriteLine("Nedan listas de gruppaktiviteter som du är anmäld på:");
                    foreach (ReservingEntity signUp in activity.participants)
                    {
                        int count = 1;
                        if (user == signUp)
                        {
                            Console.WriteLine($"{count}. {activity.activityID},  {activity.timeSlot.reservations[0]}"); //for now an activity can only have one reservation, but beware of future changes...
                            count++;
                        }
                        else
                        {
                            Console.WriteLine($"{user} hittas ej som anmäld på någon gruppaktivitet");
                        }
                    }
                }
                Console.WriteLine("Ange den aktivitet du vill avanmäla dig från:");
                int answerInt = int.Parse(Console.ReadLine());

                foreach (Activity activity in activities)
                {
                    int count = 0;
                    foreach (ReservingEntity signUp in activity.participants)
                    {
                        if (user == signUp)
                        {
                            count++;
                        }
                        if (count == answerInt)
                        {
                            activity.participants.Remove(user);
                            Console.WriteLine($"Användaren {user.name} är nu avanmäld från {activity.activityDetails}");
                            break;
                        }
                    }
                }
            }
            else if (user.status == "Staff")
            {
                Console.WriteLine("Nedan listas de gruppaktiviteter som finns i schemat:");
                int count = 1;
                foreach (Activity activity in activities)
                {
                    Console.WriteLine($"{count}. {activity.activityDetails}, starttid: {activity.timeSlot.reservations[0].startTime}"); //for now an activity can only have one reservation, but beware of future changes...
                    count++;
                }
                Console.WriteLine("Ange den gruppaktivitet som ska tas bort:");
                int answerInt = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine($"Gruppaktiviteten {activities[answerInt].activityDetails} är nu borttaget från schemat");
                data1.StatusChangeEmail(activities[answerInt].activityID);
                activities.RemoveAt(answerInt);
            }
        }
        public void ModifyActivity(Database data, ReservingEntity owner)
        {
            Console.WriteLine("Enter the new activity details:");
            string updatedDetails = Console.ReadLine();

            Console.WriteLine("Enter the new participant limit:");
            int updatedLimit = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new time slot (YYYY-MM-DD HH:MM):");
            string updatedTime = Console.ReadLine();
            Console.WriteLine("Ange hur många minuter aktiviteten ska hålla på:");
            double durationMinutes = double.Parse(Console.ReadLine());

            Calendar updatedCalendar = new Calendar(DateTime.Parse(updatedTime), durationMinutes, owner);

            Console.WriteLine("Enter the new trainer's name: ");
            foreach (Trainer trainer in data.trainerObjects)
            {
                Console.WriteLine(trainer);
            }
            int updatedTrainer = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new space name:");
            foreach (Space space in data.spaceObjects)
            {
                Console.WriteLine(space);
            }
            int updatedSpace = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new equipment name:");
            foreach (Equipment equipment in data.equipmentObjects)
            {
                Console.WriteLine(equipment);
            }
            int updatedEquipment = int.Parse(Console.ReadLine());

            //this.activityDetails = updatedDetails;
            //this.participantLimit = updatedLimit;
            //this.timeSlot = updatedCalendar;
            //this.trainer = data.trainerObjects[updatedTrainer];
            //this.equipment = data.equipmentObjects[updatedEquipment]; ;
            //this.space = data.spaceObjects[updatedSpace]; ;
        }

        public void SignUp(ReservingEntity user, Database data1)
        {
            int x = 1;

            foreach (Activity activity in activities)
            {
                Console.WriteLine($"{x}, {activity.activityDetails}");
                x++;
            }

            Console.WriteLine("Which activity do you want to sign up for?");
            int answer = int.Parse(Console.ReadLine()) -1;

            if (user.status == "Member")
            {
                if (activities[answer].participants.Count < activities[answer].participantLimit && !activities[answer].participants.Contains(user))
                {
                    activities[answer].participants.Add(user);
                }
                else
                {
                    Console.WriteLine("\nParticipant limit reached for this activity.");
                }
            }

            else if (user.status == "Staff")
            {
                int y = 1;

                foreach (ReservingEntity staffUser in data1.userObjects)
                {
                    Console.WriteLine($"{y}, {staffUser.name}, {staffUser.phone}");
                    y++;
                }

                Console.WriteLine("Which activity do you want to sign up the member for?");
                int staffAnswer = int.Parse(Console.ReadLine()) - 1;

                if (activities[answer].participants.Count < activities[answer].participantLimit && !activities[answer].participants.Contains(user))
                {
                    activities[answer].participants.Add(data1.userObjects[staffAnswer]);
                }
                else
                {
                    Console.WriteLine("\nParticipant limit reached for this activity.");
                }
            }
        }

        public override string ToString()
        {
            return $"{activities}";
        }

    }
}

