#pragma warning disable
using System.Collections;
using UnityEngine;

public class SoundFxHandler : MonoBehaviour
{
    public static SoundFxHandler Instance { get; private set; }

    [SerializeField]
    private AudioSource soundPrefab;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(SoundFx sound, Vector2 position, float loopCount = 1f)
    {
        if (sound.clip)
        {
            AudioSource src = Instantiate(soundPrefab, position, Quaternion.identity);
            src.gameObject.SetActive(true);
            src.clip = sound.clip;
            src.volume = sound.volume;
            src.pitch = sound.pitch;
            src.Play();
            src.time = sound.startTime;
            StartCoroutine(DestroyObj(src.gameObject, sound.clip.length * loopCount - sound.stopTime - sound.startTime));
        }
    }

    IEnumerator DestroyObj(GameObject obj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(obj);
    }
}
