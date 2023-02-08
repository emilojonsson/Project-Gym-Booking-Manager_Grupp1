using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class GroupActivity
    {
        public string activityID;
        public string activityDetails;
        public int participantLimit = 20; //To be changed later...
        public List<ReservingEntity> participants;
        public Calendar timeSlot;
        public Trainer instructor;
        public Space space;
        public Equipment equipment;
        public GroupActivity(string activityID, string activityDetails)
        {
            this.activityID = activityID;
            this.activityDetails = activityDetails;
            this.timeSlot = new Calendar();
            this.space = new Space(Space.Category.Hall, "TestSpace");
        }
        public GroupActivity(string activityID, string activityDetails, Space space, Trainer trainer, Equipment equipment)
        {
            this.activityID = activityID;
            this.activityDetails = activityDetails;
            this.timeSlot = new Calendar();
            this.space = space;
            this.instructor = trainer;
            this.equipment = equipment;
        }

        public void SignUp(ReservingEntity user)
        {
            //Todo
        }
        public void Modify()
        {
            //Todo
        }
        public override string ToString()
        {
            return $"{nameof(activityID)}:{activityID},{nameof(activityDetails)}:{activityDetails},{nameof(participantLimit)}:{participantLimit}"; //for now...
        }

    }
}
