using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Buttons : MonoBehaviour
{

    public StateScript Ss;
    public GameObject DE;
    public bool scboll;
    public bool scboolConst = false;
    public Text Message;
    private Vector3 StartScale, TargetScale;
    private bool flagsc,flagCam;
    private string Napr;
    public Sprite[] icons;
    Vector3 NewPosCam;
    public GameObject[] GObtn;
    HelpScript HS;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Ss = GameObject.Find("RussiaConturNew").GetComponent<StateScript>();
            Message = GameObject.Find("HelpTxt").GetComponent<Text>();
        }
        StartScale = gameObject.transform.localScale;
        TargetScale = new Vector3(StartScale.x * 1.1f, StartScale.y * 1.1f, StartScale.z * 1f);
        flagsc = false;
      if(SceneManager.GetActiveScene().buildIndex==1)  HS = GameObject.Find("WindowHelp").GetComponent<HelpScript>();
    }
    public Vector3 Clamp(Vector3 value)
    {
        value.x = Mathf.Clamp(value.x, -10, 10);
        value.y = Mathf.Clamp(value.y, -5, 5);
        value.z = Mathf.Clamp(value.z, -11, -1.5f);
        return value;
    }

    void Update()
    {
        if (scboll == true)
        {
            switch (flagsc)
            {
                case true:
                    gameObject.transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
                    break;
                case false:
                    gameObject.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
                    break;

            }
            if (gameObject.transform.localScale.x > TargetScale.x) flagsc = true;
            if (gameObject.transform.localScale.x <= StartScale.x)
            {
                flagsc = false;
                if (scboolConst == false) scboll = false;
                gameObject.transform.localScale = StartScale;
            }

        }

        if (flagCam == true) Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Clamp((Camera.main.transform.position + NewPosCam)), Time.deltaTime);
    }

    void OnMouseUpAsButton()
    {
        if (gameObject.name == "BtnStart")
        {
            scboll = true;
            if (DE.name == "InputField")
                PlayerPrefs.SetString("NameLider", DE.GetComponent<InputField>().text);
           // GameObject PH1 = GameObject.Find("NameChange");
           // GameObject PH2 = GameObject.Find("MenuCircle");
            if (GObtn[0] != null) GObtn[0].SetActive(false);
            if (GObtn[1] != null) GObtn[1].SetActive(true);
            //SceneManager.LoadScene("MainScene");
        }
        if (Ss != null)
        {
            if (Ss.PerksAll == false)
            {
                switch (gameObject.name)
                {
                    case "ButGiveLider":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.SettleLider();
                        break;
                    case "ButDoesOfParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        //  Ss.PartyActionsHide();
                        break;
                    case "ButAgitMinParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(false, StateScript.PartyActionEn.Agit);
                        break;
                    case "ButAgitPlParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(true, StateScript.PartyActionEn.Agit);
                        break;
                    case "ButSovMinParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(false, StateScript.PartyActionEn.Sov);
                        break;
                    case "ButSovPlParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(true, StateScript.PartyActionEn.Sov);
                        break;
                    case "ButRecMinParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(false, StateScript.PartyActionEn.Rec);
                        break;
                    case "ButRecPlParty":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(true, StateScript.PartyActionEn.Rec);
                        break;
                    case "ButMoneyMinP":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(false, StateScript.PartyActionEn.Money);
                        break;
                    case "ButMoneyPlP":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(true, StateScript.PartyActionEn.Money);
                        break;
                    case "ButHideMinP":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(false, StateScript.PartyActionEn.Hide);
                        break;
                    case "ButHidePlP":
                        scboll = true;
                        //if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                        Ss.PartyActionsUse(true, StateScript.PartyActionEn.Hide);
                        break;
                    case "SocNet":
                        scboll = true;
                        GetComponentInParent<Image>().sprite = icons[1];
                        break;
                    case "ButPanMap":
                        scboll = true;
                        Ss.MenuOnOff(0);
                        break;
                    case "ButPanLider":
                        Ss.MenuOnOff(2);
                        scboll = true;
                        break;
                    case "ButPanParty":
                        Ss.MenuOnOff(1);
                        scboll = true;
                        break;
                    case "ButAgitMin":
                        scboll = true;
                        Ss.LiderActionsUse(false, StateScript.LiderActionEn.Agit);
                        break;
                    case "ButAgitPl":
                        scboll = true;
                        Ss.LiderActionsUse(true, StateScript.LiderActionEn.Agit);
                        break;
                    case "ButMoneyMin":
                        scboll = true;
                        Ss.LiderActionsUse(false, StateScript.LiderActionEn.Money);
                        break;
                    case "ButMoneyPl":
                        scboll = true;
                        Ss.LiderActionsUse(true, StateScript.LiderActionEn.Money);
                        break;
                    case "ButPartMin":
                        scboll = true;
                        Ss.LiderActionsUse(false, StateScript.LiderActionEn.Part);
                        break;
                    case "ButPartPl":
                        scboll = true;
                        Ss.LiderActionsUse(true, StateScript.LiderActionEn.Part);
                        break;
                    case "ButHidMin":
                        scboll = true;
                        Ss.LiderActionsUse(false, StateScript.LiderActionEn.Hide);
                        break;
                    case "ButHidPl":
                        scboll = true;
                        Ss.LiderActionsUse(true, StateScript.LiderActionEn.Hide);
                        break;
                    case "ButLearnMin":
                        scboll = true;
                        Ss.LiderActionsUse(false, StateScript.LiderActionEn.Learn);
                        break;
                    case "ButLearnPl":
                        scboll = true;
                        Ss.LiderActionsUse(true, StateScript.LiderActionEn.Learn);
                        break;
                    case "ButPerkAllRegions":
                        scboll = true;
                        Ss.PerksAll = true;
                        Ss.ComParVoid(true);
                        break;
                    case "CommonParamFalse":
                        scboll = true;
                        Ss.ComParVoid(true);
                        break;
                    case "CommonParamTrue":
                        scboll = true;
                        Ss.ComParVoid(false);
                        break;

                    /*
                case "PartyBizPerk":
                    scboll = true;
                    Ss.PerkUp(StateScript.PerkActionEn.Money);
                    break;
                case "PartyDeepAgitPerk":
                    scboll = true;
                    Ss.PerkUp(StateScript.PerkActionEn.DeepAgit);
                    break;
                case "PartyUpUnityPerk":
                    scboll = true;
                    Ss.PerkUp(StateScript.PerkActionEn.Unity);
                    break;
                case "BtnSocNet":
                    scboll = true;
                    Ss.PerkUp(StateScript.PerkActionEn.SocNet);
                    break;
                case "BtnUpMoney":
                    scboll = true;
                    print("SocnetMoney");
                    Ss.PerkUp(StateScript.PerkActionEn.LidMoney);
                    break;
                case "BtnShadow":
                    scboll = true;
                    Ss.PerkUp(StateScript.PerkActionEn.Shadow);
                    break;
                case "BtnSpeak":
                    scboll = true;
                    Ss.PerkUp(StateScript.PerkActionEn.Speak);
                    break;*/
                    case "BtnLvlLid1":
                        scboll = true;
                        Ss.PerkLvlUp(1, gameObject);
                        break;
                    case "BtnLvlLid2":
                        scboll = true;
                        Ss.PerkLvlUp(2, gameObject);
                        break;
                    case "BtnLvlLid3":
                        scboll = true;
                        Ss.PerkLvlUp(3, gameObject);
                        break;
                    case "BtnShadowLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(4, gameObject);
                        break;
                    case "BtnShadowLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(5, gameObject);
                        break;
                    case "BtnShadowLvl3":
                        scboll = true;
                        Ss.PerkLvlUp(6, gameObject);
                        break;
                    case "BtnPassiveMoneyLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(7, gameObject);
                        break;
                    case "BtnPassiveMoneyLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(8, gameObject);
                        break;
                    case "BtnPropLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(9, gameObject);
                        break;
                    case "BtnPropLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(10, gameObject);
                        break;
                    case "BtnLvlParty1":
                        scboll = true;
                        Ss.PerkLvlUp(11, gameObject);
                        break;
                    case "BtnLvlParty2":
                        scboll = true;
                        Ss.PerkLvlUp(12, gameObject);
                        break;
                    case "BtnLvlParty3":
                        scboll = true;
                        Ss.PerkLvlUp(13, gameObject);
                        break;
                    case "BtnLvlParty4":
                        scboll = true;
                        Ss.PerkLvlUp(14, gameObject);
                        break;
                    case "BtnUnityLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(15, gameObject);
                        break;
                    case "BtnUnityLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(16, gameObject);
                        break;
                    case "BtnUnityLvl3":
                        scboll = true;
                        Ss.PerkLvlUp(17, gameObject);
                        break;
                    case "BtnLearnLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(18, gameObject);
                        break;
                    case "BtnLearnLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(19, gameObject);
                        break;
                    case "BtnLearnLvl3":
                        scboll = true;
                        Ss.PerkLvlUp(20, gameObject);
                        break;
                    case "BtnAgitLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(21, gameObject);
                        break;
                    case "BtnAgitLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(22, gameObject);
                        break;
                    case "BtnAgitLvl3":
                        scboll = true;
                        Ss.PerkLvlUp(23, gameObject);
                        break;
                    case "BtnRedArmy":
                        scboll = true;
                        Ss.PerkLvlUp(24, gameObject);
                        break;
                    case "BtnMoneyLvl1":
                        scboll = true;
                        Ss.PerkLvlUp(25, gameObject);
                        break;
                    case "BtnMoneyLvl2":
                        scboll = true;
                        Ss.PerkLvlUp(26, gameObject);
                        break;
                    case "BtnMoneyLvl3":
                        scboll = true;
                        Ss.PerkLvlUp(27, gameObject);
                        break;
                    case "BtnPartyLocLvl1":
                        scboll = true;
                        Ss.PerkLvlLocUp(1, gameObject, null);
                        break;
                    case "BtnPartyLocLvl2":
                        scboll = true;
                        Ss.PerkLvlLocUp(2, gameObject, null);
                        break;
                    case "BtnPartyLocLvl3":
                        scboll = true;
                        Ss.PerkLvlLocUp(3, gameObject, null);
                        break;
                    case "BtnPartyLocLvl4":
                        scboll = true;
                        Ss.PerkLvlLocUp(4, gameObject, null);
                        break;
                    case "BtnRedArmyLoc":
                        scboll = true;
                        Ss.PerkLvlLocUp(5, gameObject, null);
                        break;
                    case "BtnPropLocLvl0":
                        scboll = true;
                        Ss.PerkLvlLocUp(6, gameObject, null);
                        break;
                    case "BtnPropLocLvl1":
                        scboll = true;
                        Ss.PerkLvlLocUp(7, gameObject, null);
                        break;
                    case "BtnPropLocLvl2":
                        scboll = true;
                        Ss.PerkLvlLocUp(8, gameObject, null);
                        break;
                    case "BtnPropLocLvl3":
                        scboll = true;
                        Ss.PerkLvlLocUp(9, gameObject, null);
                        break;
                    case "BtnMoneyLocLvl1":
                        scboll = true;
                        Ss.PerkLvlLocUp(10, gameObject, null);
                        break;
                    case "BtnMoneyLocLvl2":
                        scboll = true;
                        Ss.PerkLvlLocUp(11, gameObject, null);
                        break;
                    case "BtnShadowLoc":
                        scboll = true;
                        Ss.PerkLvlLocUp(12, gameObject, null);
                        break;
                    case "BtnUnityLoc":
                        scboll = true;
                        Ss.PerkLvlLocUp(13, gameObject, null);
                        break;
                    case "ButCheckColorAdhs":
                        scboll = true;
                        Ss.ViewMode = true;
                        Ss.ColorSetAdhs();
                        break;
                    case "ButCheckColorSimp":
                        scboll = true;
                        Ss.ColorSetDefault();
                        Ss.ViewMode = false;
                        break;
                    case "ButCheckColorLvlParty":
                        scboll = true;
                        Ss.ColorSetParty();
                        Ss.ViewMode = false;
                        break;
                }
            }
            else
            {
                switch (gameObject.name)
                {
                    case "ButPerkAllRegions":
                        scboll = true;
                        Ss.PerksAll = false;
                        break;
                    case "BtnPartyLocLvl1":
                        scboll = true;
                        Ss.PerksAllRegions(1, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPartyLocLvl2":
                        scboll = true;
                        Ss.PerksAllRegions(2, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPartyLocLvl3":
                        scboll = true;
                        Ss.PerksAllRegions(3, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPartyLocLvl4":
                        scboll = true;
                        Ss.PerksAllRegions(4, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnRedArmyLoc":
                        scboll = true;
                        Ss.PerksAllRegions(5, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPropLocLvl0":
                        scboll = true;
                        Ss.PerksAllRegions(6, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPropLocLvl1":
                        scboll = true;
                        Ss.PerksAllRegions(7, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPropLocLvl2":
                        scboll = true;
                        Ss.PerksAllRegions(8, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnPropLocLvl3":
                        scboll = true;
                        Ss.PerksAllRegions(9, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnMoneyLocLvl1":
                        scboll = true;
                        Ss.PerksAllRegions(10, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnMoneyLocLvl2":
                        scboll = true;
                        Ss.PerksAllRegions(11, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnShadowLoc":
                        scboll = true;
                        Ss.PerksAllRegions(12, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "BtnUnityLoc":
                        scboll = true;
                        Ss.PerksAllRegions(13, gameObject);
                        Ss.PerksAll = false;
                        break;
                    case "CloseHelp":
                        HS.HideHelp();
                        break;
                }
            }
        }
        switch (gameObject.name)
        {
            case "PauseBut":
                scboll = true;
              //  Ms.PauseSet();
                if (PlayerPrefs.GetString("SoundVal") != "No") gameObject.GetComponent<AudioSource>().Play();
                break;
            case "Home":
                scboll = true;
                SceneManager.LoadScene(0);
                break;
            case "Restart":
                SceneManager.LoadScene(1);
                Time.timeScale = 1;
                break;
        }
    }
    void OnMouseUp()
    {
        flagCam = false;
        if (SceneManager.GetActiveScene().name == "MainScene") Message.text = "";
        
    }
    void OnMouseDown()
    {
        switch (gameObject.name)
        {
            case "CamZUp":
                scboll = true;
                NewPosCam = Vector3.zero;
                NewPosCam.z += 3;
                flagCam = true;
                break;
            case "CamZDown":
                scboll = true;
                NewPosCam = Vector3.zero;
                NewPosCam.z -= 3;
                flagCam = true;
                break;
            case "CamUp":
                scboll = true;
                NewPosCam = Vector3.zero;
                NewPosCam.y += 3;
                flagCam = true;
                break;
            case "CamDown":
                scboll = true;
                NewPosCam = Vector3.zero;
                NewPosCam.y -= 3;
                flagCam = true;
                break;
            case "CamLeft":
                scboll = true;
                NewPosCam = Vector3.zero;
                NewPosCam.x -= 3;
                flagCam = true;
                break;
            case "CamRight":
                scboll = true;
                NewPosCam = Vector3.zero;
                NewPosCam.x += 3;
                flagCam = true;
                break;
            case "BtnLvlLid1":
                scboll = true;
                Message.text = "Лидерские качества первого уровня. Требуется: " + Ss.Perks.LvlLid1.LearnCurrent +" / " + Ss.Perks.LvlLid1.LearnMax + "Знаний";
                break;
            case "BtnLvlLid2":
                scboll = true;
                Message.text = "Лидерские качества второго уровня. Требуется: " + Ss.Perks.LvlLid2.LearnCurrent1 + " / " + Ss.Perks.LvlLid2.LearnMax1 + "Знаний"+ Ss.Perks.LvlLid2.LearnCurrent2 + " / " + Ss.Perks.LvlLid2.LearnMax2 + "Пропаганды";
                break;
            case "BtnLvlLid3":
                scboll = true;
                Message.text = "Лидерские качества третьего уровня. Требуется: " + Ss.Perks.LvlLid3.LearnCurrent1 + " / " + Ss.Perks.LvlLid3.LearnMax1 + "Знаний" + Ss.Perks.LvlLid3.LearnCurrent2 + " / " + Ss.Perks.LvlLid3.LearnMax2 + "Конспирации";
                break;
            case "BtnShadowLvl1":
                scboll = true;
                Message.text = "Конспирационные способности первого уровня. Требуется: " + Ss.Perks.LvlShadow1.LearnCurrent + " / " + Ss.Perks.LvlShadow1.LearnMax + " Конспирации";
                break;
            case "BtnShadowLvl2":
                scboll = true;
                Message.text = "Конспирационные способности второго уровня. Требуется: " + Ss.Perks.LvlShadow2.LearnCurrent + " / " + Ss.Perks.LvlShadow2.LearnMax + " Конспирации";
                break;
            case "BtnShadowLvl3":
                scboll = true;
                Message.text = "Конспирационные способности третьего уровня. Требуется: " + Ss.Perks.LvlShadow3.LearnCurrent + " / " + Ss.Perks.LvlShadow3.LearnMax + " Конспирации";
                break;
            case "BtnPassiveMoneyLvl1":
                scboll = true;
                Message.text = "Пассивный доход первого уровня. Требуется: " + Ss.Perks.LvlPassiveMoney1.LearnCurrent + " / " + Ss.Perks.LvlPassiveMoney1.LearnMax + " Заработок";
                break;
            case "BtnPassiveMoneyLvl2":
                scboll = true;
                Message.text = "Пассивный доход второго уровня. Требуется: " + Ss.Perks.LvlPassiveMoney2.LearnCurrent + " / " + Ss.Perks.LvlPassiveMoney2.LearnMax + " Заработок";
                break;
            case "BtnPropLvl1":
                scboll = true;
                Message.text = "Пропагандисткие способности первого уровня. Требуется: " + Ss.Perks.LvlProp1.LearnCurrent + " / " + Ss.Perks.LvlProp1.LearnMax + " Пропаганды";
                break;
            case "BtnPropLvl2":
                scboll = true;
                Message.text = "Пропагандисткие способности второго уровня. Требуется: " + Ss.Perks.LvlProp1.LearnCurrent + " / " + Ss.Perks.LvlProp1.LearnMax + " Пропаганды"; break;
            case "BtnLvlParty1":
                scboll = true;
                Message.text = "Партия первого уровня. Требуется: " + Ss.Perks.LvlParty1.LearnCurrent + " / " + Ss.Perks.LvlParty1.LearnMax + " Партостроительства";
                break;
            case "BtnLvlParty2":
                scboll = true;
                Message.text = "Партия второго уровня. Требуется: " + Ss.Perks.LvlParty2.LearnCurrent1 + " / " + Ss.Perks.LvlParty2.LearnMax1 + " Партостроительства, " + Ss.Perks.LvlParty2.LearnCurrent2 + " / " + Ss.Perks.LvlParty2.LearnMax2 + " Знаний ";
                break;
            case "BtnLvlParty3":
                scboll = true;
                Message.text = "Партия третьего уровня. Требуется: " + Ss.Perks.LvlParty3.LearnCurrent1 + " / " + Ss.Perks.LvlParty3.LearnMax1 + " Партостроительства, " + Ss.Perks.LvlParty3.LearnCurrent2 + " / " + Ss.Perks.LvlParty3.LearnMax2 + " Конспирации ";
                break;
            case "BtnLvlParty4":
                scboll = true;
                Message.text = "Партия четвертого уровня. Требуется: " + Ss.Perks.LvlParty4.LearnCurrent1 + " / " + Ss.Perks.LvlParty4.LearnMax1 + " Партостроительства, " + Ss.Perks.LvlParty4.LearnCurrent2 + " / " + Ss.Perks.LvlParty4.LearnMax2 + " Пропаганды ";
                break;
            case "BtnUnityLvl1":
                scboll = true;
                Message.text = "Единство первого уровня. Требуется: " + Ss.Perks.LvlUnity1.LearnCurrent + " / " + Ss.Perks.LvlUnity1.LearnMax + " Партостроительства";
                break;
            case "BtnUnityLvl2":
                scboll = true;
                Message.text = "Единство второго уровня. Требуется: " + Ss.Perks.LvlUnity2.LearnCurrent + " / " + Ss.Perks.LvlUnity2.LearnMax + " Партостроительства";
                break;
            case "BtnUnityLvl3":
                scboll = true;
                Message.text = "Единство третьего уровня. Требуется: " + Ss.Perks.LvlUnity3.LearnCurrent + " / " + Ss.Perks.LvlUnity3.LearnMax + " Партостроительства";
                break;
            case "BtnLearnLvl1":
                scboll = true;
                Message.text = "Единство первого уровня. Требуется: " + Ss.Perks.LvlLearn1.LearnCurrent + " / " + Ss.Perks.LvlLearn1.LearnMax + " Знаний";
                break;
            case "BtnLearnLvl2":
                scboll = true;
                Message.text = "Единство второго уровня. Требуется: " + Ss.Perks.LvlLearn2.LearnCurrent + " / " + Ss.Perks.LvlLearn2.LearnMax + " Знаний";
                break;
            case "BtnLearnLvl3":
                scboll = true;
                Message.text = "Единство третьего уровня. Требуется: " + Ss.Perks.LvlLearn3.LearnCurrent + " / " + Ss.Perks.LvlLearn3.LearnMax + " Знаний";
                break;
            case "BtnAgitLvl1":
                scboll = true;
                Message.text = "Пропаганда первого уровня. Требуется: " + Ss.Perks.LvlAgit1.LearnCurrent + " / " + Ss.Perks.LvlAgit1.LearnMax + " Пропаганды";
                break;
            case "BtnAgitLvl2":
                scboll = true;
                Message.text = "Пропаганда второго уровня. Требуется: " + Ss.Perks.LvlAgit2.LearnCurrent + " / " + Ss.Perks.LvlAgit2.LearnMax + " Пропаганды";
                break;
            case "BtnAgitLvl3":
                scboll = true;
                Message.text = "Пропаганда третьего уровня. Требуется: " + Ss.Perks.LvlAgit3.LearnCurrent + " / " + Ss.Perks.LvlAgit3.LearnMax + " Пропаганды";
                break;
            case "BtnRedArmy":
                scboll = true;
                Message.text = "Создание красной армии. Требуется: " + Ss.Perks.RedArmy.LearnCurrent1 + " / " + Ss.Perks.RedArmy.LearnMax1 + " Партостроя" + Ss.Perks.RedArmy.LearnCurrent2 + " / " + Ss.Perks.RedArmy.LearnMax2 + " Заработка" + Ss.Perks.RedArmy.LearnCurrent3 + " / " + Ss.Perks.RedArmy.LearnMax3 + " Пропаганды";
                break;
            case "BtnMoneyLvl1":
                scboll = true;
                Message.text = "Доход первого уровня. Требуется: " + Ss.Perks.LvlMoney1.LearnCurrent + " / " + Ss.Perks.LvlMoney1.LearnMax + " Заработок";
                break;
            case "BtnMoneyLvl2":
                scboll = true;
                Message.text = "Доход второго уровня. Требуется: " + Ss.Perks.LvlMoney2.LearnCurrent + " / " + Ss.Perks.LvlMoney2.LearnMax + " Заработок";
                break;
            case "BtnMoneyLvl3":
                scboll = true;
                Message.text = "Доход третьего уровня. Требуется: " + Ss.Perks.LvlMoney3.LearnCurrent + " / " + Ss.Perks.LvlMoney3.LearnMax + " Заработок";
                break;
            case "BtnPartyLocLvl1":
                scboll = true;
                Message.text = "Уровень партии в регионе первого уровня. Требуется: " + Ss.current.PerksLoc.LvlLocParty1.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocParty1.LearnMax + " Партостроительства";
                break;
            case "BtnPartyLocLvl2":
                scboll = true;
                Message.text = "Уровень партии в регионе второго уровня. Требуется: " + Ss.current.PerksLoc.LvlLocParty2.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocParty2.LearnMax + " Советостроительства";
                break;
            case "BtnPartyLocLvl3":
                scboll = true;
                Message.text = "Уровень партии в регионе третьего уровня. Требуется: " + Ss.current.PerksLoc.LvlLocParty3.LearnCurrent1 + " / " + Ss.current.PerksLoc.LvlLocParty3.LearnMax1 + " Советостроительства" + Ss.current.PerksLoc.LvlLocParty3.LearnCurrent2 + " / " + Ss.current.PerksLoc.LvlLocParty3.LearnMax2 + " Набора в партию";
                break;
            case "BtnPartyLocLvl4":
                scboll = true;
                Message.text = "Уровень партии в регионе властногого уровня. Требуется: " + Ss.current.PerksLoc.LvlLocParty4.LearnCurrent1 + " / " + Ss.current.PerksLoc.LvlLocParty4.LearnMax1 + " Советостроительства" + Ss.current.PerksLoc.LvlLocParty4.LearnCurrent2 + " / " + Ss.current.PerksLoc.LvlLocParty4.LearnMax2 + " Конспирацию";
                break;
            case "BtnRedArmyLoc":
                scboll = true;
                Message.text = "Создание красной армии в регионе. Требуется: " + Ss.current.PerksLoc.LocRedArmy.LearnCurrent1 + " / " + Ss.current.PerksLoc.LocRedArmy.LearnMax1 + " Набора в партию" + Ss.current.PerksLoc.LocRedArmy.LearnCurrent2 + " / " + Ss.current.PerksLoc.LocRedArmy.LearnMax2 + " Советостроения";
                break;
            case "BtnPropLocLvl0":
                scboll = true;
                Message.text = "Уровень агитации в регионе первого уровня. Требуется: " + Ss.current.PerksLoc.LvlLocProp0.LearnCurrent1 + " / " + Ss.current.PerksLoc.LvlLocProp0.LearnMax1 + " Агитации" + Ss.current.PerksLoc.LvlLocProp0.LearnCurrent2 + " / " + Ss.current.PerksLoc.LvlLocProp0.LearnMax2 + " Набора в партию";
                break;
            case "BtnPropLocLvl1":
                scboll = true;
                Message.text = "Уровень агитации в регионе второго уровня. Требуется: " + Ss.current.PerksLoc.LvlLocProp1.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocProp1.LearnMax + " Агитации";
                break;
            case "BtnPropLocLvl2":
                scboll = true;
                Message.text = "Уровень агитации в регионе третьего уровня. Требуется: " + Ss.current.PerksLoc.LvlLocProp2.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocProp2.LearnMax + " Агитации";
                break;
            case "BtnPropLocLvl3":
                scboll = true;
                Message.text = "Уровень агитации в регионе четвертого уровня. Требуется: " + Ss.current.PerksLoc.LvlLocProp3.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocProp3.LearnMax + " Агитации";
                break;
            case "BtnMoneyLocLvl1":
                scboll = true;
                Message.text = "Доход первого уровня. Требуется: " + Ss.current.PerksLoc.LvlLocMoney1.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocMoney1.LearnMax + " Заработок";
                break;
            case "BtnMoneyLocLvl2":
                scboll = true;
                Message.text = "Доход второго уровня. Требуется: " + Ss.current.PerksLoc.LvlLocMoney2.LearnCurrent + " / " + Ss.current.PerksLoc.LvlLocMoney2.LearnMax + " Заработок";
                break;
            case "BtnShadowLoc":
                scboll = true;
                Message.text = "Конспирация ячейки партии. Требуется: " + Ss.current.PerksLoc.LocShadow.LearnCurrent + " / " + Ss.current.PerksLoc.LocShadow.LearnMax + " Конспирации";
                break;
            case "BtnUnityLoc":
                scboll = true;
                Message.text = "Единство ячейки партии. Требуется: " + Ss.current.PerksLoc.LocUnity.LearnCurrent + " / " + Ss.current.PerksLoc.LocUnity.LearnMax + "Советостроения ";
                break;
        }
        /*switch (gameObject.name)
        {
            case "PlusCityReg":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = "Усилить регенерацию щита городов.";
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = "Increase the strength of the city shield restoration.";
                break;
            case "PlusIncome":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = ("Увеличить выработку ресурсов городами");
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = ("Increase the generation of resources by cities");
                break;
            case "PlusPowOrb":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = "Увеличить силу главного лазера.";
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = "Increase the strength of the main laser.";
                break;
            case "PlusBattOrb":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = ("Улучшить солнечные батареи спутника. Увеличивает скорость восстановления заряда.");
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = ("Improve satellite solar arrays. Increases the rate of charge recovery.");
                break;
            case "ButLasertPlus":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = ("Создать дополнительный автоматический лазер(наносит в три раза меньше урона). Отключаются нажатием на шкалу орбитальной энергии.");
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = ("Create an additional automatic laser (deals three times less damage). Disable by clicking on the scale of orbital energy.");
                break;
            case "ButRaketPlus":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = ("Купить ракету. Наносит " + 100 * Ms.LvlCR + " урона.");
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = ("Buy a rocket. Deals " + 100 * Ms.LvlCR + " damage.");
                break;
            case "ActiveLaz":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = ("Включить/выключить автоматические лазеры.");
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = ("Enable / disable automatic lasers.");
                break;
            case "PauseBut":
                if (PlayerPrefs.GetString("Localiz") == "RU")
                    Message.text = ("Пауза.");
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                    Message.text = ("Pause.");
                break;
            case "InverseLazer":
                if (Ms.LazNP == true) Napr = "Разрушение";
                if (Ms.LazNP == false) Napr = "Передача энергии";
                if (PlayerPrefs.GetString("Localiz") == "RU")
                {
                    if (Ms.LazNP == true) Napr = "Разрушение";
                    if (Ms.LazNP == false) Napr = "Передача энергии";
                    Message.text = ("Инверсировать главный лазер для передачи энергии городам. Текущее состояние: " + Napr);
                }
                else if (PlayerPrefs.GetString("Localiz") == "EN")
                {
                    if (Ms.LazNP == true) Napr = "Destroy";
                    if (Ms.LazNP == false) Napr = "Energy transfer";
                    Message.text = ("Invert the main laser to transmit energy to cities. Current state:" + Napr);
                }
                break;
            case "Home":
                SceneManager.LoadScene("StartScene");
                Time.timeScale = 1;
                break;
        }*/
    }
}
