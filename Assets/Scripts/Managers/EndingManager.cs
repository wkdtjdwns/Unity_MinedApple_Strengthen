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

        game_text.text = string.Format("난이도 : {0}\n\n마플 키 : {1}cm\n\n사용한 금액 : {2:#,###}원\n사용한 방지권 : {3:#,###}개\n사용한 특별 재화 : {4:#,###}개\n\n획득한 금액 : {5:#,###}원\n획득한 방지권 : {6:#,###}개\n획득한 특별 재화 : {7:#,###}개", info_manager.difficult, info_manager.minedapple_height, info_manager.use_money, info_manager.use_prevention_num, info_manager.use_special_item_num, info_manager.get_money, info_manager.get_prevention_num, info_manager.get_special_item_num);

        success_and_fail_text.text = string.Format("강화 성공 횟수 : {0:#,###}번\n강화 실패 횟수 : {1:#,###}번", info_manager.success_num, info_manager.fail_num);

        Invoke("TheEnd", 45f);
    }

    private void TheEnd()
    {
        anim.SetBool("is_ending", false);

        // 유니티에서만 나가짐 (빌드를 할 때에는 오류를 일으키게 됨 | UnityEditor 네임스페이스를 사용할 수 없기 때문)
        /*UnityEditor.EditorApplication.isPlaying = false;*/

        // 유니티가 아닌 모든 곳에서 나가짐
        Application.Quit();
    }
}
