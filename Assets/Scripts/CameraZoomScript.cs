using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zoom_on_obj;
    public GameObject zoom_off_obj;

    public GameObject[] zoom_object;
    void Start()
    {
        zoom_on_obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Zoom_on()
    {
        zoom_object[0].SetActive(false);
        zoom_object[1].SetActive(true);
        zoom_on_obj.SetActive(true);
        zoom_off_obj.SetActive(false);
    }
    public void Zoom_off()
    {
        zoom_object[0].SetActive(true);
        zoom_object[1].SetActive(false);
        zoom_on_obj.SetActive(false);
        zoom_off_obj.SetActive(true);
    }
}
