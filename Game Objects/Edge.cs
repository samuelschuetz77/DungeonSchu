namespace HeroQuest
{
    public class Edge
    {
        public int Target { get; set; }
        public Attribute RequiredStatType { get; set; }
        public int MinimumStatValue { get; set; }
        public string RequiredItem { get; set; }

        public Edge(int target, Attribute requiredStatType = Attribute.Intelligence, string requiredItem = null, int minimumStatValue = 0)
        {
            Target = target;
            RequiredStatType = requiredStatType;
            RequiredItem = requiredItem;
            MinimumStatValue = minimumStatValue;
        }
    }
}