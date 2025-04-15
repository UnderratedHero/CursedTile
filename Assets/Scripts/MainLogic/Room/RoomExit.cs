using System.Linq;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private string _layerName;
    [SerializeField] private Room _currentRoom;

    private EventHandler _onFinalStep;
    private bool _isFinalStep = false;
    private Room _nextRoom;

    public void SetRoom(Room room) 
    {
        _currentRoom = room;
    }

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
        if (other.gameObject.layer != LayerMask.NameToLayer(_layerName) || other == null)
            return;

        if (_isFinalStep)
        {
            _onFinalStep?.TriggerAction();
            return;
        }

        Transform enterTransform = null;
        foreach (Transform child in _nextRoom.transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.name.Contains("Enter"))
            {
                enterTransform = child;
                break;
            }
        }

        other.transform.position = enterTransform.position;
    }
}
