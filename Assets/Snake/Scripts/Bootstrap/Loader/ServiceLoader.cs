using UnityEngine;

namespace Snake.Bootstrap.Loader
{
    public static class ServiceLoader
    {
        public static T Load<T>(string serviceName) where T : Service.Service
        {
            GameObject serviceGameObject = new GameObject(serviceName);
            T service = serviceGameObject.AddComponent<T>();
            return service;
        }
    }
}