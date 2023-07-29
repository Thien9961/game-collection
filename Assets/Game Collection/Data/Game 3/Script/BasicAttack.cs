using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;

public class BasicAttack : Ability
{
    public float damage;
    public Vector3 launchforce;
    public List<GameObject> projectile;
    public int projectileIndex = 0;


    public override void waitforinput(int inputType, ref bool cnd)
    {
        if (UnityEngine.Input.GetKey(key))
            GetComponent<Animator>().SetBool("attacking", true);
        else
            GetComponent<Animator>().SetBool("attacking", false);
        base.waitforinput(inputType, ref cnd);

    }
    public override void start()
    {
        Debug.Log(gameObject.name + " is performing basic attack");
        Vector3 v = new Vector3(transform.position.x, projectile[projectileIndex].transform.position.y+transform.position.y, transform.position.z);
        Projectile projectileScript= Instantiate(projectile[projectileIndex],v, transform.rotation).GetComponent<Projectile>();
        projectileScript.damage += damage;
        projectileScript.Faction = GetComponent<Lifeform>().Faction;
        if (projectileScript.rb != null )
            projectileScript.rb.AddForce(launchforce, ForceMode.Impulse);
    }
}
