using System;
using UnityEngine;

namespace GameScene.Characters
{
    public class IntValue
    {
        public event Action<int> OnChanged;

        private int _value;

        public IntValue(int integer)
        {
            Set(integer);
        }

        public void Set(int value)
        {
            if (value < 0)
            {
                Debug.LogError("В смену урона входит отрицательное значение!");
            }
            _value = Math.Max(0, value);
            
            OnChanged?.Invoke(_value);
        }

        public static implicit operator int(IntValue d) => d._value;
    }
}
