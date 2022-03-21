using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickMouse : MonoBehaviour
{
    [SerializeField] GameObject MouseImage;
    [SerializeField] GameObject Player;
    Player _player;
    
    Image _image;

    private bool plus = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        _image = MouseImage.GetComponent<Image>();
        _image.color = _image.color - new Color32(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.m_chargePower >= 100)
        {
            MouseImage.SetActive(true);
            if (_image.color.a < 0.4)
            {
                plus = true;
            }
            if(_image.color.a > 1.0)
            {
                plus = false;
            }
            if (plus == false)
            {
                _image.color = _image.color - new Color32(0, 0, 0, 1);
            }
            else
            {
                _image.color = _image.color + new Color32(0, 0, 0, 1);
            }
        }
        else
        {
            MouseImage.SetActive(false);
        }
    }
}
