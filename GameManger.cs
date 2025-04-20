using System.Threading;

namespace HeroQuest
{
    public class GameManager
    {
        private Hero hero;
        private Map map;
        private ChallengeBST challengeTree;
        private List<Room> rooms; // List of rooms for treasure and challenge dispersal.
        private string playerName; // Store the player's name.
        private Stack<int> visitedRooms = new Stack<int>(); // Use a stack to track visited rooms.
        private Stack<Treasure> treasureStack = new Stack<Treasure>();

        private static readonly List<Item> AllItems = new List<Item>
        {
            new Item { Name = "Sword", Attribute = Attribute.Strength, Enhancement = 5 },
            new Item { Name = "Shield", Attribute = Attribute.Strength, Enhancement = 3 },
            new Item { Name = "Bow", Attribute = Attribute.Agility, Enhancement = 4 },
            new Item { Name = "Dagger", Attribute = Attribute.Agility, Enhancement = 2 },
            new Item { Name = "Staff", Attribute = Attribute.Intelligence, Enhancement = 5 },
            new Item { Name = "Spellbook", Attribute = Attribute.Intelligence, Enhancement = 4 },
            new Item { Name = "Potion of Strength", Attribute = Attribute.Strength, Enhancement = 3 },
            new Item { Name = "Potion of Agility", Attribute = Attribute.Agility, Enhancement = 3 },
            new Item { Name = "Potion of Intelligence", Attribute = Attribute.Intelligence, Enhancement = 3 },
            new Item { Name = "Potion of Health", Attribute = Attribute.Health, Enhancement = 10 },
            new Item { Name = "Lockpick", Attribute = Attribute.Agility, Enhancement = 1 },
            new Item { Name = "Torch", Attribute = Attribute.Intelligence, Enhancement = 1 },
            new Item { Name = "Helmet", Attribute = Attribute.Strength, Enhancement = 2 },
            new Item { Name = "Boots", Attribute = Attribute.Agility, Enhancement = 2 },
            new Item { Name = "Gloves", Attribute = Attribute.Strength, Enhancement = 1 },
            new Item { Name = "Ring of Power", Attribute = Attribute.Intelligence, Enhancement = 5 },
            new Item { Name = "Amulet of Protection", Attribute = Attribute.Health, Enhancement = 5 },
            new Item { Name = "Cloak of Shadows", Attribute = Attribute.Agility, Enhancement = 4 },
            new Item { Name = "Belt of Giants", Attribute = Attribute.Strength, Enhancement = 4 },
            new Item { Name = "Bracers of Defense", Attribute = Attribute.Health, Enhancement = 3 },
            new Item { Name = "Elixir of Life", Attribute = Attribute.Health, Enhancement = 15 },
            new Item { Name = "Scroll of Wisdom", Attribute = Attribute.Intelligence, Enhancement = 3 },
            new Item { Name = "Smoke Bomb", Attribute = Attribute.Agility, Enhancement = 2 },
            new Item { Name = "Trap Disarm Kit", Attribute = Attribute.Agility, Enhancement = 3 },
            new Item { Name = "Battle Axe", Attribute = Attribute.Strength, Enhancement = 6 },
            new Item { Name = "Crossbow", Attribute = Attribute.Agility, Enhancement = 5 },
            new Item { Name = "Magic Wand", Attribute = Attribute.Intelligence, Enhancement = 4 },
            new Item { Name = "Healing Herb", Attribute = Attribute.Health, Enhancement = 8 },
            new Item { Name = "Golden Key", Attribute = Attribute.Intelligence, Enhancement = 1 },
            new Item { Name = "Dragon Scale", Attribute = Attribute.Health, Enhancement = 10 },
            new Item { Name = "Phoenix Feather", Attribute = Attribute.Health, Enhancement = 12 },
            new Item { Name = "Warhammer", Attribute = Attribute.Strength, Enhancement = 7 },
            new Item { Name = "Throwing Knives", Attribute = Attribute.Agility, Enhancement = 3 },
            new Item { Name = "Crystal Ball", Attribute = Attribute.Intelligence, Enhancement = 2 },
            new Item { Name = "Ring of Fortitude", Attribute = Attribute.Health, Enhancement = 6 },
            new Item { Name = "Boots of Speed", Attribute = Attribute.Agility, Enhancement = 5 },
            new Item { Name = "Gauntlets of Strength", Attribute = Attribute.Strength, Enhancement = 5 },
            new Item { Name = "Potion of Invincibility", Attribute = Attribute.Health, Enhancement = 20 },
            new Item { Name = "Talisman of Wisdom", Attribute = Attribute.Intelligence, Enhancement = 4 },
            new Item { Name = "Cape of Evasion", Attribute = Attribute.Agility, Enhancement = 3 }
        };

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Hero's Quest!");
            Console.WriteLine("=========================");
            Console.Write("Enter your name, brave adventurer: ");
            playerName = Console.ReadLine(); // Get the player's name.
            Console.WriteLine($"Welcome, {playerName}! Your adventure begins now...");
            Thread.Sleep(2000); // Pause for 2 seconds.
            Console.Clear();

