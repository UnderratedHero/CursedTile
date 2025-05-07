using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] private List<GameObject> _loot;
    [SerializeField] private Health _health;

    [Range(0, 100)]
    [SerializeField] private float _lootDropChance = 10f;

    private void OnEnable()
    {
        _health.OnEntityDead += SpawnLoot;
    }

    private void SpawnLoot()
    {
        var random = Random.Range(0, 100);
        if (random < _lootDropChance)
            return;

        var randomItem = Random.Range(0, _loot.Count);
        Instantiate(_loot[randomItem], transform.position, Quaternion.identity);
    }

    private void OnDisable()
    {
        _health.OnEntityDead -= SpawnLoot;
    }
}
