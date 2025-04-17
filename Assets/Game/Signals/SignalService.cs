using System;
using System.Collections.Generic;

namespace Game.Signals
{
    public class SignalService : ISignalService
    {
        private readonly Dictionary<Type, List<Delegate>> observers = new Dictionary<Type, List<Delegate>>();

        public void Subscribe<T>(SignalCallback<T> callback) where T : Signal, new()
        {
            Type localType = typeof(T);

            if (observers.ContainsKey(localType))
            {
                observers[localType].Add(callback);
            }
            else
            {
                observers.Add(localType, new List<Delegate> { callback });
            }
        }

        public void RemoveSignal<T>(SignalCallback<T> callback) where T : Signal, new()
        {
            if (observers.ContainsKey(typeof(T)))
            {
                List<Delegate> temp = new List<Delegate>();
                temp.AddRange(observers[typeof(T)]);

                List<Delegate> toRemove = temp.FindAll(t => t.Target.Equals(callback.Target));
                foreach (Delegate item in toRemove)
                {
                    temp.Remove(item);
                }

                observers[typeof(T)] = temp;
            }
        }

        public void Publish<T>(T signalObject = default) where T : Signal, new()
        {
            if (signalObject == null)
            {
                signalObject = new T();
            }

            if (observers.TryGetValue(typeof(T), out List<Delegate> callbacks))
            {
                foreach (Delegate item in callbacks)
                {
                    item?.DynamicInvoke(signalObject);
                }
            }
        }
    }
}