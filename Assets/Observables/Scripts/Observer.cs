 
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
            observables = new Dictionary<int,ObservableTransform>();
            subscriptions = new Dictionary<EventTypes, List<ObservableTransform.ObservableChange>>();
        }
        public IDictionary<int,ObservableTransform> observables { get; private set; }

        IDictionary<EventTypes, List<ObservableTransform.ObservableChange>> subscriptions;
        public bool AddEventAction(EventTypes e, ObservableTransform.ObservableChange action)
        {
            if (!subscriptions.ContainsKey(e))
                subscriptions.Add(e, new List<ObservableTransform.ObservableChange>());

            if (!subscriptions[e].Contains(action))
            {
                subscriptions[e].Add(action);
                foreach (var o in observables.Values)
                {
                    switch (e)
                    {
                        case EventTypes.OnEnabled:
                            o.OnEnabled += action;
                            break;
                        case EventTypes.OnDisabled:
                            o.OnDisabled += action;
                            break;
                        case EventTypes.OnDestroyed:
                            o.OnDestroyed += action;
                            break;
                        case EventTypes.OnTransformChange:
                            o.OnTransformChanged += action;
                            break;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Remove an action from all subscribed events on observables.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool RemoveEventAction(EventTypes e, ObservableTransform.ObservableChange action)
        {
            if (subscriptions.ContainsKey(e))
            {
                if (subscriptions[e].Contains(action))
                {
                    foreach (var o in observables.Values)
                    {
                        switch (e)
                        {
                            case EventTypes.OnEnabled:
                                o.OnEnabled -= action;
                                break;
                            case EventTypes.OnDisabled:
                                o.OnDisabled -= action;
                                break;
                            case EventTypes.OnDestroyed:
                                o.OnDestroyed -= action;
                                break;
                            case EventTypes.OnTransformChange:
                                o.OnTransformChanged -= action;
                                break;
                        }
                    }
                    return subscriptions[e].Remove(action);
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
            if (o != null && !observables.ContainsKey(o.Hash))
            {
                if (subscriptions.ContainsKey(EventTypes.OnDisabled))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnDisabled])
                        o.OnDisabled += onEnableActions;

                if (subscriptions.ContainsKey(EventTypes.OnEnabled))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnEnabled])
                        o.OnEnabled += onEnableActions;

                if (subscriptions.ContainsKey(EventTypes.OnDestroyed))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnDestroyed])
                        o.OnDestroyed += onEnableActions;

                if (subscriptions.ContainsKey(EventTypes.OnTransformChange))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnTransformChange])
                        o.OnTransformChanged += onEnableActions;

                o.OnDestroyed += ObservableDestroyed;
                observables.Add(o.Hash,o);
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
            if (observables.ContainsKey(o.Hash))
            {
                if (subscriptions.ContainsKey(EventTypes.OnDisabled))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnDisabled])
                        o.OnDisabled -= onEnableActions;

                if (subscriptions.ContainsKey(EventTypes.OnEnabled))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnEnabled])
                        o.OnEnabled -= onEnableActions;

                if (subscriptions.ContainsKey(EventTypes.OnDestroyed))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnDestroyed])
                        o.OnDestroyed -= onEnableActions;

                if (subscriptions.ContainsKey(EventTypes.OnTransformChange))
                    foreach (var onEnableActions in subscriptions[EventTypes.OnTransformChange])
                        o.OnTransformChanged -= onEnableActions;

                o.OnDestroyed -= ObservableDestroyed;
                return observables.Remove(o.Hash);
            }
            return false;
        }
        void ObservableDestroyed(int hash)
        {
            RemoveObservable(observables[hash]);
        }
        public enum EventTypes
        {
            OnEnabled,
            OnDisabled,
            OnDestroyed,
            OnTransformChange
        }
    }
}