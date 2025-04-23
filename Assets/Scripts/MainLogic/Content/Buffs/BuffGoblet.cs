using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class BuffGoblet : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _dicePrefab;
    [SerializeField] private List<Transform> _dicePoints;
    [SerializeField] private Buff _buff;

    private Dictionary<int, GameObject> _occupiedPointInexes = new Dictionary<int, GameObject>();
    private GameObject _player;

    private const string Ui = "Ui";
    private const string QuestonModalUi = "QuestonModal";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Character>(out var character))
            return;

        _text.text = _buff.CurrentConfig.BuffName + $" To get buff roll: {_buff.CurrentConfig.Points}";
        _ui.SetActive(true);
        foreach (var dice in _occupiedPointInexes)
            dice.Value.SetActive(true);

        _player = collision.gameObject;
        SpawnDice(0);
    }    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Character>(out var character))
            return;

        _ui.SetActive(false);
        foreach (var dice in _occupiedPointInexes)
            dice.Value.SetActive(false);

        _player = null;
    }

    private void SpawnDice(int index)
    {
        if (_occupiedPointInexes.ContainsKey(index) )
            return;

        var dice = Instantiate(_dicePrefab, _dicePoints[index].position, Quaternion.identity, transform);
        _occupiedPointInexes.TryAdd(index, dice);
    }

    public void ByuDice()
    {
        var character = _player.GetComponent<Character>();
     
        if (character.Health.EntityHealth <= 10 ||  _occupiedPointInexes.Count == _dicePoints.Count)
            return;
        
        character.GetDiceDamage(_buff.CurrentConfig.Cost);
        SpawnDice(_occupiedPointInexes.Count);
    }

    public void RollAllDices()
    {
        var resultSum = 0;
        var completedRolls = 0;
        var totalRolls = _occupiedPointInexes.Count;

        foreach (var diceObject in _occupiedPointInexes.Values)
        {
            var dice = diceObject.GetComponent<Dice>();
            dice.RollTheDiceRoutine(v =>
            {
                resultSum += v;
                completedRolls++;

                if (completedRolls == totalRolls)
                {
                    OnAllDiceRolledAsync(resultSum); 
                }
            });
        }
    }

    private async Task OnAllDiceRolledAsync(int resultSum)
    {
        if (_buff.CurrentConfig.Points > resultSum)
        {
            Destroy(gameObject);
            return;
        }

        _buff.UseConfig(_player);

        await Task.Delay(1000);
        Destroy(gameObject);
    }
}
