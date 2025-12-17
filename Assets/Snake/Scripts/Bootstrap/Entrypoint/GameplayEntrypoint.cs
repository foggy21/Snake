using Snake.Bootstrap.Loader;
using Snake.Service;

namespace Snake.Bootstrap.Entrypoint
{
    public class GameplayEntrypoint
    {
        public void InitializeServices()
        {
            ServiceLoader.Load<InputService>("Input Service");
            ServiceLoader.Load<GridService>("Grid Service");
            ServiceLoader.Load<SnakeService>("Snake Service");
            ServiceLoader.Load<EndGameService>("End Game Service");
        }
    }
}