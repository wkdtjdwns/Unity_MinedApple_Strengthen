using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    /* 아이템 목록
     * 시작의 증표
     * 과학 연료
     * 예산 초과
     * 핸섬가이의 양심
     * 핸섬가이의 정성
     * 전설의 양산형 강철
     * 까치발
     * 걸죽바
     * 소금 */
    [SerializeField]
    private Text[] item_texts;

    /* 검 목록
     * 핫초코 검
     * 강철의 레진 검
     * 빨간 열쇠의 검
     * 4티어 플랫폼 검
     * 170.0이 되기 위한 검
     * 용광로 + 수소 검
     * 씨근풀 검
     * 4의 기운을 받은 검
     * 꾸몽 입술 검
     * 봉인된 꾸몽의 검 */
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
        // 난이도에 따라서 난이도를 알려주는 텍스트를 바꿈
        if (difficult_manager.difficult == "easy") { show_difficult_texts[0].text = "겁쟁이"; }
        else if (difficult_manager.difficult == "normal") { show_difficult_texts[1].text = "보통"; }
        else { show_difficult_texts[2].text = "헉!"; }
    }

    private void Update()
    {
        prevention_remaining_text.text = string.Format("방지권 : {0}개", game_manager.prevention_remaining);

        ItemTextUpdate();
        SaveSwordTextUpdate();
        NextIngredientTextUpdate();
        IngredientRemainingTextUpdate();
        GoodsPriceTextUpdate();
    }

    private void ItemTextUpdate()
    {
        item_texts[0].text = string.Format("{0} : {1}개", item_manager.fail_item_names[0], item_manager.fail_item_remainings[0]);
        item_texts[1].text = string.Format("{0} : {1}개", item_manager.fail_item_names[1], item_manager.fail_item_remainings[1]);
        item_texts[2].text = string.Format("{0} : {1}개", item_manager.fail_item_names[2], item_manager.fail_item_remainings[2]);
        item_texts[3].text = string.Format("{0} : {1}개", item_manager.fail_item_names[3], item_manager.fail_item_remainings[3]);
        item_texts[4].text = string.Format("{0} : {1}개", item_manager.fail_item_names[4], item_manager.fail_item_remainings[4]);
        item_texts[5].text = string.Format("{0} : {1}개", item_manager.fail_item_names[5], item_manager.fail_item_remainings[5]);
        item_texts[6].text = string.Format("{0} : {1}개", item_manager.fail_item_names[6], item_manager.fail_item_remainings[6]);
        item_texts[7].text = string.Format("{0} : {1}개", item_manager.fail_item_names[7], item_manager.fail_item_remainings[7]);
        item_texts[8].text = string.Format("{0} : {1}개", item_manager.fail_item_names[8], item_manager.fail_item_remainings[8]);
    }

    private void SaveSwordTextUpdate()
    {
        save_sword_texts[0].text = string.Format("{0} : {1}개", sword.sword_names[19], item_manager.save_sword_remainings[0]);
        save_sword_texts[1].text = string.Format("{0} : {1}개", sword.sword_names[20], item_manager.save_sword_remainings[1]);
        save_sword_texts[2].text = string.Format("{0} : {1}개", sword.sword_names[21], item_manager.save_sword_remainings[2]);
        save_sword_texts[3].text = string.Format("{0} : {1}개", sword.sword_names[22], item_manager.save_sword_remainings[3]);
        save_sword_texts[4].text = string.Format("{0} : {1}개", sword.sword_names[23], item_manager.save_sword_remainings[4]);
        save_sword_texts[5].text = string.Format("{0} : {1}개", sword.sword_names[24], item_manager.save_sword_remainings[5]);
        save_sword_texts[6].text = string.Format("{0} : {1}개", sword.sword_names[25], item_manager.save_sword_remainings[6]);
        save_sword_texts[7].text = string.Format("{0} : {1}개", sword.sword_names[26], item_manager.save_sword_remainings[7]);
        save_sword_texts[8].text = string.Format("{0} : {1}개", sword.sword_names[27], item_manager.save_sword_remainings[8]);
        save_sword_texts[9].text = string.Format("{0} : {1}개", sword.sword_names[28], item_manager.save_sword_remainings[9]);
    }

    // 특별한 재료가 사용되는 검 강화 전 단계에 나오는 텍스트
    private void NextIngredientTextUpdate()
    {
        // 각 검에 따라서 알려줘야할 텍스트를 다르게 함
        switch (sword.sword_index)
        {
            // 특별한 재료를 사용하지 않기 때문에 필요 없음
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

            /* Rich Text란?
             * 사용 이유 : "~~~ <color=red>강조</color> ~~~"와 같이 임의의 텍스트에게만 효과를 주기 위해서 "Rich Text" 사용
             * 사용 방법 : 존재하는 태그 파라미터를 사용함 예) "333 <color=red>중요한 텍스트</color> 333" 등등
             * 태그 파라미터
             *  <size=n>텍스트</size> -> 특정 단어 크기조절
             *  <b>텍스트</b> -> 특정 단어 굵게 표시
             *  <i>텍스트</i> -> 특정 단어 기울이기
             *  <color=#헥스코드>텍스트</color> OR <color=red>텍스트</color> -> 특정 단어 색상변경

             * 물론 겹칠 수 있음
             *  <size=33><b><i><color=red>텍스트</color></i></b></size> */

            case 20: tip_text.text = string.Format("Tip\n다음 강화재료는\n<color=#FF5C5C>{0} {1}개</color>입니다", sword.sword_names[19], sword.special_sword_prices[0]); break;
            case 21: tip_text.text = string.Format("Tip\n다음 강화재료는\n<color=#FF5C5C>{0} {1}개</color>입니다", sword.sword_names[20], sword.special_sword_prices[1]); break;
            case 22: tip_text.text = string.Format("Tip\n다음 강화재료는\n<color=#FF5C5C>{0} {1}개</color>입니다", item_manager.fail_item_names[6], sword.special_sword_prices[2]); break;
            case 23: tip_text.text = string.Format("Tip\n다음 강화재료는\n<color=#FF5C5C>{0} {1}개</color>입니다", sword.sword_names[22], sword.special_sword_prices[3]); break;
            case 24: tip_text.text = string.Format("Tip\n다음 강화재료는\n<color=#FF5C5C>{0} {1}개</color>입니다", item_manager.fail_item_names[7], sword.special_sword_prices[4]); break;
            case 26: tip_text.text = string.Format("Tip\n다음 강화재료는\n<color=#FF5C5C>{0} {1}개</color>입니다", item_manager.fail_item_names[8], sword.special_sword_prices[5]); break;
            case 29: tip_text.text = string.Format("Tip\n축하드립니다!\n<color=#FF5C5C>{0}번</color>의 시도 끝에\n모든 검을 강화하셨습니다!\n강화 버튼을 눌러 엔딩을 감상하세요!", info_manager.success_num + info_manager.fail_num); break;
        }
    }

    // 현재 가지고 있는 강화 재료를 보여주는 텍스트 업데이트
    private void IngredientRemainingTextUpdate()
    {
        // 무조건 돈이 필요한 검 인덱스 처리
        if (sword.sword_index < 21) { ingredient_text.text = string.Format("돈 : {0:#,###}원", game_manager.money); }

        // 다른 재료가 필요 할 수도 있는 검 인덱스 처리
        else
        {
            switch (sword.sword_index)
            {
                // 돈이 필요한 검의 경우에는 돈을 보여주고
                case 26:
                case 28:
                    // {0:#,###} -> 천 단위 구분 (,)
                    ingredient_text.text = string.Format("돈 : {0:#,###}원", game_manager.money); break;

                // 다른 재료가 필요한 경우에는 각각 다른 재료를 보여줌
                case 21:
                    ingredient_text.text = string.Format("보유 {0} : {1}개", sword.sword_names[19], item_manager.save_sword_remainings[0]); break;

                case 22:
                    ingredient_text.text = string.Format("보유 {0} : {1}개", sword.sword_names[20], item_manager.save_sword_remainings[1]); break;

                case 23:
                    ingredient_text.text = string.Format("보유 {0} : {1}개", item_manager.fail_item_names[6], item_manager.fail_item_remainings[6]); break;

                case 24:
                    ingredient_text.text = string.Format("보유 {0} : {1}개", sword.sword_names[22], item_manager.save_sword_remainings[4]); break;

                case 25:
                    ingredient_text.text = string.Format("보유 {0} : {1}개", item_manager.fail_item_names[7], item_manager.fail_item_remainings[7]); break;

                case 27:
                    ingredient_text.text = string.Format("보유 {0} : {1}개", item_manager.fail_item_names[8], item_manager.fail_item_remainings[8]); break;
            }
        }
    }

    private void GoodsPriceTextUpdate()
    {
        goods_price_texts[0].text = string.Format("{0:#,###}원", goods.prices[0]);
        goods_price_texts[1].text = string.Format("{0:#,###}원", goods.prices[1]);
        goods_price_texts[2].text = string.Format("{0:#,###}원", goods.prices[2]);
        goods_price_texts[3].text = string.Format("{0:#,###}원", goods.prices[3]);
        goods_price_texts[4].text = string.Format("{0:#,###}원", goods.prices[4]);
        goods_price_texts[5].text = string.Format("{0:#,###}원", goods.prices[5]);
    }
}
