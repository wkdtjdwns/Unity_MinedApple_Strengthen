using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    // 아이템의 가격들
    public int[] prices;

    private GameObject difficult_manager_obj;
    private DifficultManager difficult_manager;

    private void Awake()
    {
        prices = new int[6];

        difficult_manager_obj = GameObject.Find("DifficultManager").gameObject;
        difficult_manager = difficult_manager_obj.GetComponent<DifficultManager>();

        // 난이도에 따라서 아이템의 가격을 정함
        if (difficult_manager.difficult == "easy" )
        {
            prices[0] = 1000000;
            prices[1] = 169900;
            prices[2] = 10000000;
            prices[3] = 1000000;
            prices[4] = 2500000;
            prices[5] = 15000000;
        }

        else if (difficult_manager.difficult == "normal")
        {
            prices[0] = 1250000;
            prices[1] = 169900;
            prices[2] = 12500000;
            prices[3] = 1250000;
            prices[4] = 2750000;
            prices[5] = 17500000;
        }

        else
        {
            prices[0] = 1500000;
            prices[1] = 169900;
            prices[2] = 15000000;
            prices[3] = 1500000;
            prices[4] = 3000000;
            prices[5] = 20000000;
        }
    }
}
