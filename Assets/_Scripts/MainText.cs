using TMPro;
using UnityEngine;

public class MainText : MonoBehaviour
{
    TMP_Text text;
    void Awake()
    {
        text = GetComponent<TMP_Text>();
        GameManager.OnGameStateChanged += onStateChanged;
    }

    private void onStateChanged(GameState state)
    {
        if (state == GameState.PCTimeout)
            text.text = "";
    }
}
