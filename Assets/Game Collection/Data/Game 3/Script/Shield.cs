using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Shield : MonoBehaviour
{
    public float shieldLife;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Projectile>() != null)
        {
            if(other.GetComponent<Projectile>().Faction!=gameObject.GetComponent<Lifeform>().Faction)
            {
                shieldLife -= other.GetComponent<Projectile>().damage;
                if (shieldLife < 0)
                    other.GetComponent<Projectile>().damage = Mathf.Abs(shieldLife);
                else
                    other.GetComponent<Projectile>().death();
            }   
        }
    }
    public void activate(float shieldlife)
    {
        GetComponent<Shield>().shieldLife= shieldlife;
        gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!(shieldLife > 0))
            gameObject.SetActive(false);
    }
}
