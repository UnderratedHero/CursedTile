using System.Collections;
using UnityEngine;

public class RouletteSpin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _rotatingDelay = 10f;
    [SerializeField] private Rigidbody2D _rigidBody;

    private bool _isRotating = false;
    private bool _isRotateClockwise = true;

    private void Start()
    {
        _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    private void FixedUpdate()
    {
        if (!_isRotating)
            return;

        var torque = _isRotateClockwise ? -_rotationSpeed : _rotationSpeed;
        _rigidBody.AddTorque(torque);
    }

    public void StartRotating(bool rotateClockwise)
    {
        _isRotating = true;
        _isRotateClockwise = rotateClockwise;
        StartCoroutine(StopAfterDelayRoutine(_rotatingDelay));
    }

    private IEnumerator StopAfterDelayRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isRotating = false;
    }
}
