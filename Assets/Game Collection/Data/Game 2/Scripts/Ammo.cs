using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;

public class Ammo : MonoBehaviour
{
    Game2.Player playerScript;
    Game2.GameManager gmScript;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>()!= null)
        {
            Wpn wpnScript= playerScript.getwpn(gmScript.weapon).GetComponent<Wpn>();
            Target targetScript =collision.gameObject.GetComponent<Target>();
            Destroy(gameObject);
            targetScript.takedmg(wpnScript.damage);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Game2.Player>();
        gmScript = GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-10)
            Destroy(gameObject);
    }
}
