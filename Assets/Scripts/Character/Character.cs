using Assets.Scripts.Character;
using UnityEngine;

public class Character : MonoBehaviour, IControllable
{
    [SerializeField] private GameObject _charachter;
    [SerializeField] private float _moveSpeed = 10.0f;

    public void Move(Vector2 vector)
    {
        _charachter.transform.position += new Vector3(vector.x, vector.y) * _moveSpeed;
     
    }
}
