using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DiceButton : MonoBehaviour
{
    public PlayerScript playerScript;
    public GameObject Move_Button;
    public int Move_result1;
    public int Move_result2;
    // Start is called before the first frame update
    void Start()
    {
        //Move_Button.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move_click()
    {
        playerScript.dice_select = true;
        Move_result1 = Random.Range(1, 7);//// 今回は１〜６の目が出るダイス
        Move_result2 = Random.Range(1, 7);//// 今回は１〜６の目が出るダイス

        Debug.Log("1:"+Move_result1);
        Debug.Log("2:" + Move_result2);
    }
}
