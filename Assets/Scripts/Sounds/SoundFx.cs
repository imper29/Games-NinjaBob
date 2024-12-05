using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class SoundFx : ScriptableObject
{
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(-3f, 3f)]
    public float pitch;
    public float startTime, stopTime;
}
