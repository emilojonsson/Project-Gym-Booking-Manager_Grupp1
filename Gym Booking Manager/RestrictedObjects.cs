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
            for (int n = 0; n < data.restrictedObjects.Count; n++)
            {
                if (data.restrictedObjects[input - 1] == data.restrictedObjects[n])
                {
                    if (data.restrictedObjects[input - 1].space == data.restrictedObjects[n].space)
                    {
                        Console.WriteLine(data.restrictedObjects[input - 1]);
                        data.spaceObjects.Add(data.restrictedObjects[input - 1].space);
                        data.restrictedObjects.RemoveAt(input - 1);
                        foreach (Space a in data.spaceObjects)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    else if (data.restrictedObjects[input - 1].equipment == data.restrictedObjects[n].equipment)
                    {
                        Console.WriteLine(data.restrictedObjects[input - 1]);
                        data.restrictedObjects.RemoveAt(input - 1);
                    }
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
                data.restrictedObjects.Add(restricted);
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
                data.restrictedObjects.Add(restricted);
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
