using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public DiceButton diceButton;
    public Mapscript map;
    
    public GameObject player;//プレイヤーの現在地を配列で表現,onoff
    public GameObject under_mass;

    public int player_now =0;//現在地
    public int mass_max = 21; 
    int move_mass = 0;
    public bool my_turn = false;
    public bool dice_select = false;
    public float speed = 1;
    int branch_flag = 0;

    Vector3 start_position;
    Vector3 next_positon;
    public  bool start_move=true;//最初動かすための処理
    public float nextMove = 0;
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
            start_position = transform.position;
            next_positon = transform.position;
            Branch();
            dice_select = false;
        }

        if (move_mass >= 1&&branch_flag==0)//移動処理
        {
            transform.position=Vector3.Lerp(start_position, next_positon, 1);
        }else if (move_mass == 0)
        {
            Time.timeScale = 1;
        }


        
    }

    private void OnCollisionExit(Collision col)
    {
        start_position = next_positon;
        int count = 0;
        //分岐があるのか判定
        if (col.gameObject.GetComponent<MassManager>().isLeft == true)
            count++;
        else if (col.gameObject.GetComponent<MassManager>().isRight == true)
            count++;
        else if (col.gameObject.GetComponent<MassManager>().isUp == true)
            count++;
        else if (col.gameObject.GetComponent<MassManager>().isDown == true)
            count++;
        Debug.Log(count);

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



                //massname = col.gameObject.name;//移動させる名前を取得
                //under_mass = col.GetComponent<gameObject>();

                //player_now += 1;//スタート地点の処理も後で入れる
                //Invoke("Branch", 0.3f);
                //マスとの設置判定からどれだけ移動したか把握する
                move_mass -= 1;
                //Branch();
            }
            else
            {

            }



        }

    }

    private void OnCollisionEnter(Collision collision)//初回（サイコロを振った時の動作を何とかしたい）
    {
        
        if (start_move == true)
        {
            int count = 0;
            //分岐があるのか判定
            if (collision.gameObject.GetComponent<MassManager>().isLeft == true)
                count++;
            else if (collision.gameObject.GetComponent<MassManager>().isRight == true)
                count++;
            else if (collision.gameObject.GetComponent<MassManager>().isUp == true)
                count++;
            else if (collision.gameObject.GetComponent<MassManager>().isDown == true)
                count++;
            Debug.Log(count);


            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Start")
            {
                if (count == 1)
                {
                    if (collision.gameObject.GetComponent<MassManager>().isLeft == true)//次の座標へプラス
                        next_positon.x += collision.gameObject.GetComponent<MassManager>().isLeft_mass;
                    else if (collision.gameObject.GetComponent<MassManager>().isRight == true)
                        next_positon.x += collision.gameObject.GetComponent<MassManager>().isRight_mass;
                    else if (collision.gameObject.GetComponent<MassManager>().isUp == true)
                        next_positon.z += collision.gameObject.GetComponent<MassManager>().isUp_mass;
                    else if (collision.gameObject.GetComponent<MassManager>().isDown == true)
                        next_positon.z += collision.gameObject.GetComponent<MassManager>().isDown_mass;

                    //massname = col.gameObject.name;//移動させる名前を取得
                    //under_mass = col.GetComponent<gameObject>();

                    //player_now += 1;//スタート地点の処理も後で入れる
                    //Invoke("Branch", 0.3f);
                    //マスとの設置判定からどれだけ移動したか把握する
                    move_mass -= 1;
                    //Branch();
                }
                else
                {
                }
            }
        }

    }

    public void Branch()
    {
        
    }

    
}
