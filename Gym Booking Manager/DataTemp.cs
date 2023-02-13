using System;
using System.Collections.Generic;
using System.Linq;
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

        //Method to load current DataBase to program!
        public void LoadDataBase()
        {
            spaceObjects = gymDatabase.Read<Space>(); //Load DataBase to list spaceObjects
            trainerObjects = gymDatabase.Read<Trainer>(); //Load DataBase to list trainerObjects
            equipmentObjects = gymDatabase.Read<Equipment>(); //Load DataBase to list trainerObjects
            userObjects = LoadViaDataContractSerialization<List<ReservingEntity>>("user.xml");
            activities = LoadViaDataContractSerialization<List<Activity>>("activity.xml");
       
        }
        //Method to Save current objects to DataBase!
        public void SaveToDataBase()
        {
            SaveViaDataContractSerialization(activities, "activity.xml");
            SaveViaDataContractSerialization(userObjects, "user.xml");
            gymDatabase.Create(equipmentObjects);
            gymDatabase.Create(spaceObjects);
            gymDatabase.Create(trainerObjects);

        }
        //Not used in current program, after testing these should be removed if they are not needed!
        public void LoadFile(string entity, string filePath)
        {
            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string fpathUser = $"{storage}{sep}{filePath}";

            using (StreamReader infile = new StreamReader(fpathUser))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    //Behövs normaliseras!!!! så man kan styra vilket object och lista som används!!! hmmmmmmm
                    //If-statment that deteminde witch object and list to use!!
                    if(entity == "user")
                    {
                        string[] attrs = line.Split('|');
                        ReservingEntity user = new ReservingEntity(attrs[0], attrs[1], attrs[2], attrs[3], attrs[4]);
                        userObjects.Add(user);
                    }
                    else
                    {
                        Console.WriteLine($"{entity} dose not exist in database");
                    }
                }
            }
        }
        //Not used in current program, after testing these should be removed if they are not needed!
        public void SaveListToDatabase(string entity, string filePath)
        {
            char sep = Path.DirectorySeparatorChar;
            string storage = $"GymDB{sep}storage";
            Directory.CreateDirectory(storage);
            string fpathUser = $"{storage}{sep}{filePath}";

            using (StreamWriter outfile = new StreamWriter(fpathUser))
            {
                //If-statment that deteminde witch object and list to use
                if (entity == "user")
                {
                    foreach (ReservingEntity user in userObjects)
                    {
                        if (user != null)
                            outfile.WriteLine($"{user.name}|{user.uniqueID}|{user.phone}|{user.email}|{user.status}"); //Spara dessa i strängar som avgör hur infomraion skrivs ut!!!
                    }
                }
                else
                {
                    Console.WriteLine($"{entity} dose not exist in database");
                }
            }
        }
        // New Methods to create a funktionall Database, use the DataContractSerializer class in .net to create readeble .xml files
        // All classes we want to save or load to new DataBase must have [DataContract] above class and all its fields a [DataMember]
        // With this method we do not need the DataBase method we inherited from former Team
        // The Save method rewrite the entire file, for simplisity we should in the begingen just use this as a method to be called on 
        // exiting the program and save all current lists!
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
    }
}
