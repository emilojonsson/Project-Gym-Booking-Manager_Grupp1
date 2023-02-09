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
        public void AddActivity(ReservingEntity user)
        {
            Console.WriteLine("Ange information om aktiviteten:");
            string? activityDetails = Console.ReadLine();
            DateTime uniqueTimeToID = DateTime.Now;
            string activityID = uniqueTimeToID.ToString("yyyy/MM//dd HH:mm");
            Console.WriteLine("Ange hur många deltagare det maximalt kan vara på aktiviteten:");
            int participantsLimit = int.Parse(Console.ReadLine());
            Console.WriteLine("Ange vilken timme aktiviteten ska starta:");
            DateTime startTime = DateTime.Parse(Console.ReadLine());
            Calendar changethisnameofvariabel = new Calendar(startTime, user);
            //Console.WriteLine("Ange vilken tränarroll du kommer att ha till denna aktivitet:");
            //Trainer traintest = 

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

