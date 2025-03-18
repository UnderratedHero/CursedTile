using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Scrollbar _volumeSlider;
    [SerializeField] private Scrollbar _musicSlider;

    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _volumeAudioSource;

    private void Start()
    {
        if (_volumeSlider == null || _musicAudioSource == null)
        {
            Debug.LogError("VolumeControl: Slider или AudioSource не назначены.");
            return;
        }

        _volumeSlider.value = _volumeAudioSource.volume;
        _musicSlider.value = _musicAudioSource.volume;

        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusic);
    }

    private void ChangeVolume(float value)
    {
        _volumeAudioSource.volume = value;
        
    }

    private void ChangeMusic(float value)
    {
        _musicAudioSource.volume = value;
    }

    private void OnDestroy()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        }

        if (_musicSlider != null)
        {
            _musicSlider.onValueChanged.RemoveListener(ChangeMusic);
        }
    }
}