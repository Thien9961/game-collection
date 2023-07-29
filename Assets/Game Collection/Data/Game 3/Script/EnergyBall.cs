using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : Projectile
{
    public float speed, impactForce;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        rb.MovePosition(transform.position + transform.forward.normalized * speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider collider)
    {
        Lifeform victim = collider.GetComponent<Lifeform>();
        if (victim != null)
        {
            switch (allowedTarget)
            {
                case allowedTarget.ALLIED:
                    {
                        if (victim.Faction == Faction)
                        {
                            Debug.Log(collider.name + " was hit by" + gameObject.name);
                            victim.takedamage(gameObject, damage, Vector3.zero);
                            victim.rb.AddForce(transform.forward.normalized * impactForce, ForceMode.Impulse);
                            if (OnImpactDestroy)
                                Destroy(gameObject);
                        }
                        break;
                    }
                case allowedTarget.ENEMY:
                    {
                        if (victim.Faction != Faction)
                        {
                            Debug.Log(collider.name + " was hit by" + gameObject.name);
                            victim.takedamage(gameObject, damage, Vector3.zero);
                            victim.rb.AddForce(transform.forward.normalized * impactForce, ForceMode.Impulse);
                            if (OnImpactDestroy)
                                Destroy(gameObject);
                        }
                        break;
                    }
                case allowedTarget.ALL:
                    {
                        if (victim != null)
                        {
                            victim.takedamage(gameObject, damage, Vector3.zero);
                            victim.rb.AddForce(transform.forward.normalized * impactForce, ForceMode.Impulse);
                            Debug.Log(collider.name + " was hit by" + gameObject.name);
                            if (OnImpactDestroy)
                                Destroy(gameObject);
                        }
                        break;
                    }
                default:
                    break;
            }

        }
    }
}