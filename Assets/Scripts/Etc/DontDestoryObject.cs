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

    // DifficultManager ������Ʈ�� �ı����� �ʰ� ��
    private void DifficultManagerDontDestroyOnLoad()
    {
        // var : �ش� ������ �Ҵ�Ǵ� ���� ���� �ڷ����� ������
        // �ش� ��ũ��Ʈ�� ������ �ִ� ��� ���� ������Ʈ�� ã��
        var objs = GameObject.FindGameObjectsWithTag("DifficultManager");

        // ã�� ��� ������Ʈ�� 1������ �� �ı����� �ʴ� ������Ʈ�� �����ϰ�
        if (objs.Length == 1) { DontDestroyOnLoad(objs[0]); }

        // 2�� �̻��� ���� �ش� ������Ʈ�� �ı��ؼ� �ߺ��� ����
        else { for (int index = 1; index < objs.Length; index++) { Destroy(objs[index]); } }
    }

    // InfoManager ������Ʈ�� �ı����� �ʰ� ��
    private void InfoManagerDontDestroyOnLoad()
    {
        var objs = GameObject.FindGameObjectsWithTag("InfoManager");
        if (objs.Length == 1) { DontDestroyOnLoad(objs[0]); }
        else { for (int index = 1; index < objs.Length; index++) { Destroy(objs[index]); } }
    }

    // SoundManager ������Ʈ�� �ı����� �ʰ� ��
    private void SoundManagerDontDestroyOnLoad()
    {
        var objs = GameObject.FindGameObjectsWithTag("SoundManager");
        if (objs.Length == 1) { DontDestroyOnLoad(objs[0]); }
        else { for (int index = 1; index < objs.Length; index++) { Destroy(objs[index]); } }
    }
}
