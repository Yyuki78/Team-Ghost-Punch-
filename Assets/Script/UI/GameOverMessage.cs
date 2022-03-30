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
                text.text = "今日からホームレス…";
                break;
            case 2:
                text.text = "業者に連絡するか…";
                break;
            case 3:
                text.text = "このための実家？";
                break;
            case 4:
                text.text = "保険入ってない…";
                break;
        }
        if (Timer.TimeOut)
        {
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text += "\n0時就寝がデフォです";
                    break;
                case 2:
                    text.text += "\n鐘の音がする…";
                    break;
            }
        }else if (_manager.IsDeadGhostNone)
        {
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text += "\nタイトル画面がヒント";
                    break;
                case 2:
                    text.text += "\nそんな日もあるさ…";
                    break;
            }
        }else if (_manager.IsDeadGhostFive)
        {
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text += "\n精進するのみ…";
                    break;
                case 2:
                    text.text += "\n( ´∀｀ )";
                    break;
            }
        }
    }
}
