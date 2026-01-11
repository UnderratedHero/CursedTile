using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileDistribution : MonoBehaviour
{
    [SerializeField] private List<TileCluster> _tileClusters;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _rotationThreshold = 0.1f;
    private float _prevAngularVelocity;

    private void Update()
    {
        var currAngularVelocity = _rigidBody.angularVelocity;

        if (_tileClusters.Any(v => v.gameObject.activeInHierarchy == true))
            return;

        if (Mathf.Abs(_rigidBody.angularVelocity) < _rotationThreshold &&
             Mathf.Abs(currAngularVelocity) < Mathf.Abs(_prevAngularVelocity))
        {
            foreach (var cluster in _tileClusters)
                cluster.gameObject.SetActive(true);
        }
        
        _prevAngularVelocity = currAngularVelocity;
    }
}
