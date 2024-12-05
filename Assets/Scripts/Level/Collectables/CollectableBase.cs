using Characters;
using System.Collections;
using UnityEngine;

namespace Level.Collectables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class CollectableBase<T> : MonoBehaviour where T : CollectableBase<T>
    {
        public delegate void OnCollectedDelegate(T collectable, int totalCollected);
        public static event OnCollectedDelegate OnCollectedEvent;
        private static int totalCollected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CharacterBase c = collision.GetComponent<CharacterBase>();
            if (c && CanCollect(c))
            {
                totalCollected++;
                OnCollectedEvent?.Invoke(this as T, totalCollected);
                StartCoroutine(Collect(c));
                GetComponent<Collider2D>().enabled = false;
            }
        }

        protected abstract bool CanCollect(CharacterBase c);
        protected abstract IEnumerator Collect(CharacterBase c);
    }
}