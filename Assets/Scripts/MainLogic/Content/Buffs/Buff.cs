using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private List<BuffConfig> _configs;
    
    private BuffConfig _currentConfig;

    public BuffConfig CurrentConfig { get { return _currentConfig; } }

    private void Start()
    {
        int random = Random.Range(0, _configs.Count);
        _currentConfig = _configs[random];
    }

    public void UseConfig(GameObject characterObject)
    {
        var character = characterObject.GetComponent<Character>();
        character.SetBuff(_currentConfig);
    }
}
