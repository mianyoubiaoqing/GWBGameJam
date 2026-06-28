using System;
using System.Collections.Generic;
using UnityEngine;

namespace GWBGameJam
{
    public static class EventBus<T> where T : struct
    {
        private static readonly List<Action<T>> _handlers = new();
        private static readonly List<Action<T>> _handlersBuffer = new();
        private static bool _publishing;

        public static void Subscribe(Action<T> handler)
        {
            if (_publishing)
            {
                _handlersBuffer.Add(handler);
                return;
            }
            if (!_handlers.Contains(handler))
                _handlers.Add(handler);
        }

        public static void Unsubscribe(Action<T> handler)
        {
            if (_publishing)
            {
                _handlersBuffer.Remove(handler);
                return;
            }
            _handlers.Remove(handler);
        }

        public static void Publish(T eventData)
        {
            _publishing = true;
            for (int i = 0; i < _handlers.Count; i++)
            {
                try
                {
                    _handlers[i]?.Invoke(eventData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventBus<{typeof(T).Name}>] Handler threw: {e}");
                }
            }
            _publishing = false;

            foreach (var h in _handlersBuffer)
                _handlers.Add(h);
            _handlersBuffer.Clear();
        }
    }
}
