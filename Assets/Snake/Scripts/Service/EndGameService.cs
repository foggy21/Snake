using Snake.Scripts.Actor;
using UnityEngine;

namespace Snake.Service
{
    public class EndGameService : Service
    {
        private const string RESOURCE_GAME_OVER_SCREEN = "Prefab/GameOver";
        public static EndGameService Instance { get; private set; }

        private GameOverActor _gameOverActor;

        private void Awake()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeGameOverActor();
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeGameOverActor()
        {
            GameObject gameOverScreen = Resources.Load<GameObject>(RESOURCE_GAME_OVER_SCREEN);
            gameOverScreen = Instantiate(gameOverScreen);
            _gameOverActor = gameOverScreen.AddComponent<GameOverActor>();
        }
        
        private void OnEnable()
        {
            SnakeService.Instance.OnGameOver += GameOver;
        }

        private void OnDisable()
        {
            SnakeService.Instance.OnGameOver -= GameOver;
        }

        private void GameOver()
        {
            _gameOverActor.ShowGameOver();
        }
    }
}