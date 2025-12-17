using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Snake.Entity
{
    public sealed class Snake
    {
        public static Snake Instance { get; }
        
        private Head _head;
        private readonly List<BodyPart> _bodyParts = new();

        public List<BodyPart> BodyParts => _bodyParts;
        
        private Snake() { }

        static Snake()
        {
            Instance = new Snake();
        }

        public void Initialize(Vector2Int startPosition, byte countOfBodyParts)
        {
            InitializeBodyParts(startPosition, countOfBodyParts);
            InitializeHead(startPosition);
        }

        private void InitializeBodyParts(Vector2Int startPosition, byte countOfBodyParts)
        {
            Vector2Int offsetPosition = startPosition - Vector2Int.right * countOfBodyParts;
            for (byte i = 0; i < countOfBodyParts; i++)
            {
                BodyPart behindBodyPart = _bodyParts.LastOrDefault();

                BodyPart bodyPart = new BodyPart(
                    offsetPosition,
                    behindBodyPart);
                
                _bodyParts.Add(bodyPart);

                offsetPosition += Vector2Int.right;
            }
        }

        private void InitializeHead(Vector2Int startPosition)
        {   
            BodyPart behindBodyPart = _bodyParts.LastOrDefault();
            Head head = new Head(
                startPosition,
                behindBodyPart);
            
            _bodyParts.Add(head);
            _head = head;
        }

        public void Move()
        {
            _head.Move(_head.MovementDirection);
        }

        public void Move(Vector2Int direction)
        {
            _head.MoveInDirection(direction);
        }
    }
}