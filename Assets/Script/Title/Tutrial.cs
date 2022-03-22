using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutrial : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private Player _player;
    private Transform _transform;
    private Vector3 pos;
    [SerializeField] GameObject Enemy1;

    [SerializeField] GameObject Ground;

    [SerializeField] GameObject Mirror;
    [SerializeField] Light _light;
    private bool once = false;

    [SerializeField] GameObject HumanMouse;
    [SerializeField] GameObject GhostMouse;
    Image _image;
    private bool plus = false;

    [SerializeField] GameObject DropObject;
    Rigidbody _rigid;
    [SerializeField] GameObject StartGameObject;
    [SerializeField] GameObject StartGameText;

    [SerializeField] GameObject WASD;
    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        _transform = Player.GetComponent<Transform>();
        pos = _transform.position;
        WASD.SetActive(false);
        StartCoroutine("ShowWASD");
        Enemy1.SetActive(false);
        Ground.SetActive(true);
        _rigid = DropObject.GetComponent<Rigidbody>();
        _rigid.useGravity = false;
        HumanMouse.SetActive(false);
        GhostMouse.SetActive(false);
        _image = GhostMouse.GetComponent<Image>();

        int GamePlayLog = PlayerPrefs.GetInt("GameStart");
        if (GamePlayLog == 1)
        {
            StartGameObject.SetActive(true);
            StartGameText.SetActive(true);
        }
        else
        {
            StartGameObject.SetActive(false);
            StartGameText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy1 == null)
        {
            Ground.SetActive(false);
            Mirror.SetActive(false);
            GhostMouse.SetActive(false);
            _rigid.useGravity = true;
        }
        else
        {
            if (_player.IsDead == true)
            {
                //EnemyÇåƒÇ—èoÇ∑
                Enemy1.SetActive(true);
                HumanMouse.SetActive(false);
                GhostMouse.SetActive(true);
                if (_image.color.a < 0.4)
                {
                    plus = true;
                }
                if (_image.color.a > 1.0)
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
                GhostMouse.SetActive(false);
            }
        }
        if (_transform.position != pos)
        {
            WASD.SetActive(false);
            if (once == false)
            {
                once = true;
                StartCoroutine(ShowLight());
            }
        }
        if (_player.m_chargePower >= 100)
        {
            _light.intensity = 5;
            StartCoroutine(ShowHumanMouse());
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) || Input.GetButton("Charge"))
            {
                HumanMouse.SetActive(false);
            }
        }
    }

    private IEnumerator ShowWASD()
    {
        yield return new WaitForSeconds(7f);
        if (_transform.position == pos)
        {
            WASD.SetActive(true);
        }
        yield break;
    }

    private IEnumerator ShowLight()
    {
        yield return new WaitForSeconds(5f);
        if (_player.IsChargeMode) yield break;
        _light.intensity = 125;
        yield break;
    }

    private IEnumerator ShowHumanMouse()
    {
        yield return new WaitForSeconds(2f);
        if (_player.IsDead) yield break;
        HumanMouse.SetActive(true);
        yield break;
    }
}
