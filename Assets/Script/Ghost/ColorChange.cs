using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer sprite;
    private bool once = false;
    private bool once2 = false;

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
        if (_player.IsDead == false && _player.IsChargeMode == false && once == false && once2 == false)
        {
            once2 = true;
            StopAllCoroutines();
            StartCoroutine("Transparent");
        }
        if (once == true)
        {
            if (_player.IsChargeMode == true)
            {
                StopAllCoroutines();
                StartCoroutine("Transparent3");
                once = false;
            }
            if (_player.IsDead == true)
            {
                StopAllCoroutines();
                StartCoroutine("Transparent2");
                once = false;
            }
        }
        if (_thunder.canSee == true)
        {
            canSee();
        }
        /*
        if (_player.IsChargeMode == true && once == true && _player.IsDead == false)
        {
            StopCoroutine("Trasparent2");
            StartCoroutine("Transparent2");
            once = false;
        }
        if (_player.IsDead == true && once == true && _player.IsChargeMode == false)
        {
            StopCoroutine("Trasparent2");
            StartCoroutine("Transparent2");
            once = false;
        }
        if (_thunder.canSee == true && _player.IsDead == false && _player.IsChargeMode == false)
        {
            canSee();
        }*/
    }

    public IEnumerator Transparent()
    {
        //‚ä‚Á‚­‚è“§–¾‰»‚·‚é
        for (int i = 0; i < 255; i++)
        {
            if(sprite.material.color.a <= 0f) break;
            sprite.material.color = sprite.material.color - new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        sprite.material.color = new Color32(255, 255, 255, 0);
        once = true;
        once2 = false;
        yield break;
    }

    public IEnumerator Transparent2()
    {
        //‚ä‚Á‚­‚èŒ©‚¦‚é‚æ‚¤‚É‚È‚é
        for (int i = 0; i < 255; i++)
        {
            if (sprite.material.color.a >= 1f) break;
            sprite.material.color = sprite.material.color + new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        sprite.material.color = new Color32(255, 255, 255, 255);
        yield break;
    }

    public IEnumerator Transparent3()
    {
        //‘¬‚­Œ©‚¦‚é‚æ‚¤‚É‚È‚é
        for (int i = 0; i < 51; i++)
        {
            if (sprite.material.color.a >= 1f) break;
            sprite.material.color = sprite.material.color + new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(0.01f);
        }
        sprite.material.color = new Color32(255, 255, 255, 255);
        yield break;
    }

    public void canSee()
    {
        //if (once == false) return;
        //once = false;
        StopCoroutine("Transparent");
        StopCoroutine("Transparent2");
        StopCoroutine("Transparent3");
        sprite.material.color = new Color32(255, 255, 255, 255);
        StartCoroutine("Transparent");
    }
}