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
        public void ViewSchedule(Database data1, ReservingEntity user)
        {
            //Todo Here we need to think about a way for the owner (staff) to also be able to view his schedule. Right now we visualize the participants
            if (user.status == "Member")
            {
                Console.WriteLine($"Activities {user.name} are signed up:");
                foreach (Activity activity in activities)
                {
                    if (activity.participants.Contains(user))
                    {
                        Console.WriteLine(activity);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine($"Registrations {user.name} are signed up:");
                Console.WriteLine();
                foreach (Space space in data1.spaceObjects)
                {
                    space.ViewReservations(space, user);
                }
                foreach (Equipment equipment in data1.equipmentObjects)
                {
                    equipment.ViewReservations(equipment, user);
                }
                foreach (Trainer trainer in data1.trainerObjects)
                {
                    trainer.ViewReservations(trainer, user);
                }
                Console.WriteLine();
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
            Console.WriteLine("Do you want to add an activity from template (yes/no):");
            if (Console.ReadLine().ToLower() == "yes")
            {
                Console.WriteLine("Select which template you want to use");
                int count = 1;
                foreach (Activity activity in data.templateActivityObjects)
                {
                    Console.WriteLine($"{count}. {activity.activityDetails}");
                }
                int template = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine("Add start date and time when the first occurens of the activity will take place 'YYYY/MM/DD hh:mm':");
                DateTime firstDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Please add for how many weeks this activity will repeat itself:");
                int repeats = int.Parse(Console.ReadLine());
                for (int i = 0; i < repeats; i++)
                {
                    //data.templateActivityObjects[template] här ska jag adda data vidare (datum) och sedan kopiera över objektet till activitylistan 
                }
            }
            else
            {
                Console.WriteLine("Please add activity details (label of the session):");
                string? activityDetails = Console.ReadLine();
            
                DateTime uniqueTimeToID = DateTime.Now;
                string activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm"); //picked this for now. it is at least unique
            
                Console.WriteLine("What is the maximum amount of participants:");
                int participantLimit = int.Parse(Console.ReadLine());

                bool bookingNotComplete = true;
                int addedSpace = 0;
                int addedTrainer = 0;
                int addedEquipment = 0;
                while (bookingNotComplete)
                {
                    Console.WriteLine("At what date and time will the activity start 'YYYY/MM/DD hh:mm':");
                    DateTime timeSlot = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("How many minutes will the activity last:");
                    double durationMinutes = double.Parse(Console.ReadLine());

                    Console.WriteLine();
                    bool spaceBookingComplete = false;
                    while (!spaceBookingComplete)
                    {
                        Console.WriteLine("Please add which space that will be reserved for the activity:");
                        foreach (Space space in data.spaceObjects)
                        {
                            Console.WriteLine($"{data.spaceObjects.IndexOf(space) + 1} - {space.name}");
                        }
                        addedSpace = int.Parse(Console.ReadLine()) - 1;
                        spaceBookingComplete = data.spaceObjects[addedSpace].MakeReservation(owner, timeSlot, durationMinutes);
                    }

                    Console.WriteLine();
                    bool trainerBookingComplete = false;
                    while (!trainerBookingComplete)
                    {
                        Console.WriteLine("Please add which trainer that will be reserved for the activity:");
                        foreach (Trainer trainer in data.trainerObjects)
                        {
                            Console.WriteLine($"{data.trainerObjects.IndexOf(trainer) + 1} - {trainer.name}");
                        }
                        addedTrainer = int.Parse(Console.ReadLine()) - 1;
                        trainerBookingComplete = data.trainerObjects[addedTrainer].MakeReservation(owner, timeSlot, durationMinutes);
                    }

                    Console.WriteLine();
                    bool equipmentBookingComplete = false;
                    while (!equipmentBookingComplete)
                    {
                        Console.WriteLine("Please add which equipment that will be reserved for the activity:");
                        foreach (Equipment equipment in data.equipmentObjects)
                        {
                            Console.WriteLine($"{data.equipmentObjects.IndexOf(equipment) + 1} - {equipment.name}");
                        }
                        addedEquipment = int.Parse(Console.ReadLine()) - 1;
                        equipmentBookingComplete = data.equipmentObjects[addedEquipment].MakeReservation(owner, timeSlot, durationMinutes);
                    }
                    Console.WriteLine();

                    if (spaceBookingComplete == trainerBookingComplete == equipmentBookingComplete == true)
                    {
                        activities.Add(new Activity(activityID, activityDetails, participantLimit, timeSlot, durationMinutes, owner, 
                            data.spaceObjects[addedSpace], data.trainerObjects[addedTrainer], data.equipmentObjects[addedEquipment]));
                        bookingNotComplete = false;
                    }
                }
            }
        }

        public void RemoveActivity(ReservingEntity user, Database data1)
        {
            if (user.status == "Member")
            {
                int count = 1;
                Console.WriteLine("Below you can see all the group activities in the schedule that you are signed up to:");
                foreach (Activity activity in activities)
                {
                    foreach (ReservingEntity signUp in activity.participants)
                    {
                        if (user == signUp)
                        {
                            Console.WriteLine($"{count}. {activity.activityDetails},  {activity.timeSlot.reservations[0].startTime}"); //for now an activity can only have one reservation, but beware of future changes...
                            count++;
                        }
                    }
                }
                if (count > 1)
                {
                    Console.WriteLine("Select the activity that you want to withdraw your participation from:");
                    int answerInt = int.Parse(Console.ReadLine());

                    count = 0;
                    foreach (Activity activity in activities)
                    {
                        foreach (ReservingEntity signUp in activity.participants)
                        {
                            if (user == signUp)
                            {
                                count++;
                            }
                            if (count == answerInt)
                            {
                                activity.participants.Remove(user);
                                Console.WriteLine($"User {user.name} will not any longer participate in {activity.activityDetails}");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{user.name} is not signed up to any group activities");
                }
            }
            else if (user.status == "Staff")
            {
                Console.WriteLine("Below you can see all the group activities in the schedule:");
                int count = 1;
                foreach (Activity activity in activities)
                {
                    Console.WriteLine($"{count}. {activity.activityDetails}, start time: {activity.timeSlot.reservations[0].startTime}"); //for now an activity can only have one reservation, but beware of future changes...
                    count++;
                }
                Console.WriteLine("Select the activity that you want to remove:");
                int answerInt = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine($"Group activity {activities[answerInt].activityDetails} is now removed from the schedule");
                data1.StatusChangeEmail(activities[answerInt].activityID);
                activities.RemoveAt(answerInt);
            }
        }

        public void ModifyActivity(Database data, ReservingEntity user)
        {
            Console.WriteLine("Here is a list of all activities");
            int x = 1;
            foreach (Activity activity in activities)
            {
                Console.WriteLine($"{x}. {activity.activityDetails}, time: {activity.timeSlot.reservations[0].startTime}");
                x++;
            }

            Console.WriteLine("Choose which one you want to modify: ");
            int xAnswer = int.Parse(Console.ReadLine()) - 1;           

            if (xAnswer < 0 || xAnswer >= activities.Count)
            {
                Console.WriteLine("Error");
                return;
            }

            Activity selectedActivity = activities[xAnswer];

            Console.WriteLine("Enter the new activity details:");
            string updatedDetails = Console.ReadLine();

            Console.WriteLine("Enter the new participant limit:");
            int updatedLimit = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new time slot (YYYY-MM-DD HH:MM):");
            string updatedTime = Console.ReadLine();

            Console.WriteLine("How long will the activity be, write in minutes:");
            double durationMinutes = double.Parse(Console.ReadLine());

            Calendar updatedCalendar = new Calendar(DateTime.Parse(updatedTime), durationMinutes, user);

            Console.WriteLine("Enter the new trainer's name: ");
            foreach (Trainer trainer in data.trainerObjects)
            {
                Console.WriteLine($"{data.trainerObjects.IndexOf(trainer)} - {trainer.name}");
            }
            int updatedTrainer = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new space name:");
            foreach (Space space in data.spaceObjects)
            {
                Console.WriteLine($"{data.spaceObjects.IndexOf(space)} - {space.name}");
            }
            int updatedSpace = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new equipment name:");
            foreach (Equipment equipment in data.equipmentObjects)
            {
                Console.WriteLine($"{data.equipmentObjects.IndexOf(equipment)} - {equipment.name}");
            }
            int updatedEquipment = int.Parse(Console.ReadLine());

            selectedActivity.activityDetails = updatedDetails;
            selectedActivity.participantLimit = updatedLimit;
            selectedActivity.timeSlot = updatedCalendar;
            selectedActivity.trainer = data.trainerObjects[updatedTrainer];
            selectedActivity.space = data.spaceObjects[updatedSpace];
            selectedActivity.equipment = data.equipmentObjects[updatedEquipment];

            // Prompt the user to confirm that the new data is correct
            Console.WriteLine("Here is the updated activity:");
            Console.WriteLine($"Activity Details: {selectedActivity.activityDetails}");
            Console.WriteLine($"Participant Limit: {selectedActivity.participantLimit}");
            Console.WriteLine($"Time Slot: {selectedActivity.timeSlot}");
            Console.WriteLine($"Trainer: {selectedActivity.trainer}");
            Console.WriteLine($"Space: {selectedActivity.space}");
            Console.WriteLine($"Equipment: {selectedActivity.equipment}");

            Console.WriteLine("Is this data correct? (Y/N)");
            string answer = Console.ReadLine();

            if (answer.ToLower() != "y")
            {
                Console.WriteLine("Activity was not updated.");
                return;
            }
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
        public void AddTemplateActivity(ReservingEntity owner, Database data)
        {
            DateTime timeSlot = new DateTime(1, 1, 1);

            Console.WriteLine("Please add activity details (label of the session):");
            string? activityDetails = Console.ReadLine();

            DateTime uniqueTimeToID = DateTime.Now;
            string activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm"); //picked this for now. it is at least unique

            Console.WriteLine("What is the maximum amount of participants:");
            int participantLimit = int.Parse(Console.ReadLine());

            Console.WriteLine("How many minutes will the activity last:");
            double durationMinutes = double.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("Please add which space that will be reserved for the activity:");
            foreach (Space space in data.spaceObjects)
            {
                Console.WriteLine($"{data.spaceObjects.IndexOf(space) + 1} - {space.name}");
            }
            int addedSpace = int.Parse(Console.ReadLine()) - 1;
            bool spaceBookingComplete = data.spaceObjects[addedSpace].MakeReservation(owner, timeSlot, durationMinutes);

            Console.WriteLine();
            Console.WriteLine("Please add which trainer that will be reserved for the activity:");
            foreach (Trainer trainer in data.trainerObjects)
            {
                Console.WriteLine($"{data.trainerObjects.IndexOf(trainer) + 1} - {trainer.name}");
            }
            int addedTrainer = int.Parse(Console.ReadLine()) - 1;
            bool trainerBookingComplete = data.trainerObjects[addedTrainer].MakeReservation(owner, timeSlot, durationMinutes);

            Console.WriteLine();
            Console.WriteLine("Please add which equipment that will be reserved for the activity:");
            foreach (Equipment equipment in data.equipmentObjects)
            {
                Console.WriteLine($"{data.equipmentObjects.IndexOf(equipment) + 1} - {equipment.name}");
            }
            int addedEquipment = int.Parse(Console.ReadLine()) - 1;
            bool equipmentBookingComplete = data.equipmentObjects[addedEquipment].MakeReservation(owner, timeSlot, durationMinutes);
            Console.WriteLine();

            activities.Add(new Activity(activityID, activityDetails, participantLimit, timeSlot, durationMinutes, owner,
                data.spaceObjects[addedSpace], data.trainerObjects[addedTrainer], data.equipmentObjects[addedEquipment]));
        }

    }
}

