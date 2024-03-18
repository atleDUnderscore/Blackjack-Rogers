using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardNumber;
    public string cardSuit;

    public Card(int cardNum, string cardSuite)
    {
        cardNumber = cardNum;
        cardSuit = cardSuite;
    }

    public void CardSetValue(int cardNum, string cardSuite)
    {
        cardNumber = cardNum;
        cardSuit = cardSuite;
    }
    
    public int CardNumber
    { get { return cardNumber; } set {  cardNumber = value; } }
    public string CardSuit
    { get { return cardSuit; } set { cardSuit = value; } }
}   
