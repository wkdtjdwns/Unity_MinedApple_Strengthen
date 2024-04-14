using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private Text minedapple_height_price_text;
    [SerializeField]
    private Text minedapple_height_text;

    [SerializeField]
    private GameObject sword_obj;
    private Sword sword;

    [SerializeField]
    private GameObject goods_obj;
    private Goods goods;

    private GameObject game_manager_obj;
    private GameManager game_manager;

    private GameObject info_manager_obj;
    private InfoManager info_manager;

    private void Awake()
    {
        sword = sword_obj.GetComponent<Sword>();
        goods = goods_obj.GetComponent<Goods>();

        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        info_manager_obj = GameObject.Find("InfoManager").gameObject;
        info_manager = info_manager_obj.GetComponent<InfoManager>();
    }

    public void Purchasegoods(string type)
    {
        // 아이템의 타입에 따라서 다른 효과를 줌
        switch (type)
        {
            case "warp 9":
                // 돈이 없으면 실행하지 않음
                if (game_manager.money < goods.prices[0]) { SoundManager.instance.PlaySound("fail"); return; }

                // 돈을 지불한 후
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[0];
                info_manager.use_money += Convert.ToUInt64(goods.prices[0]);


                // +9강 검으로 이동하며 +9강 검을 도감에 추가하며 상점에서 나감
                sword.sword_index = 9;
                sword.UpdateSwordInfo();
                Guide.instance.Unlock(sword.sword_index);

                this.gameObject.SetActive(false);
                break;

            // 이스터 에그와 비슷한 아이템임
            case "growth":
                // 돈이 없으면 실행하지 않음
                if (game_manager.money < goods.prices[1]) { SoundManager.instance.PlaySound("fail"); return; }

                // 돈을 지불한 후
                SoundManager.instance.PlaySound("purchase");

                game_manager.money -= goods.prices[1];
                info_manager.use_money += Convert.ToUInt64(goods.prices[1]);

                // 가격을 바꿈
                goods.prices[1] = 170000;
                minedapple_height_price_text.text = "170,000원";

                // 이미 바뀌어 있다면 다른 텍스트로 치환하고 아래 내용을 실행하지 않음
                if (minedapple_height_text.text == "170.0") { minedapple_height_text.text = "마트롤ㅋ"; return; }

                // 마플 키 텍스트의 내용을 바꿈
                minedapple_height_text.text = "170.0";
                info_manager.minedapple_height = "170.0";
                break;

            case "warp 14":
                // 돈이 없으면 실행하지 않음
                if (game_manager.money < goods.prices[2]) { SoundManager.instance.PlaySound("fail"); return; }

                // 돈을 지불한 후
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[2];
                info_manager.use_money += Convert.ToUInt64(goods.prices[2]);

                // +14강 검으로 이동하며 +14강 검을 도감에 추가하며 상점에서 나감
                sword.sword_index = 14;
                sword.UpdateSwordInfo();
                Guide.instance.Unlock(sword.sword_index);

                this.gameObject.SetActive(false);
                break;

            case "get prevention 1":
                // 돈이 없으면 실행하지 않음
                if (game_manager.money < goods.prices[3]) { SoundManager.instance.PlaySound("fail"); return; }

                // 돈을 지불한 후
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[3];
                info_manager.use_money += Convert.ToUInt64(goods.prices[3]);

                // 방지권을 1개 얻음
                game_manager.prevention_remaining++;
                info_manager.get_prevention_num++;
                break;

            case "get prevention 3":
                // 돈이 없으면 실행하지 않음
                if (game_manager.money < goods.prices[4]) { SoundManager.instance.PlaySound("fail"); return; }

                // 돈을 지불한 후
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[4];
                info_manager.use_money += Convert.ToUInt64(goods.prices[4]);

                // 방지권을 3개 얻음
                game_manager.prevention_remaining += 3;
                info_manager.get_prevention_num += 3;
                break;

            case "warp 15":
                // 돈이 없으면 실행하지 않음
                if (game_manager.money < goods.prices[5]) { SoundManager.instance.PlaySound("fail"); return; }

                // 돈을 지불한 후
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[5];
                info_manager.use_money += Convert.ToUInt64(goods.prices[5]);

                // +15강 검으로 이동하며 +15강 검을 도감에 추가하며 상점에서 나감
                sword.sword_index = 15;
                sword.UpdateSwordInfo();
                Guide.instance.Unlock(sword.sword_index);

                this.gameObject.SetActive(false);
                break;
        }
    }
}
