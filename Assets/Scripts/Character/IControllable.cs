using UnityEngine;

namespace Assets.Scripts.Character
{
    public interface IControllable
    {
        public float LastDashTime { get; }
        public float DashCooldown { get; }
        public bool IsDashing { get; }

        public void Move(Vector2 vector);
        public void Attack();
        public void Unack();

        public void SwitchWeapon();

        public void Dash();
    }
}
