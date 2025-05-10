using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Camera _mainCam;
    private Vector3 _direction;

    public Vector3 Direction {  get { return _direction; } }


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
        var mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(_mainCam.transform.position.z - transform.position.z);
        mousePos = _mainCam.ScreenToWorldPoint(mousePos);

        _direction = mousePos - transform.position;

        if (_direction.sqrMagnitude > 0.001f)
        {
            float rotZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}
