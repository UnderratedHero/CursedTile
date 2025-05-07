using UnityEngine;

public class SelectorSpin : MonoBehaviour
{
    [SerializeField] private RouletteSpin _rouletteSpin;
    [SerializeField] private TileSelector _tileSelector;

    private void Start()
    {
        if (TileDataManager.Instance == null)
            return;

        TileDataManager.SelfDestroy();
    }

    private void Update()
    {
        if (_rouletteSpin.IsRotating())
            return;

        if (_rouletteSpin.IsStarted())
            _tileSelector.gameObject.SetActive(true);
    }
}
