using UnityEngine;

namespace Snake.Entity
{
    public class Head : BodyPart
    {
        public Vector2 MovementDirection => _movementDirection;
        
        public Head(Vector2 position, BodyPart behindBodyPart)
            : base(position, behindBodyPart)
        { }

        public void MoveInDirection(Vector2 direction)
        {
            Rotate(direction);
            Move(direction);
            _movementDirection = direction;
        }

        public override void Move(Vector2 direction)
        {
            _position += direction;
            _behindBodyPart?.Move(direction);
            _movementDirection = direction;
        }
    }
}