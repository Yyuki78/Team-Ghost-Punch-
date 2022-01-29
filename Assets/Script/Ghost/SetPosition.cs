using UnityEngine;
using System.Collections;

public class SetPosition : MonoBehaviour
{

    //�����ʒu
    private Transform myTransform;
    private Vector3 startPosition;
    //�ړI�n
    private Vector3 destination;

    void Start()
    {
        //�@�����ʒu��ݒ�
        myTransform = this.transform;
        startPosition = myTransform.position;
        SetDestination(transform.position);
    }

    //�@�����_���Ȉʒu�̍쐬
    public void CreateRandomPosition()
    {
        //�@�����_����Vector2�̒l�𓾂�
        var randDestination = Random.insideUnitCircle * 6;
        //�@���ݒn�Ƀ����_���Ȉʒu�𑫂��ĖړI�n�Ƃ���
        SetDestination(startPosition + new Vector3(randDestination.x, 0, randDestination.y));
    }

    //�@�ړI�n��ݒ肷��
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //�@�ړI�n���擾����
    public Vector3 GetDestination()
    {
        return destination;
    }
}