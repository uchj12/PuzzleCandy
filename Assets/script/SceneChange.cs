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
    public void scenechange()//�V�[����ς���
    {
        panel.SetActive(true);//�t�F�[�h���N��
        panel.GetComponent<fadeout>().isFadeOut = true;
        //�����𑗂点�ăV�[����ς���
        DOVirtual.DelayedCall(0.3f,
           () =>
           {
               OptionPanel.SetActive(false);
               SceneManager.LoadScene(SceneNumber);
           }
       );
    }
}
