using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Ground : MonoBehaviour
{
    public List<GameObject> other;
    public GameObject ground;
    Player player_script;
    GameManager gm_script;
    // Start is called before the first frame update
    void Start()
    {
        gm_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        player_script = gm_script.pc.GetComponent<Player>();
        int j = 0;
        float  x, y, z;
        for (int i = 0; i < 25; i++)
        {
            j = Random.Range(0, other.Count);
            y = other[j].transform.position.y;
            x = Random.Range(-400.0f, 400.0f)+ other[j].transform.position.x; 
            z = Random.Range(-900f, 900.0f)+other[j].transform.position.z;
            while (x >= -250f && x <= 250f)
                x = Random.Range(-400.0f, 400.0f);
            Vector3 pos = new Vector3(x, y, z);
            Instantiate(other[j], pos, other[j].transform.rotation).transform.SetParent(gameObject.transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!player_script.isdead && !player_script.ispaused)
        {
            transform.Translate(Vector3.back * Time.deltaTime * player_script.dz);
            if (transform.position.z <= -1325)
            {
                gm_script.gndLimit--;
                Destroy(gameObject);
            }
            else if (transform.position.z <= 0 && gm_script.gndLimit < 2 && !player_script.isdead && !player_script.ispaused)
            {
                Vector3 pos = new Vector3(ground.transform.position.x, ground.transform.position.y, ground.transform.position.z + 2000);
                Instantiate(ground, pos, ground.transform.rotation);
                gm_script.gndLimit++;
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * player_script.dz);
            }
        }
    }
}
