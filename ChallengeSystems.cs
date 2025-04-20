namespace HeroQuest{
    public class CombatSystem
    {
        public bool ResolveCombat(Hero hero, Challenge challenge)
        {
            return hero.Strength >= challenge.MinimumStatValue;
        }
    }
    public class TrapSystem
    {
        public bool ResolveTrap(Hero hero, Challenge challenge)
        {
            return hero.Agility >= challenge.MinimumStatValue;
        }
    }
    public class PuzzleSystem
    {
        public bool SolvePuzzle(Hero hero, Challenge challenge)
        {
            return hero.Intelligence >= challenge.MinimumStatValue;
        }
    }

}