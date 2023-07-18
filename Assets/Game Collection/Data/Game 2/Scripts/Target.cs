using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game2;

public class Target : MonoBehaviour
{
    public float pts = 1f,hp = 1f,timebonus=0f,energybonus=0f;
    public ParticleSystem exp;
    public AudioClip clip;
    Game2.GameManager gmScript;
    private Color colorBase;

    // Start is called before the first frame update
    private void recover()
    {
        GetComponent<Renderer>().material.color = colorBase;
    }
    public void takedmg(float damage)
    {
        hp -= damage;
        AudioSource.PlayClipAtPoint(clip, transform.position);
        GetComponent<Renderer>().material.color = Color.red;
        Invoke(nameof(recover), 0.2f);
    }
    void Start()
    {
        colorBase = GetComponent<Renderer>().material.color;
        gmScript =GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
            Destroy(gameObject);
        if (!(hp > 0))
        {
            Instantiate(exp, transform.position,exp.transform.rotation).Play(); 
            gmScript.score+=pts;
            gmScript.time.value += timebonus*gmScript.fps;
            gmScript.energy.value += energybonus;
            Destroy(gameObject);
        }
    }
}
