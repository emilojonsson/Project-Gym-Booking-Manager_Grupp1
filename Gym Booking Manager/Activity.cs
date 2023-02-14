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
        public Calendar timeSlot; //this one could be renamed to startTime
        [DataMember]
        public Trainer trainer;
        [DataMember]
        public Space space;
        [DataMember]
        public Equipment equipment;

        public Activity()
        {

        }

        public Activity(string activityID, string activityDetails, int participantLimit, DateTime startTime, double durationMinutes, ReservingEntity owner, Space space, Trainer trainer, Equipment equipment)
        {
            this.activityID = activityID;
            this.activityDetails = activityDetails;
            this.participantLimit = participantLimit;
            this.timeSlot = new Calendar(startTime, durationMinutes, owner);
            this.space = space;
            this.trainer = trainer;
            this.equipment = equipment;
        }


        public override string ToString()
        {
            return $"{nameof(activityDetails)}:{activityDetails},{nameof(trainer)}:{trainer},{nameof(participantLimit)}:{participantLimit}, Number of Participants: {participants.Count}"; 
        }
    }
}
