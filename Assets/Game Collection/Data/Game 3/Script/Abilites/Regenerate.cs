using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : Ability
{
    public float amount,rate;
    public ParticleSystem vfx;
    // Start is called before the first frame update
    void regen()
    {
        Lifeform user = GetComponent<Lifeform>();
        if (user.hp.value > 0)
            user.hp.value += amount;
        else
            CancelInvoke();
    }
    void Start()
    {
        InvokeRepeating(nameof(regen), 0,rate);
        if(vfx != null)
            Instantiate(vfx,transform.position,vfx.transform.rotation).transform.parent=transform;
    }
}
