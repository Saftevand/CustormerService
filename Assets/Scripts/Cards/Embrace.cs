using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Embrace : BaseCard
{
    private int value = 5;
    private bool isOffensive = false;
    private string title = "Embrace";
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
        dropzoneAttributes.AddArmor(this.value);
    }

    public override int getCost()
    {
        return this.cost;
    }

    public override string getFlavourText()
    {
        return string.Format("Shield yourself for {0} damage", value.ToString());
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
