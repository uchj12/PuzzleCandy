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
    public void scenechange()
    {
        panel.SetActive(true);
        panel.GetComponent<heedout>().isFadeOut = true;
        DOVirtual.DelayedCall(0.3f,
           () =>
           {
            
               SceneManager.LoadScene(SceneNumber);
               OptionPanel.SetActive(false);
           }
       );
    }
}
