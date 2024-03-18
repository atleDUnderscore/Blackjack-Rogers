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
    [SerializeField] GameObject cardPrefab;
    Player player;
    Computer com;
    GameObject[] usedCards;

    int iRef;
    int redoNumber;
    string redoSuit;
    //int maxCards = 5;
    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false);
        gamePanel.SetActive(false);
        iRef = 0;
        usedCards = new GameObject[52];
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

    /*public void Redo()
    {
        int rand = Random.Range(1, 3);
        if(rand == 1)
        {
            Debug.Log("Same card different day");
            Card card = new Card(redoNumber, redoSuit);
            foreach (Card c in usedCards)
            {
                if (c.CardNumber == card.CardNumber && c.CardSuit == card.CardSuit)
                {
                    Debug.Log("Rerolled like kismet");
                }
            }
        }
        else if(rand == 2)
        {
            Debug.Log("New Card, not old card");
            GenCard();
        }
        
    }*/

    public void GenCard()
    {
        int tempCardNum = Random.Range(1, 14);
        Debug.Log("Card Num: " + tempCardNum);
        redoNumber = tempCardNum;
        int tempCardSuitNum = Random.Range(1, 5);
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
        redoSuit = tempCardSuit;
        GameObject card = Instantiate(cardPrefab);
        card.GetComponent<Card>().CardSetValue(tempCardNum, tempCardSuit);
        
        Debug.Log(card.GetComponent<Card>().CardSuit);
        if (iRef > 0)
        {
            Debug.Log(usedCards[0].ToString());
            foreach (GameObject c in usedCards)
            {
                if (c.GetComponent<Card>().cardNumber == card.GetComponent<Card>().cardNumber && c.GetComponent<Card>().cardSuit == card.GetComponent<Card>().cardSuit)
                {
                    //GenCard();
                    Debug.Log("Rerolled like kismet");
                }
            }
        }
        usedCards[iRef] = card;

        Debug.Log(card.GetComponent<Card>().CardNumber.ToString() + " of " + card.GetComponent<Card>().CardSuit);
        iRef++;
    }
}
