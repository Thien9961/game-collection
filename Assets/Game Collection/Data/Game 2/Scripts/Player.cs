using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Data;
using UnityEditor;
using Unity.VisualScripting;

namespace Game2
{
    public class Player : MonoBehaviour

    {
        public UnityEngine.UI.Slider mouseSensivity;
        Game2.GameManager gmScript;
        Wpn wpnScript;
        float dx=20;
        // Start is called before the first frame update
        public GameObject getwpn(List<GameObject> wpnList)
        {
            int j,i;
            for (i = 0; i < wpnList.Count; i++)
                for (j = 0; j < transform.childCount; j++)
                    if (transform.GetChild(j).name == wpnList[i].name)
                        return transform.GetChild(j).gameObject;
            return null;
        }
        void Start()
        {
            gmScript = GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if(gmScript.isplaying)
            {
                float a = transform.rotation.eulerAngles.x + -1 * dx * Time.deltaTime * Input.GetAxis("Mouse Y") * mouseSensivity.value, b= transform.rotation.eulerAngles.y + dx * Time.deltaTime * Input.GetAxis("Mouse X") * mouseSensivity.value;
                if (a < 60)
                    transform.rotation = Quaternion.Euler(a, b, 0);
                else if(a>=60 && a<180)
                    transform.rotation = Quaternion.Euler(60, b, 0);
                else if (a>180 && a<=300)
                    transform.rotation = Quaternion.Euler(-60, b, 0);
                else
                    transform.rotation = Quaternion.Euler(a, b, 0);
            }       
        }
    }
}

