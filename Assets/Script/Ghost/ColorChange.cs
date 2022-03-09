using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer sprite;
    private bool once = false;

    [SerializeField] GameObject Player;
    private Player _player;

    [SerializeField] GameObject StageEffect;
    private ThunderEffect _thunder;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.material.color = sprite.material.color - new Color32(0, 0, 0, 0);
        _player = Player.GetComponent<Player>();
        _thunder = StageEffect.GetComponent<ThunderEffect>();
    }
    void Update()
    {
        if (_player.IsDead == false && once == false)
        {
            once = true;
            StartCoroutine("Transparent");
            StopCoroutine("Trasparent2");

        }
        if (_player.IsChargeMode == true && once == true)
        {
            StopCoroutine("Trasparent2");
            StartCoroutine("Transparent2");
            once = false;
        }
        if (_player.IsDead == true && once == true)
        {
            StopCoroutine("Trasparent2");
            StartCoroutine("Transparent2");
            once = false;
        }
        if (_thunder.canSee == true)
        {
            canSee();
        }
    }

    public IEnumerator Transparent()
    {
        for (int i = 0; i < 255; i++)
        {
            if(sprite.material.color.a <= 0) yield break;
            sprite.material.color = sprite.material.color - new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    public IEnumerator Transparent2()
    {
        for (int i = 0; i < 255; i++)
        {
            sprite.material.color = sprite.material.color + new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        yield break;
    }

    public void canSee()
    {
        if (sprite.material.color.a <= 1)
        {
            sprite.material.color = new Color32(255, 255, 255, 255);
            if (_player.IsDead == false && _player.IsChargeMode == false)
            {
                StopCoroutine("Transparent");
                StartCoroutine("Transparent");
            }
        }
    }
}