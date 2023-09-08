using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Env : MonoBehaviour
{
    public static  float speedZ;
    public List<GameObject> misc;
    public Bounds[] area;
    public int maxObject = 25;
    // Start is called before the first frame update
    void GenerateObject(List<GameObject> list,int areaIndex)
    {
        int j = 0;
        float x, y, z;
        for (int i = 0; i < Random.Range(0, maxObject); i++)
        {
            j = Random.Range(0, list.Count);
            y = misc[j].transform.position.y;
            x = Random.Range(area[areaIndex].min.x, area[areaIndex].max.x)+ transform.position.x + area[areaIndex].center.x;
            z = Random.Range(area[areaIndex].min.z, area[areaIndex].max.z)+ transform.position.z+ area[areaIndex].center.z;
            Vector3 pos = new Vector3(x, y, z);
            maxObject--;
            Instantiate(misc[j], pos, misc[j].transform.rotation).transform.parent=transform;

        }
    }

    void Start()
    {
        for(int i= 0; i < area.Length; ++i)
            GenerateObject(misc, i);
            
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -2000)
            Destroy(gameObject);
        GameManager manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (manager.isplaying)
            transform.Translate(Vector3.back * Env.speedZ * Time.deltaTime);
    }
}
