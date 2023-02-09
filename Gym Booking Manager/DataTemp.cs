using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class DatabaseTemp
    {
        public List<Equipment> equipmentObjects = new List<Equipment>();
        public List<Space> spaceObjects = new List<Space>();
        public List<Trainer> trainerObjects = new List<Trainer>();
        public List<GroupSchedule> scheduleObjects = new List<GroupSchedule>();
        public List<ReservingEntity> userObjects = new List<ReservingEntity>();
    }
}
