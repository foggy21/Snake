using UnityEngine;

namespace Snake.Entity
{
    public class Head : BodyPart
    {
        public Head(Vector2Int position, BodyPart behindBodyPart)
            : base(position, behindBodyPart)
        { }

        public void MoveInDirection(Vector2Int direction)
        {
            Quaternion angle = Rotate(direction);
            if (!IsOppositeAngel(angle))
            {
                _rotation = angle;
                Move(direction);
                _movementDirection = direction;
            }
        }

        public override void Move(Vector2Int direction)
        {
            Grid.Instance.LiberateCell(this);
            _position += direction;
            
            if (Grid.Instance.IsLiberatedCell(_position))
            {
                Grid.Instance.OccupyCell(this);   
            }
            
            _behindBodyPart?.Move(direction);
        }

        private bool IsOppositeAngel(Quaternion angle)
        {
            float angleDiff = Quaternion.Angle(_rotation, angle);
            return Mathf.Approximately(angleDiff, 180);
        }
    }
}