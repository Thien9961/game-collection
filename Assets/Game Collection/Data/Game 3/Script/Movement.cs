using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.Timeline;

public class Movement : Ability
{
    public float speed=1;
    Lifeform lfScript;
    bool movable=true;

    private void Start()
    {
        lfScript=GetComponent<Lifeform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider c = GetComponent<Collider>();
        if (collision.collider.transform.position.y>c.bounds.center.y-c.bounds.extents.y)
            movable=false;
    }

    private void OnCollisionExit(Collision collision)
    {
        movable = true;
    }

    public override void start()
    {
        
        if (lfScript.state==Lifeform.STATE_NORMAL)
        {
            if(speed>0)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if(speed<0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            if(movable)
                transform.Translate(transform.forward * Time.deltaTime * speed);
        }
    }

}
