using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float m_moveSpeed = 1.0f;


    Rigidbody m_rigidbody = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        updateMove();
    }

    void updateMove() {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var input = new Vector3(x, 0, y);
        var velocity = input * m_moveSpeed;

        m_rigidbody.velocity = velocity;
    }
}
