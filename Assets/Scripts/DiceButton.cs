using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class DiceButton : MonoBehaviour
{
    public TurnManager turnManager;

    public GameObject[] player;
    [SerializeField]
    public GameObject move_Button;
    [SerializeField]
    public GameObject[] select_Button;

    [SerializeField]
    public GameObject Dice_Button;

    public int Move_result1;
    public int Move_result2;
    public int Dice_Effect =0;
    public Text move_text1;
    public Text move_text2;
    public Text dice_Text;
    public int select_num = 0;

    public int move_Buttonflag = 0;
    public int Dice_Buttonflag = 0;


    // Start is called before the first frame update
    void Start()
    {
        move_Button.SetActive(false);
        select_Button[0].SetActive(false);
        select_Button[1].SetActive(false);
        Dice_Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ボタンの表示切り替え
        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && move_Buttonflag == 0)
        {
            move_Button.SetActive(true);
        }else if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && move_Buttonflag == 1)
        {
            move_Button.SetActive(false);
            select_Button[0].SetActive(true);
            select_Button[1].SetActive(true);
        }else if(move_Buttonflag == 2)
        {
            move_Button.SetActive(false);
            select_Button[0].SetActive(false);
            select_Button[1].SetActive(false);
        }

        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && Dice_Buttonflag == 1)
        {
            Dice_Button.SetActive(true);
            dice_Text.text = "Bonus";
        }
        else if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && Dice_Buttonflag == 2)
        {
            Dice_Button.SetActive(true);
            dice_Text.text = "Minus";
        }



    }

    public void Dice()
    {
        Dice_Effect = Random.Range(1, 7);// 今回は１〜６の目が出るダイス
        Dice_Buttonflag = 0;
        Dice_Button.SetActive(false);
    }

    public void Move_click()
    {
        
        Move_result1 = Random.Range(1, 7);// 今回は１〜６の目が出るダイス
        Move_result2 = Random.Range(1, 7);// 今回は１〜６の目が出るダイス

        move_text1.text = Move_result1.ToString();
        move_text2.text = Move_result2.ToString();
        move_Buttonflag = 1;
    }
    public void Move1 (){
        select_num = Move_result1;
        move_text1.text = "select";
        move_text2.text = "select";
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().dice_select = true;
        //player[0].GetComponent<PlayerScript>().dice_select = true;
        
    }

    public void Move2()
    {
        select_num = Move_result2;
        move_text2.text = "select";
        move_text1.text = "select";
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().dice_select = true;
        //player[0].GetComponent<PlayerScript>().dice_select = true;
        
    }

    public void Stop_start()
    {
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().dice_select = true;
        //player[0].GetComponent<PlayerScript>().dice_select = true;
    }
}
