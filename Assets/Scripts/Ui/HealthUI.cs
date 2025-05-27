using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _deathUi;
    [SerializeField] private Statistics _stats;

    private Health _health;

    private void OnDisable()
    {
        _health.OnEntityDead -= SetDeathUIActive;
    }

    private void Start()
    {
        var character = FindObjectOfType<Character>();
        _health = character.GetComponent<Health>();

        _health.OnEntityDead += SetDeathUIActive;
    }

    private void Update()
    {
        _healthBar.fillAmount = _health.EntityHealth / _health.EntityMaxHealth;
    }

    private void SetDeathUIActive()
    {
        _stats.SetInfo();
        _deathUi.SetActive(true);
    }
}
