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
    public List<GameObject> vehicle;
    public int score;
    public bool isplaying;
    public GameObject restartmenu,enviroment;
    public static GameObject player; 
    public TextMeshProUGUI scoretxt;

    public void pausegame()
    {
        if (isplaying)
            isplaying = false;
        else
            isplaying= true;
    }
    public void restart()
    {
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    public void LoadGame1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGame2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGame3()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadGame4()
    {
        SceneManager.LoadScene(4);
    }
    public void Exit()
    {
        Application.Quit();
    }

    private void Start()
    {
        isplaying = true;
        Road.SetVehicle(vehicle);
        int index = Random.Range(0, vehicle.Count);
        player=Instantiate(vehicle[index], vehicle[index].transform.position, vehicle[index].transform.rotation);
        Instantiate(enviroment);
        
    }
    private void Update()
    {
        player.GetComponent<Vehicle>().controlable = true;
        score += (int)Time.deltaTime;
        scoretxt.SetText("Score: " + score);
        Env.speedZ += Time.deltaTime * 10;
    }
}
