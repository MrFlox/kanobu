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

    void OnMouseDown()
    {
        GameManager.Instance.newFigure(figure);
    }


}
