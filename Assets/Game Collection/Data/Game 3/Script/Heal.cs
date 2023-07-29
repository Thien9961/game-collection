using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum healType
{
    INSTANT,
    REGENERATE,
    OVERTIME,
}
public class Heal : Ability
{

    public float amount,rate;
    public int healMaxTimes;
    Lifeform lfScript;
    public healType type;
    bool healing;
    int healTimes;
    // Start is called before the first frame update
    void regen()
    {
        if (healTimes < healMaxTimes)
        {
            healTimes++;
            lfScript.hp.value += amount;
        } 
        else
        {
            CancelInvoke();
            healing=false;
        }
    }
    void Start()
    {
        healing = false;
        lfScript = GetComponent<Lifeform>();
    }
    public override void start()
    {
        if (type == healType.INSTANT)
        {
            lfScript.hp.value += amount;
        }
        else if(type == healType.OVERTIME)
        {
            healing = true;
            healTimes = 0;
            InvokeRepeating("regen", 0, rate);
        }
        else
        {
            healMaxTimes = Mathf.RoundToInt(Mathf.Infinity);
            InvokeRepeating("regen", 0, rate);
        }
            
    }
}
