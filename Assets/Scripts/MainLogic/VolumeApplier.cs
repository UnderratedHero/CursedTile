using UnityEngine;

public class VolumeApplier : MonoBehaviour
{
    [SerializeField] private AudioSource _soundAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private bool _isNeedToContinueTrack;

    private void Start()
    {
        if (VolumeControl.Instance == null)
        {
            Debug.LogWarning("VolumeControl instance not found. VolumeApplier skipped.");
            return;
        }

        if (_soundAudioSource != null)
            _soundAudioSource.volume = VolumeControl.Instance.GetVolume();

        if (_musicAudioSource != null && _isNeedToContinueTrack)
        {
            _musicAudioSource.volume = VolumeControl.Instance.GetMusicVolume();

            _musicAudioSource.timeSamples = VolumeControl.Instance.GetSavedSamplePosition();
            _musicAudioSource.Play();
        }
    }
}
