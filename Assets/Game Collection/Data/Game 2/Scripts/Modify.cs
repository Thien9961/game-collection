using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modify : Effect
{
    public float score, time, energy; 
    public int ammo;
    // Start is called before the first frame update

    public override void trigger()
    {
        Game2.GameManager manager=GameObject.Find("Game Manager").GetComponent<Game2.GameManager>();
        Game2.Player player = manager.player.GetComponent<Game2.Player>();
        manager.score += score;
        player.energy.value += energy;
        player.GetWpn().GetComponent<Wpn>().clip += ammo;
        manager.time.value += time;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
