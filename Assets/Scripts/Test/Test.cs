using NavMeshPlus.Components;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;

    private void Start()
    {
        _surface.BuildNavMesh();
    }
}
