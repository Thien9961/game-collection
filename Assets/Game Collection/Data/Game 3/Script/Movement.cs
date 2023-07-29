using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Movement : Ability
{
    public KeyCode keyNegative;
    public float speed=1;
    Lifeform lfScript;

    public void startNegative()
    {
        GetComponent<Animator>().SetBool("moving",true);
        if (lfScript.state == Lifeform.STATE_NORMAL)
        {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(-1 * transform.forward * Time.deltaTime * speed);
        }
    }
    public void waitforNegativeInput(int inputType, ref bool cnd)
    {
        if (isready)
        {
            switch (inputType)
            {
                case 2:
                    {
                        cnd = Input.GetKeyUp(keyNegative);
                        break;
                    }

                case 1:
                    {
                        cnd = Input.GetKey(keyNegative);
                        break;
                    }

                case 0:
                    {

                        cnd = Input.GetKeyDown(keyNegative);
                        break;
                    }

                case 3:
                    {
                        cnd = true;
                        break;
                    }

                default:
                    {
                        cnd = false;
                        break;
                    }

            }
            if (cnd)
            {
                GetComponent<Animator>().SetBool("moving", cnd);
                if (cooldown > 0)
                {
                    isready = false;
                    Invoke(nameof(reset), cooldown);
                }
                startNegative();
            }
        }
    }
    public override void waitforinput(int inputType, ref bool cnd)
    {
        base.waitforinput(inputType, ref cnd);
        GetComponent<Animator>().SetBool("moving", cnd);
    }
    private void Start()
    {
        lfScript=GetComponent<Lifeform>();
    }

    public override void start()
    {
        GetComponent<Animator>().SetBool("moving", true);
        if (lfScript.state==Lifeform.STATE_NORMAL)
        {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(transform.forward * Time.deltaTime * speed);
        }
    }

}
