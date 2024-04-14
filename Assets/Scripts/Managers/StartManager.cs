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

    // ������ �����ϸ� ������ StartScene���� �����ϰ� ��
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad() { if (SceneManager.GetActiveScene().name.CompareTo("StartScene") != 0) { SceneManager.LoadScene("StartScene"); } }

    private void Awake()
    {
        start_anim = game_start_text.GetComponent<Animator>();

        difficult_manager_obj = GameObject.Find("DifficultManager").gameObject;
        difficult_manager = difficult_manager_obj.GetComponent<DifficultManager>();

        // �ִϸ��̼� ����
        start_anim.SetBool("is_start", true);

        SoundManager.instance.PlayBgm("start");
    }

    // ��ŸƮ ���� �ƴ� �ٸ� ������ �Ѿ�� �ִϸ��̼� ���� (�ٸ� ������ �Ѿ�� ��� ������Ʈ�� �ı��ϴ� ���� �̿���)
    private void OnDisable() { start_anim.SetBool("is_start", false); }

    private void Update()
    {
        // ���콺 ��Ŭ�� �Ǵ� Esc�� �ƴ� �ٸ� ��� Ű�� ������ �� ���̵� ���� â�� �߰� ��
        if (!Input.GetMouseButtonDown(0) && Input.anyKeyDown) { SoundManager.instance.PlaySound("button"); set_difficult_group.SetActive(true); }

        // Esc�� ������ ���̵� ���� â�� ���� ��
        if (Input.GetKeyDown(KeyCode.Escape)) { SoundManager.instance.PlaySound("button"); set_difficult_group.SetActive(false); }
    }

    public void SetDifficult(string type)
    {
        // ���̵��� �����ϰ�
        difficult_manager.difficult = type;

        // ���̵� ���� â�� ����
        set_difficult_group.SetActive(false);

        // Bgm�� �ٲ� ��
        SoundManager.instance.PlayBgm("game");

        // GameScene���� �̵���
        SceneManager.LoadScene("GameScene");
    }
}
