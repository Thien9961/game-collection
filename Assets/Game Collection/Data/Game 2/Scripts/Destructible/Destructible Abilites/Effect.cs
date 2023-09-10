using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Event
{
    ALIVE,
    DEATH,
    BIRTH,
    TAKE_DAMAGE,
    NONE
}
public class Effect : MonoBehaviour
{
    public Event eventType;
    // Start is called before the first frame update
    public void debug()
    {
        switch(eventType)
        {
            case Event.ALIVE:
                {
                    Debug.Log(name + " is Alive");
                    break;
                }
            case Event.DEATH:
                {
                    Debug.Log(name + "is Death");
                    break;
                }
            case Event.TAKE_DAMAGE:
            {
                    Debug.Log(name + " is Taking Damage");
                    break;
            }
            case Event.BIRTH: 
            {
                    Debug.Log(name + " is Born");
                    break;
            }   
        }    
    }
    public virtual void trigger()
    {
        debug();     
    }
    public void RegisterEvent(Event e)
    {
        if (eventType == e)
            trigger();
    }
    protected virtual void Start()
    {
        RegisterEvent(Event.BIRTH);  
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RegisterEvent(Event.ALIVE);
    }
}
