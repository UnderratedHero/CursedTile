using UnityEngine;

namespace Assets.Scripts.Character
{
    public interface IControllable
    {
        public void Move(Vector2 vector);
        public void Attack();
        public void Unack();

        public void SwitchWeapon();
    }
}
