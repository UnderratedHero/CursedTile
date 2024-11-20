using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Camera _mainCam;

    private void Start()
    {
        if (_mainCam == null)
        {
            _mainCam = Camera.main;
        }

        if (_mainCam == null)
        {
            Debug.LogError("Main Camera not found! Please assign a camera.");
        }
    }

    private void Update()
    {
        if (_mainCam == null) return;

        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        var mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;

        var direction = mousePos - transform.position;

        var rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
