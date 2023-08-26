using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;

public class Ammunition : MonoBehaviour
{
    public AudioClip onDestroySfx;
    public ParticleSystem onDestroyVfx;
    public float damage;
    void playSfx(AudioClip whichclip)
    {
        if (whichclip != null)
        {
            GameObject au = new GameObject();
            au.transform.position = transform.position;
            au.AddComponent<AudioSource>();
            au.AddComponent<Cleaner>();
            au.GetComponent<AudioSource>().PlayOneShot(whichclip);
        }
    }
    public void DamageTarget(GameObject victim,float damage)
    {
        if (victim.GetComponent<Target>() != null)
        {
            Target script = victim.GetComponent<Target>();
            script.TakeDamage();
            script.hp -= damage;            
            playSfx(onDestroySfx);
            if(onDestroyVfx!=null)
                onDestroyVfx.Play();
            Destroy(gameObject);  
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude>30)
            DamageTarget(collision.gameObject, damage);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-10)
            Destroy(gameObject);
    }
}
public class Cleaner : MonoBehaviour
{
    private void Update()
    {
        if(GetComponent<AudioSource>().isPlaying==false)
            Destroy(gameObject);     
    }
}
