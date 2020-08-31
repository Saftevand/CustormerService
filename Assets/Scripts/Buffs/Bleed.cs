using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : BaseDebuff
{
    private GameObject dropzone;
    private int value = 0;
    public void Start()
    {
        dropzone = GameObject.FindGameObjectWithTag("EnemyDropZone");
    }


    public override void endOfTurnAction()
    {
        DZAttributes dropzoneAttributes = GameObject.FindWithTag(dropzone.tag).GetComponent<DZAttributes>();
        dropzoneAttributes.changeHealth(this.value, true);
        this.value -= 1;
        if (this.value < 1)
        {
            Destroy(this);
        }
    }
}
