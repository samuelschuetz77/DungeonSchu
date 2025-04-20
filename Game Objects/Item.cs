namespace HeroQuest{
    public class Item {
        public string Name {get;set;}
        public Attribute Attribute{get;set;}
        public int Enhancement{get;set;} //by how much does it increase/decrease attribute value
    }

    public enum Attribute{Strength, Intelligence, Agility, Health}

}
