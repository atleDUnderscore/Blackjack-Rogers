using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int handTotal;
    public GameObject[] hand;
    int iRef;

    /*public Player(int handCount)
    {
        hand = new GameObject[handCount];
        iRef = 0;
    }*/

    int HandTotal
    {
        get { return handTotal;}
        set { handTotal += value; }

    }

    public void SetHandZero()
    {
        handTotal = 0;
        iRef = 0;
    }

    public void AddToHand(GameObject card)
    {
        if (iRef <= hand.Length)
        {
            hand[iRef] = card;
            iRef++;
        }
    }

    public void SetHandSize(int size)
    {
        hand = new GameObject[size];
    }


}
