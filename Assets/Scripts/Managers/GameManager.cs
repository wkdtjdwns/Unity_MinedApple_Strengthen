using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject go_save_sword_button_obj;
    [SerializeField]
    private GameObject go_trade_button_obj;
    [SerializeField]
    private GameObject sword_save_button_obj;
    [SerializeField]
    private GameObject go_shop_button_obj;
    [SerializeField]
    private GameObject sell_button_obj;
    [SerializeField]
    private GameObject open_guide_button_obj;
    [SerializeField]
    private GameObject upgrade_button_obj;
    [SerializeField]
    private GameObject go_ending_button_obj;

    public GameObject use_prevention_button_obj;
    public GameObject ask_go_ending_obj;

    public GameObject get_item_obj;
    public Button get_item_btn;
    public Text get_item_text;
    public Image item_image;

    public GameObject fail_group;
    public Text guide_prevention_text;

    public Text show_page_text;
    public int shop_page;
    public GameObject shop_group;
    public GameObject[] shop_page_objs;

    public Text show_trade_page_text;
    public int trade_page;
    public GameObject trade_group;
    public GameObject[] trade_page_objs;

    public int guide_page;
    public GameObject guide_group;

    public GameObject lock_sword_group;
    public GameObject unlock_sword_group;

    public Image guide_sword_image;
    public Text guide_reference_text;
    public Text guide_sword_name_text;
    public Text guide_sword_info_text;
    public Text guide_page_text;

    public String[] guide_sword_explains;
    public Text guide_sword_explain_text;

    public GameObject show_remaining_group;
    public GameObject show_save_sword_remaining_group;

    public int prevention_remaining;
    public int money;

    [SerializeField]
    private GameObject sword_obj;
    private Sword sword;

    private GameObject item_manager_obj;
    private ItemManager item_manager;

    private GameObject difficult_manager_obj;
    private DifficultManager difficult_manager;

    private GameObject info_manager_obj;
    private InfoManager info_manager;

    private void Awake()
    {
        sword = sword_obj.GetComponent<Sword>();

        item_manager_obj = GameObject.Find("ItemManager").gameObject;
        item_manager = item_manager_obj.GetComponent<ItemManager>();

        difficult_manager_obj = GameObject.Find("DifficultManager").gameObject;
        difficult_manager = difficult_manager_obj.GetComponent<DifficultManager>();

        info_manager_obj = GameObject.Find("InfoManager").gameObject;
        info_manager = info_manager_obj.GetComponent<InfoManager>();

        prevention_remaining = 0;

        // 난이도에 따라서 시작할 때 주는 돈을 설정함
        if (difficult_manager.difficult == "easy") { money = 1000000; info_manager.difficult = "겁쟁이"; }
        else if (difficult_manager.difficult == "normal") { money = 3333333; info_manager.difficult = "보통"; }
        else { money = 6666666; info_manager.difficult = "헉!"; }
    }

    private void Start()
    {
        if (difficult_manager.difficult == "easy") { info_manager.get_money += Convert.ToUInt64(1000000); }
        else if (difficult_manager.difficult == "normal") { info_manager.get_money += Convert.ToUInt64(3333333); }
        else { info_manager.get_money += Convert.ToUInt64(6666666); }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { ask_go_ending_obj.SetActive(false); }

        SetButtonObjActive();
        CheatKey();
    }

    private void SetButtonObjActive()
    {
        // 최대 강화 검이면 업그레이드를 불가능하게 하고 엔딩을 보게 함
        if (sword.sword_index >= 29)
        {
            upgrade_button_obj.SetActive(false);
            go_ending_button_obj.SetActive(true);
        }

        // 아니면 엔딩을 보지 못하게 하고 업그레이드를 하게 함
        else
        {
            upgrade_button_obj.SetActive(true);
            go_ending_button_obj.SetActive(false);
        }

        // 19강 이상, 29강 미만일 때 검 저장을 가능하게 하고
        if (sword.sword_index >= 19 && sword.sword_index <= 28) { sword_save_button_obj.SetActive(true); }

        // 아니면 불가능하게 함
        else { sword_save_button_obj.SetActive(false); }

        // 1강일 때  아이템 교환소 방문만 가능하게 함
        if (sword.sword_index == 1) { go_trade_button_obj.SetActive(true); }

        // 2강 이상일 때 아이템 교환소 방문을 불가능하게 함
        else if (sword.sword_index >= 2) { go_trade_button_obj.SetActive(false); }

        // 28, 29강 검이면 판매가 불가능하도록 함
        if (sword.sword_index >= 28) { sell_button_obj.SetActive(false); }

        // 1강 이상일 때
        else if (sword.sword_index > 0)
        {
            // 판매만 가능하게 함
            sell_button_obj.SetActive(true);
            go_shop_button_obj.SetActive(false);

            // 도감을 보지 못하게 함
            open_guide_button_obj.SetActive(false);

            // 보유중인 검을 보지 못하게 함
            go_save_sword_button_obj.SetActive(false);
        }

        // 0강일 때
        else
        {
            // 상점 방문만 가능하게 함
            go_shop_button_obj.SetActive(true);
            sell_button_obj.SetActive(false);

            // 도감을 볼 수 있게 함
            open_guide_button_obj.SetActive(true);

            // 아이템 창 방문만 가능하게 함
            go_save_sword_button_obj.SetActive(true);
            go_trade_button_obj.SetActive(false);
        }
    }

    private void CheatKey()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F1)) { money += 333333; info_manager.get_money += Convert.ToUInt64(333333); }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F2)) { prevention_remaining += 3; info_manager.get_prevention_num += 3; }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F3)) { if (sword.sword_index >= 29) return; sword.sword_index++; sword.UpdateSwordInfo(); Guide.instance.Unlock(sword.sword_index); }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F4)) { if (sword.sword_index <= 0) return; sword.sword_index--; sword.UpdateSwordInfo(); Guide.instance.Unlock(sword.sword_index); }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F5)) { sword.sword_index = 28; sword.UpdateSwordInfo(); Guide.instance.Unlock(sword.sword_index); }
    }
}
