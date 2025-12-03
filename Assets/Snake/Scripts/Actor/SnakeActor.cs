using System;
using System.Collections.Generic;
using Snake.Entity;
using Snake.Service;
using UnityEngine;

namespace Snake.Scripts.Actor
{
    public class SnakeActor : Actor
    {
        private const string RESOURCE_BODY_PATH = "Snake/Body";
        private const string RESOURCE_HEAD_PATH = "Snake/Head";
        private static SnakeActor Instance { get; set; }
   
        private List<Transform> _bodyParts = new();

        private void Awake()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void InitializeBody(Entity.Snake snake)
        {
            GameObject headGameObject = Resources.Load<GameObject>(RESOURCE_HEAD_PATH);
            GameObject bodyGameObject = Resources.Load<GameObject>(RESOURCE_BODY_PATH);
            
            foreach (var bodyPart in snake.BodyParts)
            {
                if (bodyPart is Head)
                {
                    GameObject head = Instantiate(
                        headGameObject,
                        bodyPart.Position,
                        bodyPart.Rotation,
                        transform);
                    _bodyParts.Add(head.transform);
                    continue;
                }
                GameObject body = Instantiate(
                    bodyGameObject,
                    bodyPart.Position,
                    bodyPart.Rotation,
                    transform);
                _bodyParts.Add(body.transform);
            }
        }

        public void MoveBody(Entity.Snake snake)
        {
            for (int i = _bodyParts.Count - 1; i >= 0; i--)
            {
                _bodyParts[i].position = snake.BodyParts[i].Position;
                _bodyParts[i].rotation = snake.BodyParts[i].Rotation;
            }
        }
    }
}