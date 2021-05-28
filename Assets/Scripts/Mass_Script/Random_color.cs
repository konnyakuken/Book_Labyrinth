using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_color : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの色をRGBA値を用いて変更する
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
