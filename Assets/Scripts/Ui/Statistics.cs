using TMPro;
using UnityEditor;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    public static Statistics Instance { get; private set; }

    private TextMeshProUGUI _killsAmountText;
    private TextMeshProUGUI _roomsAmountText;
    
    private static int _killsAmount;
    private static int _roomsAmount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private TextMeshProUGUI FindText(string objectName)
    {
        var tmps = FindObjectsOfType<TextMeshProUGUI>(true);
        foreach (var tmp in tmps)
        {
            if (tmp.name == objectName)
                return tmp;
        }
        return null;
    }

    public void SetInfo()
    {
        if (_killsAmountText == null||  _roomsAmountText == null)
        {
            _killsAmountText = FindText("KillsAmount");
            _roomsAmountText = FindText("RoomsAmount");
        }

        _killsAmountText.text = _killsAmount.ToString();
        _roomsAmountText.text = _roomsAmount.ToString();
    }

    public static void AddKill()
    {
        _killsAmount++;
    }

    public static void AddRoom()
    {
        _roomsAmount++;
    }
}
