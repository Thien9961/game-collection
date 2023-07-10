using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2
{
    public class Spawner : MonoBehaviour
    {
        public List<GameObject> target;
        public float force=20;

        
        Target targetScript;
        void targetSpawn()
        {
            
            GameObject go = Instantiate(target[0],target[0].transform.position, target[0].transform.rotation); ;
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up*force, ForceMode.Impulse);
        }
        // Start is called before the first frame update
        void Start()
        {
            
            InvokeRepeating("targetSpawn", 3, Random.Range(1, 3));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

