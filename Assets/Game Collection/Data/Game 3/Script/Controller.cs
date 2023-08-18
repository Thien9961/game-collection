using Game3;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class Controller : MonoBehaviour
{
    public float attackRange;
    private BasicAttack basicattack;
    private Movement movement;

    public allowedTarget attackTarget;
    private enum Signal
    {
        POSITIVE,
        NEGATIVE,
        NONE
    }
    private Signal signalMove = Signal.POSITIVE;
    // Start is called before the first frame update
    private bool isAttacking;


    private void move(Signal s)
    {
        bool moveCnd = false;
        if (s == Signal.POSITIVE)
            movement.waitforinput(Ability.ON, ref moveCnd);
        else if (s == Signal.NEGATIVE)
            movement.waitforNegativeInput(Ability.ON, ref moveCnd);
    }

    private void attack(ref bool attacking,GameObject whichtarget)
    {
        bool attackCnd = false;
        Lifeform target=whichtarget.GetComponent<Lifeform>();
        if (target != null)
        {
            switch (attackTarget)
            {
                case allowedTarget.ENEMY:
                    {
                        if (target.Faction != GetComponent<Lifeform>().Faction)
                        {
                            attacking = true;
                            basicattack.waitforinput(Ability.ON, ref attackCnd);

                        }
                        else
                            attacking = false;
                        break;
                    }
                case allowedTarget.ALLIED:
                    {
                        if (target.Faction == GetComponent<Lifeform>().Faction)
                        {
                            attacking = true;
                            basicattack.waitforinput(Ability.ON, ref attackCnd);

                        }
                        else
                            attacking = false;
                        break;
                    }
                case allowedTarget.ALL:
                    {
                        attacking = true;
                        basicattack.waitforinput(Ability.ON, ref attackCnd);

                        break;
                    }
                default:
                    {
                        attacking = false;
                        break;
                    }
            }
        }
        
    }
    public void control()
    {

        RaycastHit hitGnd, hitTarget;
        bool detectTarget;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bool detectGnd = Physics.Raycast(new Ray(origin, Vector3.down), out hitGnd, 1);
        UnityEngine.Debug.DrawRay(origin, Vector3.down, Color.green);
        detectTarget = Physics.Raycast(new Ray(transform.position, transform.forward), out hitTarget, attackRange, (int)faction.WARRIOR);
        if (detectTarget)
            attack(ref isAttacking, hitTarget.collider.gameObject);
        else
            isAttacking=false;
        if (detectGnd && !isAttacking)
            move(signalMove);
        else if (!detectGnd && !isAttacking)
        {
            if (signalMove == Signal.POSITIVE)
                signalMove = Signal.NEGATIVE;
            else if (signalMove == Signal.NEGATIVE)
                signalMove = Signal.POSITIVE;
            move(signalMove);
        }
        else
            move(Signal.NONE);
    }
    void Start()
    {
        basicattack = GetComponent<BasicAttack>();
        movement = GetComponent<Movement>();
        basicattack.damage += GetComponent<Enemy>().atk;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
