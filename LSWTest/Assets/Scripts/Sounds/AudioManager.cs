using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    [SerializeField] private Sound[] _sounds;

    private void Awake()
    {
        if (_instance != null )
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        foreach(Sound sound in _sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        Play("Rain");
    }

    public void Play(string id)
    {
        Sound soundToPlay = Array.Find(_sounds, sound => sound.Id == id);

        if (soundToPlay == null)
        {
            Debug.LogWarning($"Sound with id {id} not found");
            return;
        }
        
        soundToPlay.Source.Play();
    }
}
