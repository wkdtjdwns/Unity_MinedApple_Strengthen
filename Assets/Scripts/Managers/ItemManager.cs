using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    /* ������ ���
     * ������ ��ǥ (8��)
     * ���� ���� (9��)
     * ���� �ʰ� (10��)
     * �ڼ������� ��� (11��)
     * �ڼ������� ���� (12��)
     * ������ ����� ö (13��)
     * ��ġ�� (14��)
     * ���׹� (15��)
     * �ұ� (16��) */
    public string[] fail_item_names;
    public int[] fail_item_remainings;
    public Sprite[] fail_item_sprites;

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
     * �ٸ� ������ �� */
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
