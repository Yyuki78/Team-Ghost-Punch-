using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPDisplay : MonoBehaviour
{
    Image EnemyHPImage;

    [SerializeField]
    GameObject DeadImage;

    [SerializeField]
    GameObject Enemy;
    EnemyStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHPImage = GetComponent<Image>();
        _status = Enemy.GetComponent<EnemyStatus>();
        DeadImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_status.Life)
        {
            case 6:
                EnemyHPImage.fillAmount = 1f;
                break;
            case 5:
                EnemyHPImage.fillAmount = 5 / 6f;
                break;
            case 4:
                EnemyHPImage.fillAmount = 4 / 6f;
                break;
            case 3:
                EnemyHPImage.fillAmount = 3 / 6f;
                break;
            case 2:
                EnemyHPImage.fillAmount = 2 / 6f;
                break;
            case 1:
                EnemyHPImage.fillAmount = 1 / 6f;
                break;
            case 0:
                EnemyHPImage.fillAmount = 0f;
                DeadImage.SetActive(true);
                break;
            default:
                break;
        }
    }
}
