using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public static MusicHandler Instance { get; private set; }
    private AudioSource audioSource;
    private AudioListener listener;

    private Music activeMusic;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        listener = GetComponent<AudioListener>();

        Level.LevelData.OnLevelLoaded += LevelData_OnLevelLoaded;
    }

    private void LevelData_OnLevelLoaded(Level.LevelData lvl, bool reloading)
    {
        PlayMusic(lvl.music);
    }

    public void PlayMusic(Music music)
    {
        if (!activeMusic.Equals(music))
        {
            audioSource.Stop();
            audioSource.clip = music.audio;
            audioSource.Play();

            activeMusic = music;
        }
    }
}
[System.Serializable]
public struct Music
{
    public AudioClip audio;
}
