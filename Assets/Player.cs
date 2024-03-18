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

    public int HandTotal
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

    public void GetHandTotal()
    {
        handTotal = 0;
        int tempAdd = 0;
        GameObject[] tempAceArray = new GameObject[4];
        int i = 0;
        foreach (GameObject card in hand)
        {
            if(card != null)
            {
                if(card.GetComponent<Card>().CardNumber == 1)
                {
                    tempAceArray[i] = card;
                    i++; 
                }
                else if(card.GetComponent<Card>().CardNumber == 11 || card.GetComponent<Card>().CardNumber == 12 || card.GetComponent<Card>().CardNumber == 13)
                {
                    tempAdd += 10;
                }
                else
                {
                    tempAdd += card.GetComponent<Card>().CardNumber;
                }
                
            }
        }
        foreach(GameObject card in tempAceArray)
        {
            int tempAceTotalOne = tempAdd + 1;
            int tempAceTotalElev = tempAdd + 11;
            if(card != null)
            {
                if (tempAceTotalElev <= 21)
                {
                    tempAdd += 11;
                }
                else if (tempAceTotalElev > 21)
                {
                    tempAdd += 1;
                }
            }
        }
        handTotal = tempAdd;
    }


}
