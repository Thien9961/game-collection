using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Bounds bound;
    // Start is called before the first frame update
    void Start()
    {
        bound=new Bounds(transform.position, new Vector3(100, 5, 100));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
