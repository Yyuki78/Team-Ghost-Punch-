using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    [SerializeField] GameObject Mirror1;
    [SerializeField] GameObject Mirror2;
    [SerializeField] GameObject Mirror3;
    [SerializeField] GameObject Mirror4;
    [SerializeField] GameObject Mirror5;
    PlayerCharge _charge1;
    PlayerCharge _charge2;
    PlayerCharge _charge3;
    PlayerCharge _charge4;
    PlayerCharge _charge5;

    private float Timer1 = 8f;
    private float Timer2 = 8f;
    private float Timer3 = 8f;
    private float Timer4 = 8f;
    private float Timer5 = 8f;

    private float NowTimer1;
    private float NowTimer2;
    private float NowTimer3;
    private float NowTimer4;
    private float NowTimer5;

    // Start is called before the first frame update
    void Start()
    {
        _charge1 = Mirror1.GetComponent<PlayerCharge>();
        _charge2 = Mirror2.GetComponent<PlayerCharge>();
        _charge3 = Mirror3.GetComponent<PlayerCharge>();
        _charge4 = Mirror4.GetComponent<PlayerCharge>();
        _charge5 = Mirror5.GetComponent<PlayerCharge>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_charge1.ChargeTime)
        {
            Timer1 = Timer1 -= Time.deltaTime;
        }

        if (_charge2.ChargeTime)
        {
            Timer2 = Timer2 -= Time.deltaTime;
        }

        if (_charge3.ChargeTime)
        {
            Timer3 = Timer3 -= Time.deltaTime;
        }

        if (_charge4.ChargeTime)
        {
            Timer4 = Timer4 -= Time.deltaTime;
        }

        if (_charge5.ChargeTime)
        {
            Timer5 = Timer5 -= Time.deltaTime;
        }

        if (Timer1 <= 0)
        {
            _charge1.FinishArea = true;
        }
        if (Timer2 <= 0)
        {
            _charge2.FinishArea = true;
        }
        if (Timer3 <= 0)
        {
            _charge3.FinishArea = true;
        }
        if (Timer4 <= 0)
        {
            _charge4.FinishArea = true;
        }
        if (Timer5 <= 0)
        {
            _charge5.FinishArea = true;
        }
    }
}
