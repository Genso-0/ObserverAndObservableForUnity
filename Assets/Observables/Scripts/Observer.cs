
using System;
using System.Collections.Generic;

namespace Observables
{
    /// <summary>
    /// Observes gameobjects with the ObservableTransform component. It allows for subscribing and unsubscribing actions from events in an organised fashion.
    /// </summary>
    public class Observer
    {
        public Observer()
        {
            observables = new Dictionary<int, ObservableTransform>();
            subscriptions = new Dictionary<ObservableEventTypes, List<ObservableTransform.ObservableChange>>();
            observableEventTypesArray = Enum.GetValues(typeof(ObservableEventTypes));
        }
        public IDictionary<int, ObservableTransform> observables { get; private set; }
        Array observableEventTypesArray;
        IDictionary<ObservableEventTypes, List<ObservableTransform.ObservableChange>> subscriptions;
        public bool AddEventAction(ObservableEventTypes eventType, ObservableTransform.ObservableChange action)
        { 
            if (!subscriptions.ContainsKey(eventType))
                subscriptions.Add(eventType, new List<ObservableTransform.ObservableChange>());

            if (!subscriptions[eventType].Contains(action))
            {
                subscriptions[eventType].Add(action);
                foreach (var o in observables.Values)
                {
                    o.AddAction(eventType, action);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Remove an action from all subscribed events on observables.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool RemoveEventAction(ObservableEventTypes eventType, ObservableTransform.ObservableChange action)
        {
            if (subscriptions.ContainsKey(eventType))
            {
                if (subscriptions[eventType].Contains(action))
                {
                    foreach (var o in observables.Values)
                    {
                        o.RemoveAction(eventType, action);
                    }
                    return subscriptions[eventType].Remove(action);
                }
            }
            return false;
        }
        /// <summary>
        /// Add an observable and subscribe all pre existing actions to the relevant events.
        /// </summary>
        /// <param name="o"></param>
        public bool AddObservable(ObservableTransform o)
        {
            if (o != null && !observables.ContainsKey(o.GetHash()))
            {
                foreach (ObservableEventTypes type in observableEventTypesArray)
                {
                    Add(type, o);
                }

                o.AddAction(ObservableEventTypes.OnDestroyed, ObservableDestroyed);
                observables.Add(o.GetHash(), o);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove observable and unsubscribe from all its events.
        /// </summary>
        /// <param name="o"></param>
        public bool RemoveObservable(ObservableTransform o)
        {
            if (observables.ContainsKey(o.GetHash()))
            {
                foreach (ObservableEventTypes type in observableEventTypesArray)
                {
                    Remove(type, o);
                }

                o.RemoveAction(ObservableEventTypes.OnDestroyed, ObservableDestroyed);
                return observables.Remove(o.GetHash());
            }
            return false;
        }
        private void Add(ObservableEventTypes eventType, ObservableTransform o)
        {
            if (subscriptions.ContainsKey(eventType))
                foreach (var action in subscriptions[eventType])
                    o.AddAction(eventType, action);
        }
        private void Remove(ObservableEventTypes eventType, ObservableTransform o)
        {
            if (subscriptions.ContainsKey(eventType))
                foreach (var action in subscriptions[eventType])
                    o.RemoveAction(eventType, action);
        }
        private void ObservableDestroyed(int hash)
        {
            RemoveObservable(observables[hash]);
        }
    }
}