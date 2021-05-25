using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public DiceButton diceButton;
    public Map map;
    public GameObject player;//プレイヤーの現在地を配列で表現,onoff
    public int player_now =0;//現在地
    public int mass_max = 21; 
    int move_mass = 0;
    public bool my_turn = false;
    public bool dice_select = false;
    public float speed = 1;
    int branch_flag = 0;

    Vector3 start_position;
    Vector3 end_positon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dice_select== true)//ダイスが振られた時trueになる(DiceButtonスクリプトからbool)
        {
            move_mass = diceButton.Move_result1;//diceのダイスの出目
            Branch();
            dice_select = false;
        }

        if (move_mass >= 1&&branch_flag==0)//移動処理
        {
            transform.position=Vector3.Lerp(start_position, end_positon, 1);
        }else if (move_mass == 0)
        {
            Time.timeScale = 1;
        }


        if (branch_flag == 1)//スタートの分岐処理
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                start_position = map.massGameObjects[player_now].transform.position;
                start_position.y += 0.65f;//マスを参照しているので埋まらないようにｙ軸プラスしている。
                end_positon = map.massGameObjects[player_now + 1].transform.position;
                end_positon.y += 0.65f;
                branch_flag = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                player_now = 22;
                start_position = map.massGameObjects[0].transform.position;
                start_position.y += 0.65f;
                end_positon = map.massGameObjects[player_now].transform.position;
                end_positon.y += 0.65f;
                branch_flag = 0;
            }
        }else if (branch_flag == 2)//分岐２右下
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                start_position = map.massGameObjects[player_now].transform.position;
                start_position.y += 0.65f;
                end_positon = map.massGameObjects[player_now + 1].transform.position;
                end_positon.y += 0.65f;
                branch_flag = 0;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                player_now = 36;
                start_position = map.massGameObjects[4].transform.position;
                start_position.y += 0.65f;
                end_positon = map.massGameObjects[player_now].transform.position;
                end_positon.y += 0.65f;
                branch_flag = 0;
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground"|| collision.gameObject.tag == "Start")
        {
            
            
            player_now += 1;//スタート地点の処理も後で入れる
            Invoke("Branch", 0.3f);
            //マスとの設置判定からどれだけ移動したか把握する
            move_mass -= 1;
            //Branch();


        }
        
    }

    private void OnCollisionStay (Collision collision2)
    {
        //start_position = collision2.gameObject.position;
    }

    public void Branch()
    {
        switch (player_now)//分岐の処理
        {
            case 21://スタートへ戻る処理
                player_now = 0;
                start_position = map.massGameObjects[21].transform.position;
                start_position.y += 0.65f;//スタート地点
                end_positon = map.massGameObjects[0].transform.position;
                end_positon.y += 0.65f;//ゴール地点
                break;
            case 0: //スタート分岐
                Debug.Log("何処へ行く？");
                branch_flag = 1;

                break;
            case 4: //分岐2
                Debug.Log("何処へ行く？");
                branch_flag = 2;

                break;
            case 38: //右下合流
                player_now = 28;
                start_position = map.massGameObjects[38].transform.position;
                start_position.y += 0.65f;
                end_positon = map.massGameObjects[player_now].transform.position;
                end_positon.y += 0.65f;

                break;
            case 35: //右合流
                player_now = 7;
                start_position = map.massGameObjects[35].transform.position;
                start_position.y += 0.65f;
                end_positon = map.massGameObjects[player_now].transform.position;
                end_positon.y += 0.65f;

                break;

            default: //その他
               
                start_position = map.massGameObjects[player_now].transform.position;
                start_position.y += 0.65f;
                end_positon = map.massGameObjects[player_now + 1].transform.position;
                end_positon.y += 0.65f;
                break;
        }
    }
}
