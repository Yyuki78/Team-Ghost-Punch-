using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSlider : MonoBehaviour
{
    Slider hpSlider;

    [SerializeField] GameObject Player;
    Player _player;

    // Use this for initialization
    void Start()
    {
        hpSlider = GetComponent<Slider>();
        _player = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = _player.m_chargePower;
    }
}
