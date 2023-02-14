using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    internal class RestrictedObjects
    {
        public Space space;
        public Equipment equipment;
        public RestrictedObjects(Space space)
        {
            this.space = space;
        }
        public RestrictedObjects(Equipment equipment) 
        {
            this.equipment = equipment;
        }
        public override string ToString()
        {
            if (equipment == null)
            {
                return $"Space = {space.name}";
            }
            else if (space == null)
            {
                return $"Equipment = {equipment.name}";
            }
            else
                return null;
        }
    }
}
