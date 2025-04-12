using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    
    private Health _health;

    private void Start()
    {
        var character = FindObjectOfType<Character>();
        _health = character.GetComponent<Health>();
    }

    private void Update()
    {
        _healthBar.fillAmount = _health.EntityHealth / _health.EntityMaxHealth;
    }
}
