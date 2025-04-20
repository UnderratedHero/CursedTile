using System;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    public event Action<Collider2D> OnEnter;
    public event Action<Collider2D> OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit?.Invoke(collision);
    }
}
