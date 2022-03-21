using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFollowCamera : MonoBehaviour
{
    [SerializeField]
    float m_height = 10.0f;

    [SerializeField] GameObject Player;
    private Player _player;

    //プレイヤーの体の位置
    [SerializeField]
    Transform m_target = null;
    //プレイヤーの幽体の位置
    [SerializeField]
    Transform m_target2 = null;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        if (m_target == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            m_target = player?.transform;
        }
        if (m_target2 == null)
        {
            var player = GameObject.FindGameObjectWithTag("PlayerGhost");
            m_target2 = player?.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsDead == false)
        {
            if (m_target)
            {
                transform.position = m_target.position + new Vector3(0, m_height, 3);
            }
        }
        else
        {
            if (m_target2)
            {
                transform.position = m_target2.position + new Vector3(0, m_height, 3);
            }
        }


    }
}
