using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Gym_Booking_Manager
{
    internal class DatabaseTemp
    {
        //Create an instance of the DataBase, use for save/load space,equipment,trainer objects
        GymDatabaseContext gymDatabase = new GymDatabaseContext();

        //Static lists that we will use to load data from DataBase
        public List<Equipment> equipmentObjects = new List<Equipment>();

        public List<Space> spaceObjects = new List<Space>();

        public List<Trainer> trainerObjects = new List<Trainer>();

        public List<Activity> activities = new List<Activity>();

        public List<ReservingEntity> userObjects = new List<ReservingEntity>();

        public List<RestrictedObjects> restrictedObjects = new List<RestrictedObjects>();

        public GroupSchedule schedule = new GroupSchedule();

        //Add New list, restricter objects!!

        //Method to load current DataBase to program!
        public void LoadDataBase()
        {
            spaceObjects = gymDatabase.Read<Space>(); //Load DataBase to list spaceObjects
            trainerObjects = gymDatabase.Read<Trainer>(); //Load DataBase to list trainerObjects
            equipmentObjects = gymDatabase.Read<Equipment>(); //Load DataBase to list trainerObjects
            userObjects = LoadViaDataContractSerialization<List<ReservingEntity>>("user.xml");
            activities = LoadViaDataContractSerialization<List<Activity>>("activity.xml");
            foreach(Activity a in activities)
            {
                schedule.activities.Add(a);
            }
        }
        //Method to Save current objects to DataBase!
        public void SaveToDataBase()
        {
            SaveViaDataContractSerialization(activities, "activity.xml");
            SaveViaDataContractSerialization(userObjects, "user.xml");
            SaveViaDataContractSerialization(restrictedObjects, "restrictedObjects.xml");
            gymDatabase.Create(equipmentObjects);
            gymDatabase.Create(spaceObjects);
            gymDatabase.Create(trainerObjects);
        }
        static void SaveViaDataContractSerialization<T>(T serializableObject, string filePath)
        {
            //ToDo, make a method of this filepath insted of repeting in both methods!
            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string fpathUser = $"{storage}{sep}{filePath}";

            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };
            var writer = XmlWriter.Create(fpathUser, settings);
            serializer.WriteObject(writer, serializableObject);
            writer.Close();
        }
        static T LoadViaDataContractSerialization<T>(string filePath)
        {
            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string fpathUser = $"{storage}{sep}{filePath}";

            var fileStream = new FileStream(fpathUser, FileMode.Open);
            //Need a try catch block here so that program do not crash if file is empty!
            var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
            var serializer = new DataContractSerializer(typeof(T));
            T serializableObject = (T)serializer.ReadObject(reader, true);
            reader.Close();
            fileStream.Close();
            return serializableObject;
        }
        public void StatusChangeEmail(string a)
        {
            foreach(Activity b in schedule.activities)
            {
                if(b.activityID == a)
                {
                    using (StreamWriter sw = new StreamWriter("email.txt"))
                    for (int n = 0; n < b.participants.Count; n++)
                    {
                        sw.WriteLine($"{b.participants[n].email} your activity {b.activityID} has been canceled");
                    }
                }
            }
        }
        public void LogAlteration(string cause, string refrense)
        {
            DateTime time = DateTime.Now;
            using (StreamWriter sw = File.AppendText("test.txt"))
            {
                sw.WriteLine($"{time} : {cause} : {refrense}");
            }
        }
        public void ViewRestrictedObject()
        {
            int a = 1;
            foreach(RestrictedObjects rs in restrictedObjects)
            {
                Console.WriteLine($"{a}: {rs}");
                a++;
            }
        }
        //TODO, add a try/catch to stop method from crashing if worng input is given
        public void DropRestrictedObjects()
        {
            int input;
            ViewRestrictedObject();
            Console.WriteLine("Type number corresponding with object to drop restriction:> ");
            input = Convert.ToInt32(Console.ReadLine());
            for(int n = 0; n < restrictedObjects.Count; n++)
            {
                if (restrictedObjects[input-1] == restrictedObjects[n])
                {
                    if (restrictedObjects[input - 1].space == restrictedObjects[n].space)
                    {
                        Console.WriteLine(restrictedObjects[input - 1]);
                        spaceObjects.Add(restrictedObjects[input - 1].space);
                        restrictedObjects.RemoveAt(input - 1);
                        foreach (Space a in spaceObjects)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    else if (restrictedObjects[input - 1].equipment == restrictedObjects[n].equipment)
                    {
                        Console.WriteLine(restrictedObjects[input - 1]);
                        restrictedObjects.RemoveAt(input - 1);
                    }
                }
            }
        }
        //TODO, add a try/catch to stop method from crashing if worng input is given
        public void SetRestrictedStatus()
        {
            Console.WriteLine("Wich object do you want to restrict?");
            Console.Write("Space or Equipment:> ");
            string input = Console.ReadLine();
            if(input.ToLower() == "space")
            {
                int n = 1;
                foreach(Space a in spaceObjects)
                {
                    Console.WriteLine($"{n} : {a.name}");
                    n++;
                }
                Console.WriteLine("Type number corresponding with object to set restriction:> ");
                int number = Convert.ToInt32(Console.ReadLine());
                RestrictedObjects restricted = new RestrictedObjects(spaceObjects[number-1]);
                restrictedObjects.Add(restricted);
                spaceObjects.RemoveAt(number-1);
            }
            else if( input.ToLower() == "equipment")
            {
                int n = 1;
                foreach(Equipment a in equipmentObjects)
                {
                    Console.WriteLine($"{n} : {a.name}");
                    n++;
                }
                Console.WriteLine("Type number corresponding with object to set restriction:> ");
                int number = Convert.ToInt32(Console.ReadLine());
                RestrictedObjects restricted = new RestrictedObjects(equipmentObjects[number-1]);
                restrictedObjects.Add(restricted);
                equipmentObjects.RemoveAt(number-1);
            }
        }
        public void UserManagement(ReservingEntity user)
        {
            if(user.status == "Non-member")
            {
                //A payment method should be implemented here
                List<ReservingEntity> dayPass = new List<ReservingEntity>();
                Console.WriteLine("Enter name:> ");
                string name = ReturnString();
                ReservingEntity tempMember = new ReservingEntity(name, "333", "", "", "Member");
                dayPass.Add(tempMember);
            }
            else if(user.status == "Staff")
            {
                //Do Something
                Console.WriteLine("[1] Purchase daypass");
                Console.WriteLine("[2] Add new user");
                Console.WriteLine("[3] Remove user");
                string input = Console.ReadLine();
                if(input == "1")
                {
                    userObjects[3].status = "Member";
                }
                else if(input == "2")
                {
                    Console.WriteLine("Name: ");
                    string name = ReturnString();
                    Console.WriteLine("UniqueID: ");
                    string uniqueID = ReturnString();
                    Console.WriteLine("Phone: ");
                    string phone = ReturnString();
                    Console.WriteLine("Email: ");
                    string email = ReturnString();
                    Console.WriteLine("Status: ");
                    string status = ReturnString();
                    ReservingEntity newMember = new ReservingEntity(name,uniqueID,phone,email,status);
                    userObjects.Add(newMember);
                    
                }
                else if (input == "3")
                {
                    int n = 1;
                    foreach (ReservingEntity u in userObjects)
                    {
                        Console.WriteLine($"{n} : {u.name}");
                        n++;
                    }
                    Console.WriteLine("Type number corresponding with user to remove:> ");
                    int number = Convert.ToInt32(Console.ReadLine());
                    userObjects.RemoveAt(number - 1);
                }
            }
        }
        public string ReturnString()
        {
            Console.Write(":> ");
            string input = Console.ReadLine();
            return input;
        }
        public void ViewEquipments()
        {
            Console.WriteLine();
            foreach (Equipment allEquipments in equipmentObjects)
            {
                Console.WriteLine($"-- {allEquipments} --");
            }
            Console.WriteLine();
        }

        public void ViewSpaces()
        {
            Console.WriteLine();
            foreach (Space allSpaces in spaceObjects)
            {
                Console.WriteLine($"-- {allSpaces} --");
            }
            Console.WriteLine();
        }
        public void ViewTrainer()
        {
            Console.WriteLine();
            foreach (Trainer allTrainer in trainerObjects)
            {
                Console.WriteLine($"-- {allTrainer} --");

            }
            Console.WriteLine();
        }


        public void LoadTraining(ReservingEntity user, string userInput)
        {
            int a = 1;
            Console.WriteLine();
            Console.WriteLine("Do choice:");
            Console.WriteLine();
            if (userInput == "1")
            {
                foreach (Trainer allTrainer in trainerObjects)
                {
                    Console.WriteLine($"[{a++}] {allTrainer}");
                }
            }
            if (userInput == "2")
            {
                foreach (Equipment allEquipment in equipmentObjects)
                {
                    Console.WriteLine($"[{a++}] {allEquipment}");
                }
            }
            if (userInput == "3")
            {
                foreach (Space allSpaces in spaceObjects)
                {
                    Console.WriteLine($"[{a++}] {allSpaces}");
                }
            }
            int inputChoice = Int32.Parse(Console.ReadLine());
            Console.Write("Start time (YYYY-MM-DD HH:MM): ");
            DateTime timeSlot = DateTime.Parse(Console.ReadLine());
            Console.Write("Activity length in minutes: ");
            double durationMinutes = double.Parse(Console.ReadLine());
            string choosen = "";
            if (userInput == "1")
            {
                trainerObjects[inputChoice - 1].MakeReservation(user, timeSlot, durationMinutes);
                choosen = $"{trainerObjects[inputChoice - 1]}";
            }
            if (userInput == "2")
            {
                equipmentObjects[inputChoice - 1].MakeReservation(user, timeSlot, durationMinutes);
                choosen = $"{equipmentObjects[inputChoice - 1]}";
            }
            if (userInput == "3")
            {
                spaceObjects[inputChoice - 1].MakeReservation(user, timeSlot, durationMinutes);
                choosen = $"{equipmentObjects[inputChoice - 1]}";
            }
            Console.WriteLine();
            Console.WriteLine("-- Reservation registred! --");
            Console.WriteLine();
            Console.WriteLine($"User: {user.name}");
            Console.WriteLine($"Activity: {choosen}");
            Console.WriteLine($"Start time: {timeSlot}");
            Console.WriteLine($"Activity length: {durationMinutes} minutes");
        }
        public void MakeRes(ReservingEntity user)
        {
            Console.WriteLine();
            Console.WriteLine("Make reservation for:");
            Console.WriteLine("[1] Trainer");
            Console.WriteLine("[2] Equipment");
            Console.WriteLine("[3] Space");
            string userInput;
            userInput = Console.ReadLine();
            LoadTraining(user, userInput);
        }
        public void MakeResStaff()
        {
            int a = 1;
            foreach (ReservingEntity allUsers in userObjects)
            {
                Console.WriteLine($"[{a++}] {allUsers.name}");
            }
            Console.WriteLine();
            Console.WriteLine("Select user");
            int userselect = Int32.Parse(Console.ReadLine());
            ReservingEntity user = userObjects[userselect - 1];
            Console.WriteLine("Make reservation for:");
            Console.WriteLine("[1] Trainer");
            Console.WriteLine("[2] Equipment");
            Console.WriteLine("[3] Space");
            string userInput;
            userInput = Console.ReadLine();
            LoadTraining(user, userInput);

        }
    }
}
