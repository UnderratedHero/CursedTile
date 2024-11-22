using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Health _health;

    private GameObject _exit;

    private void OnEnable()
    {
        _health.OnEntityDead += SetActive;
    }

    void Start()
    {
        var parentTransform = transform.parent.parent;
        var room = parentTransform.GetComponent<Room>();
        _exit = room.ExitPoint;
    }

    private void OnDestroy()
    {
        _health.OnEntityDead -= SetActive;
    }

    private void SetActive()
    {
        _exit.SetActive(true);
    }
}
