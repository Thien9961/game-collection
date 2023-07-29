using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game2;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

namespace Game3
{
    public enum faction
    {
        WARRIOR,
        MONSTER,
        NEUTRAL
    }
    public class GameManager : MonoBehaviour
    {
        public bool isplaying;
        public List<UnityEngine.UI.Button> buff;
        public  int diffBase=0, diffIncr=3;
        public GameObject menuPause, menuRestart, buttonPause,menuBuff,player;
        public TextMeshProUGUI txtWave, txtEnemy;
        public Bounds worldbound;

        // Start is called before the first frame update
        public void gameResume()
        {
            isplaying = true;
            menuPause.SetActive(false);
            menuBuff.SetActive(false);
            buttonPause.SetActive(true);
        }
        public void gameRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        void Start()
        {
            Cmr c = GameObject.Find("Main Camera").GetComponent<Cmr>();
            c.character = Instantiate(player, player.transform.position, player.transform.rotation);
            isplaying = true;
            worldbound=new Bounds(Vector3.zero,Vector3.one*200);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
