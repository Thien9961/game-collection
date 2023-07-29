using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Ability
{
    public bool grounded=true;
    public float jumforce=10;

    Lifeform lfScript;
    // Start is called before the first frame update
    protected virtual void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }
    void Start()
    {
        lfScript = GetComponent<Lifeform>();
        lfScript.rb = GetComponent<Rigidbody>();
    }
    public override void start() 
    {
        if (lfScript.state==Lifeform.STATE_NORMAL && grounded)
            if (grounded && lfScript.state==Lifeform.STATE_NORMAL)
                lfScript.rb.AddForce(Vector3.up* jumforce, ForceMode.Impulse);
    }
    private void Update()
    {
        GetComponent<Animator>().SetBool("onGnd", grounded);
    }
}
