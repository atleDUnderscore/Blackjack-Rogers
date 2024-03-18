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
    [SerializeField] GameObject winLosePanel;
    [SerializeField] TMP_Text winLoseText;
    [SerializeField] TMP_Text totalText;
    Player player;
    Computer com;
    GameObject[] usedCards;
    GameObject[] playerCardPos;
    GameObject[] comCardPos;
    bool bust;

    int iRef;
    [SerializeField] int pRef;
    [SerializeField] int aRef;
    int cRef;
    int bRef;
    int redoNumber;
    string redoSuit;
    int maxCards = 5;
    [SerializeField] int playerTotal;
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
        bust = false;
        playerTotal = 0;
        winLosePanel.SetActive(false);
        playerCardPos = GameObject.FindGameObjectsWithTag("PlayerCard");
        comCardPos = GameObject.FindGameObjectsWithTag("ComCard");

    }

    public void Update()
    {
        totalText.text = playerTotal.ToString();
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Gone");
    }

    public void SceneRL()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayGame()
    {
        Debug.Log("Plays videojuego");
        gamePanel.SetActive(true);
        player = new Player();
        com = new Computer(16);
        com.SetHandSize(2);
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
            totalText.text = playerTotal.ToString();
            pRef++;
            aRef++;
            CheckForBust();
        }
        
    }

    public void CheckForBust()
    {
        player.GetHandTotal();
        playerTotal = player.HandTotal;
        if (player.HandTotal > 21)
        {
            handText.text = "Busted";
            bust = true;
            CheckAgainstCom();
        }
    }

    public void CheckAgainstCom()
    {
        com.GetHandTotal();
        if (com.HandTotal <= com.MaxTotal && com.HandTotal > player.HandTotal || bust)
        {
            winLosePanel.SetActive(true);
            winLoseText.text = "Computer Wins";
            Debug.Log("Com Wins");
        }
        else if (com.HandTotal >= com.MaxTotal && !bust || player.HandTotal > com.HandTotal && !bust)
        {
            winLosePanel.SetActive(true);
            winLoseText.text = "Player Wins";
            Debug.Log("Player Wins");
        }
    }

    public void AddToCom()
    {
        GameObject c;
        GenComCard();
        c = usedCards[iRef - 1];
        if (bRef == 1)
        {
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
        }
        com.AddToHand(usedCards[iRef - 1]);
        bRef--;
    }


    public void GenCard()
    {
        int tempCardNum = Random.Range(1, 14);

        redoNumber = tempCardNum;
        int tempCardSuitNum = Random.Range(1, 5);

        string tempCardSuit;
        if(tempCardSuitNum == 1)
        {
            tempCardSuit = "Spades";

        }
        else if(tempCardSuitNum == 2)
        {
            tempCardSuit = "Hearts";

        }
        else if( tempCardSuitNum == 3)
        {
            tempCardSuit = "Clubs";

        }
        else
        {
            tempCardSuit = "Diamonds";

        }
        redoSuit = tempCardSuit;

        GameObject card = Instantiate(cardPrefab, playerCardPos[aRef].transform.position, playerCardPos[aRef].transform.rotation, playerCardPos[aRef].transform.parent);
        card.GetComponent<Card>().CardSetValue(tempCardNum, tempCardSuit);


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
                
            }
        }
        usedCards[iRef] = card;

        iRef++;
        
        
        
    }

    public void GenComCard()
    {
        int tempCardNum = Random.Range(1, 14);

        redoNumber = tempCardNum;
        int tempCardSuitNum = Random.Range(1, 5);

        string tempCardSuit;
        if (tempCardSuitNum == 1)
        {
            tempCardSuit = "Spades";

        }
        else if (tempCardSuitNum == 2)
        {
            tempCardSuit = "Hearts";

        }
        else if (tempCardSuitNum == 3)
        {
            tempCardSuit = "Clubs";

        }
        else
        {
            tempCardSuit = "Diamonds";

        }
        redoSuit = tempCardSuit;

        GameObject card = Instantiate(cardPrefab, comCardPos[bRef].transform.position, comCardPos[bRef].transform.rotation, comCardPos[bRef].transform.parent);
        card.GetComponent<Card>().CardSetValue(tempCardNum, tempCardSuit);


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
            }
        }
        usedCards[iRef] = card;

        iRef++;

    }
}
