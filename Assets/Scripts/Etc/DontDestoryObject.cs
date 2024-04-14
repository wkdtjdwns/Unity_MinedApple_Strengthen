using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryObject : MonoBehaviour
{
    private void Awake()
    {
        DifficultManagerDontDestroyOnLoad();
        InfoManagerDontDestroyOnLoad();
        SoundManagerDontDestroyOnLoad();
    }

    // DifficultManager 오브젝트를 파괴하지 않게 함
    private void DifficultManagerDontDestroyOnLoad()
    {
        // var : 해당 변수에 할당되는 값에 따라서 자료형을 설정함
        // 해당 스크립트를 가지고 있는 모든 게임 오브젝트를 찾고
        var objs = GameObject.FindGameObjectsWithTag("DifficultManager");

        // 찾은 모든 오브젝트가 1개뿐일 때 파괴하지 않는 오브젝트를 생성하고
        if (objs.Length == 1) { DontDestroyOnLoad(objs[0]); }

        // 2개 이상일 때는 해당 오브젝트를 파괴해서 중복을 없앰
        else { for (int index = 1; index < objs.Length; index++) { Destroy(objs[index]); } }
    }

    // InfoManager 오브젝트를 파괴하지 않게 함
    private void InfoManagerDontDestroyOnLoad()
    {
        var objs = GameObject.FindGameObjectsWithTag("InfoManager");
        if (objs.Length == 1) { DontDestroyOnLoad(objs[0]); }
        else { for (int index = 1; index < objs.Length; index++) { Destroy(objs[index]); } }
    }

    // SoundManager 오브젝트를 파괴하지 않게 함
    private void SoundManagerDontDestroyOnLoad()
    {
        var objs = GameObject.FindGameObjectsWithTag("SoundManager");
        if (objs.Length == 1) { DontDestroyOnLoad(objs[0]); }
        else { for (int index = 1; index < objs.Length; index++) { Destroy(objs[index]); } }
    }
}
