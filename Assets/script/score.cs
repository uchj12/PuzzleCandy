using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class score : MonoBehaviour//�X�R�A���e�L�X�g�ɔ��f
{
    public TextMeshProUGUI score_text;
    int Score;

    void Start()
    {
        Score = PlayerPrefs.GetInt("score");
    }

    void Update()
    {
        score_text.text = "SCORE:" + Score;
    }
}
