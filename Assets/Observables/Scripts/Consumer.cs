 
using System.Collections.Generic;
using UnityEngine;

namespace Observables
{ 
    public class Consumer : MonoBehaviour
    {
        Observer observer;
        public bool gizmosOn;
        [SerializeField] List<ObservableTransform> toBeAdded;
        [SerializeField] List<ObservableTransform> toBeRemoved;
        void Start()
        { 
            observer = new Observer(); 
            //Adding actions we wish to be called when an observable raises an event.
            observer.AddEventAction(Observer.EventTypes.OnDisabled, OnTargetDisabled);
            observer.AddEventAction(Observer.EventTypes.OnDestroyed, OnTargetDestroyed);
            observer.AddEventAction(Observer.EventTypes.OnEnabled, OnTargetEnabled);

            AddObservables();//Note even though we are adding the observables after adding the events, the observer will handle
            //organising the actions to the newly added observables
        }
        [ContextMenu("Add observables")]
        void AddObservables()
        {
            foreach (var o in toBeAdded)
            {
                observer.AddObservable(o);
            }
        }
        [ContextMenu("Remove observables")]
        void RemoveObservables()
        {
            foreach (var o in toBeRemoved)
            {
                observer.RemoveObservable(o);
            }
        }
        void OnTargetDisabled(int hash)
        {
            print($"My target {hash} was disabled");
        }
        void OnTargetDestroyed(int hash)
        {
            print($"My target {hash} was destroyed");
        }
        void OnTargetEnabled(int hash)
        {
            print($"My target {hash} was enabled");
        }
        void OnDrawGizmos()
        {
            if (gizmosOn && observer != null)
            {
                if (observer.observables != null)
                    foreach (var o in observer.observables)
                    {
                        Gizmos.DrawLine(transform.position, o.transform.position);
                    }
            }
        }
    }
}