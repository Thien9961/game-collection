using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using System;

namespace Game2
{
    public class GameManager : MonoBehaviour
    {
        public int fps;
        public bool isplaying;
        public float score,gameTime=180,energyCap=100,duration=10f;
        public TextMeshProUGUI scoreTxt,ammoTxt;
        public GameObject restartmenu,pausemenu;
        public List<GameObject> weapon;
        public UnityEngine.UI.Slider time,energy,progess;

        Game2.Player playerScript;
        Wpn wpnScript;
        // Start is called before the first frame update
        private void energyManager()
        {
            GameObject wpnCurrent=playerScript.getwpn(weapon) ;
            if (energy.value >= energyCap && wpnCurrent.name == weapon[0].name)
            {
                Destroy(wpnCurrent);
                player_givewpn(GameObject.Find("Player"), weapon[UnityEngine.Random.Range(1, weapon.Count)]);
            }
            else if (wpnCurrent.name != weapon[0].name && energy.value > 0)
                energy.value -= energyCap/duration/(1/Time.deltaTime);
            else if (wpnCurrent.name != weapon[0].name)
            {
                Destroy(wpnCurrent);
                player_givewpn(GameObject.Find("Player"), weapon[0]);
            }

        }
        public void player_givewpn(GameObject whichuser, GameObject whichwpn)
        {
            GameObject newwpn = Instantiate(whichwpn, whichwpn.transform.position, whichuser.transform.rotation);
            wpnScript=newwpn.GetComponent<Wpn>();
            newwpn.transform.parent = whichuser.transform;
            newwpn.name=whichwpn.name;
            newwpn.SetActive(true);
        }
        public void gameResume()
        {
            isplaying = true;
            pausemenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        public void gamePause()
        {
            pausemenu.SetActive(true);
            isplaying = false;
        }
        public void gameRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);           
        }
        void Start()
        {
            fps = (int)(1 / Time.deltaTime);
            isplaying = true;
            time.value = gameTime * fps ;
            energy.value= 0;
            energy.maxValue = energyCap;
            score = 0;
            playerScript=GameObject.Find("Player").GetComponent<Game2.Player>();
            player_givewpn(GameObject.Find("Player"),weapon[0]);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        void hud_update()
        {
            scoreTxt.SetText("Score: " + score);
            ammoTxt.SetText(wpnScript.ammoClip+"/"+wpnScript.ammoClipMax);
            if (time.value > 0 && isplaying)
                time.value -= Time.deltaTime;
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
            }
        }

        // Update is called once per frame
        void Update()
        {
            hud_update();
            energyManager();
            if (Input.GetKeyDown(KeyCode.Escape))
                gamePause(); 
            
        }
    }
}

