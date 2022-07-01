using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class score : MonoBehaviour//スコアをテキストに反映
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
