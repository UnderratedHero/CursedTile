using TMPro;
using UnityEngine;

public class ArrowsAmountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Character _character;


    private void Start()
    {
        _character = FindObjectOfType<Character>();

    }

    private void Update()
    {
        _text.text = _character.ArrowsAmount.ToString() + "/10";
    }

}
