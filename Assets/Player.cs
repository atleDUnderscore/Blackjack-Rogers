using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int handTotal;

    int HandTotal
    {
        get { return handTotal;}
        set { handTotal += value; }

    }

    public void SetHandZero()
    {
        handTotal = 0;
    }
}
