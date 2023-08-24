using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
 public class Ability : MonoBehaviour
{
    public float cooldown;
    public KeyCode key;
    public static readonly int PRESS=0,HOLD=1,RELEASE=2,ON=3,OFF=4;
    public bool isready=true;
    public AudioClip onCastSfx=null;
    protected virtual void reset()
    {
        isready = true;
    }
    public virtual void waitforinput(int inputType,ref bool cnd)
    {
        if (isready)
        {
            switch (inputType)
            {
                case 2:
                    {
                        cnd = Input.GetKeyUp(key);
                        break;
                    }

                case 1:
                    {
                        cnd = Input.GetKey(key);
                        break;
                    }

                case 0:
                    {

                        cnd = Input.GetKeyDown(key);
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
                if (cooldown > 0)
                {
                    isready = false;
                    Invoke(nameof(reset), cooldown);
                }
                start();
            }
        }
    }
    public virtual void start()
    {
        Debug.Log("Ability start");
        GetComponent<AudioSource>().PlayOneShot(onCastSfx);
    }
}
