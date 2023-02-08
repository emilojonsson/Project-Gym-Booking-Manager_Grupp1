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
    //
    internal class GroupSchedule
    {
        public List<GroupActivity> activities = new List<GroupActivity>();
        public void ViewSchedule(ReservingEntity user)
        {
            //Todo
            foreach(GroupActivity activity in activities)
            {
                Console.WriteLine(activity);
            }
        }
        public void AddActivity(ReservingEntity user, string activityDetails)
        {
            //Todo
        }
        public void UpdateActivity(ReservingEntity user, string activityDetails, string activityID)
        {
            //Todo
        }
        public void RemoveActivity(ReservingEntity user, string activityID)
        {
            //Todo
        }
        public override string ToString()
        {
            return $"{activities}";
        }

    }
}

