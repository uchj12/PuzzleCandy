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
            if (oldpos != Vector2.zero)
            {
                CatchPiece = true;
                piece.GetComponent<setBlock>().Color_judgement();
                piece.transform.position = Mouse.transform.position + new Vector3(0,2,-1);
                piece.transform.localScale = new Vector3(1, 1, 1);
                gamesystem.GetComponent<Gamesystem>().ColorChange();
            }
        }
        else 
        {
            CatchPiece = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
          piece.GetComponent<setBlock>().ReleaseColor();
        if (oldpos != Vector2.zero)
            {
            piece.transform.position = Mouse.transform.position + new Vector3(0, 2, 0);
            piece.GetComponent<setBlock>().judgement();

                if (piece.GetComponent<setBlock>().deleteflag == true)
                {
                    piece.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    piece.transform.position = oldpos;
                }
            }            
        }        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
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
    {
        if (CatchPiece == false)
        {
            piece = null;
        }
    }
}
