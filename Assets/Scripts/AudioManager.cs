using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _sound;
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void PlaySingle(AudioClip clip)
    {
        _sfxSource.clip = clip;
        _sfxSource.Play();
    }
    public void PlayMusic()
    {
        _musicSource.Play();
    }
    public void EndMusic()
    {
        _musicSource.Pause();
    }
}
