using UnityEngine;

namespace Snake.Entity
{
    public class BodyPart
    {
        protected Vector2Int _position;
        protected Quaternion _rotation;
        
        protected Vector2Int _movementDirection = Vector2Int.right;

        protected BodyPart _behindBodyPart;

        public Vector2Int Position => _position;
        public Quaternion Rotation => _rotation;

        public BodyPart(Vector2Int position, BodyPart behindBodyPart)
        {
            _position = position;
            _rotation = Quaternion.identity;
            _behindBodyPart = behindBodyPart;
            Grid.Instance.OccupyCell(_position);
        }
        
        public virtual void Move(Vector2Int direction)
        {
            Grid.Instance.LiberateCell(_position);
            _position += _movementDirection;
            _rotation = Rotate(_movementDirection);
            Grid.Instance.OccupyCell(_position);
            
            _behindBodyPart?.Move(_movementDirection);
            _movementDirection = direction;
        }

        public void AddBehindBodyPart(BodyPart bodyPart)
        {
            _behindBodyPart = bodyPart;
        }

        protected Quaternion Rotate(Vector2Int direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }
    }
}