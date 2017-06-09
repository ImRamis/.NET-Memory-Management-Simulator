namespace Simulate
{
    internal class Block
    {
        private static int _id;

        public Block()
        {
            Touched = false;
            Index = ++_id;
        }

        public int Index { get; set; }
        public bool Touched { get; set; }
        public float Free { get; set; }
        public float Used { get; set; }
        public float PercentFilled { get; set; }
    }
}