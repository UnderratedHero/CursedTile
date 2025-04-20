using UnityEngine;

public class EnemyMoveAnimationControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementThreshold = 0.01f;

    private Vector3 _previousPosition;
    private static readonly int MoveUp = Animator.StringToHash("Walk Up");
    private static readonly int MoveDown = Animator.StringToHash("Walk Down");
    private static readonly int MoveLeft = Animator.StringToHash("Walk Left");
    private static readonly int MoveRight = Animator.StringToHash("Walk Right");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private int _currentTrigger = Idle;

    private void Start()
    {
        _previousPosition = transform.position;
    }

    private void Update()
    {
        Vector3 delta = transform.position - _previousPosition;

        if (delta.magnitude > _movementThreshold)
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                UpdateTrigger(delta.x > 0 ? MoveRight : MoveLeft);
            }
            else
            {
                UpdateTrigger(delta.y > 0 ? MoveUp : MoveDown);
            }
        }
        else
        {
            UpdateTrigger(Idle);
        }

        _previousPosition = transform.position;
    }

    private void UpdateTrigger(int newTrigger)
    {
        if (_currentTrigger == newTrigger)
            return;

        _animator.ResetTrigger(_currentTrigger);
        _animator.SetTrigger(newTrigger);
        _currentTrigger = newTrigger;
    }
}
