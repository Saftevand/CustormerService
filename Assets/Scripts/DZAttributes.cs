using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
//using System;

public class DZAttributes : MonoBehaviour
{
    public int health = 10;
    public int armor = 5;
    public List<GameObject> buffs = new List<GameObject>();

    void Start()
    {
        updateHealth();
    }

    private void updateHealth(int amount=0)
    {
        UnityEngine.UI.Text healthText = GetChildComponentByName<UnityEngine.UI.Text>("Health");    
        health += amount;        
        healthText.text = health.ToString();
    }
    private void updateArmor(int amount=0)
    {
        UnityEngine.UI.Text armorText = GetChildComponentByName<UnityEngine.UI.Text>("Armor");
        armor += amount;
        armorText.text = armor.ToString();
    }

    private T GetChildComponentByName<T>(string name) where T : Component
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
     

    public void changeHealth(int amount, bool isDamage)
    {
        if (isDamage)
        {
            if (armor > 0)
            {
                int dif = armor - amount;
                if (dif > 0)
                {
                    updateArmor(-amount);
                }
                else
                {
                    updateArmor(-armor);
                    updateHealth(dif);
                }
            }
            else
            {
                updateHealth(-amount);

            }
        }
        else
        {
            updateHealth(amount);
        }
        if(health < 1)
        {
            if (this.tag == "EnemyDropZone")
            {
                SceneManager.LoadScene("Victory");
            }
            else
            {
                SceneManager.LoadScene("Defeat");
            }
        }
    }

    public void AddArmor(int value)
    {
        armor += value;
        updateArmor();
    }

    public void EndOfTurnAction()
    {
        armor = 0;
        updateArmor();

        foreach (GameObject item in buffs)
        {
            BaseDebuff buff = item.GetComponent<BaseDebuff>();
            buff.endOfTurnAction();
        }
    }
}
