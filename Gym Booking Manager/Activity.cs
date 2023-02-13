using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    [DataContract]
    internal class Activity
    {
        [DataMember]
        public string activityID;
        [DataMember]
        public string activityDetails;
        [DataMember]
        public int participantLimit;
        [DataMember]
        public List<ReservingEntity> participants = new List<ReservingEntity>();
        [DataMember]
        public Calendar timeSlot;
        //public DateTime currentDateTime = DateTime.Now; // Test var
        [DataMember]
        public Trainer trainer;
        [DataMember]
        public Space space;
        [DataMember]
        public Equipment equipment;
        public Activity()
        {

        }
        public Activity(string activityID, string activityDetails, int participantLimit, DateTime timeSlot, ReservingEntity owner, Space space, Trainer trainer, Equipment equipment)
        {
            this.activityID = activityID;
            this.activityDetails = activityDetails;
            this.participantLimit = participantLimit;
            this.timeSlot = new Calendar(timeSlot, owner);
            this.space = space;
            this.trainer = trainer;
            this.equipment = equipment;
        }

        public void SignUp(ReservingEntity user)
        {
            //Todo
            if (participants.Count < participantLimit)
            {
                participants.Add(user);
            }
            else
            {
                Console.WriteLine("Participant limit reached for this activity.");
            }
        }

        public void ModifyActivity(DatabaseTemp data)
        {
            Console.WriteLine("Enter the new activity details:");
            string updatedDetails = Console.ReadLine();

            Console.WriteLine("Enter the new participant limit:");
            int updatedLimit = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the new time slot (YYYY-MM-DD HH:MM):");
            string updatedTime = Console.ReadLine();

            Calendar updatedCalendar = new Calendar(DateTime.Parse(updatedTime));

            Console.WriteLine("Enter the new trainer's name: ");
            foreach(Trainer trainer in data.trainerObjects)
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

            this.activityDetails = updatedDetails;
            this.participantLimit = updatedLimit;
            this.timeSlot = updatedCalendar;
            this.trainer = data.trainerObjects[updatedTrainer];
            this.equipment = data.equipmentObjects[updatedEquipment]; ;
            this.space = data.spaceObjects[updatedSpace]; ;
        }


        //public override string ToString()
        //{
        //    return $"{nameof(activityID)}:{activityID},{nameof(activityDetails)}:{activityDetails},{nameof(participantLimit)}:{participantLimit}"; //for now...
        //}

        public override string ToString()
        {
            return $"{nameof(activityDetails)}:{activityDetails},{nameof(trainer)}:{trainer},{nameof(participantLimit)}:{participantLimit}, Number of Participants: {participants.Count}"; 
        }

    }
}
