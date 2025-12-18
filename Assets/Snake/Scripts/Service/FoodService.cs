using System;
using Snake.Entity;
using Snake.Scripts.Actor;
using UnityEngine;
using Grid = Snake.Entity.Grid;

namespace Snake.Service
{
    public class FoodService : Service
    {
        private const byte VALUE = 1;
        private const string RESOURCE_FOOD = "Bonus/Food";
        
        public static FoodService Instance { get; private set; }
        
        private Food _activeFood;
        private FoodActor _activeFoodActor;

        private void Awake()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                SpawnFood();
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void SpawnFood()
        {
            _activeFood = new Food(VALUE);
            _activeFood.Initialize();
            
            GameObject food = Resources.Load<GameObject>(RESOURCE_FOOD);
            food = Instantiate(food, 
                new Vector3(
                    _activeFood.Position.x,
                    _activeFood.Position.y,
                    0),
                Quaternion.identity);
            _activeFoodActor = food.AddComponent<FoodActor>();
        }

        public void ApplyFood()
        {
            _activeFood.GiveBonus();
            _activeFood.Remove();
            Grid.Instance.LiberateCell(_activeFood);
            _activeFood = null;
            
            _activeFoodActor.Remove();
            
            SpawnFood();
        }
    }
}