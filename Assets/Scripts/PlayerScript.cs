using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public DiceButton diceButton;
    public Mapscript map;

    public GameObject player;//プレイヤーの現在地を配列で表現,onoff
    public GameObject under_mass;

    public int player_now = 0;//現在地

    int move_mass = 0;
    public bool my_turn = false;
    public bool dice_select = false;//サイコロが来た時
    public bool move = false;//移動中かどうかの判定
    public float speed = 1;


    Vector3 start_position;
    Vector3 next_positon;
    bool start_move = true;//最初動かすための処理

    bool branch_flag = false;
    bool branch_Left = false;//分岐時何処があるのかを把握
    bool branch_Right = false;
    bool branch_Up = false;
    bool branch_Down = false;
    // Start is called before the first frame update
    void Start()
    {
        next_positon.y += 0.65f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dice_select == true)//ダイスが振られた時trueになる(DiceButtonスクリプトからbool)
        {
            move_mass = diceButton.select_num;//diceのダイスの出目
            
            start_position = transform.position;

            dice_select = false;
            move = true;
        }

        if (move == true)//移動処理
                         //move_mass >= 1&&branch_flag==0
        {
            transform.position = Vector3.Lerp(start_position, next_positon, 1f);
        }
        else if (move_mass == 0)
        {
            Time.timeScale = 1;
            move = false;
            
        }

        if (branch_flag == true)//とりあえず十字キーで移動
        {
            if (branch_Left == true && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                next_positon.x += -2.1f;
                branch_flag = false;
            }
            else if (branch_Right == true && Input.GetKeyDown(KeyCode.RightArrow))
            {
                next_positon.x += 2.1f;
                branch_flag = false;
            }
            else if (branch_Up == true && Input.GetKeyDown(KeyCode.UpArrow))
            {
                next_positon.z += 2.1f;
                branch_flag = false;
            }
            else if (branch_Down == true && Input.GetKeyDown(KeyCode.DownArrow))
            {
                next_positon.z += -2.1f;
                branch_flag = false;
            }
        }


    }

    private void OnCollisionEnter(Collision col)
    {
        start_position = next_positon;

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

        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Start")
        {
            if (count == 1)
            {
                if (col.gameObject.GetComponent<MassManager>().isLeft == true)//次の座標へプラス
                    next_positon.x += col.gameObject.GetComponent<MassManager>().isLeft_mass;
                else if (col.gameObject.GetComponent<MassManager>().isRight == true)
                    next_positon.x += col.gameObject.GetComponent<MassManager>().isRight_mass;
                else if (col.gameObject.GetComponent<MassManager>().isUp == true)
                    next_positon.z += col.gameObject.GetComponent<MassManager>().isUp_mass;
                else if (col.gameObject.GetComponent<MassManager>().isDown == true)
                    next_positon.z += col.gameObject.GetComponent<MassManager>().isDown_mass;

                //Debug.Log(next_positon);
                //player_now += 1;//スタート地点の処理も後で入れる
                //Invoke("Branch", 0.3f);
                //マスとの設置判定からどれだけ移動したか把握する
                move_mass -= 1;
                //Branch();
            }
            else
            {
                Debug.Log("何処へ行きますか？,十字キーで移動");
                branch_flag = true;

            }


        }
        if (move_mass <= 0)
        {
            move = false;
        }

    }

}
