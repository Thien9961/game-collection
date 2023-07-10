using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Move : MonoBehaviour
{
    public bool[] resource_type = {true,true,true};
    public float resource_amount=0;
    public float resource_spawnchance=0;
    public ParticleSystem particle;

    AudioSource sfx;
    Player player_script;
    GameManager gm_script;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        player_script = gm_script.pc.GetComponent<Player>();
        if (collision.gameObject.CompareTag("Player") )
        {
            Instantiate(particle, transform.position, particle.transform.rotation);
            Destroy(gameObject);
            sfx.Play();
            gm_script.score++;
            player_script.diff += player_script.diff_increaserate * gm_script.score;
            player_script.diff_increaserate /= 2f;
            if (gameObject.CompareTag("Good") || gameObject.CompareTag("Bad"))
            {
                for (int i = 0; i < resource_type.Length; i++)
                {
                    if (resource_type[i])
                        gm_script.slider[i].value += Random.Range(resource_amount,2*resource_amount);
                }
            }
            else
            {
                for(int i = 0; i < resource_type.Length; i++)
                {
                    if (resource_type[i])
                        gm_script.slider[i].value += resource_amount;
                }
            }
        }       
    }
    void Start()
    {
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();
        gm_script = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        player_script = gm_script.pc.GetComponent<Player>();
        if (!gameObject.CompareTag("Player"))
            transform.GetChild(0).Rotate(Vector3.forward * 90*Time.deltaTime);
        if (transform.position.z > -450 && !player_script.ispaused && !player_script.isdead)
            transform.Translate(Vector3.back*player_script.dz*Time.deltaTime * player_script.diff);
        if(transform.position.z<=-450)
            Destroy(gameObject);
    }
}
