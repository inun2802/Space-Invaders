using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() 
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            if (s.clip == null)
            {
                Debug.LogWarning("Audio clip is not assigned for sound: " + s.name);
                continue;
            }

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start ()
    {
        Play("MainTheme");
        Play("MainTheme2");
        Play("FinalBoss");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound with name " + name + " not found.");
            return;
        }
        s.source.Play();
    }

    public void StopBackgroundMusic()
    {
        foreach (Sound bgMusic in sounds)
        {
            if (bgMusic.name == "MainTheme" || bgMusic.name == "MainTheme2" || bgMusic.name == "FinalBoss")
            {
                if (bgMusic.source.isPlaying)
                {
                    bgMusic.source.Stop();
                }
            }
        }
    }

}
