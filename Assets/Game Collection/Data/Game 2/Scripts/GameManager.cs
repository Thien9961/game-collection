using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using System;
using static UnityEngine.EventSystems.EventTrigger;

namespace Game2
{
    public class GameManager : MonoBehaviour
    {
        public int fps,DefaulWpnIndex=0;
        public bool isplaying;
        public float score = 0;
        public float gameTime=60;
        public TextMeshProUGUI scoreTxt;
        public GameObject restartmenu,pausemenu;
        public UnityEngine.UI.Slider time;
        public GameObject player;
        public GameObject[] Weapons;
        // Start is called before the first frame update
        public void Resume()
        {
            isplaying = true;
            pausemenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void PauseGame()
        {
            pausemenu.SetActive(true);
            isplaying = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);           
        }

        public void Exit()
        {
            SceneManager.LoadScene(0);
        }
        void Start()
        {
            isplaying = true;
            gameTime /= Time.fixedDeltaTime;
            time.maxValue = gameTime;
            time.value = time.maxValue;
            score = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player = Instantiate(player);
            player.GetComponent<Player>().gmScript=GetComponent<Game2.GameManager>();
            player.GetComponent<Player>().SetWpn(Weapons[DefaulWpnIndex]);
            GameObject.Find("Camera").transform.parent= player.transform;
            RenderSettings.ambientIntensity = 1.25f;
        }
        void hud_update()
        {
            scoreTxt.SetText("Score: " + score); 
            if (time.value > 0 && isplaying)
                time.value --;   
            else if(time.value>0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;        
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isplaying = false;
                restartmenu.SetActive(true);
                GameObject.Find("Canvas").transform.Find("Crosshair").gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            hud_update();
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseGame(); 
        }
    }
}

