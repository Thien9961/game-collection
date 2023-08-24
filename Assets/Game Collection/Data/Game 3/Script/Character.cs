

using System.Collections.Generic;
using UnityEngine;

public class Character : Lifeform
{
    public float atk = 0;
    private BasicAttack basicattack;
    private Movement movement;
    private Jump jump;
    private Heal heal;
    private Switch[] wpn;

    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        basicattack = GetComponent<BasicAttack>();
        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
        heal = GetComponent<Heal>();
        wpn=GetComponents<Switch>();
        basicattack.damage += atk;
    }

    protected override void death()
    {
        animator.Play("Die");
        manager.isplaying = false;
        manager.menuRestart.SetActive(true);
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(manager.isplaying)
        {
            bool atkCnd=false,moveCnd=false,jumpCnd=false,healCnd=false,wpnCnd=false;
            basicattack.waitforinput(BasicAttack.HOLD,ref atkCnd);
            movement.waitforinput(Movement.HOLD, ref moveCnd);
            movement.waitforNegativeInput(Movement.HOLD, ref moveCnd);
            jump.waitforinput(Jump.PRESS, ref jumpCnd);
            heal.waitforinput(Heal.PRESS, ref healCnd);
            foreach (Ability a in wpn)
                a.waitforinput(Ability.PRESS, ref wpnCnd );
        }
          
    }
}
