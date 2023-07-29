
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : Lifeform
{
    public float atk=0;
    public int diffPts,wave;
    Game3.Spawner spawner;
    private BasicAttack basicattack;
    private Movement movement;
    Controller controller;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        spawner = GameObject.Find("Spawner").GetComponent<Game3.Spawner>();
        controller=GetComponent<Controller>();
    }
    private void OnDestroy()
    {
        spawner.counterEnemy--;
    }
    protected override void death()
    {
        animator.Play("Die");
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (manager.isplaying)
            controller.control();
    }
}
