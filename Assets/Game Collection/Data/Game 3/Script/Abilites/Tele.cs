using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : Ability
{
    public float delay;
    public Vector3 destination;
    public ParticleSystem Vfx;
    // Start is called before the first frame update
    void appear()
    {

        transform.Translate(destination);
        if (onCastSfx != null)
            GetComponent<AudioSource>().PlayOneShot(onCastSfx);
        if (Vfx != null)
            Instantiate(Vfx, gameObject.transform.position + Vfx.transform.position, Vfx.transform.rotation);
        transform.gameObject.SetActive(true);
        GetComponent<Rigidbody>().useGravity = true;
    }
    public override void start()
    {
        base.start();   
        transform.gameObject.SetActive(false);
        if (Vfx != null)
            Instantiate(Vfx, transform.position+Vfx.transform.position, Vfx.transform.rotation);
        if (Mathf.Clamp(delay, 0, Mathf.Infinity) > 0)
        {
            Invoke(nameof(appear), delay);
            GetComponent<Rigidbody>().useGravity = false;
        }
            
        else
            appear();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
