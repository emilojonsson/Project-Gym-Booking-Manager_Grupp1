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
        public string activityID { get; set; }
        [DataMember]
        public string activityDetails { get; set; }
        [DataMember]
        public int participantLimit { get; set; }
        [DataMember]
        public List<ReservingEntity> participants { get; set; } = new List<ReservingEntity>();
        [DataMember]
        public Calendar timeSlot { get; set; } //this one could be renamed to startTime
        [DataMember]
        public Trainer trainer { get; set; }
        [DataMember]
        public Space space { get; set; }
        [DataMember]
        public Equipment equipment { get; set; }

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
            return $"Activity: {activityDetails}\nTrainer: {trainer}\nSpace: {space}\nEqupment: {equipment}\nParticipantLimit: {participantLimit}\nStart time: {timeSlot.reservations[0].startTime}\nDurration minutes: {timeSlot.reservations[0].durationMinutes}\nNumber of Participants: {participants.Count}\n"; 
        }
    }
}
