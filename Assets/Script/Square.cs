using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public NumBlock CurSquare;

    public int index1;
    public int index2;
    Square[,] squares;
    MainBoard board;

    private void Start()
    {
        board = FindAnyObjectByType<MainBoard>();
    }
    public void Init(int x, int y, Square[,] squares)
    {
        index1 = x;
        index2 = y;
        this.squares = squares;
    }

    IEnumerator Move(int x, int y)
    {
        float lerpTime = 0.04f;
        float time = 0;
        Vector3 start = CurSquare.transform.position;
        Vector3 end = squares[x, y].transform.position;

        NumBlock nb = CurSquare;
        CurSquare = null;
        squares[x, y].CurSquare = nb;

        while (time < lerpTime)
        {
            time += Time.deltaTime;

            nb.transform.position = Vector2.Lerp(start, end, time / lerpTime);
            yield return new WaitForFixedUpdate();
        }       
    }
    public IEnumerator MoveRight()
    {
        if (!CurSquare) yield break;

        int y = index2;
        for (int i = index2 + 1; i < squares.GetLength(1); i++)
        {
            if (squares[index1, i].CurSquare)
            {
                if (!squares[index1, i].CurSquare.isReserved && squares[index1, i].CurSquare.Num == CurSquare.Num)
                {
                    NumBlock nb = squares[index1, i].CurSquare;
                    CurSquare.isReserved = true;
                    board.isMoved = true;
                    CurSquare.BeforeCombine();
                    yield return StartCoroutine(Move(index1, i));                   
                    squares[index1, i].CurSquare.Combine();
                    Destroy(nb.gameObject);
                    yield break;
                }
                break;
            }
            y = i;

        }

        if (y != index2) board.isMoved = true;

        yield return StartCoroutine(Move(index1, y));
    }

    public IEnumerator MoveLeft()
    {
        if (!CurSquare) yield break;

        int y = index2;
        for (int i = index2 - 1; i >= 0; i--)
        {
            if (squares[index1, i].CurSquare)
            {
                if (!squares[index1, i].CurSquare.isReserved && squares[index1, i].CurSquare.Num == CurSquare.Num)
                {
                    NumBlock nb = squares[index1, i].CurSquare;
                    CurSquare.isReserved = true;

                    board.isMoved = true;
                    CurSquare.BeforeCombine();

                    yield return StartCoroutine(Move(index1, i));
                    squares[index1, i].CurSquare.Combine();
                    Destroy(nb.gameObject);

                    yield break;
                }
                break;
            }
            y = i;
        }

        if (y != index2) board.isMoved = true;
        yield return StartCoroutine(Move(index1, y));
    }

    public IEnumerator MoveDown()
    {
        if (!CurSquare) yield break;

        int y = index1;
        for (int i = index1 - 1; i >= 0; i--)
        {
            if (squares[i, index2].CurSquare)
            {
                if (!squares[i, index2].CurSquare.isReserved && squares[i, index2].CurSquare.Num == CurSquare.Num)
                {
                    NumBlock nb = squares[i, index2].CurSquare;
                    CurSquare.isReserved = true;

                    board.isMoved = true;
                    CurSquare.BeforeCombine();

                    yield return StartCoroutine(Move(i, index2));
                    squares[i, index2].CurSquare.Combine();
                    Destroy(nb.gameObject);

                    yield break;
                }
                break;
            }
            y = i;

        }

        if (y != index1) board.isMoved = true;


        yield return StartCoroutine(Move(y, index2));
    }

    public IEnumerator MoveUp()
    {
        if (!CurSquare) yield break;

        int y = index1;
        for (int i = index1 + 1; i < squares.GetLength(0); i++)
        {
            if (squares[i, index2].CurSquare)
            {
                if (!squares[i, index2].CurSquare.isReserved && squares[i, index2].CurSquare.Num == CurSquare.Num)
                {
                    NumBlock nb = squares[i, index2].CurSquare;
                    board.isMoved = true;
                    CurSquare.isReserved = true;

                    CurSquare.BeforeCombine();

                    yield return StartCoroutine(Move(i, index2));
                    squares[i, index2].CurSquare.Combine();
                    Destroy(nb.gameObject);

                    yield break;
                }
                break;
            }
            y = i;

        }
        if (y != index1)
            board.isMoved = true;

        yield return StartCoroutine(Move(y, index2));
    }
}
