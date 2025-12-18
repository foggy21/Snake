using System;
using Snake.Service;

namespace Snake.Entity
{
    public abstract class Bonus : GridObject
    {
        protected byte _value;

        public byte Value => _value;

        public event Action<Bonus> OnGiveBonus;

        public Bonus(byte value)
        {
            _value = value;
        }
        
        public void GiveBonus()
        {
            OnGiveBonus?.Invoke(this);
            
        }

        public virtual void Initialize()
        {
            OnGiveBonus += SnakeService.Instance.GetBonus;
        }

        public void Remove()
        {
            OnGiveBonus -= SnakeService.Instance.GetBonus;
        }
    }
}