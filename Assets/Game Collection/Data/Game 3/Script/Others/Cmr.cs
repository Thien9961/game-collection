using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmr : MonoBehaviour
{
    public GameObject character;
    public Vector3 offset;
    public Material[] skyMaterial;
    public AudioClip[] ambientSfx;
    // Start is called before the first frame update
    void Start()
    {
        int rng = Random.Range(0, skyMaterial.Length);
        RenderSettings.skybox = skyMaterial[rng];
        rng = Random.Range(0, ambientSfx.Length);
        AudioSource source = GetComponent<AudioSource>();
        source.clip = ambientSfx[rng];
        source.loop = true;
        source.Play();
    }
    public void begin(GameObject player)
    {
        Camera cam = GetComponent<Camera>();
        for (int i = 0; i < player.transform.childCount; i++)
        {
            if (player.transform.GetChild(i).name == "Canvas")
            {
                GameObject g = player.transform.GetChild(i).gameObject;
                transform.position = player.transform.position + offset;
                g.transform.GetChild(0).transform.position = cam.WorldToScreenPoint(transform.position-offset*1.05f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = character.transform.position+offset;
    }
}
