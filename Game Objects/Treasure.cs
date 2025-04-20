namespace HeroQuest
{
    public class Treasure
    {
        public string Name { get; set; }
        public Attribute AffectedAttribute { get; set; }
        public int BoostValue { get; set; } // The amount by which the stat is boosted.

        public Treasure(string name, Attribute affectedAttribute, int boostValue)
        {
            Name = name;
            AffectedAttribute = affectedAttribute;
            BoostValue = boostValue;
        }

        // Apply the treasure's effect to the hero.
        public void ApplyToHero(Hero hero)
        {
            switch (AffectedAttribute)
            {
                case Attribute.Strength:
                    hero.Strength += BoostValue;
                    break;
                case Attribute.Agility:
                    hero.Agility += BoostValue;
                    break;
                case Attribute.Intelligence:
                    hero.Intelligence += BoostValue;
                    break;
                case Attribute.Health:
                    hero.Health += BoostValue;
                    break;
            }
        }
    }
}