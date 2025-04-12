using Cinemachine;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;

    void Start()
    {
        var character = FindObjectOfType<Character>();
        _camera.Follow = character.transform;
    }
   
}
