using System.Linq;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private Room _currentRoom;

    private EventHandler _onFinalStep;
    private bool _isFinalStep = false;
    private Room _nextRoom;

    private void Start()
    {
        if (!transform.root.TryGetComponent<RoomPlacer>(out var placer) ||
            !transform.root.TryGetComponent(out _onFinalStep))
            return;

        if (placer.Rooms.Count - 1 == _currentRoom.Id)
        {
            _isFinalStep = true;
            return;
        }

        _nextRoom = placer.Rooms.FirstOrDefault(v => v.Id == _currentRoom.Id + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isFinalStep)
        {
            _onFinalStep?.TriggerAction();
            return;
        }    

        if (other == null)
            return;

        other.transform.position = _nextRoom.EnterPoint.transform.position;
    }
}
