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
        // 0���� ������ ����Ǿ� ����
        sword_unlock_list[0] = true;

        // �ٸ� �˵��� ó���� ������ ����
        for (int index = 1; index < sword_unlock_list.Length; index++) { sword_unlock_list[index] = false; }
    }

    public void Unlock(int index) { sword_unlock_list[index] = true; }
}
