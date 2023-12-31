using System.Collections;
using System.Collections.Generic;
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
    public ParticleSystem muzzleflash;
    public AudioClip firingSfx, reloadingSfx;
    static public TextMeshProUGUI ammoTxt;
    bool isready=true;
    // Start is called before the first frame update 

    
    IEnumerator firing(float sec)
    {
        yield return new WaitForSeconds(sec);
        isready = true;
        transform.parent.GetComponent<Animator>().SetBool("attacking", false);
    }

    IEnumerator reloading(float sec)
    {
        
        yield return new WaitForSeconds(sec);
        isready = true;
        transform.parent.GetComponent<Animator>().SetBool("reloading", false);
        
    }
    void wpn_fire()
    {
        if (isready && clip>0)
        {
            isready = false;
            Vector3 muzzle = transform.parent.transform.Find("Camera").transform.position;
            if (firingSfx!=null)
                GetComponent<AudioSource>().PlayOneShot(firingSfx);
            GameObject user=transform.parent.gameObject;
            clip--;        
            ParticleSystem p = Instantiate(muzzleflash);
            p.transform.parent = transform;
            p.transform.localPosition = muzzleflash.transform.localPosition;
            p.transform.rotation = muzzleflash.transform.localRotation;
            p.transform.localScale = muzzleflash.transform.localScale;
            var v = p.main;
            v.stopAction = ParticleSystemStopAction.Destroy;
            p.Play(false);
            StartCoroutine(firing(cooldown));
            Instantiate(ammunition, muzzle, ammunition.transform.rotation).GetComponent<Rigidbody>().AddForce(transform.parent.forward * launchForce, ForceMode.Impulse);
            transform.parent.GetComponent<Animator>().SetBool("attacking", true);
        }
    }

    void wpn_reload()
    {
        if (isready)
        {
            isready = false;
            transform.parent.GetComponent<Animator>().SetBool("reloading", true);
            if (reloadingSfx != null)
                GetComponent<AudioSource>().PlayOneShot(reloadingSfx);
            StartCoroutine(reloading(reloadTime));
            while (clip < clipMax)
                clip++;
        }
        
    }

    void Start()
    {
        ammoTxt =GameObject.Find("Canvas").transform.Find("Ammo Text").GetComponent<TextMeshProUGUI>();
        muzzleflash = transform.Find("Muzzle Flash").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoTxt.SetText("Ammo: "+clip + "/" + clipMax);
        
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

