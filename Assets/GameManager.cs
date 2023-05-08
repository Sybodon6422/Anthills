using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int money;
    public bool SpendMoney(int moneySpent)
    {
        if(moneySpent > money)
        {
            return false;
        }
        else
        {
            money -= moneySpent;
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
