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


    public void Color_judgement()
    {
        deleteflag = false;
        this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x + 0.5f), Mathf.Floor(this.transform.position.y + 0.5f), Mathf.Floor(this.transform.position.z));
        for (int i = 0; i < Train.Length; i++)
        {
            if (gameObject.GetComponent<Gamesystem>().Check(Train[i]))
            {
             
            }
            else
            {
                deleteflag = true;

            }
        }
        if (deleteflag == false)
        {

            for (int i = 0; i < Train.Length; i++)
            {

                //gameObject.GetComponent<Gamesystem>().Color_OFF();
                ReleaseColor();
                gameObject.GetComponent<Gamesystem>().Color_ON(Train[i]);
            }
        }
        else {
            ChangeColorPiece();
        }
    }

    public void judgement()
    {
        deleteflag = false;
        this.transform.position = new Vector3(Mathf.Floor(this.transform.position.x + 0.5f), Mathf.Floor(this.transform.position.y + 0.5f), Mathf.Floor(this.transform.position.z));
        for (int i = 0; i < Train.Length; i++)
        {
            if (gameObject.GetComponent<Gamesystem>().Check(Train[i]))
            {

            }
            else
            {
                deleteflag = true;
            }
        }


        if (deleteflag == false)
        {

            for (int i = 0; i < Train.Length; i++)
            {
                gameObject.GetComponent<Gamesystem>().setObject(Train[i]);
            }
            DestroyImmediate(this.Object);
            gameObject.GetComponent<Gamesystem>().randomcreate();

            gameObject.GetComponent<Gamesystem>().ObjectDelete();

            DOVirtual.DelayedCall(0.5f,
            () =>
            {
                gameObject.GetComponent<Gamesystem>().clearcheck();
            }
            );
        }
    }

    public void grabpiece()
    {
        for (int i = 0; i < Train.Length; i++)
        {
            Train[i].transform.position = Train[i].transform.position + new Vector3(0, 1, 0);
        }
    }

    public void ChangeColorPiece()
    {
        for (int i = 0; i < Train.Length; i++)
        {
            Train[i].GetComponent<SpriteRenderer>().color = grabColor;
        }
    }

    public void ReleaseColor()
    {
        for (int i = 0; i < Train.Length; i++)
        {
            Train[i].GetComponent<SpriteRenderer>().color = defalt;
        }
    }
}
