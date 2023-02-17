using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager
{
    [DataContract]
    internal class RestrictedObjects
    {
        [DataMember]
        public Space space;
        [DataMember]
        public Equipment equipment;
        public RestrictedObjects() { }
        public RestrictedObjects(Space space)
        {
            this.space = space;
        }
        public RestrictedObjects(Equipment equipment) 
        {
            this.equipment = equipment;
        }
        public void DropRestrictedObjects(Database data)
        {
            int input;
            data.ViewRestrictedObject();
            Console.WriteLine("Type number corresponding with object to drop restriction:> ");
            input = Convert.ToInt32(Console.ReadLine());
            for (int n = 0; n < data.restrictedList.Count; n++)
            {
                if (data.restrictedList[input - 1] == data.restrictedList[n])
                {
                    if (data.restrictedList[input -1].equipment == null)
                    {
                        Console.WriteLine(data.restrictedList[input - 1]);
                        data.LogAlteration("DropRestriction", data.restrictedList[input - 1].space.name);
                        data.spaceObjects.Add(data.restrictedList[input - 1].space);
                        data.restrictedList.RemoveAt(input - 1);
                    }
                    else if (data.restrictedList[input - 1].space == null)
                    {
                        Console.WriteLine(data.restrictedList[input - 1]);
                        data.LogAlteration("DropRestriction", data.restrictedList[input - 1].equipment.name);
                        data.equipmentObjects.Add(data.restrictedList[input -1].equipment); 
                        data.restrictedList.RemoveAt(input - 1);
                    }
                    else
                        Console.WriteLine("Invalid Input");
                }
            }
        }
        public void SetRestrictedStatus(Database data)
        {
            Console.WriteLine("Wich object do you want to restrict?");
            Console.Write("Space or Equipment:> ");
            string input = Console.ReadLine();
            if (input.ToLower() == "space")
            {
                int n = 1;
                foreach (Space a in data.spaceObjects)
                {
                    Console.WriteLine($"{n} : {a.name}");
                    n++;
                }
                Console.WriteLine("Type number corresponding with object to set restriction:> ");
                int number = Convert.ToInt32(Console.ReadLine());
                RestrictedObjects restricted = new RestrictedObjects(data.spaceObjects[number - 1]);
                data.LogAlteration("Set Restrictions", data.spaceObjects[number - 1].name);
                data.restrictedList.Add(restricted);
                data.spaceObjects.RemoveAt(number - 1);
            }
            else if (input.ToLower() == "equipment")
            {
                int n = 1;
                foreach (Equipment a in data.equipmentObjects)
                {
                    Console.WriteLine($"{n} : {a.name}");
                    n++;
                }
                Console.WriteLine("Type number corresponding with object to set restriction:> ");
                int number = Convert.ToInt32(Console.ReadLine());
                RestrictedObjects restricted = new RestrictedObjects(data.equipmentObjects[number - 1]);
                data.LogAlteration("Set Restrictions", data.equipmentObjects[number - 1].name);
                data.restrictedList.Add(restricted);
                data.equipmentObjects.RemoveAt(number - 1);
            }
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
