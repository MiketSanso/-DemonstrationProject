using System;

namespace GameScene.Characters
{
    public class IntValue
    {
        public Action<int> OnChanged;

        private int _value;

        public IntValue(int integer)
        {
            Set(integer);
        }

        public void Set(int value)
        {
            _value = Math.Max(0, value);
            OnChanged?.Invoke(_value);
        }

        public static implicit operator int(IntValue d) => d._value;
    }
}
