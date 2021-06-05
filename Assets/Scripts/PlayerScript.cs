using System.Collections;
using System.Collections.Generic;
using DG.Tweening;  //DOTweenを使うときはこのusingを入れる
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerScript : MonoBehaviour
{
    public DiceButton diceButton;
    public Mapscript map;
    public TurnManager turnManager;

    public GameObject player;//プレイヤーの現在地を配列で表現,onoff
    public bool computer = false;//comか否かを判定

    public int start_count = 0;//初回以降の処理

    public int move_mass = 0;
    public bool my_turn = false;//プレイヤー管理
    public bool dice_select = false;//サイコロが来た時
    public bool move = false;//移動中かどうかの判定


    public bool turn_end = false;//ターン切り替えの判定

    public bool book_flag = false;//本を持っているかの処理

    bool branch_flag = false;
    bool branch_Left = false;//分岐時何処があるのかを把握
    bool branch_Right = false;
    bool branch_Up = false;
    bool branch_Down = false;


    public bool start_branch = true;//開始時の分岐用
    public bool branch_on = false;//移動処理中に分岐しないようにしている


    Vector3 warp_position;
    public int warp_mass;

    public int mass_name=6;//0=Nomal、1=Plus、2=Minus、3=Move、4=、Warp、5=Random、6=start

    float now_x = 0;//ワープ,Move用の値
    float now_z = 0;
    float next_x = 0;
    float next_z = 0;
    bool move_one = false;

    //NPC関係の変数宣言
    int selectmass = 0;
    bool selectmove = false;//ダイスの選択
    public bool select_com = false;//初回の一回判定

    public bool re_moveNPC = false;//moveを踏んだ後移動マスが-1になる為苦肉の策
    // Start is called before the first frame update
    void Start()
    {
        turn_end = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (my_turn == true)
        {
            if (computer == false)
            {
                if (dice_select == true)//ダイスが振られた時trueになる(DiceButtonスクリプトからbool)
                {
                    branch_on = false;
                    move_mass = diceButton.select_num;//diceのダイスの出目
                    dice_select = false;
                    move = true;
                    turn_end = true;
                    diceButton.move_Buttonflag = 2;//ボタンを非表示に
                    move_one = true;
                }
            }
            else if (computer == true && select_com == true)
            {
                NPC_move();
                turn_end = true;
                move = true;
                select_com = false;
                move_one = true;
            }

            if (move == true)//移動処理                
            {
                if (move_one == true&&branch_flag == false)
                {
                    Move_DOTween();
                }
            }

            if (move_mass == 0)
            {
                move = false;
            }

            if (GetComponent<Rigidbody>().IsSleeping()&& move_mass == 0&& turn_end==true)//ターン終了処理
            {
                Mass_status();
                //Debug.Log(mass_name);
            }

            if (branch_flag == true)//とりあえず十字キーで移動
            {
                if (branch_Left == true && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    branch_flag = false;
                    next_x -= 2.1f;
                    move_one = true;
                }
                else if (branch_Right == true && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    branch_flag = false;
                    next_x += 2.1f;
                    move_one = true;
                }
                else if (branch_Up == true && Input.GetKeyDown(KeyCode.UpArrow))
                {
                    branch_flag = false;
                    next_z += 2.1f;
                    move_one = true;
                }
                else if (branch_Down == true && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    branch_flag = false;
                    next_z -= 2.1f;
                    move_one = true;

                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)//0=Nomal、1=Plus、2=Minus、3=Move、4=、Warp、5=Random、6=start
        {
            case "Nomal":
                mass_name = 0;
                break;
            case "Plus":
                mass_name = 1;
                break;
            case "Minus":
                mass_name = 2;
                break;
            case "Move":
                mass_name = 3;
                break;
            case "Warp":
                mass_name = 4;
                break;
            case "Random":
                mass_name = 5;
                break;
            default:
                mass_name = 6;
                break;
        }
    }

    private void OnCollisionEnter(Collision col)//ターン開始時にonCollisonEnterを判定することになってる？
    {
        //Debug.Log(move_mass);


        branch_Left = false;//初期化
        branch_Right = false;
        branch_Up = false;
        branch_Down = false;

        int count = 0;
        //分岐があるのか判定
        if (col.gameObject.GetComponent<MassManager>().isLeft == true)
        {
            count++;
            branch_Left = true;
        }
        if (col.gameObject.GetComponent<MassManager>().isRight == true)
        {
            count++;
            branch_Right = true;
        }
        if (col.gameObject.GetComponent<MassManager>().isUp == true)
        {
            count++;
            branch_Up = true;
        }
        if (col.gameObject.GetComponent<MassManager>().isDown == true)
        {
            count++;
            branch_Down = true;
        }

        //Debug.Log(count);
        if (col.gameObject.tag == "Start" && start_count >= 1)
        {
            Debug.Log("止まりますか？");
            diceButton.stop_button_flag = 1;//止まるボタンの表示
        }
        else if (col.gameObject.tag == "Start")
            start_count++;
        else
            diceButton.stop_button_flag = 0;



        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Start")
        {
            if (move_mass != 1)//最後に座標が+されないように
            {
                //Debug.Log("Plus!!");
                if (count == 1)
                {//次の座標へプラス
                   
                    if (col.gameObject.GetComponent<MassManager>().isLeft == true)
                        next_x -= 2.1f;
                    else if (col.gameObject.GetComponent<MassManager>().isRight == true)
                        next_x += 2.1f;
                    else if (col.gameObject.GetComponent<MassManager>().isUp == true)
                        next_z += 2.1f;
                    else if (col.gameObject.GetComponent<MassManager>().isDown == true)
                        next_z -= 2.1f;


                }
                else
                {
                    if (computer == false)
                    {
                        Debug.Log("何処へ行きますか？,十字キーで移動");
                        move_one = false;
                        branch_flag = true;
                        /* move_one = false;
                         if (start_branch == true)
                         {
                             branch_flag = true;
                         }
                         else
                             branch_on = true;*/
                    }

                    else if (computer == true)
                    {
                        move_one = false;
                        if (start_branch == true)
                        {
                            NPC_Branch();
                        }
                        else
                        {
                            //Invoke("NPC_Branch", 0.5f);
                            branch_on = true;
                        }
                            
                        //NPC_Branch();
                        //Invoke("NPC_Branch", 0.5f);
                        Debug.Log("分岐！");
                    }


                }
            }
            
        }


    }



    public void Move_DOTween()
    {
        move_one = false;
        this.transform.DOMove(new Vector3(next_x, 0.6f,next_z), 0.5f);

        this.transform.DOMove(new Vector3(next_x, 0.6f, next_z), 0.5f).OnComplete(() =>//移動終了後実行
        {
            move_mass -= 1;
            start_branch = false;
            //Debug.Log(transform.position);
            //Debug.Log("x座標"+next_x+",z座標"+ next_z);
            Debug.Log(move_mass);
            if (move_mass == 0)
            {
                move = false;
            }
            else
            {
                move_one = true;
            }

        if (branch_on == true && computer == false)
            {
                branch_flag = true;
                branch_on = false;
            }
            else if(branch_on == true && computer == true)
            {
                NPC_Branch();
                branch_on = false;
            }else
                branch_on = false;
        });
    }

    public void Mass_status()//マス効果
    {
        //0=Nomal、1=Plus、2=Minus、3=Move、4=、Warp、5=Random、6=start
        switch (mass_name)
        {
            case 0:
                SwitchPlayer();
                break;
            case 1:

                if (computer == false)
                    diceButton.Dice_Buttonflag = 1;
                else
                {//comの処理
                    diceButton.Dice_Buttonflag = 1;
                    diceButton.Dice();
                }
                    
                break;
            case 2:
                if (computer == false)
                    diceButton.Dice_Buttonflag = 2;
                else
                {//comの処理
                    diceButton.Dice_Buttonflag = 2;
                    diceButton.Dice();
                }

                break;
            case 3:
                if (computer == false)
                {
                   diceButton.move_Buttonflag = 0;
                    turn_end = false;
                    transform.position=new Vector3(next_x, 0.68f, next_z);//oncollisonenterを発生させるため
                    
                }
                else
                {
                    transform.position = new Vector3(next_x, 0.68f, next_z);//oncollisonenterを発生させるため
                    select_com = true;
                    turn_end = false;
                    re_moveNPC = true;
                }
                break;
            case 4:
                warp_mass=Random.Range(0, 69);
                warp_position =  map.mass[warp_mass].transform.position;
                warp_position.y = 0.6f;
                transform.position = warp_position;
                next_x = warp_position.x;
                next_z= warp_position.z;
                SwitchPlayer();
                break;
            case 5:
                mass_name = Random.Range(0, 4);
                Mass_status();
                break;
            case 6:
                SwitchPlayer();
                break;

        }
    }



    public void SwitchPlayer()
    {
        re_moveNPC = false;
        turn_end = false;
        turnManager.turn_switching = true;
        my_turn = false;
        select_com = true;
        this.gameObject.SetActive(false);
        
        turnManager.turn_switch();

        diceButton.move_Buttonflag = 0;//ボタンを非表示に

    }


    public void NPC_move()
    {
            
            
            diceButton.Move_click();
            selectmass = Random.Range(1, 3);
            if (selectmass == 1)
            {
                move_mass = diceButton.Move_result1;
                selectmove = true;
            }
            else if (selectmass == 2)
            {
                move_mass = diceButton.Move_result2;                    
            }
        Debug.Log("出た数" + move_mass);
        if (re_moveNPC == true)
        {
            move_mass += 1;
        }
        


    }

    public void NPC_Branch()
    {

        selectmass = Random.Range(0, 4);
        bool branch_com = false;
        while (branch_com == false)//分岐内の判定チェック
        {
            if (branch_Left == true && selectmass == 0)
            {
                branch_com = true;
                next_x -= 2.1f;
                move_one = true;
            }
            else if (branch_Right == true && selectmass == 1)
            {
                next_x += 2.1f;
                move_one = true;
                branch_com = true;
            }else if (branch_Up == true&& selectmass == 2)
            {
                next_z += 2.1f;
                move_one = true;
                branch_com = true;

            }else if(branch_Down == true&& selectmass == 3)
            {
                next_z -= 2.1f;
                move_one = true;
                branch_com = true;
            }else
            selectmass =  (selectmass+1)%4;

        }
    }


}
