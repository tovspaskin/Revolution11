using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpScript : MonoBehaviour {
    public Text text;
    public Image picture;
    public GameObject but;
    public DateScript DS;
    public enum Acts {none, fail, victory }
    public Acts act = Acts.none;
    // Use this for initialization

    void Start ()
    {
		
	}
	
    public void SetText(string value)
    {
        text.text = value;
    }

    public void SetImage(Sprite value)
    {
        picture.gameObject.SetActive(true);
        picture.sprite = value;
    }

    public void SetActiveHelp(Sprite val, string valueT)
    {
        picture.sprite = val;
        picture.gameObject.SetActive(true);
        text.text = valueT;
        but.SetActive(true);
        DS.StopTime();
    }

    public void HideHelp()
    {
        picture = null;
        picture.gameObject.SetActive(false);
        text.text = "";
        but.SetActive(false);
        if (act == Acts.fail) { SceneManager.LoadScene(0); }
        if (act == Acts.victory) { SceneManager.LoadScene(0); }
    }

}
