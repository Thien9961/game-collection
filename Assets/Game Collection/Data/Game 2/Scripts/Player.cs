using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Data;
using UnityEditor;
using Unity.VisualScripting;
using System;
using System.Linq;

namespace Game2
{
    public class Player : MonoBehaviour

    {
        UnityEngine.UI.Slider mouseSensivity;
        public float energyCap = 100, duration = 10f;
        public Game2.GameManager gmScript;
        public UnityEngine.UI.Slider energy;
        float dx=20;
        // Start is called before the first frame update
        public void SetWpn(GameObject whichwpn)
        {
            if(whichwpn.GetComponent<Wpn>() != null && GetWpn()!=null)
            {
                Wpn wpn = GetWpn().GetComponent<Wpn>();
                Wpn newWpn = whichwpn.GetComponent<Wpn>();
                wpn.fullauto = newWpn.fullauto;
                wpn.clip = newWpn.clip;
                wpn.clipMax = newWpn.clipMax;
                wpn.firingSfx = newWpn.firingSfx;
                wpn.launchForce = newWpn.launchForce;
                wpn.muzzleflash = newWpn.muzzleflash;
                wpn.ammunition = newWpn.ammunition;
                wpn.accuracy = newWpn.accuracy;
                wpn.reloadingSfx = newWpn.reloadingSfx;
                wpn.reloadTime = newWpn.reloadTime;
                wpn.trigger = newWpn.trigger;
                wpn.gameObject.name = whichwpn.gameObject.name;
            } 
        }

        public Wpn GetWpn()
        {
            foreach (Transform t in transform)
                if (t.GetComponent<Wpn>() != null)
                    return t.GetComponent<Wpn>();
            return null;
        }
        public bool HasWeapon(Wpn weapon)
        {
            Wpn weapon1 = GetWpn();
            Wpn weapon2 = weapon;
            bool[] a = new bool[12];
            a[0] = weapon1.fullauto == weapon2.fullauto;
            a[1] = true;
            a[2] = weapon1.clipMax == weapon2.clipMax;
            a[3] = weapon1.firingSfx == weapon2.firingSfx;
            a[4] = weapon1.launchForce == weapon2.launchForce;
            a[5] = true;
            a[6] = weapon1.ammunition == weapon2.ammunition;
            a[7] = weapon1.accuracy == weapon2.accuracy;
            a[8] = weapon1.reloadingSfx == weapon2.reloadingSfx;
            a[9] = weapon1.reloadTime == weapon2.reloadTime;
            a[10] = weapon1.trigger == weapon2.trigger;
            a[11] = weapon1.gameObject.name == weapon2.gameObject.name;
            foreach (bool b in a)
                if (!b)
                    return false; 
            return true;

        }
        public void  Action()
        {
            Game2.Player player = GetComponent<Player>();
            GameObject[] list = gmScript.Weapons;
            Debug.Log("Energy: "+energy.value);
            if (HasWeapon(gmScript.Weapons[gmScript.DefaulWpnIndex].GetComponent<Wpn>()))
                if (energy.value >= energy.maxValue)
                {
                    player.SetWpn(list[1]);
                    Debug.Log(GetWpn().gameObject.name);
                }     
        }

        void Start()
        {
            
            energy = GameObject.Find("Canvas").transform.Find("Energy").GetComponent<Slider>();
            mouseSensivity= GameObject.Find("Canvas").transform.Find("Pause Menu").transform.Find("Mouse Sensivity").GetComponent<Slider>();
            energy.onValueChanged.AddListener(delegate { Action(); });
            energy.value = 0;
            energy.maxValue = energyCap;
            Animator animator = GetComponent<Animator>();
            animator.Play("Shoot_SingleShot_AR");
        }

        // Update is called once per frame
        void Update()
        {
            if(gmScript.isplaying)
            {
                Game2.Player player = GetComponent<Player>();
                GameObject[] list = gmScript.Weapons;
                if (HasWeapon(gmScript.Weapons[1].GetComponent<Wpn>()))
                {
                    if (energy.value == 0)
                    {
                        player.SetWpn(list[gmScript.DefaulWpnIndex]);
                        Debug.Log(GetWpn().gameObject.name);
                    }
                    else
                        energy.value -= energyCap / duration / (1 / Time.deltaTime);
                }
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

