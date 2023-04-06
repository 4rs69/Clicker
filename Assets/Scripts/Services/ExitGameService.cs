using System;
using Events;
using SimpleEventBus.Disposables;
using UnityEditor;
using UnityEngine;

namespace Services
{
    public class ExitGameService : MonoBehaviour, IDisposable
    {
        private CompositeDisposable _subscriptions;

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
        
        private void Awake()
        {
            _subscriptions = new CompositeDisposable
            {
                EventStreams.Game.Subscribe<ExitGameEvent>(ExitGame)
            };
        }
        
        private void ExitGame(ExitGameEvent eventData)
        {
            EditorApplication.isPlaying = false;
        }
    }
}
