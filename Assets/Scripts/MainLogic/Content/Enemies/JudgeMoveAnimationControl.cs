using UnityEngine;

public class JudgeMoveAnimationControl : AnimationControllerBase
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementThreshold = 0.01f;

    private Vector3 _previousPosition;
    private static readonly int WalkUp = Animator.StringToHash("JudgeWalkUp");
    private static readonly int WalkDown = Animator.StringToHash("JudgeWalkDown");
    private static readonly int WalkLeft = Animator.StringToHash("JudgeWalkLeft");
    private static readonly int WalkRight = Animator.StringToHash("JudgeWalkRight");
    private static readonly int Idle = Animator.StringToHash("JudgeIdle");
    private static readonly int MeleeUp = Animator.StringToHash("JudgeMeleeUp");
    private static readonly int MeleeDown = Animator.StringToHash("JudgeMeleeDown");
    private static readonly int MeleeLeft = Animator.StringToHash("JudgeMeleeLeft");
    private static readonly int MeleeRight = Animator.StringToHash("JudgeMeleeRight");

    private int _currentTrigger = Idle;

    private void Start()
    {
        _previousPosition = transform.position;
    }

    public override void SetAttackAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    SetAnimationTrigger(MeleeRight);
                }
                else
                {
                    SetAnimationTrigger(MeleeLeft);
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    SetAnimationTrigger(MeleeUp);
                }
                else
                {
                    SetAnimationTrigger(MeleeDown);
                }
            }
        }
        else
        {
            SetAnimationTrigger(Idle);
        }
    }

    private void SetWalkAnimation(Vector2 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    SetAnimationTrigger(WalkRight);
                }
                else
                {
                    SetAnimationTrigger(WalkLeft);
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    SetAnimationTrigger(WalkUp);
                }
                else
                {
                    SetAnimationTrigger(WalkDown);
                }
            }
        }
        else
        {
            SetAnimationTrigger(Idle);
        }
    }

    private void Update()
    {
        Vector3 delta = transform.position - _previousPosition;

        SetWalkAnimation(delta);

        _previousPosition = transform.position;
    }

    private void SetAnimationTrigger(int triggerHash)
    {
        _animator.ResetTrigger(WalkUp);
        _animator.ResetTrigger(WalkDown);
        _animator.ResetTrigger(WalkLeft);
        _animator.ResetTrigger(WalkRight);
        _animator.ResetTrigger(Idle);

        _animator.SetTrigger(triggerHash);
    }
}
