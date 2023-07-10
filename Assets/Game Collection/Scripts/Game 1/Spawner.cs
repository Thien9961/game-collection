using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> resource;
    GameManager gm_script;
    float chance=0;
    Player player_script;
    float[] lanecenter= { 0,0,0};
    // Start is called before the first frame update
    private void obstacleSpawn()
    {
        if (!player_script.ispaused && !player_script.isdead)
        {
            int i;
            float accumulate;
            for (int j = 0; j < 3; j++)
            {
                i = 0;
                accumulate = 0;
                float k = Random.Range(0f, 100f);
                while (i < resource.Count)
                {
                    accumulate += resource[i].GetComponent<Move>().resource_spawnchance;
                    if (k < accumulate)
                    {
                        Vector3 resource_pos = new Vector3((-Mathf.Abs(lanecenter[0]) + Mathf.Abs(lanecenter[0]) * j), resource[i].transform.position.y, resource[i].transform.position.z);
                        Instantiate(resource[i], resource_pos, resource[i].transform.rotation);
                        break;
                    }
                    i++;
                }
            }
        }
    }
    void Start()
    {
        gm_script=GameObject.Find("GameManager").GetComponent<GameManager>();
        player_script = gm_script.pc.GetComponent<Player>();
        InvokeRepeating("obstacleSpawn", 3.0f, Random.Range(1.25f, 3.25f));
        for (int j = 0; j < gm_script.lanecenter.Length; j++)
            lanecenter[j] = gm_script.lanecenter[j];
        foreach (GameObject i in resource)
        {
            chance += i.GetComponent<Move>().resource_spawnchance;
            Debug.Log("Total Spawn Chancea: " + chance);
        }
        if (chance != 100.0f)
        {
            player_script.isdead=true;  
            Debug.LogError("Total chance must be equals to 100%");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
