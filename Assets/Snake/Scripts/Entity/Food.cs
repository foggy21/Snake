using Snake.Service;

namespace Snake.Entity
{
    public class Food : Bonus
    {
        public Food(byte value) : base(value) { }

        public override void Initialize()
        {
            base.Initialize();
            _position = GridService.Instance.GetRandomFreeCell();
            Grid.Instance.OccupyCell(this);
        }
    }
}