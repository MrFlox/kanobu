using System;
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
    [SerializeField] float animationTime = .5f;
    SpriteRenderer currentSprite;

    Vector3 initialScale, initialPosition;
    void Awake()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = null;
        GameManager.OnGameStateChanged += onChangeState;

        //---------
        iTween.Defaults.easeType = iTween.EaseType.easeOutElastic;
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
            transform.localScale = initialScale;
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

        animate();
    }

    private void animate()
    {
        transform.position = new Vector3(initialPosition.x, initialPosition.y + 4, initialPosition.z);
        iTween.ScaleTo(gameObject, initialScale * 2f, animationTime);
        iTween.MoveTo(gameObject, initialPosition, animationTime);
    }
}
