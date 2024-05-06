using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private int x;
    private int y;

    private Grid grid;
    private Grid.PieceType type;
    private ColorPiece colorComponent;
    private MovablePiece movableComponent;
    private ClearablePiece clearableComponent;

    public int X {
        get { return x; }
        set {
            if (IsMovable()) {
                x = value;
            }
        }
    }

    public int Y {
        get { return y; }
        set {
            if (IsMovable()) {
                y = value;
            }
        }
    }


    public Grid.PieceType Type => type; 
    public Grid GridRef => grid; 
    public MovablePiece MovableComponent => movableComponent; 
    public ColorPiece ColorComponent => colorComponent; 
    public ClearablePiece ClearableComponent => clearableComponent; 

    private void Awake()
    {
        movableComponent = GetComponent<MovablePiece>(); 
        colorComponent = GetComponent<ColorPiece>();
        clearableComponent = GetComponent<ClearablePiece>();
    }

    public void Init(int _x, int _y, Grid _grid, Grid.PieceType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    void OnMouseEnter()
    {
        grid.EnterP(this);
    }

    void OnMouseDown()
    {
        grid.PressP(this);
    }

    void OnMouseUp()
    {
        grid.RelP();
    }

    public bool IsMovable()
    {
        return movableComponent != null;
    }

    public bool IsColored()
    {
        return colorComponent != null;
    }

    public bool IsClearable()
    {
        return clearableComponent != null;
    }
}
