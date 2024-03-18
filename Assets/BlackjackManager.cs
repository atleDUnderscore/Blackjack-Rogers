using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlackjackManager : MonoBehaviour
{

    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject gamePanel;
    Card[] usedCards;
    int iRef;
    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false);
        gamePanel.SetActive(false);
        iRef = 0;
        usedCards = new Card[52];
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Gone");
    }

    public void PlayGame()
    {
        Debug.Log("Plays videojuego");
        gamePanel.SetActive(true);
    }

    public void HelpLoad()
    {
        Debug.Log("Help Loaded");
        if(helpPanel.activeSelf)
        {
            helpPanel.SetActive(false);
        }
        else if(!helpPanel.activeSelf)
        {
            helpPanel.SetActive(true);
        }
    }

    public void GenCard()
    {
        int tempCardNum = Random.Range(0, 13);
        Debug.Log("Card Num: " + tempCardNum);
        int tempCardSuitNum = Random.Range(0, 4);
        Debug.Log("Card Suit: " + tempCardSuitNum);
        string tempCardSuit;
        if(tempCardSuitNum == 1)
        {
            tempCardSuit = "Spades";
            Debug.Log("Spades");
        }
        else if(tempCardSuitNum == 2)
        {
            tempCardSuit = "Hearts";
            Debug.Log("Hearts");
        }
        else if( tempCardSuitNum == 3)
        {
            tempCardSuit = "Clubs";
            Debug.Log("Clubs");
        }
        else
        {
            tempCardSuit = "Diamonds";
            Debug.Log("Diamonds");
        }
        Card card = new Card(tempCardNum, tempCardSuit);
        if(usedCards.Contains(card))
        {
            GenCard();
            Debug.Log("Rerolled like kismet");
        }
        else
        {
            usedCards[iRef] = card;
            Debug.Log(card.CardNumber.ToString() + " of " + card.CardSuit);
            iRef++;
        }
    }
}
