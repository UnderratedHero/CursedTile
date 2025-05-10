using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int WalkUp = Animator.StringToHash("Walk Up");
    private static readonly int WalkDown = Animator.StringToHash("Walk Down");
    private static readonly int WalkLeft = Animator.StringToHash("Walk Left");
    private static readonly int WalkRight = Animator.StringToHash("Walk Right");
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int MeleeUp = Animator.StringToHash("Melee Up");
    private static readonly int MeleeDown = Animator.StringToHash("Melee Down");
    private static readonly int MeleeLeft = Animator.StringToHash("Melee Left");
    private static readonly int MeleeRight = Animator.StringToHash("Melee Right");  
    private static readonly int DistanceUp = Animator.StringToHash("Distance Up");
    private static readonly int DistanceDown = Animator.StringToHash("Distance Down");
    private static readonly int DistanceLeft = Animator.StringToHash("Distance Left");
    private static readonly int DistanceRight = Animator.StringToHash("Distance Right");

    public void SetAttackAnimation(Vector2 direction, WeaponType type)
    {
        if (type == WeaponType.Melee)
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
        else
        {
            if (direction.magnitude > 0.01f)
            {
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                    {
                        SetAnimationTrigger(DistanceRight);
                    }
                    else
                    {
                        SetAnimationTrigger(DistanceLeft);
                    }
                }
                else
                {
                    if (direction.y > 0)
                    {
                        SetAnimationTrigger(DistanceUp);
                    }
                    else
                    {
                        SetAnimationTrigger(DistanceDown);
                    }
                }
            }
            else
            {
                SetAnimationTrigger(Idle);
            }
        }
    }

    public void SetWalkAnimation(Vector2 direction)
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
