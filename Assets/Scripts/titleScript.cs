using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour
{
    public static int player_num=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void One_person()
    {
        player_num = 1;
        SceneManager.LoadScene("Main");//シーン切り替え
    }

    public void Two_person()
    {
        player_num = 2;
        SceneManager.LoadScene("Main");//シーン切り替え
    }
}
