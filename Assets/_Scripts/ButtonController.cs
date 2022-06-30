using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Figures
{
    Paper,
    Stone,
    Scissors,
}

public class ButtonController : MonoBehaviour
{
    [SerializeField] Figures figure;

    Vector3 startScale;

    void Awake() => startScale = transform.localScale;
    void OnMouseDown() => transform.localScale = startScale * .8f;

    void OnMouseUp()
    {
        transform.localScale = startScale;
        GameManager.Instance.newFigure(figure);
    }
}
