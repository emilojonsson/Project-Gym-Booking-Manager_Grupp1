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
        public RestrictedObjects restricted = new RestrictedObjects();
        public ReservingEntity user = new ReservingEntity();
        public List<Activity> templateActivityObjects = new List<Activity>();

        //Load current DataBase to program!
        public void LoadDataBase()
        {
            equipmentObjects = LoadViaDataContractSerialization<List<Equipment>>("equipmentObjects.xml");
            spaceObjects = LoadViaDataContractSerialization<List<Space>>("spaceObjects.xml");
            trainerObjects = gymDatabase.Read<Trainer>(); //Load DataBase to list trainerObjects
            userObjects = LoadViaDataContractSerialization<List<ReservingEntity>>("user.xml");
            activities = LoadViaDataContractSerialization<List<Activity>>("activity.xml");
            if (activities != null)
            {
                foreach (Activity activity in activities)
                {
                    schedule.activities.Add(activity);
                }
            }
            else
                Console.WriteLine("No activitis were loaded!");
        }
        //Save current objectsLists to DataBase!
        public void SaveToDataBase()
        {
            if(activities == null)
            {
                Console.WriteLine("No activitys were saved to Database");
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
            SaveViaDataContractSerialization(restrictedObjects, "restrictedObjects.xml");
            SaveViaDataContractSerialization(spaceObjects, "spaceObjects.xml");
            SaveViaDataContractSerialization(trainerObjects, "trainerObjects.xml");
            SaveViaDataContractSerialization(equipmentObjects, "equipmentObjects.xml");
           
        }

        public static string FilePath(string fileName)
        {
            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string fpathUser = $"{storage}{sep}{fileName}";
            return fpathUser;
        }

        static void SaveViaDataContractSerialization<T>(T serializableObject, string fileName)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                Console.WriteLine($"{fpathFile} {ex.Message}");
                return default(T);
            }
        }

        public void StatusChangeEmail(string a)
        {
            string filePath = FilePath("email.txt");
            foreach(Activity b in schedule.activities)
            {
                if(b.activityID == a)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    for (int n = 0; n < b.participants.Count; n++)
                    {
                        sw.WriteLine($"{b.participants[n].email} your activity {b.activityDetails} has been canceled");
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
                sw.WriteLine($"{time} : {cause} : {refrense}");
            }
        }

        public void ViewRestrictedObject()
        {
            int a = 1;
            foreach (RestrictedObjects rs in restrictedObjects)
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

        public void LoadTraining(ReservingEntity user, string choice)
        {
            string choicemod = "";
            if (choice == "1")
            {
                choicemod = "Trainer";
            }
            if (choice == "2")
            {
                choicemod = "Consultation";
            }

            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string file = $"{storage}{sep}trainer.csv";
            Console.WriteLine();
            Console.WriteLine("Do choice:");
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file))
            {
                List<string> sample = new List<string>();
                string line;
                int b = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] field = line.Split(':');
                    if (field[1] == $"{choicemod},name")
                    {
                        Console.WriteLine($"[{b++}] {field[2]}");
                    }
                    sample.Add(field[2]);

                }
                string userInput = Console.ReadLine();
                Console.WriteLine("Activity start time:");
                DateTime timeSlot = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Activity time in minutes");
                double durationMinutes = double.Parse(Console.ReadLine());
                switch (userInput)
                {
                    case "1":
                        trainerObjects[0].MakeReservation(user, timeSlot, durationMinutes);
                        Console.WriteLine();
                        Console.WriteLine("--Reservation registred!--");
                        //Console.WriteLine($"test {sample[0]}");
                        break;
                    case "2":
                        trainerObjects[1].MakeReservation(user, timeSlot, durationMinutes);
                        Console.WriteLine();
                        Console.WriteLine("--Reservation registred!--");
                        break;
                }
            }
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
            switch (userInput)
            {
                case "1":
                    int a = 1;
                    Console.WriteLine("Choose:");
                    Array trainerEnums = Enum.GetValues(typeof(Trainer.Category));
                    foreach (Object i in trainerEnums)
                    {
                        Console.WriteLine($"[{a++}] {i}");
                    }
                    userInput = Console.ReadLine();
                    switch (userInput)
                    {
                        case "1":
                            LoadTraining(user, userInput);
                            break;
                        case "2":
                            LoadTraining(user, userInput);
                            break;
                    }
                    break;
            }
        }
    }
}
