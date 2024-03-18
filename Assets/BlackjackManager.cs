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
    [SerializeField] TMP_Text comHandText;
    Player player;
    Computer com;
    GameObject[] usedCards;
    GameObject[] playerCardPos;
    GameObject[] comCardPos;

    int iRef;
    [SerializeField]int pRef;
    int aRef;
    int cRef;
    int bRef;
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
        comHandText.text = "";
        pRef = 0;
        cRef = 0;
        bRef = 1;
        aRef = 0;
        playerCardPos = GameObject.FindGameObjectsWithTag("PlayerCard");
        comCardPos = GameObject.FindGameObjectsWithTag("ComCard");

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
        player.SetHandZero();
        player.SetHandSize(maxCards);
        AddToCom();
        AddToCom();
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
                handText.text += (" King" + " of " + c.GetComponent<Card>().CardSuit);
            }
            else
            {
                handText.text += (" " + c.GetComponent<Card>().CardNumber.ToString() + " of " + c.GetComponent<Card>().CardSuit);
            }
            player.AddToHand(usedCards[iRef - 1]);
            player.SetHandZero();
            player.GetHandTotal();
            if (player.HandTotal > 21)
            {
                handText.text = "Busted";
            }
            pRef++;
            aRef++;
        }
        
    }

    public void AddToCom()
    {
        GameObject c;
        GenComCard();
        c = usedCards[iRef - 1];
        if (c.GetComponent<Card>().CardNumber == 1)
        {
            comHandText.text += (" Ace" + " of " + c.GetComponent<Card>().CardSuit);
        }
        else if (c.GetComponent<Card>().CardNumber == 11)
        {
            comHandText.text += (" Jack" + " of " + c.GetComponent<Card>().CardSuit);
        }
        else if (c.GetComponent<Card>().CardNumber == 12)
        {
            comHandText.text += (" Queen" + " of " + c.GetComponent<Card>().CardSuit);
        }
        else if (c.GetComponent<Card>().CardNumber == 13)
        {
            comHandText.text += (" King" + " of " + c.GetComponent<Card>().CardSuit);
        }
        else
        {
            comHandText.text += (" " + c.GetComponent<Card>().CardNumber.ToString() + " of " + c.GetComponent<Card>().CardSuit);
        }
        bRef--;
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

        GameObject card = Instantiate(cardPrefab, playerCardPos[aRef].transform.position, playerCardPos[aRef].transform.rotation, playerCardPos[aRef].transform.parent);
        card.GetComponent<Card>().CardSetValue(tempCardNum, tempCardSuit);


        Debug.Log(card.GetComponent<Card>().CardSuit);
        if (iRef > 0)
        {
            foreach (GameObject c in usedCards)
            {
                if(c != null)
                {
                    if (c.GetComponent<Card>().cardNumber == card.GetComponent<Card>().cardNumber && c.GetComponent<Card>().cardSuit == card.GetComponent<Card>().cardSuit)
                    {
                        GenCard();
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

    public void GenComCard()
    {
        int tempCardNum = Random.Range(1, 14);
        Debug.Log("Card Num: " + tempCardNum);
        redoNumber = tempCardNum;
        int tempCardSuitNum = Random.Range(1, 5);
        Debug.Log("Card Suit: " + tempCardSuitNum);
        string tempCardSuit;
        if (tempCardSuitNum == 1)
        {
            tempCardSuit = "Spades";
            Debug.Log("Spades");
        }
        else if (tempCardSuitNum == 2)
        {
            tempCardSuit = "Hearts";
            Debug.Log("Hearts");
        }
        else if (tempCardSuitNum == 3)
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

        GameObject card = Instantiate(cardPrefab, comCardPos[bRef].transform.position, comCardPos[bRef].transform.rotation, comCardPos[bRef].transform.parent);
        card.GetComponent<Card>().CardSetValue(tempCardNum, tempCardSuit);


        Debug.Log(card.GetComponent<Card>().CardSuit);
        if (iRef > 0)
        {
            foreach (GameObject c in usedCards)
            {
                if (c != null)
                {
                    if (c.GetComponent<Card>().cardNumber == card.GetComponent<Card>().cardNumber && c.GetComponent<Card>().cardSuit == card.GetComponent<Card>().cardSuit)
                    {
                        GenComCard();
                        Debug.Log("Rerolled like kismet");
                    }
                }
                else if (c == null)
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
