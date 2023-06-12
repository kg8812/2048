using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumBlock : MonoBehaviour
{   
    public int Num { get; private set; }
    public bool isReserved = false;
    SpriteRenderer sp;
    TextMeshPro text;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        sp = GetComponent<SpriteRenderer>();
        int rand = Random.Range(1, 3);

        Num = rand == 1 ? 2 : 4;
        text.text = Num.ToString();
    }

    public void BeforeCombine()
    {
        sp.sortingOrder = 1;
        text.sortingOrder= 1;
    }
    private void Update()
    {
        text.text = Num.ToString();
    }
    public void Combine()
    {
        Num *= 2;
        isReserved = false;
        sp.sortingOrder = 2;
        text.sortingOrder = 3;
    }
}
