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
            if (oldpos != Vector2.zero)//ピースを持っている時
            {
                CatchPiece = true;
                piece.GetComponent<setBlock>().Color_judgement();//はめられるピースの色を変える
                piece.transform.position = Mouse.transform.position + new Vector3(0,2,-1);
                piece.transform.localScale = new Vector3(1, 1, 1);//ピースのサイズを元に戻す
                gamesystem.GetComponent<Gamesystem>().ColorChange();//色を変える
            }
        }
        else 
        {
            CatchPiece = false;
        }

        if (Input.GetMouseButtonUp(0) && piece != null)//ピースを離したとき
        {
            piece.GetComponent<setBlock>().ReleaseColor();//色を元の色に戻す
        if (oldpos != Vector2.zero)
            {
            piece.transform.position = Mouse.transform.position + new Vector3(0, 2, 0);
            piece.GetComponent<setBlock>().judgement();//ピースがはまるかのチェック
            }
            if (piece != null)
            {//最初の位置に戻して大きさも戻す
                piece.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                piece.transform.position = oldpos;
            }
        }        
    }

    void OnTriggerStay2D(Collider2D collision)
    {//ピースと重なっているときに掴めるようにする
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
    {//重なっていないときに掴めないようにする
        if (CatchPiece == false)
        {
            piece = null;
        }
    }
}
