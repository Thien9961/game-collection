using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ENV : MonoBehaviour
{
    public Material[] skyMaterial;
    public AudioClip[] ambientSfx;
    // Start is called before the first frame update
    void Start()
    {
        int rng= Random.Range(0, skyMaterial.Length);
        RenderSettings.skybox = skyMaterial[rng];
        rng=Random.Range(0,ambientSfx.Length);
        AudioSource source = GetComponent<AudioSource>();
        source.clip = ambientSfx[rng];
        source.loop = true;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
