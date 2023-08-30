

using System.Collections.Generic;
using UnityEngine;

public class Character : Lifeform
{
    public float atk = 0;
    private BasicAttack basicattack;
    private Movement[] movement;
    private Jump jump;
    private Switch[] wpn;
    private Guard guard;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        basicattack = GetComponent<BasicAttack>();
        movement = GetComponents<Movement>();
        jump = GetComponent<Jump>();
        wpn=GetComponents<Switch>();
        guard = GetComponent<Guard>();
        basicattack.damage += atk;
    }

    protected override void death()
    { 
        animator.SetBool("alive", false);
        manager.isplaying = false;
        manager.menuRestart.SetActive(true);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(manager.isplaying)
        {
            bool atkCnd = false, moveCnd = false, jumpCnd = false, wpnCnd = false, guardCnd=false;
            basicattack.waitforinput(BasicAttack.HOLD,ref atkCnd);
            jump.waitforinput(Jump.PRESS, ref jumpCnd);
            guard.waitforinput(Guard.PRESS, ref guardCnd);
            foreach (Ability a in wpn)
                a.waitforinput(Ability.PRESS, ref wpnCnd );
            foreach (Ability m in movement)
                m.waitforinput(Ability.HOLD,ref moveCnd);
        }
          
    }
}
