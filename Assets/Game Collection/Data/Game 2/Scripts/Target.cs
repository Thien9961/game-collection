using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

public class Target : MonoBehaviour
{
    public float hp = 1f,spawnChance=100;
    public ParticleSystem onDeathVfx;
    public AudioClip onDamagedSfx,onDeathSfx;
    Effect effect;

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
        GetComponent<AudioSource>().PlayOneShot(onDamagedSfx);
        effect.RegisterEvent(Event.TAKE_DAMAGE);
    }
    void Start()
    {
        effect=GetComponent<Effect>();
    }

    void death()
    {
        effect.RegisterEvent(Event.DEATH);
        playSfx(onDeathSfx);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
        if (!(hp > 0))
            death();
    }
}
