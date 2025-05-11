using TMPro;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killsAmountText;
    
    private static int _killsAmount;

    private void OnEnable()
    {
        _killsAmountText.text = _killsAmount.ToString();
    }

    public static void AddKill()
    {
        _killsAmount++;
    }
}
