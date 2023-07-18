using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Net.Sockets;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float diff = 1.0f, diff_increaserate = 0.2f;
    public float dx, dz;
    public float depleterate = 0.1f, depleteamount = 0.75f,resource_intialvalue=70;

    public bool ispaused;
    public bool isdead;

    GameManager gm_script;
    readonly string[] keys = {"a","d"};
    int pressedkey=-1;
    bool ismoving=false;
    float x=0,step;
    // Start is called before the first frame update


    void Start()
    {
        gm_script = GameObject.Find("GameManager").GetComponent<GameManager>();
        step = Mathf.Abs(gm_script.lanecenter[0] - gm_script.lanecenter[1]);
        // Update is called once per frame
    }
    void Update()
    {
        
        if (!gm_script.pc.GetComponent<Player>().isdead && !gm_script.pc.GetComponent<Player>().ispaused )
        {
            for (int i = 0; i < keys.Length; i++)
                if (Input.GetKeyDown(keys[i]) && !ismoving)
                    pressedkey = i;
            switch (pressedkey)
            {

                case 0:
                    {

                        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        Vector3 playerRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
                        if (playerPos.x >= gm_script.lanecenter[0] && x < step)
                        {
                            ismoving = true;
                            transform.Translate(Vector3.left * dx * Time.deltaTime);
                            x += dx * Time.deltaTime;
                        }
                        else
                        {
                            x = 0;
                            pressedkey = -1;
                            ismoving = false;
                            transform.Rotate(Vector3.up * 0);
                        }
                        break;
                    }
                case 1:
                    {
                        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        Vector3 playerRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
                        if (playerPos.x <= gm_script.lanecenter[2] && x < step)
                        {
                            x += dx * Time.deltaTime;
                            ismoving = true;
                            transform.Translate(Vector3.right * dx * Time.deltaTime);
                        }
                        else
                        {
                            x = 0;
                            pressedkey = -1;
                            ismoving = false;
                            transform.Rotate(Vector3.up * 0);
                        }
                        break;
                    }
            }
        }
    }     
}
