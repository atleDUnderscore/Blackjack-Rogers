using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int handTotal;
    public Card[] hand;
    int iRef;

    //public Player(int handCount)
    //{
        //hand = new Card[handCount];
        //iRef = 0;
    //}

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

    public void AddToHand(Card card)
    {
        if(iRef <= hand.Length)
        {
            hand[iRef] = card;
            iRef++;
        }
    }

    
}
