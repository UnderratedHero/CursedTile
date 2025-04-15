using System;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    public event Action<Collider2D> OnEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(collision);
    }
}
