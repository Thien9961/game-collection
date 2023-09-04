
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : Lifeform
{
    public float atk=0;
    public int diffPts,wave;
    Game3.GameManager manager;
    Controller controller;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        manager = GameObject.Find("Game Manager").GetComponent<Game3.GameManager>();
        controller=GetComponent<Controller>();
    }
    private void OnDestroy()
    {
        manager.counterEnemy--;
    }
    protected override void death()
    {
        playSfx(onDeathSfx);
        animator.SetBool("alive", false);
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (manager.isplaying && hp.value>0)
            controller.control();   
    }
}
