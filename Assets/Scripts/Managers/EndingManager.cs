using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private Text game_text;
    [SerializeField]
    private Text success_and_fail_text;

    [SerializeField]
    private GameObject ending_obj;
    private Animator anim;

    private GameObject info_manager_obj;
    private InfoManager info_manager;

    private void Awake()
    {
        anim = ending_obj.GetComponent<Animator>();

        info_manager_obj = GameObject.Find("InfoManager").gameObject;
        info_manager = info_manager_obj.GetComponent<InfoManager>();
    }

    private void Start()
    {
        anim.SetBool("is_ending", true);

        game_text.text = string.Format("���̵� : {0}\n\n���� Ű : {1}cm\n\n����� �ݾ� : {2:#,###}��\n����� ������ : {3:#,###}��\n����� Ư�� ��ȭ : {4:#,###}��\n\nȹ���� �ݾ� : {5:#,###}��\nȹ���� ������ : {6:#,###}��\nȹ���� Ư�� ��ȭ : {7:#,###}��", info_manager.difficult, info_manager.minedapple_height, info_manager.use_money, info_manager.use_prevention_num, info_manager.use_special_item_num, info_manager.get_money, info_manager.get_prevention_num, info_manager.get_special_item_num);

        success_and_fail_text.text = string.Format("��ȭ ���� Ƚ�� : {0:#,###}��\n��ȭ ���� Ƚ�� : {1:#,###}��", info_manager.success_num, info_manager.fail_num);

        Invoke("TheEnd", 45f);
    }

    private void TheEnd()
    {
        anim.SetBool("is_ending", false);

        // ����Ƽ������ ������ (���带 �� ������ ������ ����Ű�� �� | UnityEditor ���ӽ����̽��� ����� �� ���� ����)
        /*UnityEditor.EditorApplication.isPlaying = false;*/

        // ����Ƽ�� �ƴ� ��� ������ ������
        Application.Quit();
    }
}
