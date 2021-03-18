using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _slider;
    
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true, 60);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateMixerVolume()
    { 
        _audioMixer.audioMixer.SetFloat("volume", _slider.value);
    }
}
