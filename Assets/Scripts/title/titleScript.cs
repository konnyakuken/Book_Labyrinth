using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour
{
    public static int player_num=0;
    // Start is called before the first frame update
    public GameObject start_number;
    public GameObject select_Button;
    public GameObject title_object;
    public GameObject rule_object;
    void Start()
    {
        start_number.SetActive(false);
        select_Button.SetActive(true);
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
    public void Three_person()
    {
        player_num = 3;
        SceneManager.LoadScene("Main");//シーン切り替え
    }
    public void Four_person()
    {
        player_num = 4;
        SceneManager.LoadScene("Main");//シーン切り替え
    }

    public void Start_Button()
    {
        select_Button.SetActive(false);
        start_number.SetActive(true);
        //title_object.SetActive(false);
        //rule_object.SetActive(true);

    }
    public void Rule_Button()
    {
        //select_Button.SetActive(false);
        title_object.SetActive(false);
        rule_object.SetActive(true);

    }
}
