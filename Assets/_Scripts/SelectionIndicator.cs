using UnityEngine;

public enum PlayerTypes
{
    player,
    pc
}

public class SelectionIndicator : MonoBehaviour
{
    [SerializeField] Figures figure;
    [SerializeField] PlayerTypes type;
    [SerializeField] Sprite stone, scissors, paper;
    SpriteRenderer currentSprite;

    void Awake()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = null;
        GameManager.OnGameStateChanged += onChangeState;
    }

    private void onChangeState(GameState state)
    {
        if (type == PlayerTypes.player && state == GameState.PlayerStep)
        {

            figure = GameManager.Instance.playerFigure;
            updateImage();
        }
        else if (type == PlayerTypes.pc && state == GameState.PCStep)
        {
            figure = GameManager.Instance.pcFigure;
            updateImage();
        }

        if (type == PlayerTypes.pc && state == GameState.PCTimeout)
        {
            currentSprite.sprite = null;
        }
    }

    private void updateImage()
    {
        switch (figure)
        {
            case Figures.Paper:
                currentSprite.sprite = paper;
                break;
            case Figures.Stone:
                currentSprite.sprite = stone;
                break;
            case Figures.Scissors:
                currentSprite.sprite = scissors;
                break;
        }
    }
}
