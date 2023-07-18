using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Game2;

public class Wpn : MonoBehaviour
{
    public bool fullauto;
    public int ammoClipMax, ammoClip;
    public float reloadTime, accuracy , cooldown ,damage ;
    public GameObject ammo,muzzle,facing;
    public ParticleSystem muzzleflash;
    GameObject crosshair;
    AudioSource[] sfx;
    Game2.GameManager gmScript;
    

    bool isready=true;
    // Start is called before the first frame update
    public void give(GameObject whichuser)
    {
        GameObject userhand;
        for(int i =0;i< whichuser.transform.childCount;i++)
            if(whichuser.transform.GetChild(i).name=="Hand")
            {
                userhand = whichuser.transform.GetChild(i).gameObject;
                Instantiate(gameObject, userhand.transform.position, transform.rotation);
                break;
            }
    }
    IEnumerator firing(float sec)
    {
        yield return new WaitForSeconds(sec);
        isready = true;      
    }

    IEnumerator reloading(float sec)
    {
        
        yield return new WaitForSeconds(sec);
        isready = true;  
    }
    void wpn_fire()
    {
        if (isready && ammoClip>0)
        {
            GameObject user=transform.parent.gameObject;
            ammoClip--;
            isready = false;
            Instantiate(muzzleflash, muzzle.transform.position, muzzleflash.transform.rotation).Play();
            StartCoroutine(firing(cooldown));
            Rigidbody ammorb = Instantiate(ammo, muzzle.transform.position, ammo.transform.rotation).GetComponent<Rigidbody>();
            ammorb.AddForce((facing.transform.position-transform.position).normalized * 100f, ForceMode.Impulse);
        }
    }

    void wpn_reload()
    {
        if (ammoClip < ammoClipMax && isready)
        {
            isready = false;
            gmScript.progess.value = 0;
            gmScript.progess.gameObject.SetActive(true);
            StartCoroutine(reloading(reloadTime));
            while (ammoClip < ammoClipMax) 
                ammoClip++;
        }
    }
    void Start()
    {

        gmScript=GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
        gmScript.progess.maxValue = 1;
        gmScript.progess.value = gmScript.progess.maxValue;
        sfx =GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gmScript.isplaying)
        {
            switch (fullauto)
            {
                case false:
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                            wpn_fire();
                    }
                    break;
                default:
                    {
                        if (Input.GetAxisRaw("Fire1") == 1)
                            wpn_fire();
                    }
                    break;
            }
            if (Input.GetKeyDown(KeyCode.R))
                wpn_reload();
        }
        if(gmScript.progess.gameObject.activeSelf)
            if (gmScript.progess.value < 1)
                gmScript.progess.value += Time.deltaTime/reloadTime;
            else
                gmScript.progess.gameObject.SetActive(false);
    }
}

