using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuCircleNum : MonoBehaviour {
    public GameObject MCircle;
    public GameObject NamChang;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "Select1":
                SceneManager.LoadScene(1);
                break;
            case "Select2":
                Debug.Log("S2");
                MCircle.SetActive(false);
                NamChang.SetActive(true);
                break;
            case "Select3":
                Debug.Log("S3");
                break;
            case "Select4":
                Debug.Log("S4");
                break;
        }
    }
}
