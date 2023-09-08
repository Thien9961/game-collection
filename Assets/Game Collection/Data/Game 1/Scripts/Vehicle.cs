using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Net.Sockets;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;

public enum VehicleState
{
    MOVE_LEFT,
    MOVE_RIGHT,
    IDLE
}
public class Vehicle : MonoBehaviour
{

    public float speedX=100;
    delegate bool movement(Vector3 v);
    public KeyCode[] inputs=new KeyCode[2];
    public bool controlable=true;
    float x=0,step;
    public VehicleState state=VehicleState.IDLE;

    // Start is called before the first frame update

    bool moving(Vector3 direction)
    {

        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 playerRot = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        bool moveLeftCnd = playerPos.x > Road.lanecenter[0],moveRightCnd =playerPos.x < Road.lanecenter[2], moveCnd=false ;
        if (direction == Vector3.left)
        {
            Debug.Log("moving to left");
            state = VehicleState.MOVE_LEFT;
            moveCnd = moveLeftCnd;
        }
        else if (direction == Vector3.right)
        {
            moveCnd= moveRightCnd;
            state = VehicleState.MOVE_RIGHT;
            Debug.Log("moving to right");
        }
        if (moveCnd && x < step)
        {
            transform.Translate(direction.normalized * speedX * Time.deltaTime);
            x += speedX * Time.deltaTime;
            return true;
        }
        else
        {
            x = 0;
            state = VehicleState.IDLE;
            transform.Rotate(Vector3.up * 0);
            Debug.Log("stop moving");
        }
        return false;
    }

    void Start()
    {
        step = Mathf.Abs(Road.lanecenter[0] - Road.lanecenter[1]);
            
    }
    void Update()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().isplaying)
        {
            if (controlable)
            {
                movement positive = new movement(moving);
                movement negative = new movement(moving);
                switch (state)
                {
                    case VehicleState.IDLE:
                        {

                            if (Input.GetKeyDown(inputs[0]))
                                positive(Vector3.left);
                            if (Input.GetKeyDown(inputs[1]))
                                negative(Vector3.right);
                            break;
                        }
                    case VehicleState.MOVE_LEFT:
                        {
                            positive(Vector3.left);
                            break;
                        }
                    case VehicleState.MOVE_RIGHT:
                        {
                            negative(Vector3.right);
                            break;
                        }

                }

            }
        }
                
    }     
}
