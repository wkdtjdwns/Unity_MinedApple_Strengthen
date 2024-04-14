using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    /* ������ ���
     * ������ ��ǥ
     * ���� ����
     * ���� �ʰ�
     * �ڼ������� ���
     * �ڼ������� ����
     * ������ ����� ��ö
     * ��ġ��
     * ���׹�
     * �ұ� */
    [SerializeField]
    private Text[] item_texts;

    /* �� ���
     * ������ ��
     * ��ö�� ���� ��
     * ���� ������ ��
     * 4Ƽ�� �÷��� ��
     * 170.0�� �Ǳ� ���� ��
     * �뱤�� + ���� ��
     * ����Ǯ ��
     * 4�� ����� ���� ��
     * �ٸ� �Լ� ��
     * ���ε� �ٸ��� �� */
    [SerializeField]
    private Text[] save_sword_texts;

    [SerializeField]
    private Text[] goods_price_texts;

    [SerializeField]
    private Text[] show_difficult_texts;

    [SerializeField]
    private Text tip_text;

    [SerializeField]
    private Text ingredient_text;

    [SerializeField]
    private Text prevention_remaining_text;

    [SerializeField]
    private GameObject sword_obj;
    private Sword sword;

    [SerializeField]
    private GameObject goods_obj;
    private Goods goods;

    private GameObject game_manager_obj;
    private GameManager game_manager;

    private GameObject item_manager_obj;
    private ItemManager item_manager;

    private GameObject difficult_manager_obj;
    private DifficultManager difficult_manager;

    private GameObject info_manager_obj;
    private InfoManager info_manager;

    private void Awake()
    {
        sword = sword_obj.GetComponent<Sword>();
        goods = goods_obj.GetComponent<Goods>();

        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        item_manager_obj = GameObject.Find("ItemManager").gameObject;
        item_manager = item_manager_obj.GetComponent<ItemManager>();

        difficult_manager_obj = GameObject.Find("DifficultManager").gameObject;
        difficult_manager = difficult_manager_obj.GetComponent<DifficultManager>();

        info_manager_obj = GameObject.Find("InfoManager").gameObject;
        info_manager = info_manager_obj.GetComponent <InfoManager>();

        SetShowDifficultText();
    }

    private void SetShowDifficultText()
    {
        // ���̵��� ���� ���̵��� �˷��ִ� �ؽ�Ʈ�� �ٲ�
        if (difficult_manager.difficult == "easy") { show_difficult_texts[0].text = "������"; }
        else if (difficult_manager.difficult == "normal") { show_difficult_texts[1].text = "����"; }
        else { show_difficult_texts[2].text = "��!"; }
    }

    private void Update()
    {
        prevention_remaining_text.text = string.Format("������ : {0}��", game_manager.prevention_remaining);

        ItemTextUpdate();
        SaveSwordTextUpdate();
        NextIngredientTextUpdate();
        IngredientRemainingTextUpdate();
        GoodsPriceTextUpdate();
    }

    private void ItemTextUpdate()
    {
        item_texts[0].text = string.Format("{0} : {1}��", item_manager.fail_item_names[0], item_manager.fail_item_remainings[0]);
        item_texts[1].text = string.Format("{0} : {1}��", item_manager.fail_item_names[1], item_manager.fail_item_remainings[1]);
        item_texts[2].text = string.Format("{0} : {1}��", item_manager.fail_item_names[2], item_manager.fail_item_remainings[2]);
        item_texts[3].text = string.Format("{0} : {1}��", item_manager.fail_item_names[3], item_manager.fail_item_remainings[3]);
        item_texts[4].text = string.Format("{0} : {1}��", item_manager.fail_item_names[4], item_manager.fail_item_remainings[4]);
        item_texts[5].text = string.Format("{0} : {1}��", item_manager.fail_item_names[5], item_manager.fail_item_remainings[5]);
        item_texts[6].text = string.Format("{0} : {1}��", item_manager.fail_item_names[6], item_manager.fail_item_remainings[6]);
        item_texts[7].text = string.Format("{0} : {1}��", item_manager.fail_item_names[7], item_manager.fail_item_remainings[7]);
        item_texts[8].text = string.Format("{0} : {1}��", item_manager.fail_item_names[8], item_manager.fail_item_remainings[8]);
    }

    private void SaveSwordTextUpdate()
    {
        save_sword_texts[0].text = string.Format("{0} : {1}��", sword.sword_names[19], item_manager.save_sword_remainings[0]);
        save_sword_texts[1].text = string.Format("{0} : {1}��", sword.sword_names[20], item_manager.save_sword_remainings[1]);
        save_sword_texts[2].text = string.Format("{0} : {1}��", sword.sword_names[21], item_manager.save_sword_remainings[2]);
        save_sword_texts[3].text = string.Format("{0} : {1}��", sword.sword_names[22], item_manager.save_sword_remainings[3]);
        save_sword_texts[4].text = string.Format("{0} : {1}��", sword.sword_names[23], item_manager.save_sword_remainings[4]);
        save_sword_texts[5].text = string.Format("{0} : {1}��", sword.sword_names[24], item_manager.save_sword_remainings[5]);
        save_sword_texts[6].text = string.Format("{0} : {1}��", sword.sword_names[25], item_manager.save_sword_remainings[6]);
        save_sword_texts[7].text = string.Format("{0} : {1}��", sword.sword_names[26], item_manager.save_sword_remainings[7]);
        save_sword_texts[8].text = string.Format("{0} : {1}��", sword.sword_names[27], item_manager.save_sword_remainings[8]);
        save_sword_texts[9].text = string.Format("{0} : {1}��", sword.sword_names[28], item_manager.save_sword_remainings[9]);
    }

    // Ư���� ��ᰡ ���Ǵ� �� ��ȭ �� �ܰ迡 ������ �ؽ�Ʈ
    private void NextIngredientTextUpdate()
    {
        // �� �˿� ���� �˷������ �ؽ�Ʈ�� �ٸ��� ��
        switch (sword.sword_index)
        {
            // Ư���� ��Ḧ ������� �ʱ� ������ �ʿ� ����
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 25:
            case 27:
            case 28:
                tip_text.text = ""; break;

            /* Rich Text��?
             * ��� ���� : "~~~ <color=red>����</color> ~~~"�� ���� ������ �ؽ�Ʈ���Ը� ȿ���� �ֱ� ���ؼ� "Rich Text" ���
             * ��� ��� : �����ϴ� �±� �Ķ���͸� ����� ��) "333 <color=red>�߿��� �ؽ�Ʈ</color> 333" ���
             * �±� �Ķ����
             *  <size=n>�ؽ�Ʈ</size> -> Ư�� �ܾ� ũ������
             *  <b>�ؽ�Ʈ</b> -> Ư�� �ܾ� ���� ǥ��
             *  <i>�ؽ�Ʈ</i> -> Ư�� �ܾ� ����̱�
             *  <color=#���ڵ�>�ؽ�Ʈ</color> OR <color=red>�ؽ�Ʈ</color> -> Ư�� �ܾ� ���󺯰�

             * ���� ��ĥ �� ����
             *  <size=33><b><i><color=red>�ؽ�Ʈ</color></i></b></size> */

            case 20: tip_text.text = string.Format("Tip\n���� ��ȭ����\n<color=#FF5C5C>{0} {1}��</color>�Դϴ�", sword.sword_names[19], sword.special_sword_prices[0]); break;
            case 21: tip_text.text = string.Format("Tip\n���� ��ȭ����\n<color=#FF5C5C>{0} {1}��</color>�Դϴ�", sword.sword_names[20], sword.special_sword_prices[1]); break;
            case 22: tip_text.text = string.Format("Tip\n���� ��ȭ����\n<color=#FF5C5C>{0} {1}��</color>�Դϴ�", item_manager.fail_item_names[6], sword.special_sword_prices[2]); break;
            case 23: tip_text.text = string.Format("Tip\n���� ��ȭ����\n<color=#FF5C5C>{0} {1}��</color>�Դϴ�", sword.sword_names[22], sword.special_sword_prices[3]); break;
            case 24: tip_text.text = string.Format("Tip\n���� ��ȭ����\n<color=#FF5C5C>{0} {1}��</color>�Դϴ�", item_manager.fail_item_names[7], sword.special_sword_prices[4]); break;
            case 26: tip_text.text = string.Format("Tip\n���� ��ȭ����\n<color=#FF5C5C>{0} {1}��</color>�Դϴ�", item_manager.fail_item_names[8], sword.special_sword_prices[5]); break;
            case 29: tip_text.text = string.Format("Tip\n���ϵ帳�ϴ�!\n<color=#FF5C5C>{0}��</color>�� �õ� ����\n��� ���� ��ȭ�ϼ̽��ϴ�!\n��ȭ ��ư�� ���� ������ �����ϼ���!", info_manager.success_num + info_manager.fail_num); break;
        }
    }

    // ���� ������ �ִ� ��ȭ ��Ḧ �����ִ� �ؽ�Ʈ ������Ʈ
    private void IngredientRemainingTextUpdate()
    {
        // ������ ���� �ʿ��� �� �ε��� ó��
        if (sword.sword_index < 21) { ingredient_text.text = string.Format("�� : {0:#,###}��", game_manager.money); }

        // �ٸ� ��ᰡ �ʿ� �� ���� �ִ� �� �ε��� ó��
        else
        {
            switch (sword.sword_index)
            {
                // ���� �ʿ��� ���� ��쿡�� ���� �����ְ�
                case 26:
                case 28:
                    // {0:#,###} -> õ ���� ���� (,)
                    ingredient_text.text = string.Format("�� : {0:#,###}��", game_manager.money); break;

                // �ٸ� ��ᰡ �ʿ��� ��쿡�� ���� �ٸ� ��Ḧ ������
                case 21:
                    ingredient_text.text = string.Format("���� {0} : {1}��", sword.sword_names[19], item_manager.save_sword_remainings[0]); break;

                case 22:
                    ingredient_text.text = string.Format("���� {0} : {1}��", sword.sword_names[20], item_manager.save_sword_remainings[1]); break;

                case 23:
                    ingredient_text.text = string.Format("���� {0} : {1}��", item_manager.fail_item_names[6], item_manager.fail_item_remainings[6]); break;

                case 24:
                    ingredient_text.text = string.Format("���� {0} : {1}��", sword.sword_names[22], item_manager.save_sword_remainings[4]); break;

                case 25:
                    ingredient_text.text = string.Format("���� {0} : {1}��", item_manager.fail_item_names[7], item_manager.fail_item_remainings[7]); break;

                case 27:
                    ingredient_text.text = string.Format("���� {0} : {1}��", item_manager.fail_item_names[8], item_manager.fail_item_remainings[8]); break;
            }
        }
    }

    private void GoodsPriceTextUpdate()
    {
        goods_price_texts[0].text = string.Format("{0:#,###}��", goods.prices[0]);
        goods_price_texts[1].text = string.Format("{0:#,###}��", goods.prices[1]);
        goods_price_texts[2].text = string.Format("{0:#,###}��", goods.prices[2]);
        goods_price_texts[3].text = string.Format("{0:#,###}��", goods.prices[3]);
        goods_price_texts[4].text = string.Format("{0:#,###}��", goods.prices[4]);
        goods_price_texts[5].text = string.Format("{0:#,###}��", goods.prices[5]);
    }
}
