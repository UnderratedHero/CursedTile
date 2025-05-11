using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public static VolumeControl Instance { get; private set; }

    [SerializeField] private Scrollbar _volumeSlider;
    [SerializeField] private Scrollbar _musicSlider;
    [SerializeField] private AudioSource _audioSource;

    private float _volume = 1f;
    private float _musicVolume = 1f;
    private int _musicSamplePosition = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ApplyVolumesToSliders();

        if (_volumeSlider != null)
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        if (_musicSlider != null)
            _musicSlider.onValueChanged.AddListener(OnMusicChanged);
    }

    private void Update()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _musicSamplePosition = _audioSource.timeSamples;
        }
    }

    private void OnVolumeChanged(float value)
    {
        _volume = value;
    }

    private void OnMusicChanged(float value)
    {
        _musicVolume = value;
    }

    private void ApplyVolumesToSliders()
    {
        if (_volumeSlider != null)
            _volumeSlider.value = _volume;

        if (_musicSlider != null)
            _musicSlider.value = _musicVolume;
    }

    public float GetVolume() => _volume;
    public float GetMusicVolume() => _musicVolume;
    public int GetSavedSamplePosition() => _musicSamplePosition;

    private void OnDestroy()
    {
        if (_volumeSlider != null)
            _volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);

        if (_musicSlider != null)
            _musicSlider.onValueChanged.RemoveListener(OnMusicChanged);
    }
}
