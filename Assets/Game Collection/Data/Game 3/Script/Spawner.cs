using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game3
{
    public class Spawner : MonoBehaviour
    {
        
        public List<GameObject> enemy;
        public int counterEnemy = 0, counterWave = 0;
        Game3.GameManager gmScript;
        public bool spawning=false;
        // Start is called before the first frame update
        void spawnEnemy()
        {
            counterWave++;
            int diffWave = counterWave * gmScript.diffIncr + gmScript.diffBase, diff = 0;
            Enemy e;
            Debug.Log("Diff Wave: "+diffWave);
            while (diffWave > diff)
            {
                e = enemy[Random.Range(0, enemy.Count)].GetComponent<Enemy>();
                if (diff + e.diffPts <= diffWave)
                {
                    diff += e.diffPts;
                    counterEnemy++;
                    Vector3 v;
                    v.x = e.gameObject.transform.position.x;
                    v.y = Random.Range(5, 10);
                    v.z = Random.Range(-10, 10);
                    Instantiate(e.gameObject, v, e.gameObject.transform.rotation);
                }
            } 
            spawning = false;
        }
        void Start()
        {
            gmScript = GameObject.Find("Game Manager").GetComponent<Game3.GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            gmScript.txtEnemy.SetText("Enemy: " + counterEnemy);
            gmScript.txtWave.SetText("Wave: " + counterWave);
            if (gmScript.isplaying && counterEnemy == 0 && !spawning)
            {
                Invoke(nameof(spawnEnemy), 3);
                spawning = true;
            }
                
        }
    }
}


