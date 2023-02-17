using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.ExceptionServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Gym_Booking_Manager
{
    internal class Database
    {
        public List<Equipment> equipmentObjects { get; set; } = new List<Equipment>();
        public List<Space> spaceObjects { get; set; } = new List<Space>();
        public List<Trainer> trainerObjects { get; set; } = new List<Trainer>();
        public List<Activity> activities { get; set; } = new List<Activity>();
        public List<ReservingEntity> userObjects { get; set; } = new List<ReservingEntity>();
        public List<RestrictedObjects> restrictedObjects { get; set; } = new List<RestrictedObjects>();
        public List<RestrictedObjects> restrictedList { get; set; } = new List<RestrictedObjects>();

        public GroupSchedule schedule { get; set; } = new GroupSchedule();
        public RestrictedObjects restricted { get; set; } = new RestrictedObjects();
        public ReservingEntity user { get; set; } = new ReservingEntity();
        public List<Activity> templateActivityObjects { get; set; } = new List<Activity>();
        //public void Test()
        //{
        //    SaveViaDataContractSerialization(restrictedList, "restrictedList.xml");
        //}

        public void LoadDataBase()
        {
            equipmentObjects = LoadViaDataContractSerialization<List<Equipment>>("equipmentObjects.xml");
            spaceObjects = LoadViaDataContractSerialization<List<Space>>("spaceObjects.xml");
            trainerObjects = LoadViaDataContractSerialization<List<Trainer>>("trainerObjects.xml");
            userObjects = LoadViaDataContractSerialization<List<ReservingEntity>>("user.xml");
            activities = LoadViaDataContractSerialization<List<Activity>>("activity.xml");
            restrictedList = LoadViaDataContractSerialization<List<RestrictedObjects>>("restrictedList.xml");
            templateActivityObjects = LoadViaDataContractSerialization<List<Activity>>("templateActivities.xml");
            if (activities != null)
            {
                foreach (Activity activity in activities)
                {
                    schedule.activities.Add(activity);
                }
            }
            else
                Console.WriteLine("No activities were loaded!");
        }

        public void SaveToDataBase()
        {
            if (activities == null)
            {
                Console.WriteLine("No activities were saved to Database");
            }
            else
            {
                activities.Clear();
                foreach (Activity activity in schedule.activities)
                {
                    activities.Add(activity);
                }
                SaveViaDataContractSerialization(activities, "activity.xml");
            }
            SaveViaDataContractSerialization(userObjects, "user.xml");
            SaveViaDataContractSerialization(restrictedList, "restrictedList.xml");
            SaveViaDataContractSerialization(spaceObjects, "spaceObjects.xml");
            SaveViaDataContractSerialization(trainerObjects, "trainerObjects.xml");
            SaveViaDataContractSerialization(equipmentObjects, "equipmentObjects.xml");
            SaveViaDataContractSerialization(templateActivityObjects, "templateActivities.xml");
        }

        public static string FilePath(string fileName)
        {
            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string fpathUser = $"{storage}{sep}{fileName}";
            return fpathUser;
        }

        public static void SaveViaDataContractSerialization<T>(T serializableObject, string fileName)
        {
            string fpathUser = FilePath(fileName);
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine($"No data was saved to {fileName} {ex.Message}");
            }
        }

        public static T LoadViaDataContractSerialization<T>(string fileName)
        {
            string fpathFile = FilePath(fileName);
            try
            {
                var fileStream = new FileStream(fpathFile, FileMode.Open);
                var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
                var serializer = new DataContractSerializer(typeof(T));
                T serializableObject = (T)serializer.ReadObject(reader, true);
                reader.Close();
                fileStream.Close();
                return serializableObject;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{fpathFile} {ex.Message}");
                return default;
            }
        }

        public void StatusChangeEmail(string a)
        {
            string filePath = FilePath("email.txt");
            foreach (Activity b in schedule.activities)
            {
                if (b.activityID == a)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    for (int n = 0; n < b.participants.Count; n++)
                    {
                        sw.WriteLine($"{b.participants[n].email} your activity {b.activityDetails} has been cancelled");
                    }
                }
            }
        }

        public void LogAlteration(string cause, string refrense)
        {
            string filePath = FilePath("ActivityLog.txt");
            DateTime time = DateTime.Now;
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine($"{time} : {cause} : {refrense }");
            }
        }
        public void ViewLogfile()
        {
            string filePath = FilePath("ActivityLog.txt");
            using(StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                Console.ReadKey();
                sr.Close();
            }
            
        }
        public void ViewRestrictedObject()
        {
            int a = 1;
            foreach (RestrictedObjects rs in restrictedList)
            {
                Console.WriteLine($"{a}: {rs}");
                a++;
            }
        }

        public void ViewEquipments()
        {
            Console.WriteLine();
            foreach (Equipment allEquipments in equipmentObjects)
            {
                Console.WriteLine($"-- {allEquipments} --");
            }
            Console.WriteLine();
            Console.ReadKey();

        }

        public void ViewSpaces()
        {
            Console.WriteLine();
            foreach (Space allSpaces in spaceObjects)
            {
                Console.WriteLine($"-- {allSpaces} --");
            }
            Console.WriteLine();
            Console.ReadKey();

        }

        public void ViewTrainer()
        {
            Console.WriteLine();
            foreach (Trainer allTrainer in trainerObjects)
            {
                Console.WriteLine($"-- {allTrainer} --");

            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public void LoadTraining(ReservingEntity user, string userInput)
        {
            int a = 1;
            Console.WriteLine();
            Console.WriteLine("Please select:");
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
            int inputChoice = int.Parse(Console.ReadLine());
            Console.Write("Start time (YYYY-MM-DD HH:MM): ");
            DateTime timeSlot = DateTime.Parse(Console.ReadLine());
            Console.Write("Activity length in minutes: ");
            double durationMinutes = double.Parse(Console.ReadLine());
            string choosen = "";
            if (userInput == "1")
            {
                trainerObjects[inputChoice - 1].MakeReservation(user, timeSlot, durationMinutes);
                choosen = $"{trainerObjects[inputChoice - 1]}";
                LogAlteration("Reservations", trainerObjects[inputChoice - 1].name + timeSlot + durationMinutes);
            }
            if (userInput == "2")
            {
                equipmentObjects[inputChoice - 1].MakeReservation(user, timeSlot, durationMinutes);
                choosen = $"{equipmentObjects[inputChoice - 1]}";
                LogAlteration("Reservations", equipmentObjects[inputChoice - 1].name + timeSlot + durationMinutes);
            }
            if (userInput == "3")
            {
                spaceObjects[inputChoice - 1].MakeReservation(user, timeSlot, durationMinutes);
                choosen = $"{spaceObjects[inputChoice - 1]}";
                LogAlteration("Reservations", spaceObjects[inputChoice -1].name + timeSlot + durationMinutes);
            }
            Console.WriteLine();

            Console.WriteLine("-- Reservation registered! --");
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
            int userselect = int.Parse(Console.ReadLine());
            ReservingEntity user = userObjects[userselect - 1];
            Console.WriteLine("Make reservation for:");
            Console.WriteLine("[1] Trainer");
            Console.WriteLine("[2] Equipment");
            Console.WriteLine("[3] Space");
            string userInput;
            userInput = Console.ReadLine();
            LoadTraining(user, userInput);
        }

        public void ViewReservations(ReservingEntity user)
        {
            Console.WriteLine("All members' reservations:");
            Console.WriteLine();
            foreach (Space space in spaceObjects)
            {
                space.ViewReservations(space, user);
            }
            foreach (Equipment equipment in equipmentObjects)
            {
                equipment.ViewReservations(equipment, user);
            }
            foreach (Trainer trainer in trainerObjects)
            {
                trainer.ViewReservations(trainer, user);
            }
            Console.ReadKey();
        }
        public void CancelReservation(ReservingEntity user)
        {
            if (user.status == "Member")
            {
                Console.WriteLine("Type object you would like to cancel");
                string input = Console.ReadLine();
                if (input.ToLower() == "equipment")
                {
                    foreach (Equipment equipment in equipmentObjects)
                    {
                        equipment.CancelReservation(user, equipment);
                    }
                }
                else if (input.ToLower() == "space")
                {
                    foreach (Space space in spaceObjects)
                    {
                        space.CancelReservation(user, space);
                    }
                }
                else if (input.ToLower() == "trainer")
                {
                    foreach (Trainer trainer in trainerObjects)
                    {
                        trainer.CancelReservation(user, trainer);
                    }
                }
                else
                    Console.WriteLine("Invalid Input");
            }
            if (user.status == "Staff")
            {
                foreach (ReservingEntity u in userObjects)
                {
                    Console.WriteLine($"{u.uniqueID} : {u.name}");
                }
                Console.WriteLine("Enter userID");
                string input = Console.ReadLine();
                foreach (ReservingEntity choosenUser in userObjects)
                {
                    if (input == choosenUser.uniqueID)
                    {
                        Console.WriteLine("Type object you would like to cancel");
                        string secondInput = Console.ReadLine();
                        if (secondInput.ToLower() == "equipment")
                        {
                            foreach (Equipment equipment in equipmentObjects)
                            {
                                equipment.CancelReservation(user, equipment);
                            }
                        }
                        else if (secondInput.ToLower() == "space")
                        {
                            foreach (Space space in spaceObjects)
                            {
                                space.CancelReservation(user, space);
                            }
                        }
                        else if (secondInput.ToLower() == "trainer")
                        {
                            foreach (Trainer trainer in trainerObjects)
                            {
                                trainer.CancelReservation(user, trainer);
                            }
                        }
                        else
                            Console.WriteLine("Invalid Input");
                    }
                }
            }
        }
    }
}
