using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePiece : MonoBehaviour
{
    public GameObject piece;
    public GameObject Mouse;
    public GameObject gamesystem;
    public Vector2 oldpos;
    bool CatchPiece = false;
    // Start is called before the first frame update
    void Start()
    {
        gamesystem = GameObject.Find("EventSystem");
    }
    // Update is called once per frame
    void Update()
    {
        if (piece == null)
        {
            oldpos = Vector2.zero;
        }

        if (Input.GetMouseButton(0))
        {
            if (oldpos != Vector2.zero)//�s�[�X�������Ă��鎞
            {
                CatchPiece = true;
                piece.GetComponent<setBlock>().Color_judgement();//�͂߂���s�[�X�̐F��ς���
                piece.transform.position = Mouse.transform.position + new Vector3(0,2,-1);
                piece.transform.localScale = new Vector3(1, 1, 1);//�s�[�X�̃T�C�Y�����ɖ߂�
                gamesystem.GetComponent<Gamesystem>().ColorChange();//�F��ς���
            }
        }
        else 
        {
            CatchPiece = false;
        }

        if (Input.GetMouseButtonUp(0) && piece != null)//�s�[�X�𗣂����Ƃ�
        {
            piece.GetComponent<setBlock>().ReleaseColor();//�F�����̐F�ɖ߂�
        if (oldpos != Vector2.zero)
            {
            piece.transform.position = Mouse.transform.position + new Vector3(0, 2, 0);
            piece.GetComponent<setBlock>().judgement();//�s�[�X���͂܂邩�̃`�F�b�N
            }
            if (piece != null)
            {//�ŏ��̈ʒu�ɖ߂��đ傫�����߂�
                piece.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                piece.transform.position = oldpos;
            }
        }        
    }

    void OnTriggerStay2D(Collider2D collision)
    {//�s�[�X�Əd�Ȃ��Ă���Ƃ��ɒ͂߂�悤�ɂ���
        if (!Input.GetMouseButton(0) && collision != piece)
        {
            piece = collision.gameObject;
            oldpos = piece.transform.position;
        }
        if (piece == null)
        {
            piece = collision.gameObject;
            oldpos = piece.transform.position;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {//�d�Ȃ��Ă��Ȃ��Ƃ��ɒ͂߂Ȃ��悤�ɂ���
        if (CatchPiece == false)
        {
            piece = null;
        }
    }
}
