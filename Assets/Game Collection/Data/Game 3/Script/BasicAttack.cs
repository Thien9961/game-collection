using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;

public class BasicAttack : Ability
{
    public float damage;
    public float launchforce;
    public List<GameObject> projectile;
    public int projectileIndex = 0;

    public override void start()
    {
        Vector3 v = new Vector3(transform.position.x, projectile[projectileIndex].transform.position.y+transform.position.y, transform.position.z);
        GameObject p = Instantiate(projectile[projectileIndex], v,transform.rotation);
        Projectile projectileScript=p.GetComponent<Projectile>();
        projectileScript.damage += damage;
        projectileScript.Faction = GetComponent<Lifeform>().Faction;
        if (projectileScript.rb != null )
            projectileScript.rb.AddForce(transform.forward*launchforce, ForceMode.Impulse);
    }
}
