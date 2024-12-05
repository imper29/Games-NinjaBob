using UnityEngine;

public class ImpactSoundFx : MonoBehaviour
{
    [SerializeField]
    private SoundFx sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Level.LevelData.TimeSinceLevelStart > 0.3f)
            SoundFxHandler.Instance.PlaySound(sound, transform.position);
    }
}
