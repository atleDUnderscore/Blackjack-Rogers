using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : Player
{
    int maxTotal;
    
    public int MaxTotal
    { get { return maxTotal; } }

    public Computer(int inputMaxTotal)
    {
        maxTotal = inputMaxTotal;
    }

}
