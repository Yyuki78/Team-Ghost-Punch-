using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    float m_moveSpeed = 1.0f;
    [SerializeField]
    bool m_isChargeMode = false;
    // チャージ中か
    public bool IsChargeMode { set { m_isChargeMode = value; } get { return m_isChargeMode; } }



    Rigidbody m_rigidbody = null;
    Animator m_animator = null;
    SpriteRenderer m_spriteRenderer = null;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponentInChildren<Animator>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateMove();
        updateCharge();
    }

    void updateMove() {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var input = new Vector3(x, 0, y);
        var velocity = input * m_moveSpeed;

        m_rigidbody.velocity = velocity;
        if(m_spriteRenderer != null && Mathf.Abs(x) > 0.2f) {
            if(x > 0.0f) {
                m_spriteRenderer.flipX = true;
            } else {
                m_spriteRenderer.flipX = false;
            }
            m_animator.SetFloat("Walk", 1.0f);
        } else {
            m_animator.SetFloat("Walk", 0.0f);
        }
    }

    void updateCharge() {
        var isCharging = Input.GetButton("Charge");
        if(isCharging) {
            m_animator.SetBool("Charge", true);
        } else {
            m_animator.SetBool("Charge", false);
        }
    }
}
