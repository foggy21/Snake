using System;
using System.Linq;
using Snake.Entity;
using Snake.Scripts.Actor;
using UnityEngine;
using Grid = Snake.Entity.Grid;

namespace Snake.Service
{
    public class SnakeService : Service
    {
        private const byte START_COUNT_OF_BODY_PARTS = 3;
        
        public static SnakeService Instance { get; private set; }

        private readonly Entity.Snake _snake = Entity.Snake.Instance;
        private SnakeActor _snakeActor;

        private readonly Vector2Int _startPosition = new(21, 10);

        public event Action OnGameOver;
        
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
            MoveSnakeActor();
        }

        private void InputMove(Vector2Int inputMove)
        {
            _snake.Move(inputMove);
            MoveSnakeActor();
        }

        private void MoveSnakeActor()
        {
            _snakeActor.Move(_snake);
            CheckGameOverSituation();
        }

        private void CheckGameOverSituation()
        {
            if (IsHeadAtGridEdge() || IsHeadAtBodyPart())
            {
                DisableService();
            }
        }
        
        private bool IsHeadAtGridEdge()
        {
            BodyPart head = _snake.BodyParts.LastOrDefault();
            if (head is Head)
            {
                if (head.Position.x == Grid.Instance.Width - 1
                    || head.Position.x == 0
                    || head.Position.y == Grid.Instance.Height - 1
                    || head.Position.y == 0)
                {
                    return true;
                }
                return false;
            }
            throw new Exception("Head must be the last element of BodyParts");
        }

        private bool IsHeadAtBodyPart()
        {
            BodyPart head = _snake.BodyParts.LastOrDefault();
            if (head is Head)
            {
                if (!Grid.Instance.IsLiberatedCell(head.Position)
                    && Grid.Instance.Cells[head.Position.x, head.Position.y] != head
                    && Grid.Instance.Cells[head.Position.x, head.Position.y] is BodyPart)
                {
                    return true;
                }
                return false;
            }
            throw new Exception("Head must be the last element of BodyParts");
        }

        private void DisableService()
        {
            OnGameOver?.Invoke();
            CancelInvoke(nameof(MoveAlways));
            gameObject.SetActive(false);
        }
    }
}