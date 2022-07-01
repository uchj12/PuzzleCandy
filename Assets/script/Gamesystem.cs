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
    public GameObject[,] Block = new GameObject[Width, Height];//ピースをはめるマス
    public int[,] Color_Block = new int[Width, Height];//ピースをはめるマスの色
    public GameObject[] Train;
    public GameObject[] Color_Piece;
    Vector2 createpos1 = new Vector2(0, -4);//ピースの生成位置
    Vector2 createpos2 = new Vector2(3, -4);
    Vector2 createpos3 = new Vector2(6, -4);
    GameObject Piece1;
    GameObject Piece2;
    GameObject Piece3;
    public TextMeshProUGUI score_text;
    public int score = 0;
    int oldscore = 0;
    public int level = 0;//レベル
    public int combo = 0;//続けて消すとスコアが増える
    bool gameover = false;
    bool Deleteflag = false;
    bool ChangeColorflag = false;
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
        ColorChange();//消えるピースの色を変える
        Color_OFF();//色を変えるフラグを０にする
        score_text.text = "SCORE:" + score;
    }

    public void setObject(GameObject Object)//ピースをはめたときに、はめたブロックにピースを格納する
    {
        Block[(int)Object.transform.position.x, (int)Object.transform.position.y] = Instantiate(Object, Object.transform.position, transform.rotation);
        Block[(int)Object.transform.position.x, (int)Object.transform.position.y].layer = 1;
        Color_Block[(int)Object.transform.position.x, (int)Object.transform.position.y] = 1;//色を変えるフラグ１にする
    }

    public void Color_ON(GameObject Object)//色を変えるフラグを２にする
    {
        Color_Block[(int)Object.transform.position.x, (int)Object.transform.position.y] = 2;
    }

    public bool Check(GameObject Object)//ブロックにピースがはまっているかチェック
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

    public void Color_OFF()//色を変えるフラグを０にする
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

    public void ApplyColor()//掴んでいるピースがはめられるブロックの色を変える
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
    public void ColorChange()//消えるピースの色を変える
    {
        
        for (int i = 0; i < 9; i += 3)
        {
            
            for (int j = 0; j < 9; j += 3)//３×３のマスにはまってたら色を変える
            {
                ChangeColorflag = true;
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (!(Color_Block[i + x, j + y] <= 2 && Color_Block[i + x, j + y] > 0))
                        {
                            ChangeColorflag = false;
                        }
                    }
                }
               
                if(ChangeColorflag == true)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 27; y+=9)
                        {
                            Color_Piece[i + j * 9 + (x + y)].SetActive(true);
                            Color_Piece[i + j * 9 + (x + y)].GetComponent<Image>().color = defalt;
                        }
                    }
                }
                else //はまっていないなら色を戻す
                {
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 27; y += 9)
                        {
                            Color_Piece[i + j * 9 + (x + y)].SetActive(false);
                        }
                    }
                }
            }
        }

        for (int i = 0; i < 9; i++)//縦１列にピースがはまっていたら色を変える
        {
            ChangeColorflag = true;
            for (int j = 0; j < 9; j++)
            {
                if (!(Color_Block[i, j] <= 2 && Color_Block[i, j] > 0)) ChangeColorflag = false;
            }
            if (ChangeColorflag == true)
            {
                for (int j = 0; j < 9; j++)
                {
                    Color_Piece[i + 9 * j].SetActive(true);
                    Color_Piece[i + 9 * j].GetComponent<Image>().color = defalt;
                }
            }
        }
            for (int i = 0; i < 9; i++)//縦１列にピースがはまっていたら色を変える
            {
                ChangeColorflag = true;
                for (int j = 0; j < 9; j++)
                {
                    if (!(Color_Block[j, i] <= 2 && Color_Block[j, i] > 0)) ChangeColorflag = false;
                }
                if (ChangeColorflag == true)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Color_Piece[i * 9 + j].SetActive(true);
                        Color_Piece[i * 9 + j].GetComponent<Image>().color = defalt;
                    }

                }

        }
        ApplyColor();//掴んでいるピースがはめられるブロックの色を変える
    }

    public void ObjectDelete()//そろっているピースを消す
    {   
        sound1.PlayOneShot(sound1.clip);//はめた時の音
        for (int i = 0; i < 9; i += 3)
        {
            ChangeColorflag = true;
            for (int j = 0; j < 9; j += 3)
            {
                //３×３がはまっているかの確認
               for(int x = 0; x < 3;x++)
                {
                    for (int y = 0; y < 3; y++)
                    { 
                        if (Block[i + x, j + y] ==null)
                        {
                            ChangeColorflag = false;
                        }
                    }
                }
                if (ChangeColorflag == true)
                {
                    for (int n = 0; n < 9; n++)
                    {
                        StartCoroutine(DelayMethodY(10, i + n / 3, j + n % 3));//はまっていたらタイミングをずらして消す
                    }
                    combo++;//コンボ、スコアの加算
                    score_Plas(300);
                }

            }
        }
        for (int i = 0; i < 9; i++)
        {
            //縦１列がはまっているかの確認
            if (CheckLineX(i))
            {
                for(int j = 0;j < 9; j++)
                {
                    StartCoroutine(DelayMethodX(10,i,j));//はまっていたらタイミングをずらして消す
                }
                combo++;//コンボ、スコアの加算
                score_Plas(300);
            }
            //横１列がはまっているかの確認
            if (CheckLineY(i))
            {
                for (int j = 0; j < 9; j++)
                {
                    StartCoroutine(DelayMethodY(10, j, i));//はまっていたらタイミングをずらして消す
                }
                combo++;//コンボ、スコアの加算
                score_Plas(300);
            }
        }
        if (score == oldscore)//スコアが変わっていない（消せてない）ならコンボを０に
        {
            combo = 0;
        }
        oldscore = score;//古いスコア
        Color_OFF();//色を変えるフラグを０にする
    }

   

    public void randomcreate()//スコアによって変動するレベルごとに生成されるピースを変える
    {
        if (Piece1 == null && Piece2 == null && Piece3 == null)
        {
            if (level == 0) {//レベルが０の時
                Piece1 = Instantiate(Train[Random.Range(0, 11)], createpos1, transform.rotation);
                Piece2 = Instantiate(Train[Random.Range(0, 11)], createpos2, transform.rotation);
                Piece3 = Instantiate(Train[Random.Range(0, 11)], createpos3, transform.rotation);
            }
            if (level == 1) {//レベルが１の時
                Piece1 = Instantiate(Train[Random.Range(0, 21)], createpos1, transform.rotation);
                Piece2 = Instantiate(Train[Random.Range(0, 21)], createpos2, transform.rotation);
                Piece3 = Instantiate(Train[Random.Range(0, 21)], createpos3, transform.rotation);
            }
            if (level == 2) {//レベルが２の時
                Piece1 = Instantiate(Train[Random.Range(0, 33)], createpos1, transform.rotation);
                Piece2 = Instantiate(Train[Random.Range(0, 33)], createpos2, transform.rotation);
                Piece3 = Instantiate(Train[Random.Range(0, 33)], createpos3, transform.rotation);
            }
            if (level >= 3) {//レベルが３の時
                Piece1 = Instantiate(Train[Random.Range(0, Train.Length)], createpos1, transform.rotation);
                Piece2 = Instantiate(Train[Random.Range(0, Train.Length)], createpos2, transform.rotation);
                Piece3 = Instantiate(Train[Random.Range(0, Train.Length)], createpos3, transform.rotation);
            }
            Piece1.layer = 5;//画面に映るようにレイヤーを変更
            Piece2.layer = 5;
            Piece3.layer = 5;
            Piece2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);//画面に収まりきらないピースがあるためサイズを小さくする
            Piece3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            Piece1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            
        }
    }
    void score_Plas(int Pscore)//スコアの加算
    {
        score += Pscore * combo;
       
        level_up();
    }

    public void clearcheck()//これ以上ピースをはめることができるかのチェック
    { 
        gameover = false;
        if (Piece1 != null)//ピース１が残っていたらどこかにはまらないかのチェック
        {
            Piece1.transform.localScale = new Vector3(1f, 1, 1);
            for (int j = 0; j < Block.Length; j++)
            {
                Deleteflag = false;
                Piece1.transform.position = new Vector2((int)(j / 9), (int)(j % 9));
                for (int i = 0; i < Piece1.GetComponent<setBlock>().Train.Length; i++)
                {
                    if (!Check(Piece1.GetComponent<setBlock>().Train[i]))
                    {
                        Deleteflag = true;
                    }
                }
                if (Deleteflag == false)
                {
                    gameover = true;
                }
            }
            Piece1.transform.position = createpos1;
            Piece1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        if (Piece2 != null)//ピース3が残っていたらどこかにはまらないかのチェック
        { 
            Piece2.transform.localScale = new Vector3(1f, 1, 1);
            for (int j = 0; j < Block.Length; j++)
            {
                Deleteflag = false;
                Piece2.transform.position = new Vector2((int)(j / 9), (int)(j % 9));
                for (int i = 0; i < Piece2.GetComponent<setBlock>().Train.Length; i++)
                {
                    if (!Check(Piece2.GetComponent<setBlock>().Train[i]))
                    { 
                        Deleteflag = true;
                    }
                }
                if (Deleteflag == false)
                {
                    gameover = true;
                }
            }
            Piece2.transform.position = createpos2;
            Piece2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        if (Piece3 != null)//ピース3が残っていたらどこかにはまらないかのチェック
        { 
            Piece3.transform.localScale = new Vector3(1f, 1, 1);
            for (int j = 0; j < Block.Length; j++)
            {
                Deleteflag = false;
                Piece3.transform.position = new Vector2((int)(j / 9), (int)(j % 9));
                for (int i = 0; i < Piece3.GetComponent<setBlock>().Train.Length; i++)
                {
                    if (!Check(Piece3.GetComponent<setBlock>().Train[i]))
                    {
                        Deleteflag = true;
                    }
                }
                if (Deleteflag == false)
                {
                    gameover = true;
                }
            }
            Piece3.transform.position = createpos3;
            Piece3.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        if (gameover == false)//はまるピースがなかったらゲームオーバー
        {
         
            PlayerPrefs.SetInt("score",score);
            PlayerPrefs.Save();
            fadeImage.SetActive(true);
            fadeImage.GetComponent<fadeout>().isFadeOut = true;//フェードアウトする
            DOVirtual.DelayedCall(0.5f,
           () =>
           {
               SceneManager.LoadScene(2);//シーンを変える
           }
           );
        }
    }

    public void level_up()//スコアによってレベルを変更
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

    private IEnumerator DelayMethodY(int delayFrameCount, int x , int y)//タイミングをずらして消す
    {
        for (var i = 0; i < delayFrameCount + x * 10; i++)
        {
            yield return null;
        }
        Destroy(Block[x, y]);
        Instantiate(particleObject, Block[x, y].transform.position, transform.rotation);
    }
    
    private IEnumerator DelayMethodX(int delayFrameCount, int x, int y)//タイミングをずらして消す
    {
        for (var i = 0; i < delayFrameCount + y * 10; i++)
        {
            yield return null;
        }
        Destroy(Block[x, y]);
        Instantiate(particleObject, Block[x, y].transform.position, transform.rotation);
    }
    
    private bool CheckLineX(int i)
    {
        Deleteflag = false;
        for (int j = 0; j < 9; j++)
        {
            if (Block[i, j] == null)
            {
                Deleteflag = true;
            }
        }
        if (Deleteflag == false)
        {
            return true;
        }
     return false;
    }

    private bool CheckLineY(int i)
    {
        Deleteflag = false;
        for (int j = 0; j < 9; j++)
        {
            if (Block[j, i] == null)
            {
                Deleteflag = true;
            }
        }
        if (Deleteflag == false)
        {
            return true;
        }
        return false;
    }
}

