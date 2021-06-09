using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class DiceButton : MonoBehaviour
{
    public TurnManager turnManager;
    public SkillScript skillscript;


    public GameObject[] player;
    [SerializeField]
    public GameObject move_Button;
    [SerializeField]
    public GameObject[] select_Button;

    [SerializeField]
    public GameObject Dice_Button;

    [SerializeField]
    public GameObject stop_Button;

    [SerializeField]
    public GameObject[] branch_Button;//0=up,1=down,2=right,3=left

    [SerializeField]
    public GameObject skil_Bottun;

    [SerializeField]
    public GameObject skil_board;

    [SerializeField]
    public GameObject Exit_button;


    [SerializeField]
    public GameObject canvas_UI;//画面が見にくい為

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
    public bool skil_flag = false;
    public bool skil_end = false;//1ターンに一度スキル使用可能

    public GameObject Dice_number;
    public Text Dice_numberText;

    // Start is called before the first frame update
    void Start()
    {
        canvas_UI.SetActive(true);
        skil_board.SetActive(false);

        move_Button.SetActive(false);
        select_Button[0].SetActive(false);
        select_Button[1].SetActive(false);
        Dice_Button.SetActive(false);
        stop_Button.SetActive(false);
        skil_Bottun.SetActive(false);
        Dice_number.SetActive(false);
        for (int i = 0; i < 4; i++)
            branch_Button[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //ボタンの表示切り替え
        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && move_Buttonflag == 0 && skil_flag == false && skil_end == false)//スキルが発動したかどうかで切り替わり
        {
            move_Button.SetActive(true);
            skil_Bottun.SetActive(true);
        } else if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && move_Buttonflag == 0 && skil_flag == false && skil_end == true)
        {
            move_Button.SetActive(true);
        } else if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && move_Buttonflag == 0 && skil_flag == true)
        {
            move_Button.SetActive(false);
            skil_Bottun.SetActive(false);
        }
        else if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && move_Buttonflag == 1)
        {
            skil_Bottun.SetActive(false);
            move_Button.SetActive(false);
            select_Button[0].SetActive(true);
            select_Button[1].SetActive(true);
        } else if (move_Buttonflag == 2)
        {
            move_Button.SetActive(false);
            select_Button[0].SetActive(false);
            select_Button[1].SetActive(false);
            skil_Bottun.SetActive(false);
        }

        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && Dice_Buttonflag == 1)//プラマイマス効果表示
        {
            Dice_Button.SetActive(true);
            dice_Text.text = "Bonus";
        }
        else if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && Dice_Buttonflag == 2)
        {
            Dice_Button.SetActive(true);
            dice_Text.text = "Minus";
        }




        if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && stop_button_flag == 1)//stop確認
        {
            stop_Button.SetActive(true);
        }else if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && stop_button_flag == 0)
            stop_Button.SetActive(false);


        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == true && player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().book_flag == true&& stop_button_flag == 1)
        {//本を持ったコンピューター
            Stop_mass();
        }

        //0=up,1=down,2=right,3=left
        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().computer == false && player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag == true)
        {
            if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_Left == true)
                branch_Button[3].SetActive(true);

            if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_Right == true)
                branch_Button[2].SetActive(true);

            if(player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_Up == true)
                branch_Button[0].SetActive(true);

            if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_Down == true)
                branch_Button[1].SetActive(true);

        }

        Dice_numberText.text = (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_mass).ToString();//Diceカウント表示
        if (player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move == true)
            Dice_number.SetActive(true);
        else
            Dice_number.SetActive(false);
        


    }

    public void Dice()
    {
        Dice_Effect = Random.Range(1, 7);// 今回は１〜６の目が出るダイス
        if(Dice_Buttonflag == 1)
        {
            turnManager.page[turnManager.currentPlayer % 4] += Dice_Effect * 2;//出目×２のページを獲得
            Debug.Log(Dice_Effect * 2+"枚のページを獲得！");
        }
        else if(Dice_Buttonflag == 2)
        {
            turnManager.page[turnManager.currentPlayer % 4] -= Dice_Effect * 2;//出目×２のページを失う
            Debug.Log(Dice_Effect * 2 + "枚のページを紛失！");
        }
            

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
        stop_button_flag = 0;
        stop_Button.SetActive(false);
        for (int i = 0; i < 4; i++)
            branch_Button[i].SetActive(false);
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_mass = 0;
        //player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().end_processing();
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_count = 0;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().SwitchPlayer();
        stop_button_flag = 0;
    }

    public void Move_click()
    {
        
        Move_result1 = Random.Range(1, 7);// 今回は１〜６の目が出るダイス
        Move_result2 = Random.Range(1, 7);// 今回は１〜６の目が出るダイス
        if(skillscript.future_sight_flag == true)
        {
            Move_result1= skillscript.select_dice_num;
            Move_result2 = skillscript.select_dice_num;
            skillscript.select_dice_num = 1;
            skillscript.future_sight_flag = false;
        }

        if(skillscript.overflow == true)
        {
            move_text1.text = Move_result1.ToString()+"+2";
            move_text2.text = Move_result2.ToString()+"+2";
            Move_result1 += 2;
            Move_result2 += 2;
        }
        else
        {
            move_text1.text = Move_result1.ToString();
            move_text2.text = Move_result2.ToString();
        }
           move_Buttonflag = 1;
    }
    public void Move1 (){
        select_num = Move_result1;
        move_text1.text = "select";
        move_text2.text = "select";
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().dice_select = true;
        
    }

    public void Move2()
    {
        select_num = Move_result2;
        move_text2.text = "select";
        move_text1.text = "select";
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().dice_select = true;
        
    }

    public void Stop_start()
    {
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().dice_select = true;
    }


    public void Branch_up()
    {
        
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().next_z += 2.1f;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
        for (int i = 0; i < 4; i++)
            branch_Button[i].SetActive(false);
    }

    public void Branch_down()
    {
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().next_z -= 2.1f;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
        for (int i = 0; i < 4; i++)
            branch_Button[i].SetActive(false);
    }

    public void Branch_right()
    {
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().next_x += 2.1f;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
        for (int i = 0; i < 4; i++)
            branch_Button[i].SetActive(false);
    }

    public void Branch_left()
    {
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().next_x -= 2.1f;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().move_one = true;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().branch_flag = false;
        player[turnManager.currentPlayer % 4].GetComponent<PlayerScript>().start_branch = false;
        for (int i = 0; i < 4; i++)
            branch_Button[i].SetActive(false);
    }

    
    public void Skil_open()
    {
        skil_board.SetActive(true);
        skil_flag = true;
    }

    public void Skil_close()
    {
        skillscript.skil_page = 1;
        
        skil_flag = false;
        skil_board.SetActive(false);

        

        skillscript.StopCoroutine(skillscript._someCoroutine);


        skillscript.warning.SetActive(false);
        skillscript.skil_on.interactable = true;
        skillscript.next.interactable = true;
        skillscript.back.interactable = true;
    }



}
