using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject Menu;
    public Slider Sound_Volume;
    public GameObject icon;
    public GameObject EventSystem;
    // Start is called before the first frame update
    void Start()
    {
        Sound_Volume.value = AudioListener.volume;
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = Sound_Volume.value;
        if (icon == null)
        {
            icon = EventSystem.GetComponent<createBlock>().m_object;
        }
    }

    public void openOptions()
    {
        Menu.SetActive(true);
        icon.GetComponent<MovePiece>().enabled = false;
    }

    public void crose()
    {
        Menu.SetActive(false);
        icon.GetComponent<MovePiece>().enabled = true;
    }


}
