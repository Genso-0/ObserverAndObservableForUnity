
using System.Collections.Generic;
using UnityEngine;
namespace Observables
{
    /// <summary>
    /// This class is used to fire events on enable, disable, destroy and transform move. An observer class is necessary on the class you wish to observe this class with.
    /// </summary>
    public class ObservableTransform : MonoBehaviour
    {
        private IDictionary<ObservableEventTypes, ObservableChange> table = new Dictionary<ObservableEventTypes, ObservableChange>();
        public delegate void ObservableChange(int hash);
        private event ObservableChange OnTransformChanged;
        private event ObservableChange OnDisabled;
        private event ObservableChange OnEnabled;
        private event ObservableChange OnDestroyed;
        private int m_hash; 
        private bool initialised;
        void Init()
        {
            table.Add(ObservableEventTypes.OnTransformChange, OnTransformChanged);
            table.Add(ObservableEventTypes.OnDisabled, OnDisabled);
            table.Add(ObservableEventTypes.OnEnabled, OnEnabled);
            table.Add(ObservableEventTypes.OnDestroyed, OnDestroyed);
            m_hash = GetHashCode();
            initialised = true;
        }
        public void AddAction(ObservableEventTypes e, ObservableChange action)
        {
            if (!initialised) 
                Init(); 
            table[e] += action;
        }
        public void RemoveAction(ObservableEventTypes e, ObservableChange action)
        {
            if (!initialised) 
                Init(); 
            table[e] -= action;
        }
        public int GetHash()
        {
            if (!initialised) 
                Init(); 
            return m_hash;
        }
        void LateUpdate()
        {
            if (transform.hasChanged)
            {
                transform.hasChanged = false;
                table[ObservableEventTypes.OnTransformChange]?.Invoke(m_hash);
            }
        }
        void OnEnable()
        {
            if (!initialised) 
                Init(); 
            table[ObservableEventTypes.OnEnabled]?.Invoke(m_hash);
        }
        void OnDisable()
        { 
            table[ObservableEventTypes.OnDisabled]?.Invoke(m_hash);
        }
        void OnDestroy()
        { 
            table[ObservableEventTypes.OnDestroyed]?.Invoke(m_hash);
        }
    }
}