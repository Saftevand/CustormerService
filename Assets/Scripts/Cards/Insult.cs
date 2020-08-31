using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insult : BaseCard
{
    private int value = 5;
    private bool isOffensive = true;
    private string title = "Insult";
    private int cost = 1;

    public void Start()
    {
        UnityEngine.UI.Text Cost = GetChildComponentByName<UnityEngine.UI.Text>("Cost");
        Cost.text = getCost().ToString();
        UnityEngine.UI.Text Title = GetChildComponentByName<UnityEngine.UI.Text>("CardName");
        Title.text = getTitle().ToString();
        UnityEngine.UI.Text Flavour = GetChildComponentByName<UnityEngine.UI.Text>("CardText");
        Flavour.text = getFlavourText().ToString();
    }

    public override void DoAction(GameObject dropzone)
    {
        DZAttributes dropzoneAttributes = GameObject.FindWithTag(dropzone.tag).GetComponent<DZAttributes>();
        dropzoneAttributes.changeHealth(this.value, this.isOffensive);

        var buff = Resources.Load("Buffs/Bleed") as GameObject;
        GameObject bleed = Instantiate(buff, new Vector3(0, 0, 0), Quaternion.identity);

        // TODO -- Find BuffBar gameobjected og sæt den til at være parent til bleed -- 
        // TODO -- I stedet for at buge isoffensive, så lav et tag til attack, defence og utility cards --

        var parent = dropzoneAttributes.GetChildComponentByName<UnityEngine.UI.Image>("Buffbar");
        Debug.Log("load parent " + parent.ToString());

        bleed.transform.SetParent(parent.transform, false);

        dropzoneAttributes.buffs.Add(bleed);
    }

    private GameObject CreateBleed(GameObject buff, GameObject parent)
    {
        GameObject playerCard = Instantiate(buff, new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(parent.transform, false);
        return playerCard;
    }

    public override int getCost()
    {
        return this.cost;
    }

    public override string getFlavourText()
    {
        return string.Format("Insult your oppenent for {0} damage", value.ToString());
    }

    public override bool getIsOffensive()
    {
        return this.isOffensive;
    }

    public override void setIsOffensive(bool value)
    {
        this.isOffensive = value;
    }

    public override string getTitle()
    {
        return this.title;
    }

    public override int getValue()
    {
        return this.value;
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
