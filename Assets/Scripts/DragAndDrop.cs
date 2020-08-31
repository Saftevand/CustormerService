using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    public GameObject dropzone;

    private GameObject DiscardZone;
    private bool isDragging = false;
    private bool isOverDropZone = false;    
    private Vector2 startPos;
    

    private void Start()
    {
        DiscardZone = GameObject.Find("discardPile");
    }
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == dropzone.gameObject.tag)
        {
            isOverDropZone = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
    }

    public void startDragging()
    {
        startPos = transform.position;
        isDragging = true;
    }

    public void endDragging()
    {
        isDragging = false;
        if (isOverDropZone)
        {
            int currentEnergyValue = 0;
            UnityEngine.UI.Text currentEnergyValueComponent = null;
            GameObject energy = GameObject.Find("Energy");

            foreach(var i in energy.GetComponentsInChildren<UnityEngine.UI.Text>())
            {
                if (i.name == "Current")
                {
                    currentEnergyValueComponent = i;
                    currentEnergyValue = Convert.ToInt32(i.text);
                }
            }

            int CardCost = Convert.ToInt32(GetChildComponentByName<UnityEngine.UI.Text>("Cost").text);

            if (CardCost <= currentEnergyValue)
            {
                int temp = currentEnergyValue - CardCost;
                currentEnergyValueComponent.text = temp.ToString();
                GameObject endTurn = GameObject.Find("ButtonEndTurn");
                EndTurn et = (EndTurn)endTurn.GetComponent(typeof(EndTurn));
                et.discardCard(this.gameObject);

                BaseCard att = this.GetComponent<BaseCard>();
                att.DoAction(dropzone);
            }
            else
            {
                transform.position = startPos;
            }
            
        }
        else
        {
            transform.position = startPos;
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
