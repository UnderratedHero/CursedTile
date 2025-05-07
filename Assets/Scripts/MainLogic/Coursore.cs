using UnityEngine;

public class Coursore : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        if (cursorTexture == null)
        {
            Debug.LogError("Курсор не назначен! Проверьте поле cursorTexture.");
            return;
        }

        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
