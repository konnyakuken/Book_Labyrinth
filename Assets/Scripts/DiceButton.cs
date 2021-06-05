using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    public GameObject stop_Button;
    public int stop_button_flag=0;

    public static int winner=0;//勝利プレイヤーの表示

    public int Move_result1;
    public int Move_result2;
    public int Dice_Effect =0;
    public Text move_text1;
    public Text move_text2;
    public Text dice_Text;
    public int select_num = 0;

    public int move_Buttonflag = 0;
    public int Dice_Buttonflag = 0;


    public bool re_Move = false;


    // Start is called before the first frame update
    void Start()
    {
        move_Button.SetActive(false);
        select_Button[0].SetActive(false);
        select_Button[1].SetActive(false);
        Dice_Button.SetActive(false);
        stop_Button.SetActive(false);
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




        if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && stop_button_flag == 1)
        {
            stop_Button.SetActive(true);
        }else if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && stop_button_flag == 0)
            stop_Button.SetActive(false);


        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == true && player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().book_flag == true&& stop_button_flag == 1)
        {//本を持ったコンピューター
            Stop_mass();
        }

    }

    public void Dice()
    {
        Dice_Effect = Random.Range(1, 7);// 今回は１〜６の目が出るダイス
        if(Dice_Buttonflag == 1)
        turnManager.page[turnManager.currentPlayer % 4] += Dice_Effect*2;//出目×２のページを獲得
        else if(Dice_Buttonflag == 2)
            turnManager.page[turnManager.currentPlayer % 4] -= Dice_Effect * 2;//出目×２のページを失う

        if (turnManager.page[turnManager.currentPlayer % 4] <= 0)
            turnManager.page[turnManager.currentPlayer % 4] = 0;//マイナスにならないように調整

        Dice_Buttonflag = 0;
        Dice_Button.SetActive(false);
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
    }

    public void create_bookButton()//本を作成する関数
    {
        turnManager.page[turnManager.currentPlayer % 4] -= 100;
        //turnManager.create_book[turnManager.currentPlayer % 4] = true;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().book_flag = true;
        turnManager.book_image[turnManager.currentPlayer % 4].SetActive(true);
    }


    public void Stop_mass()//スタート止まるかどうか
    {
        
        if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().book_flag == true)
        {
            winner = turnManager.currentPlayer % 4 + 1;
            SceneManager.LoadScene("result");//シーン切り替え
        }
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_mass = 0;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
        stop_button_flag = 0;
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
