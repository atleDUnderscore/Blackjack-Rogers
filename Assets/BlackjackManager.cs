using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class BlackjackManager : MonoBehaviour
{

    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] TMP_Text handText;
    Player player;
    Computer com;
    GameObject[] usedCards;

    int iRef;
    int pRef;
    int redoNumber;
    string redoSuit;
    int maxCards = 5;
    // Start is called before the first frame update
    void Start()
    {
        helpPanel.SetActive(false);
        gamePanel.SetActive(false);
        iRef = 0;
        usedCards = new GameObject[52];
        handText.text = "";
        pRef = 0;
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
        player = new Player();
        player.SetHandSize(maxCards);
        AddToPlayer();
        AddToPlayer();

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

    public void AddToPlayer()
    {
        if (pRef < 5)
        {
            GameObject c;
            GenCard();
            c = usedCards[iRef - 1];
            if (c.GetComponent<Card>().CardNumber == 1)
            {
                handText.text += (" Ace" + " of " + c.GetComponent<Card>().CardSuit);
            }
            else if (c.GetComponent<Card>().CardNumber == 11)
            {
                handText.text += (" Jack" + " of " + c.GetComponent<Card>().CardSuit);
            }
            else if (c.GetComponent<Card>().CardNumber == 12)
            {
                handText.text += (" Queen" + " of " + c.GetComponent<Card>().CardSuit);
            }
            else if (c.GetComponent<Card>().CardNumber == 13)
            {
                handText.text += (" Queen" + " of " + c.GetComponent<Card>().CardSuit);
            }
            else
            {
                handText.text += (" " + c.GetComponent<Card>().CardNumber.ToString() + " of " + c.GetComponent<Card>().CardSuit);
            }
            player.AddToHand(usedCards[iRef - 1]);
            pRef++;
        }
        
    }


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
                if(c != null)
                {
                    if (c.GetComponent<Card>().cardNumber == card.GetComponent<Card>().cardNumber && c.GetComponent<Card>().cardSuit == card.GetComponent<Card>().cardSuit)
                    {
                        //GenCard();
                        Debug.Log("Rerolled like kismet");
                    }
                }
                else if(c == null)
                {
                    Debug.Log("This b empty");
                }
                
            }
        }
        usedCards[iRef] = card;

        Debug.Log(card.GetComponent<Card>().CardNumber.ToString() + " of " + card.GetComponent<Card>().CardSuit);
        iRef++;
    }
}
