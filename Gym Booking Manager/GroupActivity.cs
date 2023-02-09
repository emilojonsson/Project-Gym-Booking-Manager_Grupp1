using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    //
    internal class GroupActivity
    {
        public string activityID;
        public string activityDetails;
        public int participantLimit;
        public List<ReservingEntity> participants = new List<ReservingEntity>();
        public Calendar timeSlot;
        //public DateTime currentDateTime = DateTime.Now; // Test var
        public Trainer instructor;
        public Space space;
        public Equipment equipment;

        public GroupActivity() 
        {

        }
        public GroupActivity(string activityID, string activityDetails,int participantLimit, DateTime timeSlot, ReservingEntity owner, Space space, Trainer trainer, Equipment equipment)
        {
            this.activityID = activityID;
            this.activityDetails = activityDetails;
            this.participantLimit = participantLimit;
            this.timeSlot = new Calendar(timeSlot, owner);
            this.space = space;
            this.instructor = trainer;
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
        public void Modify()
        {
            //Todo
        }
        //public override string ToString()
        //{
        //    return $"{nameof(activityID)}:{activityID},{nameof(activityDetails)}:{activityDetails},{nameof(participantLimit)}:{participantLimit}"; //for now...
        //}
        public override string ToString()
        {
            return $"{nameof(activityDetails)}:{activityDetails},{nameof(instructor)}:{instructor},{nameof(participantLimit)}:{participantLimit}, Number of Participants: {participants.Count}"; 
        }

    }
}
