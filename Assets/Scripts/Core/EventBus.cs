using System;
using System.Collections.Generic;
using UnityEngine;

namespace GWBGameJam
{
    public static class EventBus<T> where T : struct
    {
        private static readonly List<Action<T>> _handlers = new();
        private static readonly List<Action<T>> _pendingSubscribe = new();
        private static readonly List<Action<T>> _pendingUnsubscribe = new();
        private static bool _publishing;

        public static void Subscribe(Action<T> handler)
        {
            if (_publishing)
            {
                _pendingUnsubscribe.Remove(handler);
                if (!_handlers.Contains(handler) && !_pendingSubscribe.Contains(handler))
                    _pendingSubscribe.Add(handler);
                return;
            }
            if (!_handlers.Contains(handler))
                _handlers.Add(handler);
        }

        public static void Unsubscribe(Action<T> handler)
        {
            if (_publishing)
            {
                _pendingSubscribe.Remove(handler);
                if (_handlers.Contains(handler) && !_pendingUnsubscribe.Contains(handler))
                    _pendingUnsubscribe.Add(handler);
                return;
            }
            _handlers.Remove(handler);
        }

        public static void Publish(T eventData)
        {
            _publishing = true;
            for (int i = 0; i < _handlers.Count; i++)
            {
                Action<T> handler = _handlers[i];
                if (_pendingUnsubscribe.Contains(handler))
                    continue;

                try
                {
                    handler?.Invoke(eventData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventBus<{typeof(T).Name}>] Handler threw: {e}");
                }
            }
            _publishing = false;

            foreach (var h in _pendingUnsubscribe)
                _handlers.Remove(h);
            _pendingUnsubscribe.Clear();

            foreach (var h in _pendingSubscribe)
                if (!_handlers.Contains(h))
                    _handlers.Add(h);
            _pendingSubscribe.Clear();
        }
    }
}
