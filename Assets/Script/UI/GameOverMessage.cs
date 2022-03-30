using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private int randomMessage;

    [SerializeField] GameObject EnemyManager;
    private EnemyManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        _manager = EnemyManager.GetComponent<EnemyManager>();
        randomMessage = Random.Range(1, 5);
        switch (randomMessage)
        {
            case 1:
                text.text = "��������z�[�����X�c";
                break;
            case 2:
                text.text = "�Ǝ҂ɘA�����邩�c";
                break;
            case 3:
                text.text = "���̂��߂̎��ƁH";
                break;
            case 4:
                text.text = "�ی������ĂȂ��c";
                break;
        }
        if (Timer.TimeOut)
        {
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text += "\n0���A�Q���f�t�H�ł�";
                    break;
                case 2:
                    text.text += "\n���̉�������c";
                    break;
            }
        }else if (_manager.IsDeadGhostNone)
        {
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text += "\n�^�C�g����ʂ��q���g";
                    break;
                case 2:
                    text.text += "\n����ȓ������邳�c";
                    break;
            }
        }else if (_manager.IsDeadGhostFive)
        {
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text += "\n���i����̂݁c";
                    break;
                case 2:
                    text.text += "\n( �L�́M )";
                    break;
            }
        }
    }
}
