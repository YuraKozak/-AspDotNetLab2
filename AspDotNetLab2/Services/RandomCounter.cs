using System;

namespace Services
{
    public class RandomCounter : ICounter
    {
        static Random rnd = new Random();
        private int _value;
        public RandomCounter() //* Конструктор
        {
            _value = rnd.Next(0, 100000);
        }
        public int Value
        {
            get { return _value; }
        }
    }
}