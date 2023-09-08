using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static float[] lanecenter = { -80, 0, 80 };
    public int maxVehicle=10;
    public static List<GameObject> car;
    public int minDistance = 10;//minimum distance between 2 vehicles

    public void GenerateVehicle(GameObject vehicle,int laneIndex,float zCoord)
    {
        Vector3 v = new Vector3(lanecenter[laneIndex], vehicle.transform.position.y, zCoord+transform.position.z);
        GameObject g=Instantiate(vehicle, v, vehicle.transform.rotation) ;
        g.transform.parent = transform;
        g.GetComponent<Vehicle>().controlable = false;
    }

    public static void SetVehicle(List<GameObject> list)
    {
        car = list;
    }
    // Start is called before the first frame update
    void Start()
    {
        List<int> spawnPos=new List<int>();

        spawnPos.Capacity = 90;
        int k = -450,n = Random.Range(0, spawnPos.Capacity);
        bool[] spawnable = new bool[spawnPos.Capacity];
        for (int j=0;j< spawnPos.Capacity; j++)
        {
            spawnPos.Add(k);
            k += minDistance;
            spawnable[j] = true;
        }
        for (int i = 0; i < Random.Range(0, maxVehicle); i++)
        {
            while (!spawnable[n])
                n = Random.Range(0, spawnPos.Capacity);
            GenerateVehicle(car[Random.Range(0, car.Count)], Random.Range(0, lanecenter.Length), spawnPos[n]);
        }
            
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
