using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] diceSides;
    [SerializeField] private int[] chances;
    [SerializeField] private SpriteRenderer rend;

    public void RollTheDiceRoutine(System.Action<int> onRollComplete)
    {
        StartCoroutine(RollTheDice(onRollComplete));
    }

    private IEnumerator RollTheDice(System.Action<int> onRollComplete)
    {
        for (int i = 0; i <= 20; i++)
        {
            int randomDiceSide = Random.Range(0, diceSides.Length);
            rend.sprite = diceSides[randomDiceSide];
            yield return new WaitForSeconds(0.05f);
        }

        int finalSide = GetRandomWeightedSide();
        onRollComplete?.Invoke(finalSide);
    }

    private int GetRandomWeightedSide()
    {
        int totalWeight = 0;
        foreach (var chance in chances)
        {
            totalWeight += chance;
        }

        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < chances.Length; i++)
        {
            cumulativeWeight += chances[i];
            if (randomValue < cumulativeWeight)
            {
                return i + 1;
            }
        }

        return chances.Length;
    }
}