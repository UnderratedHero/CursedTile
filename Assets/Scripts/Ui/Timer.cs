using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _runText;
    [SerializeField] private float _startMinutes = 1;

    [Header("Pulse Animation Settings")]
    [SerializeField] private float _pulseFrequency = 3f;
    [SerializeField] private float _pulseAmplitude = 0.1f;

    private float _countdownDuration;
    private float _endTime;
    private bool _finished;

    private void Start()
    {
        _countdownDuration = _startMinutes * 60f;
        _endTime = Time.time + _countdownDuration;
    }

    private void Update()
    {
        AnimateText();
        if (_finished) return;

        float timeLeft = _endTime - Time.time;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            _finished = true;
            _runText.gameObject.SetActive(true);
        }

        int minutes = (int)(timeLeft / 60);
        int seconds = (int)(timeLeft % 60);

        _text.text = $"{minutes:00}:{seconds:00}";
    }

    private void AnimateText()
    {
        if (!_finished) return;

        float pulse = 1f + Mathf.Sin(Time.time * _pulseFrequency) * _pulseAmplitude;
        _text.transform.localScale = new Vector3(pulse, pulse, 1f);
    }
}
