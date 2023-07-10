using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wpn : MonoBehaviour
{
    public int dmg=1;
    public float reloadTime=2;
    public int ammoClip = 6;
    public float accuracy=1;
    public float cooldown=1;
    public GameObject ammoType;
    public ParticleSystem muzzleflash;

    bool isready=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
