using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sword_obj;
    private Sword sword;

    private GameObject game_manager_obj;
    private GameManager game_manager;

    private GameObject item_manager_obj;
    private ItemManager item_manager;

    private GameObject info_manager_obj;
    private InfoManager info_manager;

    private void Awake()
    {
        sword = sword_obj.GetComponent<Sword>();

        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        item_manager_obj = GameObject.Find("ItemManager").gameObject;
        item_manager = item_manager_obj.GetComponent<ItemManager>();

        info_manager_obj = GameObject.Find("InfoManager").gameObject;
        info_manager = info_manager_obj.GetComponent<InfoManager>();
    }

    // 검을 강화 하는 메소드
    public void UpgradeSword()
    {
        // 돈을 지불하는 검의 경우
        if (sword.sword_index < 21 || sword.sword_index == 26)
        {
            // 보유한 돈이 해당 검의 강화 비용보다 적으면 실행하지 않음
            if (game_manager.money < sword.sword_prices[sword.sword_index]) { SoundManager.instance.PlaySound("fail"); return; }

            // 돈이 있으면 강화 비용을 지불함
            game_manager.money -= sword.sword_prices[sword.sword_index];

            // int형인 검의 가격을 ulong 타입의 use_money로 형변환 함
            info_manager.use_money += Convert.ToUInt64(sword.sword_prices[sword.sword_index]);
        }

        // 돈이 아닌 다른 재화를 지불하는 경우
        else
        {
            // 각각 다른 재화를 지불하게 함
            switch (sword.sword_index)
            {
                case 21:
                    if (item_manager.save_sword_remainings[0] < sword.special_sword_prices[0]) { SoundManager.instance.PlaySound("fail"); return; }
                    item_manager.save_sword_remainings[0] -= sword.special_sword_prices[0];
                    info_manager.use_special_item_num += sword.special_sword_prices[0];
                    break;

                case 22:
                    if (item_manager.save_sword_remainings[1] < sword.special_sword_prices[1]) { SoundManager.instance.PlaySound("fail"); return; }
                    item_manager.save_sword_remainings[1] -= sword.special_sword_prices[1];
                    info_manager.use_special_item_num += sword.special_sword_prices[1];
                    break;

                case 23:
                    if (item_manager.fail_item_remainings[6] < sword.special_sword_prices[2]) { SoundManager.instance.PlaySound("fail"); return; }
                    item_manager.fail_item_remainings[6] -= sword.special_sword_prices[2];
                    info_manager.use_special_item_num += sword.special_sword_prices[2];
                    break;

                case 24:
                    if (item_manager.save_sword_remainings[3] < sword.special_sword_prices[3]) { SoundManager.instance.PlaySound("fail"); return; }
                    item_manager.save_sword_remainings[3] -= sword.special_sword_prices[3];
                    info_manager.use_special_item_num += sword.special_sword_prices[3];
                    break;

                case 25:
                    if (item_manager.fail_item_remainings[7] < sword.special_sword_prices[4]) { SoundManager.instance.PlaySound("fail"); return; }
                    item_manager.fail_item_remainings[7] -= sword.special_sword_prices[4];
                    info_manager.use_special_item_num += sword.special_sword_prices[4];
                    break;

                case 27:
                    if (item_manager.fail_item_remainings[8] < sword.special_sword_prices[5]) { SoundManager.instance.PlaySound("fail"); return; }
                    item_manager.fail_item_remainings[8] -= sword.special_sword_prices[5];
                    info_manager.use_special_item_num += sword.special_sword_prices[5];
                    break;
            }
        }

        // 강화를 시도함
        bool is_success = TryUpgradeSword();

        // 성공했으면 다음 검으로 갱신하면서 도감에 저장함
        if (is_success)
        {
            SoundManager.instance.PlaySound("success");

            info_manager.success_num++;

            sword.sword_index++;
            sword.UpdateSwordInfo();
            Guide.instance.Unlock(sword.sword_index);
        }

        // 실패했으면
        else
        {
            SoundManager.instance.PlaySound("fail");

            info_manager.fail_num++;

            // 방지권을 사용할 수 있으면 얼마나 필요한지 알려주고 방지권을 사용할 수 있게 함
            if (sword.sword_index != 26 && sword.sword_index != 27)
            {
                game_manager.guide_prevention_text.text = string.Format("방지권이 있다면 오른쪽 버튼을 눌러 살릴 수 있습니다.\n{0}은(는) {1}개가 소모됩니다.", sword.sword_names[sword.sword_index], sword.need_preventions[sword.sword_index]);

                game_manager.use_prevention_button_obj.SetActive(true);
            }

            // 방지권을 사용할 수 없다면 사용할 수 없다고 알려주면서 방지권을 사용할 수 없게 함
            else
            {
                game_manager.guide_prevention_text.text = "해당 검은 방지권을 사용하실 수 없습니다.\n그냥 강화 실패를 받아들이세요. ㅋ";

                game_manager.use_prevention_button_obj.SetActive(false);
            }

            // 실패했다는 오브젝트를 활성화 함
            game_manager.fail_group.SetActive(true);

            // 0 ~ 4까지의 숫자 중 랜덤으로 가져옴 (System에 있는 Random과 UnityEngine에 있는 Random 구분)
            item_manager.ran_fail_item_num = UnityEngine.Random.Range(0, 5);

            // 8 ~ 16강의 칼이 아니거나 얻을 수 없는 아이템이 없으면 아이템을 얻는 오브젝트를 비활성화함
            if (sword.sword_index < 8 || sword.sword_index > 16 || item_manager.ran_fail_item_num <= 0) { game_manager.get_item_obj.SetActive(false); }

            // 아니면
            else
            {
                // 아이템을 얻는 오브젝트를 활성화하고
                game_manager.get_item_obj.SetActive(true);

                // 아이템 획득 버튼을 누를 수 있게 하며
                game_manager.get_item_btn.interactable = true;

                // 아이템의 정보와 아이템 획득 버튼의 텍스트를 업데이트 함 (8강부터 아이템을 얻을 수 있으니 - 8 해줌)
                game_manager.item_image.sprite = item_manager.fail_item_sprites[sword.sword_index - 8];
                game_manager.get_item_text.text = string.Format("{0} {1}개 줍기", item_manager.fail_item_names[sword.sword_index - 8], item_manager.ran_fail_item_num);
            }
        }
    }

    private bool TryUpgradeSword()
    {
        // 1 ~ 100까지의 숫자 중 랜덤으로 가져옴 (System에 있는 Random과 UnityEngine에 있는 Random 구분)
        int ran_num = UnityEngine.Random.Range(1, 101);

        // 현재 검 단계에 따라서 다른 확률로 연산함
        if (ran_num <= sword.success_rates[sword.sword_index]) { return true; }
        else { return false; }
    }

    public void GetItem()
    {
        SoundManager.instance.PlaySound("button");

        // 아아템을 얻어주고 (8강부터 아이템을 얻을 수 있으니 - 8 해줌)
        item_manager.fail_item_remainings[sword.sword_index - 8] += item_manager.ran_fail_item_num;

        // 얻은 아이템의 총 개수를 설정하며
        info_manager.get_special_item_num += item_manager.ran_fail_item_num;

        // 아이템 획득 버튼의 텍스트를 바꾼 뒤에
        game_manager.get_item_text.text = "획득 성공!";

        // 아이템 획득 버튼을 누를 수 없게 함
        game_manager.get_item_btn.interactable = false;
    }

    public void SellSword()
    {
        SoundManager.instance.PlaySound("sell");

        // 판매 비용을 획득하고
        game_manager.money += sword.sell_prices[sword.sword_index];

        // 얻은 총 골드를 올려줌
        info_manager.get_money += Convert.ToUInt64(sword.sell_prices[sword.sword_index]);

        // 검을 처음으로 돌아가게 함
        sword.sword_index = 0;
        sword.UpdateSwordInfo();
    }

    public void UsePrevention()
    {
        // 방지권이 없으면 실행하지 않음
        if (game_manager.prevention_remaining < sword.need_preventions[sword.sword_index]) { SoundManager.instance.PlaySound("fail"); return; }

        SoundManager.instance.PlaySound("button");

        // 방지권을 필요한 만큼 사용하고
        game_manager.prevention_remaining -= sword.need_preventions[sword.sword_index];

        // 사용한 방지권의 개수를 설정한 뒤
        info_manager.use_prevention_num += sword.need_preventions[sword.sword_index];

        // 그 상황 그대로 게임이 진행되도록 함
        game_manager.fail_group.SetActive(false);
    }

    public void CheckFail()
    {
        SoundManager.instance.PlaySound("button");

        // 검을 처음으로 돌아가게 하고
        sword.sword_index = 0;
        sword.UpdateSwordInfo();

        // 게임이 진행되도록 함
        game_manager.fail_group.SetActive(false);
    }

    public void GoShop()
    {
        SoundManager.instance.PlaySound("button");

        // 상점의 첫 페이지를 켬
        game_manager.shop_page = 0;

        game_manager.shop_group.SetActive(true);
        game_manager.shop_page_objs[0].SetActive(true);

        // 상점의 페이지 텍스트를 초기화 시킴
        game_manager.show_page_text.text = string.Format("{0}/{1}", game_manager.shop_page + 1, game_manager.shop_page_objs.Length);

        // 상점의 첫 페이지를 제외하고 모두 끔
        for (int index = 1; index < game_manager.shop_page_objs.Length; index++) { game_manager.shop_page_objs[index].SetActive(false); }
    }

    public void PageUp()
    {
        if (game_manager.shop_page >= game_manager.shop_page_objs.Length - 1) { SoundManager.instance.PlaySound("fail"); return; }

        game_manager.shop_page++;
        ChangePage();
    }

    public void PageDown()
    {
        if (game_manager.shop_page <= 0) { SoundManager.instance.PlaySound("fail"); return; }

        game_manager.shop_page--;
        ChangePage();
    }

    private void ChangePage()
    {
        SoundManager.instance.PlaySound("button");

        // 상점 페이지 텍스트를 갱신하고
        game_manager.show_page_text.text = string.Format("{0}/{1}", game_manager.shop_page + 1, game_manager.shop_page_objs.Length);

        // 상점의 모든 페이지를 껐다가
        for (int index = 0; index < game_manager.shop_page_objs.Length; index++) { game_manager.shop_page_objs[index].SetActive(false); }

        // 현재 페이지의 상점을 킴
        game_manager.shop_page_objs[game_manager.shop_page].SetActive(true);
    }

    public void QuitShop() { SoundManager.instance.PlaySound("button"); game_manager.shop_group.SetActive(false); }

    public void GoTrade()
    {
        SoundManager.instance.PlaySound("button");

        // 아이템 교환소의 첫 페이지를 켬
        game_manager.trade_page = 0;

        game_manager.trade_group.SetActive(true);
        game_manager.trade_page_objs[0].SetActive(true);

        // 아이템 교환소의 페이지 텍스트를 초기화 시킴
        game_manager.show_trade_page_text.text = string.Format("{0}/{1}", game_manager.trade_page + 1, game_manager.trade_page_objs.Length);

        // 아이템 교환소의 첫 페이지를 제외하고 모두 끔
        for (int index = 1; index < game_manager.trade_page_objs.Length; index++) { game_manager.trade_page_objs[index].SetActive(false); }
    }

    public void TradePageUp()
    {
        if (game_manager.trade_page >= game_manager.trade_page_objs.Length - 1) { SoundManager.instance.PlaySound("fail"); return; }

        game_manager.trade_page++;
        ChangeTradePage();
    }

    public void TradePageDown()
    {
        if (game_manager.trade_page <= 0) { SoundManager.instance.PlaySound("fail"); return; }

        game_manager.trade_page--;
        ChangeTradePage();
    }

    private void ChangeTradePage()
    {
        SoundManager.instance.PlaySound("button");

        // 아이템 교환소 페이지 텍스트를 갱신하고
        game_manager.show_trade_page_text.text = string.Format("{0}/{1}", game_manager.trade_page + 1, game_manager.trade_page_objs.Length);

        // 아이템 교환소의 모든 페이지를 껐다가
        for (int index = 0; index < game_manager.trade_page_objs.Length; index++) { game_manager.trade_page_objs[index].SetActive(false); }

        // 현재 페이지의 상점을 킴
        game_manager.trade_page_objs[game_manager.trade_page].SetActive(true);
    }

    public void TradeItem(int type)
    {
        // 교환할 아이템에 따라서 다른 아이템을 교환해줌
        switch (type)
        {
            case 0:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 5))
                {
                    item_manager.fail_item_remainings[type] -= 5;
                    info_manager.use_special_item_num += 5;

                    game_manager.prevention_remaining++;
                    info_manager.get_prevention_num++;
                }
                break;

            case 1:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 3))
                {
                    item_manager.fail_item_remainings[type] -= 3;
                    info_manager.use_special_item_num += 3;

                    game_manager.prevention_remaining++;
                    info_manager.get_prevention_num++;
                }
                break;

            case 2:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 2))
                {
                    item_manager.fail_item_remainings[type] -= 2;
                    info_manager.use_special_item_num += 2;

                    // +13강 검으로 이동하며 아이템 교환소에서 나감
                    sword.sword_index = 13;
                    sword.UpdateSwordInfo();

                    game_manager.trade_group.SetActive(false);
                }
                break;

            case 3:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 3))
                {
                    item_manager.fail_item_remainings[type] -= 3;
                    info_manager.use_special_item_num += 3;

                    game_manager.prevention_remaining += 2;
                    info_manager.get_prevention_num += 2;
                }
                break;

            case 4:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 2))
                {
                    item_manager.fail_item_remainings[type] -= 2;
                    info_manager.use_special_item_num += 2;

                    // +16강 검으로 이동하며 아이템 교환소에서 나감
                    sword.sword_index = 16;
                    sword.UpdateSwordInfo();

                    game_manager.trade_group.SetActive(false);
                }
                break;

            case 5:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 4))
                {
                    item_manager.fail_item_remainings[type] -= 4;
                    info_manager.use_special_item_num += 4;

                    game_manager.prevention_remaining += 4;
                    info_manager.get_prevention_num += 4;
                }
                break;

            case 6:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 6))
                {
                    item_manager.fail_item_remainings[type] -= 6;
                    info_manager.use_special_item_num += 6;

                    // +19강 검으로 이동하며 아이템 교환소에서 나감
                    sword.sword_index = 19;
                    sword.UpdateSwordInfo();

                    game_manager.trade_group.SetActive(false);
                }
                break;

            case 7:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 6))
                {
                    item_manager.fail_item_remainings[type] -= 6;
                    info_manager.use_special_item_num += 6;

                    game_manager.prevention_remaining += 10;
                    info_manager.get_prevention_num += 10;
                }
                break;

            case 8:
                if (CheckPossibleTrade(item_manager.fail_item_remainings[type], 3))
                {
                    item_manager.fail_item_remainings[type] -= 3;
                    info_manager.use_special_item_num += 3;

                    game_manager.prevention_remaining += 9;
                    info_manager.get_prevention_num += 9;
                }
                break;
        }
    }

    // 가지고 있는 아이템과 필요한 아이템 개수에 따라서 교환이 가능한지 판단함
    private bool CheckPossibleTrade(int have, int need)
    {
        if (have < need) { SoundManager.instance.PlaySound("fail"); return false; }
        else { SoundManager.instance.PlaySound("purchase"); return true; }
    }

    public void QuitTrade() { SoundManager.instance.PlaySound("button"); game_manager.trade_group.SetActive(false); }

    public void GoShowRemaining() { SoundManager.instance.PlaySound("button"); game_manager.show_remaining_group.SetActive(true); }

    public void QuitShowRemaining() { SoundManager.instance.PlaySound("button"); game_manager.show_remaining_group.SetActive(false); }

    public void SwordSave()
    {
        SoundManager.instance.PlaySound("button");

        // 19강부터 저장할 수 있으니 - 19를 해줌
        item_manager.save_sword_remainings[sword.sword_index - 19]++;
        info_manager.get_special_item_num++;

        // 검을 처음으로 돌아가게 함
        sword.sword_index = 0;
        sword.UpdateSwordInfo();
    }

    public void GoShowSaveSwordRemaining() { SoundManager.instance.PlaySound("button"); game_manager.show_save_sword_remaining_group.SetActive(true); }

    public void QuitShowSaveSwordRemaining() { SoundManager.instance.PlaySound("button"); game_manager.show_save_sword_remaining_group.SetActive(false); }

    public void OpenGuide() { SoundManager.instance.PlaySound("button"); game_manager.guide_page = 0; ChangeGuidePage(); game_manager.guide_group.SetActive(true); }

    public void GuidePageUp()
    {
        // 마지막 페이지는 실행 X
        if (game_manager.guide_page >= 29) { SoundManager.instance.PlaySound("fail"); return; }

        // 먼저 페이지를 늘리고
        ++game_manager.guide_page;

        // 페이지를 바꿈
        ChangeGuidePage();
    }

    public void GuidePageDown()
    {
        // 처음 페이지는 실행 X
        if (game_manager.guide_page <= 0) { SoundManager.instance.PlaySound("fail"); return; }

        // 먼저 페이지를 줄이고
        --game_manager.guide_page;

        // 페이지를 바꿈
        ChangeGuidePage();
    }

    public void ChangeGuidePage()
    {
        SoundManager.instance.PlaySound("button");

        // 언락되어 있지 않은 페이지는 잠궈둠
        game_manager.lock_sword_group.SetActive(!Guide.instance.sword_unlock_list[game_manager.guide_page]);

        // 페이지 텍스트를 갱신함
        game_manager.guide_page_text.text = string.Format("{0}/{1}", game_manager.guide_page + 1, 30);

        // 만약 해당 페이지가 잠겨있다면 검 이미지의 색상을 검은색으로 바꾸고 검에 대한 모든 정보를 감춤
        if (game_manager.lock_sword_group.activeSelf)
        {
            game_manager.guide_sword_image.sprite = sword.sword_sprites[game_manager.guide_page];
            game_manager.guide_sword_image.color = new Color(0, 0, 0, 255);

            game_manager.guide_reference_text.text = "???";
            game_manager.guide_sword_name_text.text = "???";
            game_manager.guide_sword_info_text.text = "성공률 : ???%";

            game_manager.guide_sword_explain_text.text = "???";
        }

        // 해당 페이지가 언락되어 있다면
        else
        {
            // 검 이미지의 색상을 정상적으로 바꾸모 검 이미지와 검의 이름을 바꿔줌
            game_manager.guide_sword_image.color = new Color(255, 255, 255, 255);

            game_manager.guide_sword_image.sprite = sword.sword_sprites[game_manager.guide_page];
            game_manager.guide_sword_name_text.text = sword.sword_names[game_manager.guide_page];

            // 해당 검에 대한 설명 또한 바꿔줌
            ReferenceTextUpdate();

            game_manager.guide_sword_info_text.text = string.Format("성공률 : {0}%", sword.success_rates[game_manager.guide_page]);
            if (game_manager.guide_page == 29) { game_manager.guide_sword_info_text.text = "성공률 : Unknown%"; }

            // 인스펙터 창에서의 줄바꿈을 가능하게 함
            game_manager.guide_sword_explain_text.text = game_manager.guide_sword_explains[game_manager.guide_page].Replace("\\n", "\n"); ;
        }
    }

    private void ReferenceTextUpdate()
    {
        switch (game_manager.guide_page)
        {
            case 0:
            case 1:
            case 2:
            case 14:
            case 23: game_manager.guide_reference_text.text = "사과"; break;

            case 3: game_manager.guide_reference_text.text = "물"; break;
            case 4: game_manager.guide_reference_text.text = "상추"; break;

            case 5:
            case 26: game_manager.guide_reference_text.text = "사과 & 마인크래프트"; break;

            case 6: game_manager.guide_reference_text.text = "도둑 시뮬레이터"; break;

            case 7:
            case 11:
            case 12: game_manager.guide_reference_text.text = "핸섬가이"; break;

            case 8: game_manager.guide_reference_text.text = "바운스 볼"; break;
            case 9: game_manager.guide_reference_text.text = "그라운디드"; break;
            case 10: game_manager.guide_reference_text.text = "폴리 브릿지"; break;
            case 13: game_manager.guide_reference_text.text = "리벤쳐"; break;

            case 15:
            case 25: game_manager.guide_reference_text.text = "산소 미포함"; break;

            case 16: game_manager.guide_reference_text.text = "세상이 무기"; break;
            case 17: game_manager.guide_reference_text.text = "포도 & 마인크래프트"; break;
            case 18: game_manager.guide_reference_text.text = "스마일모"; break;

            case 19:
            case 21: game_manager.guide_reference_text.text = "이스케이피스트"; break;

            case 20:
            case 22:
            case 24: game_manager.guide_reference_text.text = "아스트로니어"; break;

            case 27: game_manager.guide_reference_text.text = "브로콜리"; break;

            default: game_manager.guide_reference_text.text = "기타"; break;
        }
    }

    public void CloseGuide() { SoundManager.instance.PlaySound("button"); game_manager.guide_group.SetActive(false); }

    public void GoEnding() { SoundManager.instance.PlaySound("button"); game_manager.ask_go_ending_obj.SetActive(true); }

    public void GoEndingCheck() { SoundManager.instance.PlayBgm("ending"); SceneManager.LoadScene("EndingScene"); }
}