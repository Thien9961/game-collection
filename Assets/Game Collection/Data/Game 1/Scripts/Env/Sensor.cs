using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.player)
        {
            GameObject env = GameObject.Find("Game Manager").GetComponent<GameManager>().enviroment;
            Instantiate(env, env.transform.position + Vector3.forward * 1700, env.transform.rotation);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
