using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IControllable
{
    [SerializeField] public GameObject CharacterObject;
    [SerializeField] public float MoveSpeed = 10.0f;

    public void Move(Vector2 vector)
    {
        CharacterObject.transform.position += new Vector3(vector.x, vector.y) * MoveSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
