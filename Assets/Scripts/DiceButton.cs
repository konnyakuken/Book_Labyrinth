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
    public Text move_text1;
    public Text move_text2;
    public int select_num = 0;
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
        
        Move_result1 = Random.Range(1, 7);//// 今回は１〜６の目が出るダイス
        Move_result2 = Random.Range(1, 7);//// 今回は１〜６の目が出るダイス

        move_text1.text = Move_result1.ToString();
        move_text2.text = Move_result2.ToString();
        //Debug.Log("1:"+Move_result1);
        //Debug.Log("2:" + Move_result2);
    }
    public void Move1 (){
        select_num = Move_result1;
        move_text1.text = "select";
        move_text2.text = "select";
        playerScript.dice_select = true;
        
    }

    public void Move2()
    {
        select_num = Move_result2;
        move_text2.text = "select";
        move_text1.text = "select";
        playerScript.dice_select = true;
        
    }

    public void Stop_start()
    {
        playerScript.move=false;
    }
}