            CreateHero();
            GenerateMap();
            DisperseChallenges();
            DisperseTreasures();
            DisperseItems();
            PlayGame();
        }

        private void CreateHero()
        {
            Console.Clear();
            Console.WriteLine($"Creating {playerName}, the Hero...");
            Console.WriteLine("=====================");

            // Initialize the hero with default stats and an empty inventory.
            hero = new Hero
            {
                Strength = 10,
                Agility = 10,
                Intelligence = 10,
                Health = 20,
                Inventory = new Queue<Item>()
            };

            // Add initial items to the hero's inventory.
            hero.Inventory.Enqueue(new Item { Name = "Sword", Attribute = Attribute.Strength });
            hero.Inventory.Enqueue(new Item { Name = "Health Potion", Attribute = Attribute.Health });

            Console.WriteLine($"{playerName} the Hero now has the following stats:");
            Console.WriteLine($"Strength: {hero.Strength}, Agility: {hero.Agility}, Intelligence: {hero.Intelligence}, Health: {hero.Health}");
            Console.WriteLine("Starting inventory: Sword, Health Potion");
            Console.WriteLine("=====================");
            Thread.Sleep(2000); // Pause for 2 seconds.
            Console.Clear();
        }

        private void GenerateMap()
        {
            Console.WriteLine("Generating the map...");
            Console.WriteLine("=====================");

            // Generate a map with 15 rooms.
            map = new Map(15);
            rooms = new List<Room>();

            // Create Room objects for each room in the map.
            for (int i = 1; i <= 15; i++)
            {
                rooms.Add(new Room(i));
            }

            Console.WriteLine("Map generated with 15 rooms.");
            Console.WriteLine("=====================");
            Thread.Sleep(2000); // Pause for 2 seconds.
            Console.Clear();
        }

        private void DisperseChallenges()
        {
            Console.WriteLine("Dispersing challenges...");
            Console.WriteLine("========================");

            // Initialize the challenge BST.
            challengeTree = new ChallengeBST();

            // Add challenges to the BST.
            Random random = new Random();
            foreach (var room in rooms)
            {
                Challenge challenge = new Challenge(
                    difficulty: random.Next(1, 21), // Difficulty between 1 and 20.
                    type: (ChallengeType)random.Next(0, 3), // Random challenge type (Combat, Trap, Puzzle).
                    requiredStatType: (Attribute)random.Next(0, 3), // Random required stat (Strength, Agility, Intelligence).
                    minimumStatValue: random.Next(5, 16), // Minimum stat value between 5 and 15.
                    requiredItem: random.Next(0, 2) == 0 ? "Key" : null // 50% chance of requiring an item.
                );

                challengeTree.Insert(challenge);
                room.Challenge = challenge; // Assign the challenge to the room.
            }

            Console.WriteLine("Challenges have been dispersed into the rooms.");
            Console.WriteLine("========================");
            Thread.Sleep(2000); // Pause for 2 seconds.
            Console.Clear();
        }

