using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using UnityEngine.XR;
//using System;

public class EndTurn : MonoBehaviour
{
    //public GameObject CardAtt;
    //public GameObject CardDef;
    public GameObject PlayerHand;    
    public List<GameObject> Deck;

    public int handsize = 5;
    private GameObject DiscardPile;
    private GameObject DrawPile;
    private List<GameObject> hand = new List<GameObject>();
    private Queue<GameObject> drawpile = new Queue<GameObject>();
    private List<GameObject> discardpile = new List<GameObject>();


    void Start()
    {
        DiscardPile = GameObject.Find("discardPile");
        DrawPile = GameObject.Find("drawPile");

        Deck = CreateDeck().OrderBy(x => (Guid.NewGuid())).ToList();
        foreach (GameObject item in Deck)
        {
            drawpile.Enqueue(item);
        }

        while (hand.Count < handsize)
        {
            drawCard();
        }
        refillEnergy();
    }
    
    public void OnClick()
    {
        EndOfAction();
        refillEnergy();
        

        while (hand.Count > 0)
        {
            discardCard(hand[0]);
        }

        while(hand.Count < handsize)
        {
            drawCard();
        }       
    }

    private void EndOfAction()
    {
        GameObject ally = GameObject.FindWithTag("AllyDropZone");
        GameObject enemy = GameObject.FindWithTag("EnemyDropZone");
        ally.GetComponent<DZAttributes>().EndOfTurnAction();
        enemy.GetComponent<DZAttributes>().EndOfTurnAction();
    }

    private void refillEnergy()
    {
        GameObject energy = GameObject.Find("Energy");
        UnityEngine.UI.Text current = null;
        UnityEngine.UI.Text max = null;
        foreach (var i in energy.GetComponentsInChildren<UnityEngine.UI.Text>())
        {
            if (i.name == "Current")
            {
                current = i;
            }
            else if (i.name == "Max")
            {
                max = i;
            }
        }

        current.text = max.text;
    }

    private GameObject CreateCard(GameObject cardType, GameObject parent)
    {
        GameObject playerCard = Instantiate(cardType, new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(parent.transform, false);
        return playerCard;
    }

    private List<GameObject> CreateDeck()
    {
        List<GameObject> result = new List<GameObject>();
        /*
        for (int i = 0; i < 20; i++)
        {
            int x = UnityEngine.Random.Range(0,2); 
            if (x % 2 == 0)
            {
                GameObject temp = CreateCard(CardAtt, DrawPile);
                temp.transform.SetParent(DrawPile.transform, false);
                Attributes cardAtt = (Attributes)temp.GetComponent(typeof(Attributes));
                cardAtt.isOffensive = true;
                result.Add(temp);
            }
            else
            {
                GameObject temp = CreateCard(CardDef, DrawPile);
                temp.transform.SetParent(DrawPile.transform, false);
                Attributes cardAtt = (Attributes)temp.GetComponent(typeof(Attributes));
                cardAtt.isOffensive = false;
                result.Add(temp);
            }
        }
        */
        for (int i = 0; i < 20; i++)
        {
            int x = UnityEngine.Random.Range(0, 2);
            if (x % 2 == 0)
            {
                try
                {
                    var card = Resources.Load("Cards/Embrace");
                    GameObject temp = CreateCard((GameObject)card, DrawPile);
                    temp.transform.SetParent(DrawPile.transform, false);
                    //BaseCard cardAtt = (BaseCard)temp.GetComponent(typeof(BaseCard));
                    //cardAtt.setIsOffensive(true);
                    result.Add(temp);
                }
                catch (FileNotFoundException)
                {
                    Debug.LogError("Embrace not found");
                }
            }
            else
            {
                try
                {
                    var card = Resources.Load("Cards/Insult");
                    GameObject temp = CreateCard((GameObject)card, DrawPile);
                    temp.transform.SetParent(DrawPile.transform, false);
                    //BaseCard cardAtt = (BaseCard)temp.GetComponent(typeof(BaseCard));
                    //cardAtt.setIsOffensive(true);
                    result.Add(temp);
                }
                catch (FileNotFoundException)
                {
                    Debug.LogError("Insult not found");
                }
            }


            
            
        }
        return result;
    }

    private void drawCard()
    {
        try
        {
            GameObject card = drawpile.Dequeue();
            hand.Add(card);
            card.transform.SetParent(PlayerHand.transform, false);
        }
        catch (System.InvalidOperationException)
        {
            List<GameObject> tempList = discardpile.OrderBy(x => (Guid.NewGuid())).ToList();
            foreach (GameObject element in tempList)
            {
                element.transform.SetParent(DrawPile.transform, false);
                drawpile.Enqueue(element);
            }
            discardpile = new List<GameObject>();
            GameObject card = drawpile.Dequeue();
            hand.Add(card);
            card.transform.SetParent(PlayerHand.transform, false);
        }

        updateFields();
        

    }

    public void discardCard(GameObject card)
    {
        hand.Remove(card);
        discardpile.Add(card);
        card.transform.SetParent(DiscardPile.transform, false);
        updateFields();        
    }

    private void updateFields()
    {
        GameObject drawpilecounter = GameObject.Find("DrawPile");
        foreach (var i in drawpilecounter.GetComponentsInChildren<UnityEngine.UI.Text>())
        {
            if (i.name == "Cards")
            {
                i.text = drawpile.Count().ToString();
            }
        }
        GameObject discardpilecounter = GameObject.Find("DiscardPile");
        foreach (var i in discardpilecounter.GetComponentsInChildren<UnityEngine.UI.Text>())
        {
            if (i.name == "Cards")
            {
                i.text = discardpile.Count().ToString();
            }
        }
    }

    public T GetChildComponentByName<T>(string name) where T : Component
    {
        foreach (T component in GetComponentsInChildren<T>(true))
        {
            if (component.gameObject.name == name)
            {
                return component;
            }
        }
        return null;
    }

}
