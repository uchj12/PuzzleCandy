using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;


public class Gamesystem : MonoBehaviour
{
    public AudioSource sound1;
    AudioSource audioSource;
    public const int Width = 9;
    public const int Height = 9;
    public GameObject[,] Block = new GameObject[Width, Height];
    public int[,] Color_Block = new int[Width, Height];
    public GameObject[] Train;
    public GameObject[] Color_Piece;
    Vector2 createpos1 = new Vector2(0, -4);
    Vector2 createpos2 = new Vector2(3, -4);
    Vector2 createpos3 = new Vector2(6, -4);
    GameObject piece1;
    GameObject piece2;
    GameObject piece3;
    public TextMeshProUGUI score_text;
    public int score = 0;
    int oldscore = 0;
    public int level = 0;
    public int combo = 0;//続けて消すとスコアが増える
    bool gameover = false;
    bool deleteflag = false;
    public GameObject particleObject;
    public GameObject fadeImage;
    //public GameObject MainPanel;
    // Start is called before the first frame update

    Color32 red = new Color32(255, 255, 255, 255);
    Color32 defalt = new Color32(0, 255, 0, 255);
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        randomcreate();
    }

    // Update is called once per frame
    void Update()
    {
        ColorChange();
        Color_OFF();
       

        score_text.text = "SCORE:" + score;

    }

    public void setObject(GameObject Object)
    {

        Block[(int)Object.transform.position.x, (int)Object.transform.position.y] = Instantiate(Object, Object.transform.position, transform.rotation);
        Block[(int)Object.transform.position.x, (int)Object.transform.position.y].layer = 1;
        Color_Block[(int)Object.transform.position.x, (int)Object.transform.position.y] = 1;
    }

    public void Color_ON(GameObject Object)
    {
        Color_Block[(int)Object.transform.position.x, (int)Object.transform.position.y] = 2;
    }

    public bool Check(GameObject Object)
    {
        if ((int)Object.transform.position.x < Width && (int)Object.transform.position.x >= 0 && (int)Object.transform.position.y < Height && (int)Object.transform.position.y >= 0)
        {
            if (Block[(int)Object.transform.position.x, (int)Object.transform.position.y] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void Color_OFF()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Block[i, j] == null)
                {
                    Color_Block[i, j] = 0;
                }
            }
        }
    }

    public void ApplyColor()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (Color_Block[i, j] == 2)
                {
                    Color_Piece[i + j * 9].SetActive(true);
                    Color_Piece[i + j * 9].GetComponent<Image>().color = red;
                }

            }
        }
    }
    public void ColorChange()
    {
        for (int i = 0; i < 9; i += 3)
        {
            for (int j = 0; j < 9; j += 3)
            {
                if (Color_Block[i, j] <= 2 && Color_Block[i + 1, j] <= 2 && Color_Block[i + 2, j] <= 2 &&
                    Color_Block[i, j + 1] <= 2 && Color_Block[i + 1, j + 1] <= 2 && Color_Block[i + 2, j + 1] <= 2 &&
                    Color_Block[i, j + 2] <= 2 && Color_Block[i + 1, j + 2] <= 2 && Color_Block[i + 2, j + 2] <= 2 &&
                    Color_Block[i, j] > 0 && Color_Block[i + 1, j] > 0 && Color_Block[i + 2, j] > 0 &&
                    Color_Block[i, j + 1] > 0 && Color_Block[i + 1, j + 1] > 0 && Color_Block[i + 2, j + 1] > 0 &&
                    Color_Block[i, j + 2] > 0 && Color_Block[i + 1, j + 2] > 0 && Color_Block[i + 2, j + 2] > 0)
                {
                    Color_Piece[i + j * 9].SetActive(true);
                    Color_Piece[i + j * 9 + 1].SetActive(true);
                    Color_Piece[i + j * 9 + 2].SetActive(true);
                    Color_Piece[i + j * 9 + 9].SetActive(true);
                    Color_Piece[i + j * 9 + 10].SetActive(true);
                    Color_Piece[i + j * 9 + 11].SetActive(true);
                    Color_Piece[i + j * 9 + 18].SetActive(true);
                    Color_Piece[i + j * 9 + 19].SetActive(true);
                    Color_Piece[i + j * 9 + 20].SetActive(true);
                    Color_Piece[i + j * 9].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 1].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 2].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 9].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 10].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 11].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 18].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 19].GetComponent<Image>().color = defalt;
                    Color_Piece[i + j * 9 + 20].GetComponent<Image>().color = defalt;
                }
                else 
                {
                    Color_Piece[i + j * 9].SetActive(false);
                    Color_Piece[i + j * 9 + 1].SetActive(false);
                    Color_Piece[i + j * 9 + 2].SetActive(false);
                    Color_Piece[i + j * 9 + 9].SetActive(false);
                    Color_Piece[i + j * 9 + 10].SetActive(false);
                    Color_Piece[i + j * 9 + 11].SetActive(false);
                    Color_Piece[i + j * 9 + 18].SetActive(false);
                    Color_Piece[i + j * 9 + 19].SetActive(false);
                    Color_Piece[i + j * 9 + 20].SetActive(false);
                }
            }
        }
        for (int i = 0; i < 9; i++)
        {

            if (Color_Block[i, 0] <= 2 && Color_Block[i, 1] <= 2 && Color_Block[i, 2] <= 2&& Color_Block[i, 3] <= 2 &&
                Color_Block[i, 4] <= 2 && Color_Block[i, 5] <= 2 && Color_Block[i, 6] <= 2 && Color_Block[i, 7] <= 2 && Color_Block[i, 8] <= 2 &&
                Color_Block[i, 0] > 0 && Color_Block[i, 1] > 0 && Color_Block[i, 2] > 0 && Color_Block[i, 3] > 0 &&
                Color_Block[i, 4] > 0 && Color_Block[i, 5] > 0 && Color_Block[i, 6] > 0 && Color_Block[i, 7] > 0 && Color_Block[i, 8] > 0)
            {
                for (int j = 0; j < 9; j++)
                {
                    Color_Piece[i + 9 * j].SetActive(true);
                    Color_Piece[i + 9 * j].GetComponent<Image>().color = defalt;
                }

            }
            else if (Color_Block[i, 0] <= 2 && Color_Block[i, 1] <= 2 && Color_Block[i, 2] <= 2 && Color_Block[i, 3] <= 2 &&
                     Color_Block[i, 4] <= 2 && Color_Block[i, 5] <= 2 && Color_Block[i, 6] <= 2 && Color_Block[i, 7] <= 2 && Color_Block[i, 8] <= 2 &&
                     Color_Block[i, 0] > 0 && Color_Block[i, 1] > 0 && Color_Block[i, 2] > 0 && Color_Block[i, 3] > 0 &&
                     Color_Block[i, 4] > 0 && Color_Block[i, 5] > 0 && Color_Block[i, 6] > 0 && Color_Block[i, 7] > 0 && Color_Block[i, 8] > 0)
            {
                for (int j = 0; j < 9; j++)
                {
                    Color_Piece[i + 9 * j].SetActive(false);
                }

            }
            if (Color_Block[0, i] <= 2 && Color_Block[1, i] <= 2 && Color_Block[2, i] <= 2 && Color_Block[3, i] <= 2 &&
                Color_Block[4, i] <= 2 && Color_Block[5, i] <= 2 && Color_Block[6, i] <= 2 && Color_Block[7, i] <= 2 && Color_Block[8, i] <= 2 &&
                Color_Block[0, i] > 0 && Color_Block[1, i] > 0 && Color_Block[2, i] > 0 && Color_Block[3, i] > 0 &&
                Color_Block[4, i] > 0 && Color_Block[5, i] > 0 && Color_Block[6, i] > 0 && Color_Block[7, i] > 0 && Color_Block[8, i] > 0)
            {
                for (int j = 0; j < 9; j++)
                {
                    Color_Piece[i * 9 + j].SetActive(true);
                    Color_Piece[i * 9 + j].GetComponent<Image>().color = defalt;
                }
               
            }
            else if (Color_Block[0, i] <= 2 && Color_Block[1, i] <= 2 && Color_Block[2, i] <= 2 && Color_Block[3, i] <= 2 &&
                     Color_Block[4, i] <= 2 && Color_Block[5, i] <= 2 && Color_Block[6, i] <= 2 && Color_Block[7, i] <= 2 && Color_Block[8, i] <= 2 &&
                     Color_Block[0, i] > 0 && Color_Block[1, i] > 0 && Color_Block[2, i] > 0 && Color_Block[3, i] > 0 &&
                     Color_Block[4, i] > 0 && Color_Block[5, i] > 0 && Color_Block[6, i] > 0 && Color_Block[7, i] > 0 && Color_Block[8, i] > 0)
            {
                for (int j = 0; j < 9; j++)
                {
                    Color_Piece[i + 9 * j].SetActive(false);
                }
                //Color_Piece[i * 9].SetActive(false);
                //Color_Piece[i * 9 + 1].SetActive(false);
                //Color_Piece[i * 9 + 2].SetActive(false);
                //Color_Piece[i * 9 + 3].SetActive(false);
                //Color_Piece[i * 9 + 4].SetActive(false);
                //Color_Piece[i * 9 + 5].SetActive(false);
                //Color_Piece[i * 9 + 6].SetActive(false);
                //Color_Piece[i * 9 + 7].SetActive(false);
                //Color_Piece[i * 9 + 8].SetActive(false);
            }
        }
        ApplyColor();
    }
    public void ObjectDelete()
    {
       
        sound1.PlayOneShot(sound1.clip);
        for (int i = 0; i < 9; i += 3)
        {
            for (int j = 0; j < 9; j += 3)
            {
                if (Block[i, j] != null && Block[i + 1, j] != null && Block[i + 2, j] != null &&
                    Block[i, j + 1] != null && Block[i + 1, j + 1] != null && Block[i + 2, j + 1] != null &&
                    Block[i, j + 2] != null && Block[i + 1, j + 2] != null && Block[i + 2, j + 2] != null)
                {
                    for (int n = 0; n < 9; n++)
                    {
                        StartCoroutine(DelayMethodY(10, i + n / 3, j + n % 3));
                    }
                    combo++;
                    score_Plas(300);
                }
            }
        }
        for (int i = 0; i < 9; i++)
        {
            if (Block[i, 0] != null && Block[i, 1] != null && Block[i, 2] != null && Block[i, 3] != null &&
                Block[i, 4] != null && Block[i, 5] != null && Block[i, 6] != null && Block[i, 7] != null && Block[i, 8] != null)
            {
                for(int j = 0;j < 9; j++)
                {
                    StartCoroutine(DelayMethodX(10,i,j));
                }
                combo++;
                score_Plas(300);
            }
            if (Block[0, i] != null && Block[1, i] != null && Block[2, i] != null && Block[3, i] != null &&
                Block[4, i] != null && Block[5, i] != null && Block[6, i] != null && Block[7, i] != null && Block[8, i] != null)
            {
                for (int j = 0; j < 9; j++)
                {
                    StartCoroutine(DelayMethodY(10, j, i));
                }
                combo++;
                score_Plas(300);
            }
        }
        if (score == oldscore)
        {
            combo = 0;
        }
        oldscore = score;
        Color_OFF();
    }

   

    public void randomcreate()
    {
        if (piece1 == null && piece2 == null && piece3 == null)
        {
            
            if (level == 0) {
                piece1 = Instantiate(Train[Random.Range(0, 11)], createpos1, transform.rotation);
                piece2 = Instantiate(Train[Random.Range(0, 11)], createpos2, transform.rotation);
                piece3 = Instantiate(Train[Random.Range(0, 11)], createpos3, transform.rotation);
            }
            if (level == 1) {
                piece1 = Instantiate(Train[Random.Range(0, 21)], createpos1, transform.rotation);
                piece2 = Instantiate(Train[Random.Range(0, 21)], createpos2, transform.rotation);
                piece3 = Instantiate(Train[Random.Range(0, 21)], createpos3, transform.rotation);
            }
            if (level == 2)
            {
                piece1 = Instantiate(Train[Random.Range(0, 33)], createpos1, transform.rotation);
                piece2 = Instantiate(Train[Random.Range(0, 33)], createpos2, transform.rotation);
                piece3 = Instantiate(Train[Random.Range(0, 33)], createpos3, transform.rotation);
            }
            if (level >= 3)
            {
                piece1 = Instantiate(Train[Random.Range(0, Train.Length)], createpos1, transform.rotation);
                piece2 = Instantiate(Train[Random.Range(0, Train.Length)], createpos2, transform.rotation);
                piece3 = Instantiate(Train[Random.Range(0, Train.Length)], createpos3, transform.rotation);
            }
            piece1.layer = 5;
            piece2.layer = 5;
            piece3.layer = 5;
            piece2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            piece3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            piece1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            
        }
    }
    void score_Plas(int Pscore)
    {
        score += Pscore * combo;
       
        level_up();
    }

    public void clearcheck()
    { 
        gameover = false;
        if (piece1 != null)
        {
            piece1.transform.localScale = new Vector3(1f, 1, 1);
            for (int j = 0; j < Block.Length; j++)
            {
                deleteflag = false;
                piece1.transform.position = new Vector2((int)(j / 9), (int)(j % 9));
                for (int i = 0; i < piece1.GetComponent<setBlock>().Train.Length; i++)
                {
                    if (!Check(piece1.GetComponent<setBlock>().Train[i]))
                    {
                        deleteflag = true;
                    }
                }
                if (deleteflag == false)
                {
                    gameover = true;
                }
            }
            piece1.transform.position = createpos1;
            piece1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        if (piece2 != null)
        { 
            piece2.transform.localScale = new Vector3(1f, 1, 1);
            for (int j = 0; j < Block.Length; j++)
            {
                deleteflag = false;
                piece2.transform.position = new Vector2((int)(j / 9), (int)(j % 9));
                for (int i = 0; i < piece2.GetComponent<setBlock>().Train.Length; i++)
                {
                    if (!Check(piece2.GetComponent<setBlock>().Train[i]))
                    { 
                        deleteflag = true;
                    }
                }
                if (deleteflag == false)
                {
                    gameover = true;
                }
            }
            piece2.transform.position = createpos2;
            piece2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (piece3 != null)
        { 
            piece3.transform.localScale = new Vector3(1f, 1, 1);
            for (int j = 0; j < Block.Length; j++)
            {
                deleteflag = false;
                piece3.transform.position = new Vector2((int)(j / 9), (int)(j % 9));
                for (int i = 0; i < piece3.GetComponent<setBlock>().Train.Length; i++)
                {
                    if (!Check(piece3.GetComponent<setBlock>().Train[i]))
                    {
                        deleteflag = true;
                    }
                }
                if (deleteflag == false)
                {
                    gameover = true;
                }
            }
            piece3.transform.position = createpos3;
            piece3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        if (gameover == false)
        {
         
            PlayerPrefs.SetInt("score",score);
            PlayerPrefs.Save();
            fadeImage.SetActive(true);
            fadeImage.GetComponent<heedout>().isFadeOut = true;
            DOVirtual.DelayedCall(0.5f,
           () =>
           {
               SceneManager.LoadScene(2);
           }
           );
        }
    }

    public void level_up()
    {
        for (int i = level; i < 10; i++)
        {
            if (score > 2000 * i)
            {

                level = i;
                //レベル（難易度を上げたい）
            }
        }
    }

    private IEnumerator DelayMethodY(int delayFrameCount, int x , int y)
    {
        for (var i = 0; i < delayFrameCount + x * 10; i++)
        {
            yield return null;
        }
        Destroy(Block[x, y]);
        Instantiate(particleObject, Block[x, y].transform.position, transform.rotation);
    }
    private IEnumerator DelayMethodX(int delayFrameCount, int x, int y)
    {
        for (var i = 0; i < delayFrameCount + y * 10; i++)
        {
            yield return null;
        }
        Destroy(Block[x, y]);
        Instantiate(particleObject, Block[x, y].transform.position, transform.rotation);
    }
}
