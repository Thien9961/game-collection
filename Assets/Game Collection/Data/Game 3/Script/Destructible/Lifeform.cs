using Game3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Lifeform:MonoBehaviour
{
    public faction Faction;
    public float maxhp=1;
    public UnityEngine.UI.Slider hp;
    public Rigidbody rb;
    public int state;
    public Animator animator=null;
    public AudioClip onDeathSfx, onHurtSfx;
    public static readonly int STATE_NORMAL=0, STATE_STUNNED = 1, STATE_DEATH=2;


    protected Game3.GameManager manager;
    protected virtual void Start()
    {
        hp.maxValue = maxhp;
        hp.value = hp.maxValue;
        state = STATE_NORMAL;
        manager = GameObject.Find("Game Manager").GetComponent<Game3.GameManager>();
    }

    public virtual void takedamage(GameObject attacker,float damage,Vector3 force) 
    {
        Debug.Log(gameObject.name + " took " + damage + " damages.");
        state = STATE_STUNNED;
        rb=GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);
        hp.value -= damage;
        if(onHurtSfx != null)
            GetComponent<AudioSource>().PlayOneShot(onHurtSfx);
    }
    protected virtual void stunned()
    {
        if (!(hp.value > 0))
            state = STATE_DEATH;
        if (rb.velocity == Vector3.zero)
            state = STATE_NORMAL;
        else
            state = STATE_STUNNED;
    }
    protected void playSfx(AudioClip whichclip)
    {
        if (whichclip != null)
        {
            GameObject au = new GameObject();
            au.transform.position = transform.position;
            au.AddComponent<AudioSource>();
            au.GetComponent<AudioSource>().PlayOneShot(whichclip);
            au.AddComponent<RemoveLeak>();
        }
    }

    protected virtual void death()
    {
        Destroy(gameObject);
        playSfx(onDeathSfx);
    }
    public void stateUpdate()
    {
        rb = GetComponent<Rigidbody>();
        switch (state)
        {
            case 0:
                {
                    if (!(hp.value > 0))
                        state = STATE_DEATH;
                }
                    break;
            case 2:
                {
                    death();
                    break;
                }
            case 1:
                {
                    stunned();
                    break;
                }
        }
    }
    protected virtual void Update()
    {
        stateUpdate();
        if (!manager.worldbound.Contains(transform.position))
            state = STATE_DEATH;
    }
}
