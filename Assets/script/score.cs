using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class score : MonoBehaviour
{
    public TextMeshProUGUI score_text;
    int Score;
    // Start is called before the first frame update
    void Start()
    {
        Score = PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
    {
        score_text.text = "SCORE:" + Score;
    }
}
