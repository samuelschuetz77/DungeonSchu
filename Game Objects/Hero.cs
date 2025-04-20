namespace HeroQuest{
        public class Hero
    {
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Health { get; set; }
        public Queue<Item> Inventory { get; set; } // Inventory is implemented as a queue.

        // Time Complexity for Queue Operations:
        // Enqueue: O(1)
        // Dequeue: O(1)
        // Space Complexity: O(5), as the inventory has a fixed size of 5 items.
    }
}