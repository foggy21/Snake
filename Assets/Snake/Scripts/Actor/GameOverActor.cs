using System;
using UnityEngine;

namespace Snake.Scripts.Actor
{
    public class GameOverActor : Actor
    {
        private Canvas _canvas;

        private void Awake()
        {
            Initialize();
        }

        public override void Initialize()
        {
            _canvas = GetComponent<Canvas>();
            
            _canvas.worldCamera = Camera.main;
            
            gameObject.SetActive(false);
        }

        public void ShowGameOver()
        {
            gameObject.SetActive(true);
        }
    }
}