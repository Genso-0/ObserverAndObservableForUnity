
using UnityEngine;
namespace Observables
{
    /// <summary>
    /// This class is used to fire events on enable, disable, destroy and transform move. An observer class is necessary on the class you wish to observe this class with.
    /// </summary>
    public class ObservableTransform : MonoBehaviour
    {
        public delegate void ObservableChange(int hash);
        public event ObservableChange OnTransformChange_Args;
        public event ObservableChange OnDisabled;
        public event ObservableChange OnEnabled;
        public event ObservableChange OnDestroyed;
        public delegate void TransformChange();
        public event TransformChange OnTransformChange;
        int m_hash;
        public int Hash { get { return GetHash(); } }
 
        int GetHash()
        {
            if (m_hash == 0)
                m_hash = GetHashCode();
            return m_hash;
        }
        void LateUpdate()
        {
            if (transform.hasChanged)
            {
                transform.hasChanged = false;
                OnTransformChange?.Invoke();
                OnTransformChange_Args?.Invoke(Hash); 
            }
        }
        void OnEnable()
        {
            OnEnabled?.Invoke(Hash);
        }
        void OnDisable()
        {
            OnDisabled?.Invoke(Hash);
        }
        void OnDestroy()
        {
            OnDestroyed?.Invoke(Hash);
        }
    }
}