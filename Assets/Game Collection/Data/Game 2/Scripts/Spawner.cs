using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2
{
    public class Spawner : MonoBehaviour
    {
        public List<GameObject> target;
        private float force;
        public float spawnInterval=1.5f;

        Game2.GameManager gmScript;
        void targetSpawn()
        {
            if (gmScript.isplaying)
            {   
                int r = Random.Range(0, target.Count);
                Vector3 pos = new Vector3(Random.Range(-20, 20), target[r].transform.position.y, Random.Range(25, 45));
                Rigidbody rb = Instantiate(target[r], pos, Random.rotation).GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * force, ForceMode.Impulse);   
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            force = Random.Range(15.0f, 20.0f);
            gmScript = GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
            InvokeRepeating("targetSpawn", 0, spawnInterval);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

