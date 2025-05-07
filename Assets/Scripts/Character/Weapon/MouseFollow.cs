using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;

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
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(_mainCam.transform.position.z - transform.position.z);
        mousePos = _mainCam.ScreenToWorldPoint(mousePos);

        Vector3 direction = mousePos - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}
