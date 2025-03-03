namespace GameScene.Characters
{
    public readonly struct IntValue
    {
        private readonly int _integer;

        public IntValue(int integer)
        {
            if (integer > 0)
                _integer = integer;
            else
                _integer = 0;
        }

        public static implicit operator int(IntValue d) => d._integer;
        public static implicit operator IntValue(int b) => new IntValue(b);
    }
}
