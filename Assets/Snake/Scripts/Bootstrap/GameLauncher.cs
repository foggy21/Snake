using Snake.Bootstrap.Entrypoint;
using UnityEngine;

namespace Snake.Bootstrap
{
    public class GameLauncher : MonoBehaviour
    {
        private readonly GameplayEntrypoint _gameplayEntrypoint = new();

        private void Awake()
        {
            Launch();
            Destroy(gameObject);
        }

        private void Launch()
        {
            _gameplayEntrypoint.InitializeServices();
        }
            
    }
}