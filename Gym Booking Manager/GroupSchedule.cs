using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class GroupSchedule
    {
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
            Console.ReadKey();
        }

        public void AddActivity(ReservingEntity owner, Database data)
        {
            Console.WriteLine("Do you want to add an activity from template (yes/no):");
            if (Console.ReadLine().ToLower() == "yes")
            {
                AddActivityFromTemplate(owner, data);
            }
            else
            {
                AddActivityManually(owner, data);
            }
        }
        public void AddActivityManually(ReservingEntity owner, Database data)
        {
            Console.WriteLine("Please add activity details (label of the session):");
            string? activityDetails = Console.ReadLine();

            DateTime uniqueTimeToID = DateTime.Now;
            string activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm"); 

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

                if (spaceBookingComplete == true && trainerBookingComplete == true && equipmentBookingComplete == true)
                {
                    //todo printa valen ovanför och bekräfta att användaren vill lägga in dessa
                    //if (valet true)
                    {
                        activities.Add(new Activity(activityID, activityDetails, participantLimit, timeSlot, durationMinutes, owner,
                            data.spaceObjects[addedSpace], data.trainerObjects[addedTrainer], data.equipmentObjects[addedEquipment]));
                        bookingNotComplete = false;
                    }
                }
            }
        }

        public void AddActivityFromTemplate(ReservingEntity owner, Database data)
        {
            Console.WriteLine("Select which template you want to use");
            int count = 1;
            foreach (Activity activity in data.templateActivityObjects)
            {
                Console.WriteLine($"{count}. {activity.activityDetails}");
                count++;
            }
            int template = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Add start date & time when the first occurrence of the activity will take place 'YYYY/MM/DD hh:mm':");
            DateTime firstDate = DateTime.Parse(Console.ReadLine());

            DateTime uniqueTimeToID = DateTime.Now;
            string activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm"); 

            Console.WriteLine("Please add for how many weeks this activity will repeat itself:");
            int repeats = int.Parse(Console.ReadLine());
            int indexSpaceObject = data.spaceObjects.IndexOf(data.templateActivityObjects[template].space);
            int indexTrainerObject = data.trainerObjects.IndexOf(data.templateActivityObjects[template].trainer);
            int indexEquipmentObject = data.equipmentObjects.IndexOf(data.templateActivityObjects[template].equipment);
            bool spaceBookingComplete = false;
            bool trainerBookingComplete = false;
            bool equipmentBookingComplete = false;

            for (int i = 0; i < repeats; i++)
            {
                spaceBookingComplete = data.spaceObjects[indexSpaceObject].calendar.BookReservation(owner, firstDate, data.templateActivityObjects[template].timeSlot.reservations[0].durationMinutes);
                trainerBookingComplete = data.trainerObjects[indexTrainerObject].calendar.BookReservation(owner, firstDate, data.templateActivityObjects[template].timeSlot.reservations[0].durationMinutes);
                equipmentBookingComplete = data.equipmentObjects[indexEquipmentObject].calendar.BookReservation(owner, firstDate, data.templateActivityObjects[template].timeSlot.reservations[0].durationMinutes);
                if (spaceBookingComplete == true && trainerBookingComplete == true && equipmentBookingComplete == true)
                {
                    data.schedule.activities.Add(new Activity(activityID, data.templateActivityObjects[template].activityDetails, data.templateActivityObjects[template].participantLimit,
                    firstDate, data.templateActivityObjects[template].participantLimit, owner,
                        data.templateActivityObjects[template].space, data.templateActivityObjects[template].trainer, data.templateActivityObjects[template].equipment));
                }
                else
                {
                    if (spaceBookingComplete)
                    {
                        data.spaceObjects[indexSpaceObject].calendar.reservations.RemoveAt(data.spaceObjects[indexSpaceObject].calendar.reservations.Count - 1);
                    }
                    if (trainerBookingComplete)
                    {
                        data.trainerObjects[indexTrainerObject].calendar.reservations.RemoveAt(data.trainerObjects[indexTrainerObject].calendar.reservations.Count - 1);
                    }
                    if (equipmentBookingComplete)
                    {
                        data.equipmentObjects[indexEquipmentObject].calendar.reservations.RemoveAt(data.equipmentObjects[indexEquipmentObject].calendar.reservations.Count - 1);
                    }
                    Console.WriteLine($"This means that no resvervations has been made on the date/time of: {firstDate}");
                }
                uniqueTimeToID.AddMinutes(1);
                activityID = uniqueTimeToID.ToString("yyyy/MM/dd HH:mm");
                firstDate = firstDate.AddDays(7);
            }
        }

        public void RemoveActivity(ReservingEntity user, Database data1, bool editInsted)
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
                    Console.WriteLine($"{count}. {activity.activityDetails}, start time: {activity.timeSlot.reservations[0].startTime}"); 
                    count++;
                }
                Console.WriteLine("Select the activity that you want to remove/edit:");
                int answerInt = int.Parse(Console.ReadLine()) - 1;

                if (editInsted)
                {
                    data1.StatusChangeEmail(activities[answerInt].activityID);
                    activities.RemoveAt(answerInt);
                    data1.schedule.AddActivityManually(user, data1);
                }
                else
                {
                    Console.WriteLine($"Group activity {activities[answerInt].activityDetails} is now removed from the schedule");
                    data1.StatusChangeEmail(activities[answerInt].activityID);
                    activities.RemoveAt(answerInt);
                }
            }
        }

        public void ModifyActivity(Database data, ReservingEntity user)
        {
            data.schedule.RemoveActivity(user, data, editInsted: true);
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
                else if (activities[answer].participants.Contains(user))
                {
                    Console.WriteLine("You have already signed up for this activity.");
                }
                else if (activities[answer].participants.Count >= activities[answer].participantLimit)
                {
                    Console.WriteLine("Participant limit reached for this activity.");
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

                Console.WriteLine("Which activity do you want to sign up for the member?");
                int staffAnswer = int.Parse(Console.ReadLine()) - 1;

                if (activities[answer].participants.Contains(data1.userObjects[staffAnswer]))
                {
                    Console.WriteLine("\nThe member is already signed up for this activity.");
                }
                else if (activities[answer].participants.Count >= activities[answer].participantLimit)
                {
                    Console.WriteLine("\nParticipant limit reached for this activity.");
                }
                else
                {
                    activities[answer].participants.Add(data1.userObjects[staffAnswer]);
                }
            }
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

            data.templateActivityObjects.Add(new Activity(activityID, activityDetails, participantLimit, timeSlot, durationMinutes, owner,
                data.spaceObjects[addedSpace], data.trainerObjects[addedTrainer], data.equipmentObjects[addedEquipment]));
        }

        public void ViewTemplate(Database data)
        {
            foreach (Activity template in data.templateActivityObjects)
            {
                Console.WriteLine($"{template.activityDetails}, {template.participantLimit} participants, {template.space}, {template.trainer}, {template.equipment}");
            }
        }

        public void DeleteTemplate(Database data)
        {
            int count = 1;
            foreach (Activity template in data.templateActivityObjects)
            {
                Console.WriteLine($"{count}.{template.activityDetails}, {template.participantLimit} participants, {template.space}, {template.trainer}, {template.equipment}");
                count++;
            }
            
            Console.WriteLine("Select the template that you want to remove:");
            int answerInt = int.Parse(Console.ReadLine()) - 1;
            data.templateActivityObjects.RemoveAt(answerInt);
            Console.WriteLine("Template has been removed");
        }

        public override string ToString()
        {
            return $"{activities}";
        }
    }
}

