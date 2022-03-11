using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHP : MonoBehaviour
{
    //HP10個を受け取ってPlayerのHP表示をするストレス発散用脳死プログラム
    [SerializeField] GameObject HP1;
    [SerializeField] GameObject HP2;
    [SerializeField] GameObject HP3;
    [SerializeField] GameObject HP4;
    [SerializeField] GameObject HP5;
    [SerializeField] GameObject HP6;
    [SerializeField] GameObject HP7;
    [SerializeField] GameObject HP8;
    [SerializeField] GameObject HP9;
    [SerializeField] GameObject HP10;

    [SerializeField] GameObject Player;
    Player _player;

    // Start is called before the first frame update
    void Start()
    {
        HP1.SetActive(true);
        HP2.SetActive(true);
        HP3.SetActive(true);
        HP4.SetActive(true);
        HP5.SetActive(true);
        HP6.SetActive(true);
        HP7.SetActive(true);
        HP8.SetActive(true);
        HP9.SetActive(true);
        HP10.SetActive(true);

        _player = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_player._life)
        {
            case 9:
                HP10.SetActive(false);
                break;
            case 8:
                HP9.SetActive(false);
                break;
            case 7:
                HP8.SetActive(false);
                break;
            case 6:
                HP7.SetActive(false);
                break;
            case 5:
                HP6.SetActive(false);
                break;
            case 4:
                HP5.SetActive(false);
                break;
            case 3:
                HP4.SetActive(false);
                break;
            case 2:
                HP3.SetActive(false);
                break;
            case 1:
                HP2.SetActive(false);
                break;
            case 0:
                HP1.SetActive(false);
                break;
            default:
                break;
        }
    }
}
