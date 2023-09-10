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
        WARRIOR=1<<3,
        MONSTER=1<<6,
    }
    public class GameManager : MonoBehaviour
    {
        public bool isplaying,spawning=false;
        public  int diffBase=0, diffIncr=3;
        public GameObject menuPause, menuRestart, player;
        public TextMeshProUGUI txtWave, txtEnemy;
        public int counterEnemy = 0, counterWave = 0;
        public GameObject[] enemy;
        public Bounds worldbound;

        // Start is called before the first frame update
        public void Exit()
        {
            SceneManager.LoadScene(0);
        }
        public void gameResume()
        {
            isplaying = true;
            menuPause.SetActive(false);
        }
        public void gamePause()
        {
            isplaying = false;
        }
        public void gameRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        void Start()
        {
            isplaying = true;
        }
        public void WaveStart()
        {
            if(counterWave==0)
            {    
                Cmr c = GameObject.Find("Main Camera").GetComponent<Cmr>();
                c.character = Instantiate(player, player.transform.position, player.transform.rotation);
                c.begin(c.character);
            }
            counterWave++;
            int diffWave = counterWave * diffIncr + diffBase, diff = 0, rng;
            Debug.Log("Difficult points of wave " + counterWave + " is " + diffWave);
            List<GameObject> list = new List<GameObject>();
            GameObject world = GameObject.Find("Playable Area");
            spawning = false;
            while (diffWave > diff)
            {
                rng = Random.Range(0, world.transform.childCount);
                Game3.Spawner platform = world.transform.GetChild(rng).GetComponent<Game3.Spawner>();
                foreach (GameObject g in enemy)
                    if (g.GetComponent<Enemy>().diffPts + diff <= diffWave)
                        list.Add(g);
                platform.SpawnRandom(list.ToArray(), ref diff);
                counterEnemy++;
            }
            Debug.Log("Total difficult points of existing enemies: " + diff);
        }

        public void WaveUpdate()
        {
            if (isplaying && counterEnemy == 0 && !spawning)
            {
                Invoke(nameof(WaveStart), 3);
                spawning = true;
            }
        }

        private void UI_update()
        {
            txtEnemy.SetText("Enemy: " + counterEnemy);
            txtWave.SetText("Wave: " + counterWave);
            
        }

        // Update is called once per frame
        void Update()
        {
            UI_update();
            WaveUpdate();
        }
    }
}

public class RemoveLeak : MonoBehaviour
{
    private void Update()
    {
        if(GetComponent<AudioSource>().isPlaying)
            Destroy(gameObject);
    }
}
