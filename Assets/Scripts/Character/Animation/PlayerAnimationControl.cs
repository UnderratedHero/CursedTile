using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int WalkUp = Animator.StringToHash("Walk Up");
    private static readonly int WalkDown = Animator.StringToHash("Walk Down");
    private static readonly int WalkLeft = Animator.StringToHash("Walk Left");
    private static readonly int WalkRight = Animator.StringToHash("Walk Right");
    private static readonly int Idle = Animator.StringToHash("Idle");
  
    public void SetAnimation(Vector2 direction)
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
