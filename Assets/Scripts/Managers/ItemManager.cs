using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    /* 아이템 목록
     * 시작의 증표 (8강)
     * 과학 연료 (9강)
     * 예산 초과 (10강)
     * 핸섬가이의 양심 (11강)
     * 핸섬가이의 정성 (12강)
     * 전설의 양산형 철 (13강)
     * 까치발 (14강)
     * 걸죽바 (15강)
     * 소금 (16강) */
    public string[] fail_item_names;
    public int[] fail_item_remainings;
    public Sprite[] fail_item_sprites;

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
     * 꾸몽 봉인의 검 */
    public int[] save_sword_remainings;

    public int ran_fail_item_num;

    [SerializeField]
    private GameObject sword_obj;
    private Sword sword;

    private void Awake()
    {
        fail_item_remainings = new int[9];
        save_sword_remainings = new int[10];

        sword = sword_obj.GetComponent<Sword>();
    }
}
