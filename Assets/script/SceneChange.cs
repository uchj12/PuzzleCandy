using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class SceneChange : MonoBehaviour
{
    public GameObject panel;
    public int SceneNumber = 0;
    public GameObject OptionPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void scenechange()//シーンを変える
    {
        panel.SetActive(true);//フェードを起動
        panel.GetComponent<fadeout>().isFadeOut = true;
        //処理を送らせてシーンを変える
        DOVirtual.DelayedCall(0.3f,
           () =>
           {
               OptionPanel.SetActive(false);
               SceneManager.LoadScene(SceneNumber);
           }
       );
    }
}
