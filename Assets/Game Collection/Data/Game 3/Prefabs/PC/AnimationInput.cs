using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInput : MonoBehaviour
{
    Animator controller;
    public readonly string[]? varName = { "moving", "jumping","attacking","alive"};

    public void SetInput(bool[] input)
    {
        string[]? s=new string[input.Length];
        for (int j = 0; j < input.Length; j++)
            s[j] = varName[j] ?? null;
        for(int i=0;i<s.Length;i++)
            controller.SetBool(s[i], input[i]);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
