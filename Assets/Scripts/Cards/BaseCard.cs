using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour
{
    public abstract int getValue();
    public abstract bool getIsOffensive();
    public abstract void setIsOffensive(bool value);
    public abstract string getTitle();
    public abstract string getFlavourText();
    public abstract int getCost();


    public abstract void DoAction(GameObject dropzone);
    //{
        //DZAttributes dropzoneAttributes = GameObject.FindWithTag(dropzone.tag).GetComponent<DZAttributes>();
        //dropzoneAttributes.changeHealth(this.value, this.isOffensive);


    //}
}
