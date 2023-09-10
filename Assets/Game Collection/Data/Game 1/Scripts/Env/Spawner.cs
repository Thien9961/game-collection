using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Road road=transform.parent.Find("Road").GetComponent<Road>();
        foreach (Transform t in transform)
            t.gameObject.AddComponent<SpawnCondition>();      
         
        if (road.vehicleCount < Road.maxVehicle)
        {
            for (int i = 0; i < Random.Range(0,2); i++)
            {
                int rng = Random.Range(0, transform.childCount);              
                while (!transform.GetChild(rng).GetComponent<SpawnCondition>().spawnable)
                    rng = Random.Range(0, transform.childCount);
                SpawnCondition sc=transform.GetChild(rng).GetComponent<SpawnCondition>();
                road.vehicleCount++;
                sc.Spawn(Road.car[Random.Range(0, Road.car.Count)]);
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SpawnCondition:MonoBehaviour
{
    public bool spawnable = true;

    public void Spawn(GameObject go)
    {
        GameObject g;
        g = Instantiate(go, transform.position, go.transform.rotation);
        g.GetComponent<Vehicle>().controlable = false;
        g.transform.parent = transform;
        spawnable = false;
    }
}
