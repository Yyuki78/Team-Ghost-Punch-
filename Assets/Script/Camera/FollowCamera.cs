using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    float m_height = 10.0f;

    [SerializeField]
    Transform m_target = null;

    // Start is called before the first frame update
    void Start()
    {
        if(m_target == null) {
            var player = GameObject.FindGameObjectWithTag("Player");
            m_target = player?.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_target) {
            transform.position = m_target.position + new Vector3(0, m_height, 0);
        }
        
    }
}
