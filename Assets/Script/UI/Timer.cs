using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text text;
    float countTime = 0;
    int displaySecond = 0;
    int displayHour = 0;
    bool plus = false;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(text.color.a);
        // countTime‚ÉAƒQ[ƒ€‚ªŠJŽn‚µ‚Ä‚©‚ç‚Ì•b”‚ðŠi”[
        countTime += Time.deltaTime / 2;
        displaySecond = (int)countTime - (displayHour * 60);
        if (displaySecond >= 60)
        {
            displayHour++;
            displaySecond = 0;
        }
        if (displayHour < 6)
        {
            text.text = (18 + displayHour).ToString() + ":" + displaySecond.ToString("00");
        }
        else
        {
            text.text = (displayHour - 6).ToString("00") + ":" + displaySecond.ToString("00");

            if (text.color.a < 0.1f)
            {
                plus = true;
            }
            if (text.color.a > 0.8f)
            {
                plus = false;
            }
            if (plus == false)
            {
                text.color = text.color - new Color(0, 0, 0, 0.01f);
            }
            else
            {
                text.color = text.color + new Color(0, 0, 0, 0.01f);
            }
        }
    }
}
