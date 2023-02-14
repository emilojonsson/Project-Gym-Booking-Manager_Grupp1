using System;
using System.Collections.Generic;
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
        public void AddActivity(ReservingEntity owner, DatabaseTemp data)
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

        // TODO - Ska kunna användas av member och staff
        // - ombokning/avbokning
        // TODO - Är metoden klar eller ska den utökas med avbokning som RemoveActivity???
        public void UpdateActivity(ReservingEntity user, string activityDetails, string activityID, Activity updateActivity)
        {
            for (int i = 0; i < activities.Count; i++)
            {
                if(user.status == "Member")
                {
                    if (activities[i].activityID == activityID)
                    {
                        activities[i] = updateActivity;
                        Console.WriteLine("The activity has been updated.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The activity was not found in the schedule.");
                    }
                }

                else if(user.status == "Staff")
                {
                    if (activities[i].activityID == activityID)
                    {
                        activities[i] = updateActivity;
                        Console.WriteLine("The activity has been updated.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The activity was not found in the schedule.");
                    }
                }
            }
        }


        public void RemoveActivity(ReservingEntity user, string activityID)
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
                            Console.WriteLine($"Användaren {user.name} was removed from activity {activity.activityID}");
                            break;
                        }
                    }
                }

                //if (activity.activityID == activityID)
                //{
                //    activity.participants.Remove(user);
                //    Console.WriteLine($"The User {user.name} was removed from activity {activity.activityID}");
                //    break;
                //}
                //else
                //{
                //    Console.WriteLine($"The User was not found in the schedule.");
                //}
            }
            else if (user.status == "Staff")
            {
                //if (activity.activityID == activityID)
                //{
                //    activities.Remove(activity);
                //    Console.WriteLine($"The activity with ID {activityID} has been removed from the schedule.");
                //    // Here we need a method so that the activity gets removed from the Database!!
                //    break;
                //}
                //else
                //{
                //    Console.WriteLine($"The activity with ID {activityID} was not found in the schedule.");
                //}
            }
        }
        public override string ToString()
        {
            return $"{activities}";
        }

    }
}

