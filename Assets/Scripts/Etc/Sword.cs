using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private Text sword_name_text;
    [SerializeField]
    private Text sword_price_text;
    [SerializeField]
    private Text success_rate_text;

    public string[] sword_names;

    public int[] special_sword_prices;
    public int[] sword_prices;
    public int[] sell_prices;
    public int[] success_rates;
    public int[] need_preventions;

    public Sprite[] sword_sprites;

    [SerializeField]
    private GameObject sword_obj;
    private Image sword_image;

    public int sword_index;

    private GameObject game_manager_obj;
    private GameManager game_manager;

    private GameObject item_manager_obj;
    private ItemManager item_manager;

    private GameObject button_manager_obj;
    private ButtonManager button_manager;

    private GameObject difficult_manager_obj;
    private DifficultManager difficult_manager;

    private void Awake()
    {
        sword_index = 0;

        special_sword_prices = new int[6];
        sword_prices = new int[30];
        sell_prices = new int[30];
        success_rates = new int[30];
        need_preventions = new int[30];

        sword_image = sword_obj.GetComponent<Image>();

        game_manager_obj = GameObject.Find("GameManager").gameObject;
        game_manager = game_manager_obj.GetComponent<GameManager>();

        item_manager_obj = GameObject.Find("ItemManager").gameObject;
        item_manager = item_manager_obj.GetComponent<ItemManager>();

        button_manager_obj = GameObject.Find("ButtonManager").gameObject;
        button_manager = button_manager_obj.GetComponent<ButtonManager>();

        difficult_manager_obj = GameObject.Find("DifficultManager").gameObject;
        difficult_manager = difficult_manager_obj.GetComponent<DifficultManager>();

        Setting();
    }

    // ���� ������ �ؾ��ϱ� ������ Awake �Լ��� ����� �ڿ� �����ϵ��� ��
    private void Start() { UpdateSwordInfo(); }

    public void UpdateSwordInfo()
    {
        // ���� ������ ������
        sword_image.sprite = sword_sprites[sword_index];
        sword_name_text.text = string.Format("+{0} {1}", sword_index, sword_names[sword_index]);

        // ���� ��ȭ ������
        if (sword_index != 29) { success_rate_text.text = string.Format("������ {0}%", success_rates[sword_index]); }
        else { success_rate_text.text = "Unknown"; }

        // ���� ��ȭ ���� �Ǹ� ����
        switch (sword_index)
        {
            case 0: sword_price_text.text = "��ȭ ��� : 300��\n�Ǹ� ���� : 0��"; break;
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7: sword_price_text.text = string.Format("��ȭ ��� : {0:#,###}��\n�Ǹ� ���� : {1:#,###}��", sword_prices[sword_index], sell_prices[sword_index]); break;
            case 8: sword_price_text.text = "��ȭ ��� : 3,000��\n�Ǹ� ���� : 1����"; break;
            case 9: sword_price_text.text = "��ȭ ��� : 5,000��\n�Ǹ� ���� : 2����"; break;
            case 10: sword_price_text.text = "��ȭ ��� : 1����\n�Ǹ� ���� : 3.5����"; break;
            case 11: sword_price_text.text = "��ȭ ��� : 2����\n�Ǹ� ���� : 16����"; break;
            case 12: sword_price_text.text = "��ȭ ��� : 3.5����\n�Ǹ� ���� : 35����"; break;
            case 13: sword_price_text.text = "��ȭ ��� : 5.5����\n�Ǹ� ���� : 100����"; break;
            case 14: sword_price_text.text = "��ȭ ��� : 10����\n�Ǹ� ���� : 300����"; break;
            case 15: sword_price_text.text = "��ȭ ��� : 18����\n�Ǹ� ���� : 750����"; break;
            case 16: sword_price_text.text = "��ȭ ��� : 30����\n�Ǹ� ���� : 1õ����"; break;
            case 17: sword_price_text.text = "��ȭ ��� : 30����\n�Ǹ� ���� : 2õ����"; break;
            case 18: sword_price_text.text = "��ȭ ��� : 50����\n�Ǹ� ���� : 3õ����"; break;
            case 19: sword_price_text.text = "��ȭ ��� : 80����\n�Ǹ� ���� : 5õ����"; break;
            case 20: sword_price_text.text = "��ȭ ��� : 150����\n�Ǹ� ���� : 7õ����"; break;
            case 21: sword_price_text.text = string.Format("��ȭ ��� : ������ �� {0}��\n�Ǹ� ���� : 1���", special_sword_prices[0]); break;
            case 22: sword_price_text.text = string.Format("��ȭ ��� : ö�� ���� �� {0}��\n�Ǹ� ���� : 1.6���", special_sword_prices[1]); break;
            case 23: sword_price_text.text = string.Format("��ȭ ��� : ��ġ�� {0}��\n�Ǹ� ���� : 2.3���", special_sword_prices[2]); break;
            case 24: sword_price_text.text = string.Format("��ȭ ��� : 4Ƽ�� �÷��� �� {0}��\n�Ǹ� ���� : 3���", special_sword_prices[3]); break;
            case 25: sword_price_text.text = string.Format("��ȭ ��� : ���׹� {0}��\n�Ǹ� ���� : 4���", special_sword_prices[4]); break;
            case 26: sword_price_text.text = "��ȭ ��� : 500����\n�Ǹ� ���� : 6���"; break;
            case 27: sword_price_text.text = string.Format("��ȭ ��� : �ұ� {0}��\n�Ǹ� ���� : 8���", special_sword_prices[5]); break;
            case 28: sword_price_text.text = "��ȭ ��� : ����\n�Ǹ� ���� : �� �� ����"; break;
            case 29: sword_price_text.text = "��ȭ ��� : Unknown\n�Ǹ� ���� : Unknown"; break;
        }
    }

    private void Setting()
    {
        // ���̵��� ���� ���� ������ �ٲ� (������, ������ ����, Ư�� ��� ���� | ����, �ǸŰ��� ������)
        if (difficult_manager.difficult == "easy")
        {
            success_rates[0] = 100;
            need_preventions[0] = 1;
            special_sword_prices[0] = 1;

            success_rates[1] = 100;
            need_preventions[1] = 1;
            special_sword_prices[1] = 2;

            success_rates[2] = 100;
            need_preventions[2] = 1;
            special_sword_prices[2] = 12;

            success_rates[3] = 95;
            need_preventions[3] = 1;
            special_sword_prices[3] = 1;

            success_rates[4] = 95;
            need_preventions[4] = 1;
            special_sword_prices[4] = 15;

            success_rates[5] = 90;
            need_preventions[5] = 1;
            special_sword_prices[5] = 4;

            success_rates[6] = 90;
            need_preventions[6] = 1;

            success_rates[7] = 90;
            need_preventions[7] = 1;

            success_rates[8] = 85;
            need_preventions[8] = 1;

            success_rates[9] = 80;
            need_preventions[9] = 1;

            success_rates[10] = 80;
            need_preventions[10] = 2;

            success_rates[11] = 75;
            need_preventions[11] = 2;

            success_rates[12] = 70;
            need_preventions[12] = 2;

            success_rates[13] = 70;
            need_preventions[13] = 2;

            success_rates[14] = 65;
            need_preventions[14] = 2;

            success_rates[15] = 60;
            need_preventions[15] = 3;

            success_rates[16] = 60;
            need_preventions[16] = 4;

            success_rates[17] = 55;
            need_preventions[17] = 4;

            success_rates[18] = 50;
            need_preventions[18] = 6;

            success_rates[19] = 50;
            need_preventions[19] = 6;

            success_rates[20] = 45;
            need_preventions[20] = 8;

            success_rates[21] = 40;
            need_preventions[21] = 10;

            success_rates[22] = 40;
            need_preventions[22] = 10;

            success_rates[23] = 40;
            need_preventions[23] = 22;

            success_rates[24] = 40;
            need_preventions[24] = 23;

            success_rates[25] = 35;
            need_preventions[25] = 23;

            success_rates[26] = 50;

            success_rates[27] = 40;

            success_rates[28] = 15;
            need_preventions[28] = 40;
        }

        else if (difficult_manager.difficult == "normal")
        {
            success_rates[0] = 100;
            need_preventions[0] = 1;
            special_sword_prices[0] = 1;

            success_rates[1] = 99;
            need_preventions[1] = 1;
            special_sword_prices[1] = 2;

            success_rates[2] = 95;
            need_preventions[2] = 1;
            special_sword_prices[2] = 15;

            success_rates[3] = 94;
            need_preventions[3] = 1;
            special_sword_prices[3] = 1;

            success_rates[4] = 92;
            need_preventions[4] = 1;
            special_sword_prices[4] = 20;

            success_rates[5] = 88;
            need_preventions[5] = 1;
            special_sword_prices[5] = 7;

            success_rates[6] = 85;
            need_preventions[6] = 1;

            success_rates[7] = 80;
            need_preventions[7] = 1;

            success_rates[8] = 72;
            need_preventions[8] = 1;

            success_rates[9] = 70;
            need_preventions[9] = 1;

            success_rates[10] = 68;
            need_preventions[10] = 3;

            success_rates[11] = 65;
            need_preventions[11] = 3;

            success_rates[12] = 60;
            need_preventions[12] = 3;

            success_rates[13] = 58;
            need_preventions[13] = 3;

            success_rates[14] = 55;
            need_preventions[14] = 3;

            success_rates[15] = 50;
            need_preventions[15] = 5;

            success_rates[16] = 48;
            need_preventions[16] = 6;

            success_rates[17] = 45;
            need_preventions[17] = 6;

            success_rates[18] = 42;
            need_preventions[18] = 9;

            success_rates[19] = 38;
            need_preventions[19] = 9;

            success_rates[20] = 35;
            need_preventions[20] = 12;

            success_rates[21] = 32;
            need_preventions[21] = 15;

            success_rates[22] = 32;
            need_preventions[22] = 15;

            success_rates[23] = 32;
            need_preventions[23] = 18;

            success_rates[24] = 32;
            need_preventions[24] = 20;

            success_rates[25] = 25;
            need_preventions[25] = 20;

            success_rates[26] = 40;

            success_rates[27] = 32;

            success_rates[28] = 8;
            need_preventions[28] = 52;
        }

        else
        {
            success_rates[0] = 100;
            need_preventions[0] = 1;
            special_sword_prices[0] = 2;

            success_rates[1] = 98;
            need_preventions[1] = 1;
            special_sword_prices[1] = 3;

            success_rates[2] = 95;
            need_preventions[2] = 1;
            special_sword_prices[2] = 20;

            success_rates[3] = 93;
            need_preventions[3] = 1;
            special_sword_prices[3] = 2;

            success_rates[4] = 90;
            need_preventions[4] = 1;
            special_sword_prices[4] = 27;

            success_rates[5] = 86;
            need_preventions[5] = 1;
            special_sword_prices[5] = 10;

            success_rates[6] = 81;
            need_preventions[6] = 1;

            success_rates[7] = 75;
            need_preventions[7] = 1;

            success_rates[8] = 70;
            need_preventions[8] = 1;

            success_rates[9] = 66;
            need_preventions[9] = 1;

            success_rates[10] = 62;
            need_preventions[10] = 4;

            success_rates[11] = 61;
            need_preventions[11] = 4;

            success_rates[12] = 58;
            need_preventions[12] = 4;

            success_rates[13] = 50;
            need_preventions[13] = 4;

            success_rates[14] = 45;
            need_preventions[14] = 4;

            success_rates[15] = 40;
            need_preventions[15] = 6;

            success_rates[16] = 37;
            need_preventions[16] = 8;

            success_rates[17] = 35;
            need_preventions[17] = 8;

            success_rates[18] = 32;
            need_preventions[18] = 12;

            success_rates[19] = 30;
            need_preventions[19] = 12;

            success_rates[20] = 28;
            need_preventions[20] = 16;

            success_rates[21] = 25;
            need_preventions[21] = 20;

            success_rates[22] = 25;
            need_preventions[22] = 20;

            success_rates[23] = 25;
            need_preventions[23] = 26;

            success_rates[24] = 25;
            need_preventions[24] = 28;

            success_rates[25] = 20;
            need_preventions[25] = 28;

            success_rates[26] = 30;

            success_rates[27] = 25;

            success_rates[28] = 1;
            need_preventions[28] = 61;
        }

        sword_prices[0] = 300;
        sword_prices[1] = 300;
        sword_prices[2] = 500;
        sword_prices[3] = 500;
        sword_prices[4] = 1000;
        sword_prices[5] = 1500;
        sword_prices[6] = 2000;
        sword_prices[7] = 2000;
        sword_prices[8] = 3000;
        sword_prices[9] = 5000;
        sword_prices[10] = 10000;
        sword_prices[11] = 20000;
        sword_prices[12] = 35000;
        sword_prices[13] = 55000;
        sword_prices[14] = 100000;
        sword_prices[15] = 180000;
        sword_prices[16] = 300000;
        sword_prices[17] = 300000;
        sword_prices[18] = 500000;
        sword_prices[19] = 800000;
        sword_prices[20] = 1500000;
        sword_prices[26] = 5000000;
        sword_prices[28] = 0;

        sell_prices[0] = 0;
        sell_prices[1] = 150;
        sell_prices[2] = 400;
        sell_prices[3] = 600;
        sell_prices[4] = 800;
        sell_prices[5] = 1600;
        sell_prices[6] = 3500;
        sell_prices[7] = 6100;
        sell_prices[8] = 10000;
        sell_prices[9] = 20000;
        sell_prices[10] = 35000;
        sell_prices[11] = 160000;
        sell_prices[12] = 350000;
        sell_prices[13] = 1000000;
        sell_prices[14] = 3000000;
        sell_prices[15] = 7500000;
        sell_prices[16] = 10000000;
        sell_prices[17] = 20000000;
        sell_prices[18] = 30000000;
        sell_prices[19] = 50000000;
        sell_prices[20] = 70000000;
        sell_prices[21] = 100000000;
        sell_prices[22] = 160000000;
        sell_prices[23] = 200000000;
        sell_prices[24] = 300000000;
        sell_prices[25] = 400000000;
        sell_prices[26] = 600000000;
        sell_prices[27] = 800000000;
        sell_prices[28] = 0;
    }
}