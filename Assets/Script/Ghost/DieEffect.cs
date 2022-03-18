using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    Transform _transforom;
    EnemyStatus _status;
    SpriteRenderer sprite;
    private bool once = true;

    //ColorChange‚É“n‚µ‚ÄŽ~‚ß‚é—p
    public bool StopChange = false;

    // Start is called before the first frame update
    void Start()
    {
        _transforom = Enemy.GetComponent<Transform>();
        _status = Enemy.GetComponent<EnemyStatus>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_status.IsDie)
        {
            if (once == true)
            {
                once = false;
                StopChange = true;
                StartCoroutine(Transparent());
            }
        }
    }

    public IEnumerator Transparent()
    {
        //‚ä‚Á‚­‚è“§–¾‰»‚·‚é
        for (int i = 0; i < 122; i++)
        {
            if (sprite.material.color.a <= 0f) break;
            sprite.material.color = sprite.material.color - new Color32(0, 0, 0, 2);
            //_transforom.transform.localPosition = _transforom.transform.localPosition - new Vector3(0, 0.3f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        sprite.material.color = new Color32(255, 255, 255, 0);
        yield break;
    }
}
