using Game3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum allowedTarget
{
    ENEMY,
    ALLIED,
    ALL,
    NONE
}
public class Projectile : MonoBehaviour
{

    public faction Faction;
    public float damage;
    public Rigidbody rb;
    public allowedTarget allowedTarget;
    public bool OnImpactDestroy;

    Game3.GameManager manager;
    protected virtual void OnTriggerEnter(Collider collider)
    {
        Lifeform victim = collider.GetComponent<Lifeform>();
        if (victim != null )
        {
            switch (allowedTarget)
            {
                case allowedTarget.ALLIED:
                    {
                        if (victim.Faction == Faction )
                        {
                            Debug.Log(collider.name + " was hit by" + gameObject.name);
                            victim.takedamage(gameObject, damage, Vector3.zero);
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
                            if (OnImpactDestroy)
                                Destroy(gameObject);
                        }
                        break;
                    }
                case allowedTarget.ALL:
                    {
                        if (victim != null)
                        {
                            Debug.Log(collider.name + " was hit by" + gameObject.name);
                            victim.takedamage(gameObject, damage, Vector3.zero);
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
    // Start is called before the first frame update
    protected virtual void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<Game3.GameManager>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!manager.worldbound.Contains(transform.position))
            Destroy(gameObject);
    }
}
