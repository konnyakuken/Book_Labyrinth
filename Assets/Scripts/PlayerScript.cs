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

    public bool branch_flag = false;
    public bool branch_Left = false;//分岐時何処があるのかを把握
    public bool branch_Right = false;
    public bool branch_Up = false;
    public bool branch_Down = false;


    public bool start_branch = true;//開始時の分岐用
    public bool branch_on = false;//移動処理中に分岐しないようにしている

    GameObject Hidden_massflag ;

    Vector3 warp_position;
    public int warp_mass;

    public int mass_name=6;//0=Nomal、1=Plus、2=Minus、3=Move、4=、Warp、5=Random、6=start

    float now_x = 0;//ワープ,Move用の値
    float now_z = 0;
    public float next_x = 0;
    public float next_z = 0;
    public bool move_one = false;//1マス移動するかどうかの判定

    //NPC関係の変数宣言
    int selectmass = 0;
    bool selectmove = false;//ダイスの選択
    public bool select_com = false;//初回の一回判定

    public bool re_moveNPC = false;//moveを踏んだ後移動マスが-1になる為苦肉の策
    public bool re_moveNPCflag = false;//移動マスを+１にしたため、表示調整
    public bool re_moveflag = false;//プレイヤーがMove分岐時に進行方向を向く為


    public bool rest_flag = false;

    public float differ_x = 0;
    public float differ_z = 0;//２回オンコリジョンを踏み二回判定されてるのでその分削除

    public int anime_flagNum = 0;//animationのフラグを管理

    public bool warp_flag = false;//ワープ関係のフラグ

    //1=down、2=up、3=left、4=right
    public int direction_anime = 0;//方向転換
    public int direction = 1;//1=down、2=up、3=left、4=right
    public GameObject anime_object;

    public bool effect_flag = false;//何回も同じ処理が実行されないようにする為

    // Start is called before the first frame update
    void Start()
    {
        
        start_branch = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (rest_flag == true&&computer==false)//博打の効果
        {
            rest_flag = false;
            start_count = 0;
            move_mass = 8;
            
            next_x += differ_x;
            next_z += differ_z;
            
            diceButton.move_Button.SetActive(false); ;
            branch_flag = false;
            for (int i = 0; i < 4; i++)
                diceButton.branch_Button[i].SetActive(false);

            SwitchPlayer();
        }

        if (my_turn == true)
        {
            

            

            if (computer == false)
            {
                if (dice_select == true)//ダイスが振られた時trueになる(DiceButtonスクリプトからbool)
                {
                    if (start_branch == true)
                        branch_flag = true;

                    if(re_moveflag == false)
                        Rotation_Check();

                    branch_on = false;
                    move_mass = diceButton.select_num;//diceのダイスの出目
                    Debug.Log("出たマス数：" + move_mass);
                    dice_select = false;
                    move = true;
                    turn_end = true;
                    diceButton.move_Buttonflag = 2;//ボタンを非表示に
                    move_one = true;
                    anime_flagNum = 1;
                }
            }
            else if (computer == true && select_com == true)
            {
                if (rest_flag == true)//博打の効果
                {
                    rest_flag = false;
                    start_count = 0;
                    move_mass = 8;
                    //transform.position = new Vector3(next_x, 0.6f, next_z);

                    next_x += differ_x;
                    next_z += differ_z;

                    diceButton.move_Button.SetActive(false); ;
                    branch_flag = false;
                    for (int i = 0; i < 4; i++)
                        diceButton.branch_Button[i].SetActive(false);

                    SwitchPlayer();
                }
                else
                {
                    

                    NPC_move();
                    Rotation_Check();
                    turn_end = true;
                    move = true;
                    select_com = false;
                    move_one = true;
                }             
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
                /*//NPCのターン終了後動きを止まらせたいのだが、これだとエラーが出て上手く実装出来ない
                anime_flagNum = 0;//歩くアニメの停止
                if (Hidden_massflag.GetComponent<HiddenScript>().Hidden_falg == false)
                {
                    Hidden_massflag.GetComponent<MeshRenderer>().enabled = false;//MeshRendererをoffにする
                    Hidden_massflag.GetComponent<HiddenScript>().Hidden_falg = true;
                }


                if (mass_name == 3 && computer == true)//CPのmoveじゃない時のみ
                {

                }
                else
                {
                    direction = 1;
                    anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                }

                if (computer == true)
                {
                    DOVirtual.DelayedCall(0.7f, () => {
                        Mass_status();
                        Debug.Log(mass_name);
                    });
                }
                else
                {
                    Mass_status();
                    Debug.Log(mass_name);
                }*/
                anime_flagNum = 0;//歩くアニメの停止
                if (Hidden_massflag.GetComponent<HiddenScript>().Hidden_falg==false)
                {
                    Hidden_massflag.GetComponent<MeshRenderer>().enabled=false;//MeshRendererをoffにする
                    Hidden_massflag.GetComponent<HiddenScript>().Hidden_falg = true;
                }

                if(mass_name == 3&& computer == true)//CPのmoveじゃない時のみ
                {

                }
                else
                {
                    direction = 1;
                    anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                }

                Mass_status();
                Debug.Log(mass_name);
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
            case "Start":
                mass_name = 6;
                break;
        }

        if(other.gameObject.tag=="Hidden")
            Hidden_massflag = other.gameObject;
    }

    private void OnCollisionEnter(Collision col)//ターン開始時にonCollisonEnterを判定することになってる
    {
        //Debug.Log(move_mass);

        //Debug.Log("on!");
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

                    differ_x = next_x;
                    differ_z = next_z;
                    //1=down、2=up、3=left、4=right
                    if (col.gameObject.GetComponent<MassManager>().isLeft == true)
                    {
                        direction_anime = 3;
                        next_x -= 2.1f;
                    }  
                    else if (col.gameObject.GetComponent<MassManager>().isRight == true)
                    {
                        next_x += 2.1f;
                        direction_anime = 4;
                    }
                    else if (col.gameObject.GetComponent<MassManager>().isUp == true)
                    {
                        next_z += 2.1f;
                        direction_anime = 2;
                    }
                    else if (col.gameObject.GetComponent<MassManager>().isDown == true)
                    {
                        next_z -= 2.1f;
                        direction_anime = 1;
                    }
                        

                    differ_x -= next_x;
                    differ_z -= next_z;
                }
                else
                {
                    if (computer == false)
                    {
                        Debug.Log("何処へ行きますか？");
                        
                         move_one = false;
                        
                         branch_on = true;
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
                        //Debug.Log("分岐！");
                    }


                }
            }
            else if(count==1)//1マスの時のアニメーション判定
            {
                if (col.gameObject.GetComponent<MassManager>().isLeft == true)
                {
                    direction_anime = 3;
                }
                else if (col.gameObject.GetComponent<MassManager>().isRight == true)
                {
                    direction_anime = 4;
                }
                else if (col.gameObject.GetComponent<MassManager>().isUp == true)
                {
                    direction_anime = 2;
                }
                else if (col.gameObject.GetComponent<MassManager>().isDown == true)
                {
                    direction_anime = 1;
                }
            }

            if ( mass_name == 3&& count > 1)//止まるマスがMoveかつ分岐があるとき  分岐ボタンが早く出るため対策move_mass == 1 &&
            {
                if (computer == false)
                {
                    Debug.Log("何処へ行きますか？");
                    re_moveflag = true;
                    move_one = false;

                    branch_on = true;
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
                        branch_on = true;
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
            re_moveflag = false;

            if (move_mass == 0)
            {
                move = false;
                
            }
            else
            {
                move_one = true;
            }

            if(re_moveNPC == true)//NPCのMove対策
            {
                re_moveNPC = false;
            }

            if (direction_anime != direction)//一致するのなら
            {//1=down、2=up、3=left、4=right
                switch (direction_anime)
                {
                    case 1:
                        anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                        anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                        {
                            direction = 1;
                            Check();
                        });
                        break;
                    case 2:
                        anime_object.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                        anime_object.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                        {
                            direction = 2;
                            Check();
                        });
                        break;
                    case 3:
                        anime_object.transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                        anime_object.transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                        {
                            direction = 3;
                            Check();
                        });
                        break;
                    case 4:
                        anime_object.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                        anime_object.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                        {
                            direction = 4;
                            Check();
                        });
                        break;
                }

            }
            else
            {
                Check();
            }
            
                
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
                Debug.Log("Bounus!");
                break;
            case 2:
                if (computer == false)
                    diceButton.Dice_Buttonflag = 2;
                else
                {//comの処理
                    diceButton.Dice_Buttonflag = 2;
                    diceButton.Dice();
                }
                Debug.Log("Minus!");
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
                Debug.Log("Move!");
                break;
            case 4:
                Debug.Log("Warp!");
                move_mass = 0;//念入れの代入（NPCがワープ後に動く挙動を見せたためとりあえず）
                warp_mass =Random.Range(0, 69);
                warp_position =  map.mass[warp_mass].transform.position;
                warp_position.y = 0.6f;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;//ｙ座標をロック
                transform.position = warp_position;
                next_x = warp_position.x;
                next_z= warp_position.z;
                warp_flag = true;
                
                DOVirtual.DelayedCall(0.7f, () => {
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    next_x += differ_x;
                    next_z += differ_z;
                    SwitchPlayer();
                });
                
                //Invoke("SwitchPlayer", 0.5f);
                break;
            case 5:
                Debug.Log("Random!");
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
        
        turn_end = false;
        
        my_turn = false;
        select_com = true;

        start_branch = false;
        this.gameObject.SetActive(false);
        diceButton.move_Buttonflag = 0;//ボタンを非表示に
        diceButton.skil_end = false;//スキルを使用可能に
        if (mass_name == 6)//停止マスがスタートだった時
            start_branch = true;
        turnManager.turn_switch();

        

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
        Debug.Log("出たマス数:" + move_mass);
        anime_flagNum = 1;
        if (re_moveNPC == true)
        {

            move_mass += 1;
        }
        


    }

    public void NPC_Branch()
    {
        if (rest_flag == true)//博打の効果
        {
            rest_flag = false;
            start_count = 0;
            move_mass = 8;
            //transform.position = new Vector3(next_x, 0.6f, next_z);

            next_x += differ_x;
            next_z += differ_z;

            diceButton.move_Button.SetActive(false); ;
            branch_flag = false;
            for (int i = 0; i < 4; i++)
                diceButton.branch_Button[i].SetActive(false);

            SwitchPlayer();
        }
        else
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
                    direction = 3;//1=down、2=up、3=left、4=right
                }
                else if (branch_Right == true && selectmass == 1)
                {
                    next_x += 2.1f;
                    move_one = true;
                    branch_com = true;
                    direction = 4;//1=down、2=up、3=left、4=right
                }
                else if (branch_Up == true && selectmass == 2)
                {
                    next_z += 2.1f;
                    move_one = true;
                    branch_com = true;
                    direction = 2;//1=down、2=up、3=left、4=right

                }
                else if (branch_Down == true && selectmass == 3)
                {
                    next_z -= 2.1f;
                    move_one = true;
                    branch_com = true;
                    direction = 1;//1=down、2=up、3=left、4=right
                }
                else
                    selectmass = (selectmass + 1) % 4;

            }
            
            switch (direction)
            {
                case 1:
                    anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        //move_one = true;
                    });
                    break;
                case 2:
                    anime_object.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                       // move_one = true;
                    });
                    break;
                case 3:
                    anime_object.transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        //move_one = true;
                    });
                    break;
                case 4:
                    anime_object.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        //move_one = true;
                    });
                    break;
            }
        }
    }

    public void End_processing()
    {
        move_mass = 8;

        next_x += differ_x;
        next_z += differ_z;
    }

    public void Check()//移動後の分岐判定
    {
        if (branch_on == true && computer == false)
        {
            branch_flag = true;
            branch_on = false;
        }
        else if (branch_on == true && computer == true)
        {
            NPC_Branch();
            branch_on = false;
        }
        else
        {
            branch_on = false;

        }
    }

    public void Rotation_Check()//分岐がない時
    {
        if (direction_anime != direction)//回転処理
        {//1=down、2=up、3=left、4=right
            switch (direction_anime)
            {
                case 1:
                    anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 180, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        direction = 1;
                    });
                    break;
                case 2:
                    anime_object.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 0, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        direction = 2;
                    });
                    break;
                case 3:
                    anime_object.transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 270, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        direction = 3;
                    });
                    break;
                case 4:
                    anime_object.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast); //最短で指定の角度まで回転
                    anime_object.transform.DORotate(new Vector3(0, 90, 0), 0.2f, RotateMode.Fast).OnComplete(() =>//移動終了後実行
                    {
                        direction = 4;
                    });
                    break;
            }
            
        }
    }


}
