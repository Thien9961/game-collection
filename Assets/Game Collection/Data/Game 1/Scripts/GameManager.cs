using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> vehicle;
    public bool isplaying;
    public int score=0;
    public GameObject restartmenu,enviroment;
    public static GameObject player; 
    public TextMeshProUGUI scoretxt,speedmeter;

    public void PauseGame()
    {
        if (isplaying)
            isplaying = false;
        else
            isplaying= true;
    }
    public void RestartGame()
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

    public void LoadMain()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Env.speedZ = 0;
        restartmenu.SetActive(true);
        isplaying = false;
    }

    void UpdateScore(bool update)
    {
        if(SceneManager.GetActiveScene().buildIndex==1) 
            if(update)
            {
                score++;
                scoretxt.SetText("Score: " + score);
            }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Env.speedZ = 200;
            isplaying = true;
            Road.SetVehicle(vehicle);
            int index = Random.Range(0, vehicle.Count);
            player = Instantiate(vehicle[index], vehicle[index].transform.position, vehicle[index].transform.rotation);
            player.GetComponent<Vehicle>().controlable = true;
            GameObject env = Instantiate(enviroment);
            foreach (Transform t in env.transform)
                if (t.GetComponent<Spawner>() != null)
                    Destroy(t.gameObject);
        }
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            
            UpdateScore(isplaying);
            speedmeter.SetText(Mathf.RoundToInt(Env.speedZ / 10) + "km/h");
            if (player.GetComponent<Vehicle>().controlable == false)
                GameOver();
            if (Env.speedZ < Env.speedMax)
            {
                Env.speedZ += Time.deltaTime * 10;
            }
        }  
    }
}
