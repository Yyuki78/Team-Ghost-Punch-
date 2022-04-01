using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeGaugeBlinking : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Player _player;

    private Image _image;

    private bool plus = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.m_chargePower == 100)
        {
            if (_image.color.a >= 1.0f)
            {
                plus = false;
            }
            if (_image.color.a <= 0.5f)
            {
                plus = true;
            }
            if (plus)
            {
                _image.color += new Color(0, 0, 0, 0.01f);
            }
            else
            {
                _image.color -= new Color(0, 0, 0, 0.01f);
            }
        }
    }
}
