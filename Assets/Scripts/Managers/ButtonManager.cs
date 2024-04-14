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

    // ���� ��ȭ �ϴ� �޼ҵ�
    public void UpgradeSword()
    {
        // ���� �����ϴ� ���� ���
        if (sword.sword_index < 21 || sword.sword_index == 26)
        {
            // ������ ���� �ش� ���� ��ȭ ��뺸�� ������ �������� ����
            if (game_manager.money < sword.sword_prices[sword.sword_index]) { SoundManager.instance.PlaySound("fail"); return; }

            // ���� ������ ��ȭ ����� ������
            game_manager.money -= sword.sword_prices[sword.sword_index];

            // int���� ���� ������ ulong Ÿ���� use_money�� ����ȯ ��
            info_manager.use_money += Convert.ToUInt64(sword.sword_prices[sword.sword_index]);
        }

        // ���� �ƴ� �ٸ� ��ȭ�� �����ϴ� ���
        else
        {
            // ���� �ٸ� ��ȭ�� �����ϰ� ��
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

        // ��ȭ�� �õ���
        bool is_success = TryUpgradeSword();

        // ���������� ���� ������ �����ϸ鼭 ������ ������
        if (is_success)
        {
            SoundManager.instance.PlaySound("success");

            info_manager.success_num++;

            sword.sword_index++;
            sword.UpdateSwordInfo();
            Guide.instance.Unlock(sword.sword_index);
        }

        // ����������
        else
        {
            SoundManager.instance.PlaySound("fail");

            info_manager.fail_num++;

            // �������� ����� �� ������ �󸶳� �ʿ����� �˷��ְ� �������� ����� �� �ְ� ��
            if (sword.sword_index != 26 && sword.sword_index != 27)
            {
                game_manager.guide_prevention_text.text = string.Format("�������� �ִٸ� ������ ��ư�� ���� �츱 �� �ֽ��ϴ�.\n{0}��(��) {1}���� �Ҹ�˴ϴ�.", sword.sword_names[sword.sword_index], sword.need_preventions[sword.sword_index]);

                game_manager.use_prevention_button_obj.SetActive(true);
            }

            // �������� ����� �� ���ٸ� ����� �� ���ٰ� �˷��ָ鼭 �������� ����� �� ���� ��
            else
            {
                game_manager.guide_prevention_text.text = "�ش� ���� �������� ����Ͻ� �� �����ϴ�.\n�׳� ��ȭ ���и� �޾Ƶ��̼���. ��";

                game_manager.use_prevention_button_obj.SetActive(false);
            }

            // �����ߴٴ� ������Ʈ�� Ȱ��ȭ ��
            game_manager.fail_group.SetActive(true);

            // 0 ~ 4������ ���� �� �������� ������ (System�� �ִ� Random�� UnityEngine�� �ִ� Random ����)
            item_manager.ran_fail_item_num = UnityEngine.Random.Range(0, 5);

            // 8 ~ 16���� Į�� �ƴϰų� ���� �� ���� �������� ������ �������� ��� ������Ʈ�� ��Ȱ��ȭ��
            if (sword.sword_index < 8 || sword.sword_index > 16 || item_manager.ran_fail_item_num <= 0) { game_manager.get_item_obj.SetActive(false); }

            // �ƴϸ�
            else
            {
                // �������� ��� ������Ʈ�� Ȱ��ȭ�ϰ�
                game_manager.get_item_obj.SetActive(true);

                // ������ ȹ�� ��ư�� ���� �� �ְ� �ϸ�
                game_manager.get_item_btn.interactable = true;

                // �������� ������ ������ ȹ�� ��ư�� �ؽ�Ʈ�� ������Ʈ �� (8������ �������� ���� �� ������ - 8 ����)
                game_manager.item_image.sprite = item_manager.fail_item_sprites[sword.sword_index - 8];
                game_manager.get_item_text.text = string.Format("{0} {1}�� �ݱ�", item_manager.fail_item_names[sword.sword_index - 8], item_manager.ran_fail_item_num);
            }
        }
    }

    private bool TryUpgradeSword()
    {
        // 1 ~ 100������ ���� �� �������� ������ (System�� �ִ� Random�� UnityEngine�� �ִ� Random ����)
        int ran_num = UnityEngine.Random.Range(1, 101);

        // ���� �� �ܰ迡 ���� �ٸ� Ȯ���� ������
        if (ran_num <= sword.success_rates[sword.sword_index]) { return true; }
        else { return false; }
    }

    public void GetItem()
    {
        SoundManager.instance.PlaySound("button");

        // �ƾ����� ����ְ� (8������ �������� ���� �� ������ - 8 ����)
        item_manager.fail_item_remainings[sword.sword_index - 8] += item_manager.ran_fail_item_num;

        // ���� �������� �� ������ �����ϸ�
        info_manager.get_special_item_num += item_manager.ran_fail_item_num;

        // ������ ȹ�� ��ư�� �ؽ�Ʈ�� �ٲ� �ڿ�
        game_manager.get_item_text.text = "ȹ�� ����!";

        // ������ ȹ�� ��ư�� ���� �� ���� ��
        game_manager.get_item_btn.interactable = false;
    }

    public void SellSword()
    {
        SoundManager.instance.PlaySound("sell");

        // �Ǹ� ����� ȹ���ϰ�
        game_manager.money += sword.sell_prices[sword.sword_index];

        // ���� �� ��带 �÷���
        info_manager.get_money += Convert.ToUInt64(sword.sell_prices[sword.sword_index]);

        // ���� ó������ ���ư��� ��
        sword.sword_index = 0;
        sword.UpdateSwordInfo();
    }

    public void UsePrevention()
    {
        // �������� ������ �������� ����
        if (game_manager.prevention_remaining < sword.need_preventions[sword.sword_index]) { SoundManager.instance.PlaySound("fail"); return; }

        SoundManager.instance.PlaySound("button");

        // �������� �ʿ��� ��ŭ ����ϰ�
        game_manager.prevention_remaining -= sword.need_preventions[sword.sword_index];

        // ����� �������� ������ ������ ��
        info_manager.use_prevention_num += sword.need_preventions[sword.sword_index];

        // �� ��Ȳ �״�� ������ ����ǵ��� ��
        game_manager.fail_group.SetActive(false);
    }

    public void CheckFail()
    {
        SoundManager.instance.PlaySound("button");

        // ���� ó������ ���ư��� �ϰ�
        sword.sword_index = 0;
        sword.UpdateSwordInfo();

        // ������ ����ǵ��� ��
        game_manager.fail_group.SetActive(false);
    }

    public void GoShop()
    {
        SoundManager.instance.PlaySound("button");

        // ������ ù �������� ��
        game_manager.shop_page = 0;

        game_manager.shop_group.SetActive(true);
        game_manager.shop_page_objs[0].SetActive(true);

        // ������ ������ �ؽ�Ʈ�� �ʱ�ȭ ��Ŵ
        game_manager.show_page_text.text = string.Format("{0}/{1}", game_manager.shop_page + 1, game_manager.shop_page_objs.Length);

        // ������ ù �������� �����ϰ� ��� ��
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

        // ���� ������ �ؽ�Ʈ�� �����ϰ�
        game_manager.show_page_text.text = string.Format("{0}/{1}", game_manager.shop_page + 1, game_manager.shop_page_objs.Length);

        // ������ ��� �������� ���ٰ�
        for (int index = 0; index < game_manager.shop_page_objs.Length; index++) { game_manager.shop_page_objs[index].SetActive(false); }

        // ���� �������� ������ Ŵ
        game_manager.shop_page_objs[game_manager.shop_page].SetActive(true);
    }

    public void QuitShop() { SoundManager.instance.PlaySound("button"); game_manager.shop_group.SetActive(false); }

    public void GoTrade()
    {
        SoundManager.instance.PlaySound("button");

        // ������ ��ȯ���� ù �������� ��
        game_manager.trade_page = 0;

        game_manager.trade_group.SetActive(true);
        game_manager.trade_page_objs[0].SetActive(true);

        // ������ ��ȯ���� ������ �ؽ�Ʈ�� �ʱ�ȭ ��Ŵ
        game_manager.show_trade_page_text.text = string.Format("{0}/{1}", game_manager.trade_page + 1, game_manager.trade_page_objs.Length);

        // ������ ��ȯ���� ù �������� �����ϰ� ��� ��
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

        // ������ ��ȯ�� ������ �ؽ�Ʈ�� �����ϰ�
        game_manager.show_trade_page_text.text = string.Format("{0}/{1}", game_manager.trade_page + 1, game_manager.trade_page_objs.Length);

        // ������ ��ȯ���� ��� �������� ���ٰ�
        for (int index = 0; index < game_manager.trade_page_objs.Length; index++) { game_manager.trade_page_objs[index].SetActive(false); }

        // ���� �������� ������ Ŵ
        game_manager.trade_page_objs[game_manager.trade_page].SetActive(true);
    }

    public void TradeItem(int type)
    {
        // ��ȯ�� �����ۿ� ���� �ٸ� �������� ��ȯ����
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

                    // +13�� ������ �̵��ϸ� ������ ��ȯ�ҿ��� ����
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

                    // +16�� ������ �̵��ϸ� ������ ��ȯ�ҿ��� ����
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

                    // +19�� ������ �̵��ϸ� ������ ��ȯ�ҿ��� ����
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

    // ������ �ִ� �����۰� �ʿ��� ������ ������ ���� ��ȯ�� �������� �Ǵ���
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

        // 19������ ������ �� ������ - 19�� ����
        item_manager.save_sword_remainings[sword.sword_index - 19]++;
        info_manager.get_special_item_num++;

        // ���� ó������ ���ư��� ��
        sword.sword_index = 0;
        sword.UpdateSwordInfo();
    }

    public void GoShowSaveSwordRemaining() { SoundManager.instance.PlaySound("button"); game_manager.show_save_sword_remaining_group.SetActive(true); }

    public void QuitShowSaveSwordRemaining() { SoundManager.instance.PlaySound("button"); game_manager.show_save_sword_remaining_group.SetActive(false); }

    public void OpenGuide() { SoundManager.instance.PlaySound("button"); game_manager.guide_page = 0; ChangeGuidePage(); game_manager.guide_group.SetActive(true); }

    public void GuidePageUp()
    {
        // ������ �������� ���� X
        if (game_manager.guide_page >= 29) { SoundManager.instance.PlaySound("fail"); return; }

        // ���� �������� �ø���
        ++game_manager.guide_page;

        // �������� �ٲ�
        ChangeGuidePage();
    }

    public void GuidePageDown()
    {
        // ó�� �������� ���� X
        if (game_manager.guide_page <= 0) { SoundManager.instance.PlaySound("fail"); return; }

        // ���� �������� ���̰�
        --game_manager.guide_page;

        // �������� �ٲ�
        ChangeGuidePage();
    }

    public void ChangeGuidePage()
    {
        SoundManager.instance.PlaySound("button");

        // ����Ǿ� ���� ���� �������� ��ŵ�
        game_manager.lock_sword_group.SetActive(!Guide.instance.sword_unlock_list[game_manager.guide_page]);

        // ������ �ؽ�Ʈ�� ������
        game_manager.guide_page_text.text = string.Format("{0}/{1}", game_manager.guide_page + 1, 30);

        // ���� �ش� �������� ����ִٸ� �� �̹����� ������ ���������� �ٲٰ� �˿� ���� ��� ������ ����
        if (game_manager.lock_sword_group.activeSelf)
        {
            game_manager.guide_sword_image.sprite = sword.sword_sprites[game_manager.guide_page];
            game_manager.guide_sword_image.color = new Color(0, 0, 0, 255);

            game_manager.guide_reference_text.text = "???";
            game_manager.guide_sword_name_text.text = "???";
            game_manager.guide_sword_info_text.text = "������ : ???%";

            game_manager.guide_sword_explain_text.text = "???";
        }

        // �ش� �������� ����Ǿ� �ִٸ�
        else
        {
            // �� �̹����� ������ ���������� �ٲٸ� �� �̹����� ���� �̸��� �ٲ���
            game_manager.guide_sword_image.color = new Color(255, 255, 255, 255);

            game_manager.guide_sword_image.sprite = sword.sword_sprites[game_manager.guide_page];
            game_manager.guide_sword_name_text.text = sword.sword_names[game_manager.guide_page];

            // �ش� �˿� ���� ���� ���� �ٲ���
            ReferenceTextUpdate();

            game_manager.guide_sword_info_text.text = string.Format("������ : {0}%", sword.success_rates[game_manager.guide_page]);
            if (game_manager.guide_page == 29) { game_manager.guide_sword_info_text.text = "������ : Unknown%"; }

            // �ν����� â������ �ٹٲ��� �����ϰ� ��
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
            case 23: game_manager.guide_reference_text.text = "���"; break;

            case 3: game_manager.guide_reference_text.text = "��"; break;
            case 4: game_manager.guide_reference_text.text = "����"; break;

            case 5:
            case 26: game_manager.guide_reference_text.text = "��� & ����ũ����Ʈ"; break;

            case 6: game_manager.guide_reference_text.text = "���� �ùķ�����"; break;

            case 7:
            case 11:
            case 12: game_manager.guide_reference_text.text = "�ڼ�����"; break;

            case 8: game_manager.guide_reference_text.text = "�ٿ ��"; break;
            case 9: game_manager.guide_reference_text.text = "�׶����"; break;
            case 10: game_manager.guide_reference_text.text = "���� �긴��"; break;
            case 13: game_manager.guide_reference_text.text = "������"; break;

            case 15:
            case 25: game_manager.guide_reference_text.text = "��� ������"; break;

            case 16: game_manager.guide_reference_text.text = "������ ����"; break;
            case 17: game_manager.guide_reference_text.text = "���� & ����ũ����Ʈ"; break;
            case 18: game_manager.guide_reference_text.text = "�����ϸ�"; break;

            case 19:
            case 21: game_manager.guide_reference_text.text = "�̽������ǽ�Ʈ"; break;

            case 20:
            case 22:
            case 24: game_manager.guide_reference_text.text = "�ƽ�Ʈ�δϾ�"; break;

            case 27: game_manager.guide_reference_text.text = "����ݸ�"; break;

            default: game_manager.guide_reference_text.text = "��Ÿ"; break;
        }
    }

    public void CloseGuide() { SoundManager.instance.PlaySound("button"); game_manager.guide_group.SetActive(false); }

    public void GoEnding() { SoundManager.instance.PlaySound("button"); game_manager.ask_go_ending_obj.SetActive(true); }

    public void GoEndingCheck() { SoundManager.instance.PlayBgm("ending"); SceneManager.LoadScene("EndingScene"); }
}