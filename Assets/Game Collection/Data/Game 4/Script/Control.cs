using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float sensivity = 200;

    private void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
            {
                Color color = GetComponent<Renderer>().material.color;
                for (int i = 0; i < Puzzle.color.Length; i++)
                    if (color == Puzzle.color[i])
                    {
                        if (i + 1 < Puzzle.color.Length)
                        {
                            Puzzle.block_SetColor(gameObject, Puzzle.color[i+1]);
                            break;
                        }
                        else
                        {
                            Puzzle.block_SetColor(gameObject, Puzzle.color[0]);
                            break;
                        }
                    }
            }
                
    }

    public void confirm()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 velocity = new Vector3(Input.GetAxis("Mouse Y"), -1*Input.GetAxis("Mouse X"), 0) * Time.deltaTime * sensivity;
            GameObject parent = transform.parent.gameObject;
            Vector3 origin = Vector3.zero;
            GameObject grandparent;
            foreach (Transform child in parent.transform)
                origin += child.transform.position;
            origin /= parent.transform.childCount;
            grandparent = new GameObject("Parent of " + parent.name) ;
            grandparent.transform.position = origin;
            parent.transform.parent = grandparent.transform;
            grandparent.transform.Rotate(velocity);
            grandparent.transform.DetachChildren();
            Destroy(grandparent);

                
        }
    }
}