        private void DisperseTreasures()
        {
            Console.WriteLine("Dispersing treasures...");
            Console.WriteLine("========================");

            Random random = new Random();

            foreach (var room in rooms)
            {
                // 10% chance to place a treasure in the room.
                if (random.Next(0, 10) < 1)
                {
                    Treasure treasure = new Treasure(
                        name: "Treasure " + room.RoomNumber,
                        affectedAttribute: (Attribute)random.Next(0, 4), // Randomly select an attribute.
                        boostValue: random.Next(1, 4) // Boost value between 1 and 3.
                    );

                    room.Treasure = treasure; // Assign the treasure to the room.
                }
            }

            Console.WriteLine("Treasures have been dispersed into the rooms.");
            Console.WriteLine("========================");
            Thread.Sleep(2000); // Pause for 2 seconds.
            Console.Clear();
        }

        private void DisperseItems()
        {
            Console.WriteLine("Dispersing items...");
            Console.WriteLine("===================");

            Random random = new Random();

            foreach (var room in rooms)
            {
                // 33% chance to place an item in the room.
                if (random.Next(0, 3) < 1)
                {
                    // Randomly select an item from the list of all items.
                    Item item = AllItems[random.Next(AllItems.Count)];
                    room.IteminRoom = item; // Assign the item to the room.
                }
            }

            Console.WriteLine("Items have been dispersed into the rooms.");
            Console.WriteLine("===================");
            Thread.Sleep(2000); // Pause for 2 seconds.
            Console.Clear();
        }

