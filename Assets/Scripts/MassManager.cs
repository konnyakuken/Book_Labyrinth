using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassManager : MonoBehaviour
{
    public bool isLeft = false;//0
    public bool isRight = false;//1
    public bool isUp = false;//2
    public bool isDown = false;//3
    public bool[] cross = { false, false, false, false };

    public float isRight_mass = 2.1f;//移動速度
    public float isLeft_mass = -2.1f;
    public float isUp_mass = 2.1f;
    public float isDown_mass = -2.1f;
    // Start is called before the first frame update
    void Start()
    {
        if(isLeft == true)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
