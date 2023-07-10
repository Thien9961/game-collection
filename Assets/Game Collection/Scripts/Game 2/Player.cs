using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Game2
{
    public class Player : MonoBehaviour

    {
        public bool ispaused;
        public bool isplaying=true;
        public float score;

        public TextMeshProUGUI scoreTxt;
        public UnityEngine.UI.Slider mouseSensivity;
        public GameObject maincamera;
        

        float dx=20;
        KeyCode inputReload=KeyCode.R;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * dx*Time.deltaTime* Input.GetAxis("Mouse X") * mouseSensivity.value);
            transform.Rotate(Vector3.left * dx * Time.deltaTime * Input.GetAxis("Mouse Y") * mouseSensivity.value);
            if(Input.GetAxisRaw("Fire1")==1)
            {
                Debug.Log("Fire!");
            }
            else if (Input.GetKeyDown(inputReload))
            {
                Debug.Log("Reloading!");
            }

        }
    }
}