        private void PlayGame()
        {
            Console.WriteLine($"The adventure begins, {playerName}! Navigate the map and face challenges.");
            Console.WriteLine("===================================================");

            bool gameRunning = true;
            int currentRoom = 1;

            while (gameRunning)
            {
                Console.Clear();
                Console.WriteLine($"You are in room {currentRoom}, {playerName}.");
                Console.WriteLine("==============================");

                // Push the current room onto the stack.
                visitedRooms.Push(currentRoom);

                // Display the path taken so far.
                Console.WriteLine("Path taken:");
                foreach (var room in visitedRooms)
                {
                    Console.Write($"{room} -> ");
                }
                Console.WriteLine("End");

                // Display the hero's attributes.
                Console.WriteLine("Your attributes:");
                Console.WriteLine($"- Strength: {hero.Strength}");
                Console.WriteLine($"- Agility: {hero.Agility}");
                Console.WriteLine($"- Intelligence: {hero.Intelligence}");
                Console.WriteLine($"- Health: {hero.Health}");
                Console.WriteLine("==============================");

                // Display the hero's inventory.
                Console.WriteLine("Your inventory:");
                if (hero.Inventory.Count > 0)
                {
                    Console.WriteLine(string.Join(" ||| ", hero.Inventory.Select(item => $"{item.Name} ({item.Attribute})")));
                }
                else
                {
                    Console.WriteLine("Your inventory is empty.");
                }
                Console.WriteLine("==============================");

                // Get the current room object.
                Room currentRoomObj = rooms.FirstOrDefault(r => r.RoomNumber == currentRoom);
                if (currentRoomObj == null)
                {
                    Console.WriteLine("Error: Room not found.");
                    break;
                }

                // Check for a treasure in the current room.
                if (currentRoomObj.Treasure != null)
                {
                    Console.WriteLine($"You found a treasure: {currentRoomObj.Treasure.Name}!");
                    treasureStack.Push(currentRoomObj.Treasure); // Add the treasure to the stack.
                    currentRoomObj.Treasure.ApplyToHero(hero); // Apply the treasure's effect.
                    Console.WriteLine($"Your {currentRoomObj.Treasure.AffectedAttribute} increased by {currentRoomObj.Treasure.BoostValue}.");
                    currentRoomObj.Treasure = null; // Remove the treasure from the room.
                }

                // Display treasures collected so far.
                Console.WriteLine("Treasures collected:");
                foreach (var treasure in treasureStack)
                {
                    Console.WriteLine($"- {treasure.Name}");
                }

                // Check for a challenge in the current room.
                if (currentRoomObj.Challenge != null)
                {
                    Console.WriteLine($"A {currentRoomObj.Challenge.Type} challenge appears! Difficulty: {currentRoomObj.Challenge.Difficulty}");

                    if (currentRoomObj.Challenge.CanOvercome(hero))
                    {
                        Console.WriteLine("You overcame the challenge!");
                        challengeTree.Remove(currentRoomObj.Challenge.Difficulty);
                        currentRoomObj.Challenge = null; // Remove the challenge after completing it.
                    }
                    else
                    {
                        int healthPenalty = currentRoomObj.Challenge.CalculateHealthPenalty(hero);
                        hero.Health -= healthPenalty;
                        Console.WriteLine($"You failed the challenge and lost {healthPenalty} health. Remaining health: {hero.Health}");

                        if (hero.Health <= 0)
                        {
                            Console.WriteLine($"You have died, {playerName}. Game over.");
                            gameRunning = false;
                            break;
                        }
                    }
                }

                // Check for an item in the current room.
                if (currentRoomObj.IteminRoom != null)
                {
                    Console.WriteLine($"You found an item: {currentRoomObj.IteminRoom.Name}!");
                    Console.WriteLine($"It enhances {currentRoomObj.IteminRoom.Attribute} by {currentRoomObj.IteminRoom.Enhancement}.");
                    Console.Write("Do you want to pick it up? (yes/no): ");
                    string choice = Console.ReadLine()?.ToLower();

                    if (choice == "yes" || choice == "y")
                    {
                        if (hero.Inventory.Count >= 5)
                        {
                            Console.WriteLine("Your inventory is full. The oldest item will be discarded.");
                            Item removedItem = hero.Inventory.Dequeue(); // Remove the oldest item.

                            // Reverse the effect of the removed item.
                            switch (removedItem.Attribute)
                            {
                                case Attribute.Strength:
                                    hero.Strength -= removedItem.Enhancement;
                                    break;
                                case Attribute.Agility:
                                    hero.Agility -= removedItem.Enhancement;
                                    break;
                                case Attribute.Intelligence:
                                    hero.Intelligence -= removedItem.Enhancement;
                                    break;
                                case Attribute.Health:
                                    hero.Health -= removedItem.Enhancement;
                                    break;
                            }
                        }

                        hero.Inventory.Enqueue(currentRoomObj.IteminRoom); // Add the new item to the inventory.

                        // Apply the effect of the new item.
                        switch (currentRoomObj.IteminRoom.Attribute)
                        {
                            case Attribute.Strength:
                                hero.Strength += currentRoomObj.IteminRoom.Enhancement;
                                break;
                            case Attribute.Agility:
                                hero.Agility += currentRoomObj.IteminRoom.Enhancement;
                                break;
                            case Attribute.Intelligence:
                                hero.Intelligence += currentRoomObj.IteminRoom.Enhancement;
                                break;
                            case Attribute.Health:
                                hero.Health += currentRoomObj.IteminRoom.Enhancement;
                                break;
                        }

                        Console.WriteLine($"You picked up the {currentRoomObj.IteminRoom.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("You decided not to pick up the item.");
                    }

                    currentRoomObj.IteminRoom = null; // Remove the item from the room.
                }

                // Display connected rooms with "(Visited)" marker.
                Console.WriteLine("Connected rooms:");
                var connectedRooms = map.AdjacencyList[currentRoom];
                for (int i = 0; i < connectedRooms.Count; i++)
                {
                    int targetRoom = connectedRooms[i].Target;
                    string visitedMarker = visitedRooms.Contains(targetRoom) ? " (Visited)" : "";
                    Console.WriteLine($"{i + 1}. Room {targetRoom}{visitedMarker}");
                }

                Console.WriteLine($"{connectedRooms.Count + 1}. Exit the game.");
                Console.WriteLine($"{connectedRooms.Count + 2}. Backtrack to the previous room.");
                Console.WriteLine("==============================");

                // Get the player's choice.
                Console.Write("Choose a room to move to: ");
                string choiceRoom = Console.ReadLine();
                if (int.TryParse(choiceRoom, out int roomChoice) && roomChoice >= 1 && roomChoice <= connectedRooms.Count)
                {
                    currentRoom = connectedRooms[roomChoice - 1].Target;
                }
                else if (roomChoice == connectedRooms.Count + 1)
                {
                    Console.WriteLine($"You have exited the game, {playerName}. Farewell!");
                    gameRunning = false;
                }
                else if (roomChoice == connectedRooms.Count + 2)
                {
                    if (visitedRooms.Count > 0)
                    {
                        visitedRooms.Pop(); // Backtrack to the previous room.
                        currentRoom = visitedRooms.Peek();
                    }
                    else
                    {
                        Console.WriteLine("No previous room to backtrack to.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
        }
    }
}