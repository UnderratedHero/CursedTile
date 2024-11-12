using System.Collections.Generic;
using UnityEngine;

public class TileDistribution : MonoBehaviour
{
    [SerializeField] private List<TileCluster> _tileClusters;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _rotationThreshold = 0.1f;

    private void Update()
    {
        if (Mathf.Abs(_rigidBody.angularVelocity) > _rotationThreshold)
            return;

        foreach (var cluster in _tileClusters)
            cluster.gameObject.SetActive(true);
    }
}
