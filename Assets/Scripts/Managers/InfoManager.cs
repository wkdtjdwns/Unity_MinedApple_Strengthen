using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public int use_prevention_num;
    public int get_prevention_num;

    public ulong use_money;
    public ulong get_money;

    public int use_special_item_num;
    public int get_special_item_num;

    public int success_num;
    public int fail_num;

    public string difficult;
    public string minedapple_height;

    private void Awake() { minedapple_height = "169.9"; }
}
