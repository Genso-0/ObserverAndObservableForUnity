using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Observables
{
    /// <summary>
    /// Observes gameobjects with the ObservableTransform component. It allows for subscribing and unsubscribing actions from events in an organised fashion.
    /// </summary>
    public class Observer
    {
        public Observer()
        {
            observables = new List<ObservableTransform>();
            subscriptions = new Dictionary<EventTypes, List<ObservableTransform.ObservableChange>>();
        }
        public List<ObservableTransform> observables { get; private set; }
      
        IDictionary<EventTypes, List<ObservableTransform.ObservableChange>> subscriptions;
        public bool AddEventAction(EventTypes e, ObservableTransform.ObservableChange action)
        {
            if (!subscriptions.ContainsKey(e))
                subscriptions.Add(e, new List<ObservableTransform.ObservableChange>());

            if (!subscriptions[e].Contains(action as ObservableTransform.ObservableChange))
            {
                subscriptions[e].Add(action);
                foreach (var o in observables)
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
                            o.OnTransformChange_Args += action;
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
                    foreach (var o in observables)
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
                                o.OnTransformChange_Args -= action;
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
            if (!observables.Contains(o))
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
                        o.OnTransformChange_Args += onEnableActions;

                observables.Add(o);
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
            if (observables.Contains(o))
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
                        o.OnTransformChange_Args -= onEnableActions;

                return observables.Remove(o);
            }
            return false;
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