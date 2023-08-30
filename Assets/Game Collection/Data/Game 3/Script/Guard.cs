using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Ability
{
    public GameObject shield;
    public float shieldLife;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        shield = Instantiate(shield, transform.position, shield.transform.rotation);
        shield.transform.parent = transform;
        Vector3 v = shield.transform.localScale;
        v.x = shield.transform.localScale.x * transform.localScale.x;
        v.y = shield.transform.localScale.y * transform.localScale.y;
        v.z = shield.transform.localScale.z * transform.localScale.z;
        shield.transform.localScale = v;
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void end()
    {
        shield.SetActive(false);
    }
    public override void start()
    {
        base.start();
        shield.GetComponent<Shield>().activate(shieldLife);
        if(duration>0)
            Invoke(nameof(end), duration);
    }
}
