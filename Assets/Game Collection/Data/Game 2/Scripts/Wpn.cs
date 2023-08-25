using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Game2;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class Wpn : MonoBehaviour
{
    public KeyCode trigger = KeyCode.Mouse0,reload = KeyCode.R;
    public bool fullauto;
    public int clipMax, clip;
    public float reloadTime, accuracy, cooldown,launchForce = 100;
    public GameObject ammunition;
    static public GameObject muzzle;
    public ParticleSystem muzzleflash;
    public AudioClip firingSfx, reloadingSfx;
    static public TextMeshProUGUI ammoTxt;
    bool isready=true;
    bool isreloading = false;
    // Start is called before the first frame update 

    
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
        if (isready && clip>0)
        {
            if(firingSfx!=null)
                GetComponent<AudioSource>().PlayOneShot(firingSfx);
            GameObject user=transform.parent.gameObject;
            clip--;
            isready = false;
            Instantiate(muzzleflash, transform.position, muzzleflash.transform.rotation).Play();
            StartCoroutine(firing(cooldown));
            Instantiate(ammunition, muzzle.transform.position, ammunition.transform.rotation).GetComponent<Rigidbody>().AddForce(transform.parent.forward * launchForce, ForceMode.Impulse);
            transform.parent.GetComponent<Animator>().Play("Shoot_SingleShot_AR");
        }
    }

    void wpn_reload()
    {
        transform.parent.GetComponent<Animator>().Play("Reload");
        if(reloadingSfx!=null)
            GetComponent<AudioSource>().PlayOneShot(reloadingSfx);
        isready = false;
        isreloading=true;
        StartCoroutine(reloading(reloadTime));
        while (clip < clipMax) 
            clip++;
    }

    void Start()
    {
        ammoTxt=GameObject.Find("Canvas").transform.Find("Ammo Text").GetComponent<TextMeshProUGUI>();
        muzzle = transform.Find("Muzzle").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ammoTxt.SetText(clip + "/" + clipMax);
        
        if (GameObject.Find("Game Manager").GetComponent<Game2.GameManager>().isplaying)
        {
            if (!fullauto)
            {
                if (Input.GetKeyDown(trigger))
                    wpn_fire();
            }
            else 
                if (Input.GetKey(trigger))
                    wpn_fire();
        }  
        if (Input.GetKeyDown(reload))
            wpn_reload();
    }
}

