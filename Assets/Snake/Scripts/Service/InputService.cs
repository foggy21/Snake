using System;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Snake.Input.InputSystem;

namespace Snake.Service
{
    public sealed class InputService : Service
    {
        public static InputService Instance { get; private set; }

        public event Action<Vector2> OnInputMove;

        private InputSystem _inputSystem;

        private Vector2 _inputMove;

        private void Awake()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeInputSystem();
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeInputSystem()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Enable();
            _inputSystem.Character.Move.performed += Move;
        }

        private void Move(InputAction.CallbackContext context)
        {
            _inputMove = _inputSystem.Character.Move.ReadValue<Vector2>();
            OnInputMove?.Invoke(_inputMove);
        }
    }
}