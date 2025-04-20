namespace HeroQuest{
    public class Room
    {
        public int RoomNumber { get; set; }
        public Challenge Challenge { get; set; }
        public Item IteminRoom { get; set; }
        public Treasure Treasure { get; set; } // Add a Treasure property.

        public Room(int roomNumber)
        {
            RoomNumber = roomNumber;
        }
    }
}