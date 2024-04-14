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
        // �������� Ÿ�Կ� ���� �ٸ� ȿ���� ��
        switch (type)
        {
            case "warp 9":
                // ���� ������ �������� ����
                if (game_manager.money < goods.prices[0]) { SoundManager.instance.PlaySound("fail"); return; }

                // ���� ������ ��
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[0];
                info_manager.use_money += Convert.ToUInt64(goods.prices[0]);


                // +9�� ������ �̵��ϸ� +9�� ���� ������ �߰��ϸ� �������� ����
                sword.sword_index = 9;
                sword.UpdateSwordInfo();
                Guide.instance.Unlock(sword.sword_index);

                this.gameObject.SetActive(false);
                break;

            // �̽��� ���׿� ����� ��������
            case "growth":
                // ���� ������ �������� ����
                if (game_manager.money < goods.prices[1]) { SoundManager.instance.PlaySound("fail"); return; }

                // ���� ������ ��
                SoundManager.instance.PlaySound("purchase");

                game_manager.money -= goods.prices[1];
                info_manager.use_money += Convert.ToUInt64(goods.prices[1]);

                // ������ �ٲ�
                goods.prices[1] = 170000;
                minedapple_height_price_text.text = "170,000��";

                // �̹� �ٲ�� �ִٸ� �ٸ� �ؽ�Ʈ�� ġȯ�ϰ� �Ʒ� ������ �������� ����
                if (minedapple_height_text.text == "170.0") { minedapple_height_text.text = "��Ʈ�Ѥ�"; return; }

                // ���� Ű �ؽ�Ʈ�� ������ �ٲ�
                minedapple_height_text.text = "170.0";
                info_manager.minedapple_height = "170.0";
                break;

            case "warp 14":
                // ���� ������ �������� ����
                if (game_manager.money < goods.prices[2]) { SoundManager.instance.PlaySound("fail"); return; }

                // ���� ������ ��
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[2];
                info_manager.use_money += Convert.ToUInt64(goods.prices[2]);

                // +14�� ������ �̵��ϸ� +14�� ���� ������ �߰��ϸ� �������� ����
                sword.sword_index = 14;
                sword.UpdateSwordInfo();
                Guide.instance.Unlock(sword.sword_index);

                this.gameObject.SetActive(false);
                break;

            case "get prevention 1":
                // ���� ������ �������� ����
                if (game_manager.money < goods.prices[3]) { SoundManager.instance.PlaySound("fail"); return; }

                // ���� ������ ��
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[3];
                info_manager.use_money += Convert.ToUInt64(goods.prices[3]);

                // �������� 1�� ����
                game_manager.prevention_remaining++;
                info_manager.get_prevention_num++;
                break;

            case "get prevention 3":
                // ���� ������ �������� ����
                if (game_manager.money < goods.prices[4]) { SoundManager.instance.PlaySound("fail"); return; }

                // ���� ������ ��
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[4];
                info_manager.use_money += Convert.ToUInt64(goods.prices[4]);

                // �������� 3�� ����
                game_manager.prevention_remaining += 3;
                info_manager.get_prevention_num += 3;
                break;

            case "warp 15":
                // ���� ������ �������� ����
                if (game_manager.money < goods.prices[5]) { SoundManager.instance.PlaySound("fail"); return; }

                // ���� ������ ��
                SoundManager.instance.PlaySound("purchase");
                game_manager.money -= goods.prices[5];
                info_manager.use_money += Convert.ToUInt64(goods.prices[5]);

                // +15�� ������ �̵��ϸ� +15�� ���� ������ �߰��ϸ� �������� ����
                sword.sword_index = 15;
                sword.UpdateSwordInfo();
                Guide.instance.Unlock(sword.sword_index);

                this.gameObject.SetActive(false);
                break;
        }
    }
}
