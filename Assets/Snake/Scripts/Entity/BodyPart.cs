using UnityEngine;

namespace Snake.Entity
{
    public class BodyPart
    {
        protected Vector2 _position;
        protected Quaternion _rotation;
        
        protected Vector2 _movementDirection = Vector2.right;

        protected BodyPart _behindBodyPart;

        public Vector2 Position => _position;
        public Quaternion Rotation => _rotation;

        public BodyPart(Vector2 position, BodyPart behindBodyPart)
        {
            _position = position;
            _rotation = Quaternion.identity;
            _behindBodyPart = behindBodyPart;
        }
        
        public virtual void Move(Vector2 direction)
        {
            _position += _movementDirection;
            Rotate(_movementDirection);
            _behindBodyPart?.Move(_movementDirection);
            _movementDirection = direction;
        }

        public void AddBehindBodyPart(BodyPart bodyPart)
        {
            _behindBodyPart = bodyPart;
        }

        protected void Rotate(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}