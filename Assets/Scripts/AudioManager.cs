using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    // music
    public AudioSource musicSource;
    public AudioClip currentMusic;
    // sfx
    public AudioSource sfxSource;
    public AudioClip[] sfxClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        currentMusic = null;
    }

    public void PlayMusic(AudioClip music)
    {
        if (music == currentMusic)
        {
            return;
        }
        musicSource.clip = music;
        currentMusic = music;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        currentMusic = null;
    }

    public void PlaySFX(int index)
    {
        sfxSource.clip = sfxClips[index];
        sfxSource.Play();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }
}