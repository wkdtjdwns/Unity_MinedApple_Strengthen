using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField]
    private Text game_start_text;
    private Animator start_anim;

    [SerializeField]
    private GameObject set_difficult_group;

    private GameObject difficult_manager_obj;
    private DifficultManager difficult_manager;

    // 게임을 시작하면 무조건 StartScene에서 시작하게 함
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad() { if (SceneManager.GetActiveScene().name.CompareTo("StartScene") != 0) { SceneManager.LoadScene("StartScene"); } }

    private void Awake()
    {
        start_anim = game_start_text.GetComponent<Animator>();

        difficult_manager_obj = GameObject.Find("DifficultManager").gameObject;
        difficult_manager = difficult_manager_obj.GetComponent<DifficultManager>();

        // 애니매이션 실행
        start_anim.SetBool("is_start", true);

        SoundManager.instance.PlayBgm("start");
    }

    // 스타트 씬이 아닌 다른 씬으로 넘어가면 애니매이션 종료 (다른 씬으로 넘어가면 모든 오브젝트를 파괴하는 것을 이용함)
    private void OnDisable() { start_anim.SetBool("is_start", false); }

    private void Update()
    {
        // 마우스 좌클릭 또는 Esc가 아닌 다른 모든 키를 눌렀을 때 난이도 결정 창이 뜨게 함
        if (!Input.GetMouseButtonDown(0) && Input.anyKeyDown) { SoundManager.instance.PlaySound("button"); set_difficult_group.SetActive(true); }

        // Esc를 누르면 난이도 결정 창을 끄게 함
        if (Input.GetKeyDown(KeyCode.Escape)) { SoundManager.instance.PlaySound("button"); set_difficult_group.SetActive(false); }
    }

    public void SetDifficult(string type)
    {
        // 난이도를 결정하고
        difficult_manager.difficult = type;

        // 난이도 결정 창을 끄고
        set_difficult_group.SetActive(false);

        // Bgm을 바꾼 뒤
        SoundManager.instance.PlayBgm("game");

        // GameScene으로 이동함
        SceneManager.LoadScene("GameScene");
    }
}
