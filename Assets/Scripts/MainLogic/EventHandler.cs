using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private event Action _onActionCalled;

    public event Action OnActionCalled
    {
        add
        {
            _onActionCalled += value;
        }
        remove
        {
            _onActionCalled -= value;
        }
    }

    public void TriggerAction()
    {
        _onActionCalled?.Invoke();
    }
}
