using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instant : Projectile
{
    public Vector3 impactforce;
    public float maxDistance;
    protected override void Start()
    {
        RaycastHit hit;
        bool b = Physics.Raycast(new Ray(transform.position, transform.forward), out hit, maxDistance);
        if (b)
            if (hit.collider.gameObject.GetComponent<Lifeform>() != null)
                hit.collider.GetComponent<Lifeform>().takedamage(gameObject, damage, impactforce);
        Destroy(gameObject);
        playSfx(onHitSfx);
        playSfx(onDestroySfx);
    }
}
