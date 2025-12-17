using Snake.Scripts.Actor;
using UnityEngine;

namespace Snake.Service
{
    public class SnakeService : Service
    {
        private const byte START_COUNT_OF_BODY_PARTS = 3;
        
        public static SnakeService Instance { get; private set; }

        private readonly Entity.Snake _snake = Entity.Snake.Instance;
        private SnakeActor _snakeActor;

        private readonly Vector2Int _startPosition = new Vector2Int(23, 10);
        
        private void Awake()
        {
            Initialize();
            InvokeRepeating(nameof(MoveAlways), 0f, 0.2f);
        }

        protected override void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeSnake();
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeSnake()
        {
            _snake.Initialize(_startPosition, START_COUNT_OF_BODY_PARTS);
            InitializeSnakeActor();
        }

        private void InitializeSnakeActor()
        {
            GameObject snakeGameObject = new GameObject("Snake Actor");
            _snakeActor = snakeGameObject.AddComponent<SnakeActor>();
            _snakeActor.InitializeBody(_snake);
        }

        private void OnEnable()
        {
            InputService.Instance.OnInputMove += InputMove;
        }

        private void OnDisable()
        {
            InputService.Instance.OnInputMove -= InputMove;
        }

        private void MoveAlways()
        {
            _snake.Move();
            _snakeActor.MoveBody(_snake);
        }

        private void InputMove(Vector2Int inputMove)
        {
            _snake.Move(inputMove);
            _snakeActor.MoveBody(_snake);
        }
    }
}