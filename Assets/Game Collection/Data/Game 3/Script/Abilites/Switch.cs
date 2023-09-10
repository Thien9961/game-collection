using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Ability
{
    private BasicAttack ba;
    public int ammunitionIndex;
    public float RateOfFire;
    private void Start()
    {
        ba= GetComponent<BasicAttack>();
    }
    public override void start()
    {
        if(ammunitionIndex< ba.projectile.Count)
        {
            ba.projectileIndex = ammunitionIndex;
            ba.cooldown = RateOfFire;
        }
            
    }
}
