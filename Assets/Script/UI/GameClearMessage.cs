using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameClearMessage : MonoBehaviour
{
    //�R�����g
    [SerializeField] private TextMeshProUGUI text;
    //�����N+�X�R�A
    private int Score;
    [SerializeField] private TextMeshProUGUI Scoretext;
    [SerializeField] private Image _rankImage;
    [SerializeField] Sprite spriteS;
    [SerializeField] Sprite spriteA;
    [SerializeField] Sprite spriteB;
    [SerializeField] Sprite spriteC;

    private int randomMessage;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void updateMessage(int cleartime, float clearLife)
    {
        Score = cleartime * (int)clearLife;
        Scoretext.text = Score.ToString();
        if (Score > 700)
        {
            _rankImage.sprite = spriteS;
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text = "�����������ʂ�̐��������ꂻ����";
                    break;
                case 2:
                    text.text = "���āA�ѐH���ĐQ�邩";
                    break;
            }
        }
        else if (Score > 500)
        {
            _rankImage.sprite = spriteA;
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text = "�|���c���܂������c";
                    break;
                case 2:
                    text.text = "�Ƃɂ����Q��I\n�����̉��撣��I";
                    break;
            }
        }
        else if (Score > 300)
        {
            _rankImage.sprite = spriteB;
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text = "����������ꂽ�����c";
                    break;
                case 2:
                    text.text = "���������͗D�G�Ȏ�i�ł�";
                    break;
            }
        }
        else
        {
            _rankImage.sprite = spriteC;
            randomMessage = Random.Range(1, 3);
            switch (randomMessage)
            {
                case 1:
                    text.text = "��荇���������z���Ǝ҂ɘA�����c";
                    break;
                case 2:
                    text.text = "��s���^���Ác";
                    break;
            }
        }
    }
}
