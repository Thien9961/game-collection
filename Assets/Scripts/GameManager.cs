using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadscence()
    {
        SceneManager.LoadScene("Game");
        Debug.Log("game1");
    }
}
