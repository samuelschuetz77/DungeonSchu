namespace HeroQuest {
    public class Challenge
    {
        public int Difficulty { get; set; } // Difficulty level (1–20)
        public ChallengeType Type { get; set; } // Type of challenge (Combat, Trap, Puzzle)
        public Attribute RequiredStatType { get; set; } // Required stat type (Strength, Agility, Intelligence)
        public int MinimumStatValue { get; set; } // Minimum stat value required to succeed
        public string RequiredItem { get; set; } // Optional item required to succeed

        public Challenge(int difficulty, ChallengeType type, Attribute requiredStatType, int minimumStatValue, string requiredItem = null)
        {
            Difficulty = difficulty;
            Type = type;
            RequiredStatType = requiredStatType;
            MinimumStatValue = minimumStatValue;
            RequiredItem = requiredItem;
        }

        // Method to determine if the hero can overcome the challenge
        public bool CanOvercome(Hero hero)
        {
            int heroStatValue = GetHeroStatValue(hero);

            // Check if the hero meets the stat requirement or has the required item
            if (heroStatValue >= MinimumStatValue || HasRequiredItem(hero))
            {
                return true;
            }

            return false;
        }

        // Deduct health if the hero fails the challenge
        public int CalculateHealthPenalty(Hero hero)
        {
            int heroStatValue = GetHeroStatValue(hero);
            return heroStatValue < MinimumStatValue ? MinimumStatValue - heroStatValue : 0;
        }

        private int GetHeroStatValue(Hero hero)
        {
            if (RequiredStatType == Attribute.Strength)
            {
                return hero.Strength;
            }
            else if (RequiredStatType == Attribute.Agility)
            {
                return hero.Agility;
            }
            else if (RequiredStatType == Attribute.Intelligence)
            {
                return hero.Intelligence;
            }
            else
            {
                return 0; 
            }
        }

        private bool HasRequiredItem(Hero hero)
        {
            foreach (var item in hero.Inventory)
            {
                if (item.Name == RequiredItem)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public enum ChallengeType
    {
        Combat,
        Trap,
        Puzzle
    }

}