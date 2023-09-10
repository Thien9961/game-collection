using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;

public class Target : MonoBehaviour
{
    public float hp = 1f,spawnChance=100;
    public ParticleSystem onDeathVfx;
    public Color vfxColor= Color.white;
    public AudioClip onDamagedSfx,onDeathSfx;
    Effect[] effects;

    // Start is called before the first frame update
    void playSfx(AudioClip whichclip)
    {
        if(whichclip != null)
        {
            GameObject au = new GameObject();
            au.transform.position = transform.position;
            au.AddComponent<AudioSource>();
            au.AddComponent<Cleaner>();
            au.GetComponent<AudioSource>().PlayOneShot(whichclip);
        } 
    }

    public void TakeDamage()
    {
        if (onDamagedSfx!=null)
            GetComponent<AudioSource>().PlayOneShot(onDamagedSfx);  
    }
    void Start()
    {
        effects=GetComponents<Effect>();
        
    }

    protected void death()
    {
        foreach (Effect effect in effects)
            effect.RegisterEvent(Event.DEATH);
        playSfx(onDeathSfx);
        if(onDeathVfx!=null)
        {
            onDeathVfx.startColor = vfxColor;
            onDeathVfx.transform.localScale = transform.localScale;
            Instantiate(onDeathVfx, transform.position, onDeathVfx.transform.rotation).Play(true);
            foreach (Transform t in onDeathVfx.transform)
            {
                t.GetComponent<ParticleSystem>().startColor = vfxColor;
                t.localScale = transform.localScale;
            }
        }    
        Destroy(gameObject);  
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
        if (!(hp > 0))
            death();
    }
}
