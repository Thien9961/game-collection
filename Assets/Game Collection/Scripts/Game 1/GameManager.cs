using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using System.Net.Sockets;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<UnityEngine.UI.Slider> slider;
    public float[] lanecenter = { -80, 0, 80 };
    public GameObject gnd,spawner;
    public List<GameObject> player;
    public int gndLimit=0,index,score;
    public string playername;
    public GameObject pc;
    GameObject restartmenu;
    TextMeshProUGUI scoretxt;
    GameObject resources;
    Player player_script;


    public void pausegame()
    {
        player_script = pc.GetComponent<Player>();
        if (player_script.ispaused)
            player_script.ispaused = false;
        else
            player_script.ispaused = true;
    }
    public void restart()
    {
        SceneManager.LoadScene(1);
    }
    public void resourceDeplete()
    {
        player_script = pc.GetComponent<Player>();
        if (slider[0].value > 0 && slider[1].value > 0 && slider[2].value > 0 )
        {
            if (!player_script.ispaused)
                for (int i = 0; i < resources.transform.childCount; i++)
                    slider[i].value -= player_script.depleteamount * player_script.diff;
        }   
        else
        {
            player_script.isdead = false;
            player_script.ispaused = false;
            player_script.diff = 1.0f;
            player_script.diff_increaserate = 0.2f;
            player_script.isdead = true;
            restartmenu.SetActive(true);
        }
    }
    // Start is called before the first frame update
    public void load_scence()
    {
        SceneManager.LoadScene(1);
    }
    public void load_main_menu()
    {
        SceneManager.LoadScene(0);
    }
    private void Start()
    {
        
        index = Random.Range(0, player.Count);
        pc = player[index];
        playername = player[index].name + "(Clone)";
        player_script = pc.GetComponent<Player>();
        player_script.isdead = false;
        player_script.ispaused = false;
        player_script.diff = 1.0f;
        player_script.diff_increaserate = 0.2f;
        GameObject canvas = GameObject.Find("Canvas");
        for (int j = 0; j < canvas.transform.childCount; j++)
        {
            if (canvas.transform.GetChild(j).name == "Restart Menu")
                restartmenu = canvas.transform.GetChild(j).gameObject;
            if (canvas.transform.GetChild(j).name == "Resource UI")
                resources =canvas.transform.GetChild(j).gameObject;
            if(canvas.transform.GetChild(j).name == "Score Text")
                scoretxt = canvas.transform.GetChild(j).gameObject.GetComponent<TextMeshProUGUI>();
        }         
        for (int i = 0; i < resources.transform.childCount; i++)
        {
            slider[i] = resources.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Slider>();
            slider[i].value = player_script.resource_intialvalue;
        }
        InvokeRepeating("resourceDeplete", 0, player_script.depleterate / player_script.diff);
        Instantiate(player[index], player[index].transform.position, player[index].transform.rotation).tag = "Player";
        Instantiate(gnd,gnd.transform.position,gnd.transform.rotation);
        Instantiate(spawner, Vector3.zero, gnd.transform.rotation);
        gndLimit++;
    }
    private void Update()
    {
        scoretxt.SetText("Score: " + score);   
    }
}
