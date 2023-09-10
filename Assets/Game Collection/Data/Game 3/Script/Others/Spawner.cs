using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game3
{
    public class Spawner : MonoBehaviour
    {  
        // Start is called before the first frame update
        public void SpawnRandom(GameObject[] objects,ref int weight)
        {
            Vector3 v;
            v.x = transform.position.x;
            int j= Random.Range(0, objects.Length); 
            v.y = transform.position.y + objects[j].transform.localScale.y / 2;
            v.z = transform.position.z + Random.Range(-0.9f * transform.localScale.z/2, 0.9f * transform.localScale.z/2);
            Instantiate(objects[j], v, objects[j].transform.rotation);
            weight += objects[j].GetComponent<Enemy>().diffPts;
            Debug.Log(name + " has spawn a " + objects[j].name+"("+ objects[j].GetComponent<Enemy>().diffPts + ")");
        }
        void Start()
        {

        }



        // Update is called once per frame
        void Update()
        {

                
        }
    }
}


