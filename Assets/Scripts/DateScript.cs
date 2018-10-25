using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateScript : MonoBehaviour {

	public GameObject DateText;
	public Button ButtonSpeedDate;
    public Button ButtonStopTime;
    public Scrollbar DateScroll;
    public StateScript Statescript;
	int d,m,y,kb;
	float upd = 0.01f;

	// Use this for initialization
	void Start () {
		d = System.DateTime.Today.Day;
		m = System.DateTime.Today.Month;
		y = System.DateTime.Today.Year;
       Statescript = StateScript.State;
		DateText.GetComponent<Text>().text = System.Convert.ToString (d + "." + m + "." + y);
		kb = 0;
		ButtonSpeedDate.GetComponentInChildren<Text> ().text = System.Convert.ToString ("x" + kb);
		ButtonSpeedDate.onClick.AddListener (SpeedOnClick);
        ButtonStopTime.onClick.AddListener(StopTime);
	}
	
	// Update is called once per frame
	void Update () {
		DateScroll.size = DateScroll.size + (upd*kb);

		if (DateScroll.size >= 1)	{
			d++;
			switch (m) {
			case 1:
			case 3:
			case 5:
			case 7:
			case 8:
			case 10:
			case 12:
				if (d > 31) { Mounth(); }
				break;
			case 2:
				if (d > 28) { Mounth(); }
				break; 
			case 4:
			case 6:
			case 9:
			case 11:
				if (d > 30) { Mounth(); }
				break;
			}
			if (m > 12) { d = 1; m = 1; y++;
			}
			DateText.GetComponent<Text>().text = System.Convert.ToString (d + "." + m + "." + y);
			DateScroll.size = 0;
            Statescript.UpdateEveryDay();
        }

	}
    void Mounth()
    {
        d = 1; m++; Statescript.EveryMounth();
    }

	void SpeedOnClick ()
	{
		switch (kb) {
        case 0:
            kb = 1;
            break;
		case 1:
			kb = 2;
			break;
		case 2:
			kb = 4;
			break;
		case 4:
			kb = 8;
			break;
		case 8:
			kb = 16;
			break;
		case 16:
			kb = 0;
			break;
		}
		ButtonSpeedDate.GetComponentInChildren<Text> ().text = System.Convert.ToString ("x" + kb);
	}
    public void StopTime()
    {
        kb = 0;
        ButtonSpeedDate.GetComponentInChildren<Text>().text = System.Convert.ToString("x" + kb);
    }
}
