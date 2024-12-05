#pragma warning disable
using System.Collections;
using Characters;
using UnityEngine;

namespace Level.Collectables
{
    [RequireComponent(typeof(Animator))]
    public class CollectableCoin : CollectableBase<CollectableCoin>
    {
        [SerializeField]
        private SoundFx collectionSound;
        [SerializeField]
        private AnimationClip collectionClip;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        protected override bool CanCollect(CharacterBase c)
        {
            return true;
        }
        protected override IEnumerator Collect(CharacterBase c)
        {
            SoundFxHandler.Instance.PlaySound(collectionSound, transform.position, 1f);
            anim.SetBool("Collected", true);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
    }
}
