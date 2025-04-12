using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.OnEntityDead += DropLoot;
    }

    private void OnDestroy()
    {
        _health.OnEntityDead -= DropLoot;
    }

    private void DropLoot()
    {

    }
}
