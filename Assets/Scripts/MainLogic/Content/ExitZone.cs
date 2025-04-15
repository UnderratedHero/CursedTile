using System;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public event Action<Collider2D> OnExit;

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit?.Invoke(collision);
    }
}
