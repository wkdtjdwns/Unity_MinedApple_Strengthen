using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public static Guide instance;

    public bool[] sword_unlock_list;

    private void Awake()
    {
        instance = this;

        sword_unlock_list = new bool[30];
    }

    private void Start()
    {
        // 0강은 무조건 언락되어 있음
        sword_unlock_list[0] = true;

        // 다른 검들은 처음엔 열리지 않음
        for (int index = 1; index < sword_unlock_list.Length; index++) { sword_unlock_list[index] = false; }
    }

    public void Unlock(int index) { sword_unlock_list[index] = true; }
}
