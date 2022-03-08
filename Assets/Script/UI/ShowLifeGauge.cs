using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLifeGauge : MonoBehaviour
{
    // MobStatusÇÃDamageÇ≈é¿çsÇ≥ÇÍÇÈ
    //äÓñ{ÇÕìßñæèÛë‘
    Image _image;

    public bool IsSee = false;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine("Transparent");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSee == true)
        {
            IsSee = false;
            SeeGauge();
        }
    }

    private void SeeGauge()
    {
        _image.color = new Color32(255, 255, 255, 255);
        StopCoroutine("Transparent");
        StartCoroutine("Transparent");
    }

    private IEnumerator Transparent()
    {
        for (int i = 0; i < 255; i++)
        {
            if (_image.color.a <= 0) yield break;
            _image.color = _image.color - new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }
}
