using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fadeout : MonoBehaviour
{
    Image fadeImage;
    float red, green, blue, alfa;
    public bool isFadeOut = false;
    float fadeSpeed = 0.02f;
	// Start is called before the first frame update
	void Start()
	{
		fadeImage = GetComponent<Image>();
		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;
	}

	void Update()
	{
		if (isFadeOut)
		{
			StartFadeOut();
		}
	}

	void StartFadeOut()
	{
		fadeImage.enabled = true;  // パネルの表示をオンにする
		alfa += fadeSpeed;         // 不透明度を徐々にあげる
		SetAlpha();               // 変更した透明度をパネルに反映する
		if (alfa >= 1)
		{             // 完全に不透明になったら処理を抜ける
			isFadeOut = false;
		}
	}

	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}
}
