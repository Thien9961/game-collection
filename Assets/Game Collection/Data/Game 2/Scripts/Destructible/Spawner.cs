using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game2
{
    public class Spawner : MonoBehaviour
    {
        public List<GameObject> target;
        static private float force;
        public float spawnInterval=1.5f;
        public Vector3 spawnArea;

        Bounds bounds;
        Game2.GameManager gmScript;
        static public Vector3 RandomPointInBounds(Bounds b)
        {
            Vector3 v = new Vector3();
            v.x=Random.Range(b.min.x, b.max.x);
            v.y=Random.Range(b.min.y, b.max.y);
            v.z=Random.Range(b.min.z, b.max.z);
            return v;
        }

        static public GameObject SpawnAtLoc(GameObject whichtarget, Vector3 location)
        {
            force = Random.Range(15.0f, 20.0f);
            GameObject g = Instantiate(whichtarget,location,whichtarget.transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(Vector3.up*force,ForceMode.Impulse);
            return g;
        }
        void Spawn()
        {
            
            if (gmScript.isplaying)
            {
                float a = 0, r = Random.Range(0f, 100f); 
                int b=-1;
                do
                {
                    b++;
                    a += target[b].GetComponent<Target>().spawnChance;
                }
                while (a < r);
                force = Random.Range(15.0f, 20.0f);
                Vector3 pos = Game2.Spawner.RandomPointInBounds(bounds);
                Rigidbody rb = Instantiate(target[b], pos, Random.rotation).GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * force, ForceMode.Impulse);   
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            
            gmScript = GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
            InvokeRepeating("Spawn", 1, spawnInterval);
            Vector3 v = new Vector3(spawnArea.x * transform.parent.localScale.x, spawnArea.y * transform.parent.localScale.y, spawnArea.z * transform.parent.localScale.z);
            spawnArea = v;
            bounds=new Bounds(transform.position, spawnArea);
            transform.localScale=new Vector3(bounds.size.x/ transform.parent.localScale.x, bounds.size.y/ transform.parent.localScale.y, bounds.size.z/ transform.parent.localScale.z);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

