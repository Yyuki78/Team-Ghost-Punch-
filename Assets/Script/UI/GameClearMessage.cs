using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameClearMessage : MonoBehaviour
{
    //コメント
    [SerializeField] private TextMeshProUGUI text;
    //ランク+スコア
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
                    text.text = "明日もいつも通りの生活が送れそうだ";
                    break;
                case 2:
                    text.text = "さて、飯食って寝るか";
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
                    text.text = "掃除…しますかぁ…";
                    break;
                case 2:
                    text.text = "とにかく寝る！\n明日の俺頑張れ！";
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
                    text.text = "腹減った疲れた眠い…";
                    break;
                case 2:
                    text.text = "現実逃避は優秀な手段です";
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
                    text.text = "取り合えず引っ越し業者に連絡か…";
                    break;
                case 2:
                    text.text = "先行き真っ暗…";
                    break;
            }
        }
    }
}
