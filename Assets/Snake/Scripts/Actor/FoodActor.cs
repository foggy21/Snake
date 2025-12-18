namespace Snake.Scripts.Actor
{
    public class FoodActor : Actor
    {
        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}