namespace jwk6gcFinalProject
{
    internal class Card
    {
        public int value { get; set; }
        Suit cardSuit;
        public string path { get; set; }
        public Card(int value, Suit cardSuit, string path)
        {
            this.value = value;
            this.cardSuit = cardSuit;
            this.path = path;
        }
    }

    enum Suit
    {
        CLUBS, HEARTS,
        DIAMONDS, SPADES
    }
}