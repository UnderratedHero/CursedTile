using System.Collections;
using UnityEngine;

public class RouletteSpin : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed = 10f; 
    [SerializeField] private float _rotatingDelay = 10f;
    [SerializeField] private float _stoppingDuration = 2f; 
    [SerializeField] private Rigidbody2D _rigidBody;

    private bool _isStarted = false;
    private bool _isRotating = false;
    private bool _isStopping = false;
    private bool _isRotateClockwise = true;
    private float _initialAngularVelocity;

    private void Start()
    {
        _rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    private void FixedUpdate()
    {
        if (_isRotating && !_isStopping)
        {
            var torque = _isRotateClockwise ? -_rotationSpeed : _rotationSpeed;
            _rigidBody.AddTorque(torque);
        }
        else if (_isStopping)
        {
            _rigidBody.angularVelocity = Mathf.Lerp(_rigidBody.angularVelocity, 0f, Time.fixedDeltaTime / _stoppingDuration);

            if (Mathf.Abs(_rigidBody.angularVelocity) < 0.1f)
            {
                _rigidBody.angularVelocity = 0f;
                _isStopping = false;
                _isRotating = false;
            }
        }
    }

    public void StartRotating(bool rotateClockwise)
    {
        _isStarted = true;
        _isRotating = true;
        _isRotateClockwise = rotateClockwise;

        StartCoroutine(StartStoppingRoutine(_rotatingDelay));
    }

    private IEnumerator StartStoppingRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        _isStopping = true;
    }

    public bool IsRotating()
    {
        return _isRotating;
    }

    public bool IsStarted()
    {
        return _isStarted;
    }
}
