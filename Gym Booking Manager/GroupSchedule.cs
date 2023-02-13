﻿using System;
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

            foreach(Activity activity in activities)
            {
                if (activity.participants.Contains(user))
                {
                    Console.WriteLine(activity);    
                }
            }
        }
        public void AddActivity(ReservingEntity user, DatabaseTemp data)
        {
            Console.WriteLine("Ange information om aktiviteten:");
            string? activityDetails = Console.ReadLine();
            
            DateTime uniqueTimeToID = DateTime.Now;
            string activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm"); //choose this for now. it is at least unique
            
            Console.WriteLine("Ange hur många deltagare det maximalt kan vara på aktiviteten:");
            int participantLimit = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Ange vilken timme aktiviteten ska starta:");
            DateTime timeSlot = DateTime.Parse(Console.ReadLine());

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

            Console.WriteLine("Ange siffra för vilken utrustning som ska reserveras för aktiviteten:");
            foreach (Equipment equipment in data.equipmentObjects)
            {
                Console.WriteLine($"{data.equipmentObjects.IndexOf(equipment)} - {equipment.name}");
            }
            int addedEquipment = int.Parse(Console.ReadLine());

            activities.Add(new Activity(activityID, activityDetails, participantLimit, timeSlot, user, data.spaceObjects[addedSpace], data.trainerObjects[addedTrainer], data.equipmentObjects[addedEquipment]));
            data.spaceObjects[addedSpace].MakeReservation(user, timeSlot);
            data.trainerObjects[addedTrainer].MakeReservation(user, timeSlot);
            data.equipmentObjects[addedEquipment].MakeReservation(user, timeSlot);
        }

        // TODO - Ska kunna användas av member och staff
        // - ombokning/avbokning
        public void UpdateActivity(ReservingEntity user, string activityDetails, string activityID) 
        {
            foreach(Activity activity in activities)
            {
                if(user.status == "Member")
                {
                    if (activity.activityID == activityID)
                    {
                        //activity.participants.Update(user);
                        //Console.WriteLine(" ");
                        //break;
                    }
                    else
                    {
                        Console.WriteLine($"The Activity was not found in the schedule.");
                    }
                }

                else if(user.status == "Staff")
                {
                    // TODO
                }
            }
        }

        public void RemoveActivity(ReservingEntity user, string activityID)
        {
            //Todo
            foreach (Activity activity in activities)
            {
                if (user.status == "Member")
                {
                    //Do something
                    if(activity.activityID == activityID)
                    {
                        activity.participants.Remove(user);
                        Console.WriteLine($"The User {user.name} was removed from activity {activity.activityID}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The User was not found in the schedule.");
                    }
                }
                else if (user.status == "Staff")
                {
                    if (activity.activityID == activityID)
                    {
                        activities.Remove(activity);
                        Console.WriteLine($"The activity with ID {activityID} has been removed from the schedule.");
                        // Here we need a method so that the activity gets removed from the Database!!
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"The activity with ID {activityID} was not found in the schedule.");
                    }
                }
            }
        }
        public override string ToString()
        {
            return $"{activities}";
        }

    }
}

