namespace GoudKoorts
{
    public abstract class Field
    {
        public Field Next { get; set; }

        public MovableObject MovableObject { get; set; }

        public abstract override string ToString();

    }
}