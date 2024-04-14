using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField]
    private GameObject option;

    [SerializeField]
    private Slider bgm_slider;
    [SerializeField]
    private Slider sfx_slider;

    private bool is_option;

    private void Start()
    {
        bgm_slider.value = SoundManager.instance.bgm_volume;
        sfx_slider.value = SoundManager.instance.sfx_volume;

        is_option = false;

        // 각 슬라이더의 값이 바뀌었을 때 실행할 메소드를 설정함
        bgm_slider.onValueChanged.AddListener(ChangeBgmSound);
        sfx_slider.onValueChanged.AddListener(ChangeSfxSound);
    }

    private void Update() { if (Input.GetKeyDown(KeyCode.Escape)) { OptionOnOff(); } }

    // 옵션창을 끄고 켬
    public void OptionOnOff()
    {
        SoundManager.instance.PlaySound("button");

        is_option = !is_option;

        option.SetActive(is_option);

        Time.timeScale = is_option ? 0 : 1;
    }

    public void HearSound()
    {
        print("소리 듣기");
        SoundManager.instance.PlaySound("sell");
    }

    public void ExitGame()
    {
        SoundManager.instance.PlaySound("button");

        // 유니티에서만 나가짐 (빌드를 할 때에는 오류를 일으키게 됨 | UnityEditor 네임스페이스를 사용할 수 없기 때문)
        /*UnityEditor.EditorApplication.isPlaying = false;*/

        // 유니티가 아닌 모든 곳에서 나가짐
        Application.Quit();
    }

    // bgm과 sfx의 사운드 볼륨을 조절함
    private void ChangeBgmSound(float value) { SoundManager.instance.bgm_volume = value; }
    private void ChangeSfxSound(float value) { SoundManager.instance.sfx_volume = value; }
}
