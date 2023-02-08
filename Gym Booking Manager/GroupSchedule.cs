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
        public List<GroupActivity> activities;
        public void ViewSchedule(ReservingEntity user)
        {
            //Todo
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

    }
}

