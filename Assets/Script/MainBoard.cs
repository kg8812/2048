using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ButtonEventType
{
    Left,
    Right,Up,Down
}
public class MainBoard : Subject
{
    public Square squarePrefab;
    public NumBlock blockPrefab;

    public readonly Square[,] squares = new Square[4, 4];
    List<Square> squareList = new();

    public float squareSize = 1.8f;

    public bool isMoved = true;
    void Start()
    {
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                Square sq = Instantiate(squarePrefab, transform);              
                sq.transform.localPosition = new Vector2(j * squareSize, i * squareSize);
                squares[i, j] = sq;
                sq.Init(i, j, squares);
                squareList.Add(sq);
            }
        }

        AddNumberBlock();
    }

    public void AddNumberBlock()
    {
        if (!CheckBlockAvailable())
        {
            return;
        }

        NumBlock block = Instantiate(blockPrefab);
        int x = Random.Range(0, squareList.Count);

        while (squareList[x].CurSquare != null)
        {
            x = Random.Range(0, squareList.Count);
        }

        block.transform.SetParent(squareList[x].transform);
        block.transform.localPosition = Vector3.zero;
        squareList[x].CurSquare = block;       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Move(ButtonEventType.Left));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Move(ButtonEventType.Right));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(Move(ButtonEventType.Up));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Move(ButtonEventType.Down));
        }
    }
    IEnumerator Move(ButtonEventType type)
    {
        switch (type)
        {
            case ButtonEventType.Left:
                yield return StartCoroutine(MoveAllLeft()); 
                break;
                case ButtonEventType.Right:
                yield return StartCoroutine(MoveAllRight());
                break;
                case ButtonEventType.Up:
                yield return StartCoroutine(MoveAllUp());
                                break;
                case ButtonEventType.Down:
                yield return StartCoroutine(MoveAllDown());
                                break;
        }
        if (isMoved)
        {
            AddNumberBlock();
        }
        isMoved = false;
    }
    bool CheckBlockAvailable()
    {
        foreach(var x in squareList)
        {
            if (x.CurSquare == null)
                return true;
        }
        return false;
    }

    IEnumerator MoveAllLeft()
    {
        for (int i = 0; i < squares.GetLength(1); i++)
        {
            for (int j = 0; j < squares.GetLength(0); j++)
            {
                StartCoroutine(squares[j,i].MoveLeft());
            }
        }
        yield return new WaitForSeconds(0.04f);
    }

    IEnumerator MoveAllRight()
    {
        for (int i = squares.GetLength(1) - 1; i >= 0; i--)
        {
            for (int j = 0; j < squares.GetLength(0); j++)
            {
                StartCoroutine(squares[j, i].MoveRight());
            }
        }
        yield return new WaitForSeconds(0.04f);
    }

    IEnumerator MoveAllUp()
    {
        for (int i = squares.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
               StartCoroutine(squares[i, j].MoveUp());
            }
        }
        yield return new WaitForSeconds(0.04f);
    }

    IEnumerator MoveAllDown()
    {
        for (int i = 0; i <squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                StartCoroutine(squares[i, j].MoveDown());
            }          
        }
            yield return new WaitForSeconds(0.04f);
    }
}
