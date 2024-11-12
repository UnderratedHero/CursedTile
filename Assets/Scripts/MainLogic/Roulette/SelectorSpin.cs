using UnityEngine;

public class SelectorSpin : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _slowingLimit = 100f;
    [SerializeField] private float returnSpeed = 5f;
    private Vector3 _fixedPosition; 

    private void Awake()
    {
        _fixedPosition = transform.position;
    }

    private void Update()
    {
        transform.position = _fixedPosition;

        if (Mathf.Abs(_rigidBody.angularVelocity) > _slowingLimit)
            return;

        var newZRotation = Mathf.LerpAngle(transform.eulerAngles.z, 0, Time.deltaTime * returnSpeed);
        transform.rotation = Quaternion.Euler(0, 0, newZRotation);

        _rigidBody.angularVelocity = Mathf.Lerp(_rigidBody.angularVelocity, 0, Time.deltaTime * returnSpeed);
    }
}
