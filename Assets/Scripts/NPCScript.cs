using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class NPCScript : PlayerScript
{
    //public TurnManager turnManager;
    public DiceButton DiceButton;

    int selectmass = 0;
    int move_mass = 0;
    bool selectmove=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<PlayerScript>().computer == true)
        {
           if(selectmove == false)
            {
                DiceButton.Move_click();
                selectmass = Random.Range(0, 2);
                if (selectmass == 0)
                {
                    move_mass= DiceButton.Move_result1;
                    selectmove = true;
                }
                else if (selectmass == 1)
                {
                    move_mass = DiceButton.Move_result2;
                    selectmove = true;
                }
            }
            
        }




    }
}
