using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering;
using Game2;

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
        bool moveLeftCnd = playerPos.x-1 > Road.lanecenter[0],moveRightCnd =playerPos.x+1 < Road.lanecenter[2], moveCnd=false ;
        if (direction == Vector3.left)
        {
            state = VehicleState.MOVE_LEFT;
            moveCnd = moveLeftCnd;
        }
        else if (direction == Vector3.right)
        {
            moveCnd= moveRightCnd;
            state = VehicleState.MOVE_RIGHT;
        }
        if (moveCnd && x<step)
        {
            float dx=step / (1 / Time.deltaTime * speedX);
            
            if (x+dx > step)
            {
                dx = step-x;
                x = step;
            }       
            else
                x += dx;
            transform.position += direction.normalized * dx;      
            return true;
            
        }
        else
        {
            x = 0;
            state = VehicleState.IDLE;
            transform.Rotate(Vector3.up * 0);
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Vehicle>()!= null)
            controlable = false;
    }
    void Start()
    {
        step = Mathf.Abs(Road.lanecenter[0] - Road.lanecenter[1]);
            
    }
    void Update()
    {
        if (gameObject != GameManager.player)
            controlable = false;
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
