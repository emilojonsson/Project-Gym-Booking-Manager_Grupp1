using Gym_Booking_Manager;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("Tests")]
#endif
namespace Gym_Booking_Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Space space1 = new Space(Space.Category.Hall, "Jympaområdet");
            Space space2 = new Space(Space.Category.Hall, "Uppvärmingsdelen");
            Space space3 = new Space(Space.Category.Lane, "Tennisområdet");
            Space space4 = new Space(Space.Category.Lane, "Paddelytan");
            Space space5 = new Space(Space.Category.Studio, "Dansscenen");
            Space space6 = new Space(Space.Category.Studio, "Gymmet");
            Trainer trainer1 = new Trainer(Trainer.Category.Trainer, "Dansledare");
            Trainer trainer2 = new Trainer(Trainer.Category.Trainer, "Passledare");
            Trainer trainer3 = new Trainer(Trainer.Category.Consultation, "Racketcoach");
            Trainer trainer4 = new Trainer(Trainer.Category.Consultation, "PT");
            Equipment equipment1 = new Equipment(Equipment.Category.Small, "Tennisracket");
            Equipment equipment2 = new Equipment(Equipment.Category.Small, "Musikanläggning");
            Equipment equipment3 = new Equipment(Equipment.Category.Small, "Hopprep");
            Equipment equipment4 = new Equipment(Equipment.Category.Large, "Löparband");
            Equipment equipment5 = new Equipment(Equipment.Category.Large, "Paddelracket");
            Equipment equipment6 = new Equipment(Equipment.Category.Large, "Bänkpress");
            ReservingEntity user1 = new ReservingEntity("Anders Andersson", "1000", "100", "a.a@gmail.com", "Member");
            ReservingEntity user2 = new ReservingEntity("Berit Birgersson", "1001", "101", "b.b@gmail.com", "Member");
            ReservingEntity user3 = new ReservingEntity("Cedric Cederkvist", "1002", "102", "c.c@gmail.com", "Member");
            ReservingEntity user4 = new ReservingEntity("Daniel Damp", "1003", "103", "d.d@gmail.com", "Staff");
            ReservingEntity user5 = new ReservingEntity("Erik Erdogan", "1004", "104", "e.e@gmail.com", "Staff");
            ReservingEntity user6 = new ReservingEntity("Fredrik Fransson", "1005", "105", "f.f@gmail.com", "Service");
            GroupActivity activity1 = new GroupActivity("Danspass", "a01", space5, trainer1, equipment2);
            GroupActivity activity2 = new GroupActivity("Jympapass", "a02", space1, trainer2, equipment3);
            GroupActivity activity3 = new GroupActivity("Tennisträning", "a03", space3, trainer3, equipment1);
            GroupActivity activity4 = new GroupActivity("Löparpass", "a04", space2, trainer4, equipment4);
            GroupActivity activity5 = new GroupActivity("Gymträning", "a05", space6, trainer4, equipment6);
            activity1.participants = new List<ReservingEntity>();
            activity2.participants = new List<ReservingEntity>();
            activity3.participants = new List<ReservingEntity>();
            activity4.participants = new List<ReservingEntity>();
            activity1.participants.Add(user1);
            activity1.participants.Add(user2);
            activity2.participants.Add(user1);
            activity2.participants.Add(user2);
            activity2.participants.Add(user3);
            activity3.participants.Add(user1);
            activity3.participants.Add(user3);
            activity4.participants.Add(user2);
            activity4.participants.Add(user1);
            GroupSchedule schedule1 = new GroupSchedule();
            schedule1.activities = new List<GroupActivity>();
            schedule1.activities.Add(activity1);
            schedule1.activities.Add(activity2);
            schedule1.activities.Add(activity3);
            schedule1.activities.Add(activity4);
            schedule1.activities.Add(activity5);

            Menu m1 = new Menu();            m1.Run();
        }
    }
}