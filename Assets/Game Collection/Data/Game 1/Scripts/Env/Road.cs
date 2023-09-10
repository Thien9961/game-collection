using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public static float[] lanecenter = { -80, 0, 80 };
    public static List<GameObject> car;
    public static readonly int maxVehicle=27;
    public int vehicleCount=0;

    public static void SetVehicle(List<GameObject> list)
    {
        car = list;
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
