using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Health _health;

    private void Update()
    {
        _healthBar.fillAmount = _health.EntityHealth / _health.EntityMaxHealth;
    }
}
