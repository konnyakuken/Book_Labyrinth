using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp_color : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの色をRGBA値を用いて変更する
        GetComponent<Renderer>().material.color = Color.magenta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
