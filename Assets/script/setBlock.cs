using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class setBlock : MonoBehaviour
{

    public GameObject Object;
    public GameObject gameObject;
    public GameObject[] Train;
    public GameObject Mouse;
    Color32 defalt = new Color32(255, 255, 255, 255);
    Color32 grabColor = new Color32(255, 0, 0, 255);
    public bool deleteflag = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Color_judgement()//ピースがはめられるなら色を変える
    {
        deleteflag = false;
        this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x + 0.5f), Mathf.Floor(this.transform.position.y + 0.5f), Mathf.Floor(this.transform.position.z));
        for (int i = 0; i < Train.Length; i++)
        {
            if (!gameObject.GetComponent<Gamesystem>().Check(Train[i]))
            {
                deleteflag = true;
            }
        }
        if (deleteflag == false)
        {

            for (int i = 0; i < Train.Length; i++)
            {
                ReleaseColor();//色を元に戻す
                gameObject.GetComponent<Gamesystem>().Color_ON(Train[i]);
            }
        }
        else {
            ChangeColorPiece();
        }
    }

    public void judgement()//ピースをはめられるかのチェック
    {
        deleteflag = false;
        this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x + 0.5f), Mathf.Floor(this.transform.position.y + 0.5f), Mathf.Floor(this.transform.position.z));
        for (int i = 0; i < Train.Length; i++)
        {
            if (!gameObject.GetComponent<Gamesystem>().Check(Train[i]))
            {
                deleteflag = true;
            }
        }


        if (deleteflag == false)
        {

            for (int i = 0; i < Train.Length; i++)
            {
                gameObject.GetComponent<Gamesystem>().setObject(Train[i]);　//ピースをはめる
            }
            DestroyImmediate(this.Object);
            gameObject.GetComponent<Gamesystem>().randomcreate(); //ランダムなピースを生成

            gameObject.GetComponent<Gamesystem>().ObjectDelete(); //ピースが消せるなら消す

            DOVirtual.DelayedCall(0.5f,
            () =>
            {
                gameObject.GetComponent<Gamesystem>().clearcheck(); //まⅮあピースgあはめられるかのチェック
            }
            );
        }
    }

    public void grabpiece()//ピースを持った時にピースをずらして持つ
    {
        for (int i = 0; i < Train.Length; i++)
        {
            Train[i].transform.position = Train[i].transform.position + new Vector3(0, 1, 0);
        }
    }

    public void ChangeColorPiece()//色を変える
    {
        for (int i = 0; i < Train.Length; i++)
        {
            Train[i].GetComponent<SpriteRenderer>().color = grabColor;
        }
    }

    public void ReleaseColor()//色を元に戻す
    {
        for (int i = 0; i < Train.Length; i++)
        {
            Train[i].GetComponent<SpriteRenderer>().color = defalt;
        }
    }
}
