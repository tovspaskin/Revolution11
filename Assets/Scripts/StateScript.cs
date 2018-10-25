using UnityEngine;
using UnityEngine.UI;

public class StateScript : MonoBehaviour
{
    public static StateScript State;
    public GameObject Top;
    public GameObject reg_sel;
    public SelectRegion[] Regions;
    public GameObject[] UiElems;
    public GameObject[] UiParty;
    public GameObject[] UiReg;
    public GameObject[] UiSettleLider;
    public Sprite[] UiIm;
    public float Impact, Money;
    bool SelRegOrig = false;
    public SelectRegion current;
    public LiderClass Lider = new LiderClass();
    public PartyClass Party = new PartyClass();
    public PerkTree Perks = new PerkTree();
    public enum PartyActionEn { Agit, Sov, Rec, Money, Hide };
    public enum LiderActionEn { Agit, Part, Hide, Money, Learn };
    enum  MapMode {Simp, AdhsEnem, PartyLvl };
    public enum PerkActionEn { Money, DeepAgit, Unity, Pow, SocNet, LidMoney, Shadow, Speak };
    public bool CivilWar = false;
    public Image[] icons;
    public Image[] icons_loc;
    public Sprite[] pictures;
    GameObject LastIconsFrame = null;
    public GameObject LastIconsFrameLoc = null;
    public bool PerksAll = false;
    public float ComAgit, ComSov, ComRec, ComBiz, ComHid = 0;
    public float ComAct = 10;
    float ComSumSov = 0;
    public bool ViewMode = false;
    MapMode mapMode = MapMode.Simp;
    HelpScript Hs;

    private void Awake()
    {
        Regions = GetComponentsInChildren<SelectRegion>();
        State = this;
        Perks.Ss = this;
        ComAct = 10;
        Lider.SetName();
        Lider.Ss = this;
        Hs = GameObject.Find("WindowHelp").GetComponent<HelpScript>();
    }
    void Start()
    {
        Perks.SetUI();
        Perks.CanvelLearn();
        
    }
    void Update()
    {

    }
    public void HelpSettleLider(bool val)
    {
        UiSettleLider[0].GetComponent<Buttons>().scboolConst = val;
        UiSettleLider[1].GetComponent<Buttons>().scboolConst = val;
        if (val == true)
        {
            UiSettleLider[0].GetComponent<Buttons>().scboll = val;
            UiSettleLider[1].GetComponent<Buttons>().scboll = val;
        }
    }
    public void Select(GameObject Sel, string Name, string Pop, string Netral, string Enem, string Adhs)
    {
        //if (reg_sel != null) reg_sel.GetComponent<Renderer>().material.color = Color.white;
        if (reg_sel != null) reg_sel.GetComponent<SelectRegion>().StopPuls();
        reg_sel = Sel;
        UiReg[0].GetComponent<Text>().text = Name;
        UiReg[1].GetComponent<Text>().text = "Население: " + Pop;
        UiReg[2].GetComponent<Text>().text = "Коммунисты: " + Adhs;
        UiReg[3].GetComponent<Text>().text = "Конформисты: " + Netral;
        UiReg[4].GetComponent<Text>().text = "Капиталисты: " + Enem;
        //reg_sel.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        if (reg_sel != null) reg_sel.GetComponent<SelectRegion>().scboll = true;
        current = reg_sel.GetComponent<SelectRegion>();
        icons_loc[14].gameObject.SetActive(current.CommonPar);
        if (SelRegOrig == false) HelpSettleLider(true);
        SetComPAr();
    }
    public void UpdateEveryDay()
    {
        if (current != null)
        {
            int NumInReg = 0;
            int ChangeNum = 0;
            float locEx = 0;
            float sumlocHid = 0;
            float sumlocBiz = 0;
            float sumRegBiz = 0;
            float sumUnity = 0;
            float sumSov = 0;
            int sumPop = 0, sumCom = 0;
            int PerksLocRez = 0;
            Perks.LearnProsess(Lider.Agit, Lider.Money, Lider.PartyBuild, Lider.Learn, Lider.Hide);
            Party.Warriors = 0;
            float RegExtrim = 0;
            foreach (SelectRegion _sel in Regions)
            {
                NumInReg += _sel.PartyMen;
                ChangeNum -= _sel.PartyMen;
                _sel.LocalUpdate();
                ChangeNum += _sel.PartyMen;
                locEx += _sel.GetLocalExtrimFactor();
                sumPop += _sel.Pop;
                sumCom += _sel.Adhs;
                sumUnity += (Lider.PartyBuild * (1 + Party.Extrim) * (1+Perks.GetLvlUnity())-_sel.PartyLoc.Money*2);
                sumlocBiz += (int)_sel.PartyLoc.Money;
                sumlocHid += _sel.PartyLoc.Hide*(_sel.PerksLoc.LvlLocParty1.Resault+_sel.PerksLoc.LocShadow.Resault);
                RegExtrim += (((_sel.PartyLoc.Agit + _sel.PartyLoc.Rec) * ((float)_sel.Enem / _sel.Adhs))-_sel.PartyLoc.Hide*(1+_sel.PerksLoc.LocShadow.Resault*3));
                sumSov += _sel.PartyLoc.Sovet;
                sumRegBiz += (int)_sel.PartyLoc.Money * _sel.PartyMen;
                PerksLocRez += (int)current.PerksLoc.PerksLocSum();
                if (_sel.PerksLoc.LocRedArmy.Resault == 1) CivilWar = true;
                Party.Warriors += _sel.Warriors;
                if (ViewMode == true) _sel.gameObject.GetComponent<Renderer>().material.color = new Color((float)_sel.Adhs/ (float)_sel.Pop, 0, 0);
            }
            Party.SetNumAllReg(NumInReg);
            ComSumSov += sumSov;
            Party.Sovets += (sumSov/(Mathf.Clamp(ComSumSov-(Party.GetNumAllReg()/1000), 1, 100)));
            RegExtrim -= Lider.ExpHide;
            Party.AccelerationExtrim = Mathf.Clamp(RegExtrim+(Lider.Agit+Lider.PartyBuild-Lider.Hide*(1+Perks.LvlShadow1.Resault + Perks.LvlShadow2.Resault + Perks.LvlShadow3.Resault))*GetPartyFilials()*(1-Impact), -0.005f, 0.003f);
            Party.ChangeExtrim += Party.AccelerationExtrim;
            Party.ChangeExtrim = Mathf.Clamp(Party.ChangeExtrim, -0.04f, 0.02f);
            Party.Extrim += Party.ChangeExtrim;
            Party.Extrim = Mathf.Clamp(Party.Extrim,0,1);
            //Mathf.Clamp(Party.Extrim + Mathf.Pow(0.02f * (locEx + 3 * (Lider.Agit + Lider.PartyBuild)), 0.5f) - Mathf.Pow(0.0335f * (sumlocHid + 2 * Lider.Hide), 0.5f), 0, 1));
            Money += sumRegBiz + (1+Lider.Money) * (1 + Lider.ExpMoney*Perks.GetLvlMoney()*50)+Lider.ExpMoney* Lider.Money- GetPartyFilials()*100;           
            //Money += (int)((Mathf.Pow(2, (0.022f * (sumRegBiz + Lider.Money))) - 1) + 2 * (sumRegBiz + Lider.Money) + 10 * (Mathf.Log(5 * (sumRegBiz + Lider.Money) + 1, 1.5f)) + (sumRegBiz + Lider.Money) * (Mathf.Pow(2, 0.001f * Lider.ExpMoney) - 1) * (1 + Perks.LvlMoney1.Resault + Perks.LvlMoney2.Resault + Perks.LvlMoney3.Resault));
            float Pr = (1 + Perks.LvlUnity1.Resault + Perks.LvlUnity2.Resault + Perks.LvlUnity3.Resault);
            //float pUnity = sumUnity - (ChangeNum / (1+Party.GetNumAllReg()));
            float ChangeUnity = Mathf.Clamp(sumUnity - ChangeNum / (1 + Party.GetNumAllReg()),-0.01f, 0.01f);
            Party.Unity = Mathf.Clamp(Party.Unity+ChangeUnity, 0, 0.25f * Pr);
            //Party.Unity = Mathf.Clamp((Party.Unity + Lider.PartyBuild * 0.1f + Lider.PartyBuild * ((Mathf.Pow(2, 0.0001f * Lider.ExpImp) - 1)) - (Mathf.Clamp(ChangeNum / (10 + Party.GetNumAllReg()), 0.001f / Pr, 1))), 0, 0.25f * Pr);
            Impact = (sumCom/sumPop)*0.3f+(GetPartyFilials()/(84*4))*0.7f;
            UpdateDiagram();
            Lider.UpdExp();
            Lider.SetFullInfoLider();
            UpdateInfo(current);
            UiElems[3].GetComponent<Text>().text = "Деньги: " + Mathf.Round(Money).ToString();
            UiElems[4].GetComponent<Text>().text = "Влияение: " + Round2(Impact).ToString();
            SetComPAr();
            switch (mapMode)
            {
                case MapMode.Simp:
                    ColorSetDefault();
                    break;
                case MapMode.AdhsEnem:
                    ColorSetAdhs();
                    break;
                case MapMode.PartyLvl:
                    ColorSetParty();
                    break;
            }
            GameFinish();
        }
    }

    void GameFinish()
    {
        if (Party.Extrim >= 1) { Hs.act = HelpScript.Acts.fail; Hs.SetActiveHelp(pictures[0],"Вас нашли и поймали! Буржуазия остановила революцию в этот раз. Против исторического процесса не пойдешь, но вы этого уже не увидите..."); }
        if (Impact >= 0.97f) { Hs.act = HelpScript.Acts.victory; Hs.SetActiveHelp(pictures[1], "Победа, товарищи! Победа! Буржуи свергнуты, власти капитала пришел конец! Новая диктатура пролеталиата поведет страну и мир путеводной красной звездой к светлому будущему!"); }
    }

    public void ColorSetDefault()
    {
        mapMode = MapMode.Simp;
        foreach (SelectRegion _sel in Regions)
        {
           _sel.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    public void ColorSetAdhs()
    {
        mapMode = MapMode.AdhsEnem;
        foreach (SelectRegion _sel in Regions)
        {
            _sel.gameObject.GetComponent<Renderer>().material.color = new Color (((float)_sel.Adhs/_sel.Pop), 0, 0);
        }
    }

    public void ColorSetParty()
    {
        mapMode = MapMode.PartyLvl;
        foreach (SelectRegion _sel in Regions)
        {
            _sel.gameObject.GetComponent<Renderer>().material.color = new Color(0, _sel.PerksLoc.GetPartyLoc()*0.25f, 0);
        }
    }

    public int GetPartyFilials()
    {
        int val = 0;
        foreach (SelectRegion _sel in Regions)
        {
           val += _sel.PerksLoc.LvlLocParty1.Resault + _sel.PerksLoc.LvlLocParty2.Resault + _sel.PerksLoc.LvlLocParty3.Resault + _sel.PerksLoc.LvlLocParty4.Resault;
        }
        return val;
    }
    public void ComParVoid( bool value)
    {
        if (value)
        {
            icons_loc[14].gameObject.SetActive(true);
        }
        else { icons_loc[14].gameObject.SetActive(false); }
        current.CommonPar = value;
       
    }
    public void EveryMounth()
    {
        Money += (int)(Money * 0.01 * (Perks.LvlPassiveMoney1.Resault + Perks.LvlPassiveMoney2.Resault));
    }
    public void UpdateDiagram()
    {
        if (current != null)
        {
            UiReg[0].GetComponent<Text>().text = current.NameReg;
            UiReg[1].GetComponent<Text>().text = "Население: " + current.Pop;
            UiReg[2].GetComponent<Text>().text = "Коммунисты: " + current.Adhs;
            UiReg[3].GetComponent<Text>().text = "Конформисты: " + current.Netral;
            UiReg[4].GetComponent<Text>().text = "Капиталисты: " + current.Enem;
            UiReg[5].GetComponent<Text>().text = "Члены партии: " + current.PartyMen;
            UiReg[9].GetComponent<Text>().text = "Бойцы: " + current.Warriors;
            UiReg[6].GetComponent<Image>().fillAmount = ((float)current.Adhs / current.Pop);
            UiReg[7].GetComponent<Image>().fillAmount = (((float)current.Adhs / current.Pop)) + (((float)current.Netral / current.Pop));
        }
    }
    // p1d0r@sgeits
    public void SettleLider()
    {
        if (SelRegOrig == false)
        {
            Lider.SetRegionLider(current);
            Lider.SetFullInfoLider();
            SelRegOrig = true;
            HelpSettleLider(false);
        }
        else
        {
            if (Money >= 100000)
            {
                Lider.SetRegionLider(current);
                Lider.SetFullInfoLider();
                Money -= 10000;
            }
        }
    }
    public void PartyActionsUse(bool Polyrn, PartyActionEn Pae)
    {
        if (current != null & current.PerksLoc.LvlLocParty1.Resault == 1)
        {
            UpdateComPar(current);
            switch (Pae)
            {
                case PartyActionEn.Agit:
                    if (Polyrn == true) { if (current.PartyLoc.GetActionParty() > 0) { current.PartyLoc.Agit++; current.PartyLoc.AddActionParty(-1); } }
                    else { if (current.PartyLoc.Agit > 0) { current.PartyLoc.Agit--; current.PartyLoc.AddActionParty(1); } }
                    break;
                case PartyActionEn.Money:
                    if (Polyrn == true) { if (current.PartyLoc.GetActionParty() > 0) { current.PartyLoc.Money++; current.PartyLoc.AddActionParty(-1); } }
                    else { if (current.PartyLoc.Money > 0) { current.PartyLoc.Money--; current.PartyLoc.AddActionParty(1); } }
                    break;
                case PartyActionEn.Sov:
                    if (Polyrn == true) { if (current.PartyLoc.GetActionParty() > 0) { current.PartyLoc.Sovet++; current.PartyLoc.AddActionParty(-1); } }
                    else { if (current.PartyLoc.Sovet > 0) { current.PartyLoc.Sovet--; current.PartyLoc.AddActionParty(1); } }
                    break;
                case PartyActionEn.Rec:
                    if (Polyrn == true) { if (current.PartyLoc.GetActionParty() > 0) { current.PartyLoc.Rec++; current.PartyLoc.AddActionParty(-1); } }
                    else { if (current.PartyLoc.Rec > 0) { current.PartyLoc.Rec--; current.PartyLoc.AddActionParty(1); } }
                    break;
                case PartyActionEn.Hide:
                    if (Polyrn == true) { if (current.PartyLoc.GetActionParty() > 0) { current.PartyLoc.Hide++; current.PartyLoc.AddActionParty(-1); } }
                    else { if (current.PartyLoc.Hide > 0) { current.PartyLoc.Hide--; current.PartyLoc.AddActionParty(1); } }
                    break;
            }
            UpdateComPar(current);
            SetComPAr();
            UpdateInfo(current);
        }
    }
    public void LiderActionsUse(bool Polyrn, LiderActionEn Pae)
    {
        if (Lider.GetRegionWithLider() != null)
        {
            switch (Pae)
            {
                case LiderActionEn.Agit:
                    if (Polyrn == true) { if (Lider.GetActionLider() > 0) { Lider.Agit++; Lider.SetActionLider(-1); } }
                    else { if (Lider.Agit > 0) { Lider.Agit--; Lider.SetActionLider(1); } }
                    break;
                case LiderActionEn.Money:
                    if (Polyrn == true) { if (Lider.GetActionLider() > 0) { Lider.Money++; Lider.SetActionLider(-1); } }
                    else { if (Lider.Money > 0) { Lider.Money--; Lider.SetActionLider(1); } }
                    break;
                case LiderActionEn.Part:
                    if (Polyrn == true) { if (Lider.GetActionLider() > 0) { Lider.PartyBuild++; Lider.SetActionLider(-1); } }
                    else { if (Lider.PartyBuild > 0) { Lider.PartyBuild--; Lider.SetActionLider(1); } }
                    break;
                case LiderActionEn.Hide:
                    if (Polyrn == true) { if (Lider.GetActionLider() > 0) { Lider.Hide++; Lider.SetActionLider(-1); } }
                    else { if (Lider.Hide > 0) { Lider.Hide--; Lider.SetActionLider(1); } }
                    break;
                case LiderActionEn.Learn:
                    if (Polyrn == true) { if (Lider.GetActionLider() > 0) { Lider.Learn++; Lider.SetActionLider(-1); } }
                    else { if (Lider.Learn > 0) { Lider.Learn--; Lider.SetActionLider(1); } }
                    break;
            }
            Lider.SetFullInfoLider();
        }
    }
    public void UpdateInfo(SelectRegion SrCurr)
    {
        if (SrCurr != null)
        {
            UiParty[4].GetComponent<Text>().text = "Очки действия: " + SrCurr.PartyLoc.GetActionParty().ToString();
            UiParty[3].GetComponent<Text>().text = "Агитация: " + SrCurr.PartyLoc.Agit.ToString();
            UiParty[0].GetComponent<Text>().text = "Создание советов: " + Round2(SrCurr.PartyLoc.Sovet).ToString();
            UiParty[1].GetComponent<Text>().text = "Набор членов: " + SrCurr.PartyLoc.Rec.ToString();
            UiParty[2].GetComponent<Text>().text = "Взносы: " + Round2(SrCurr.PartyLoc.Money).ToString();
            UiParty[14].GetComponent<Text>().text = "Подполье: " + SrCurr.PartyLoc.Hide.ToString();
            UiParty[5].GetComponent<Text>().text = "В партии: " + Party.GetNumAllReg().ToString();
            UiParty[6].GetComponent<Text>().text = Round2(Party.Extrim * 100).ToString() + "%";
            UiParty[7].GetComponent<Text>().text = "Советы: " + Round2(Party.Sovets).ToString();
            UiParty[9].GetComponent<Image>().fillAmount = Party.Extrim;
            UiParty[17].GetComponent<Image>().fillAmount = Party.Unity;
            UiParty[16].GetComponent<Text>().text = Round2(Party.Unity * 100).ToString() + "%";
            UiParty[8].GetComponent<Text>().text = "Статус партии: " + GetStatusText();
            UiParty[19].GetComponent<Text>().text = "Бойцы: " + Party.Warriors;
        }
    }
    public void UpdateComPar(SelectRegion SrCurr)
    {
        if (SrCurr != null)
        {
            if (SrCurr.CommonPar == true & SrCurr.PerksLoc.LvlLocParty1.Resault > 0)
            {
                ComAgit = SrCurr.PartyLoc.Agit;
                ComSov = SrCurr.PartyLoc.Sovet;
                ComRec = SrCurr.PartyLoc.Rec;
                ComBiz = SrCurr.PartyLoc.Money;
                ComHid = SrCurr.PartyLoc.Hide;
                ComAct = SrCurr.PartyLoc.GetActionParty();

            }
            
        }
    }
    public void SetComPAr() {
        foreach (SelectRegion _sel in Regions)
        {
            if (_sel.CommonPar == true & _sel.PerksLoc.LvlLocParty1.Resault > 0)
            {
                _sel.PartyLoc.Agit = ComAgit;
                _sel.PartyLoc.Sovet = ComSov;
                _sel.PartyLoc.Rec = ComRec;
                _sel.PartyLoc.Money = ComBiz;
                _sel.PartyLoc.Hide = ComHid;
                _sel.PartyLoc.SetActionParty((int)ComAct);
            }
        }
    }
    public static float Round2(float value)
    {
        float I = (value * 100);
        int J = (int)I;
        I = J;
        I = I / 100;
        return (I);
    }
    public string GetStatusText()
    {
        int R = (current.PerksLoc.LvlLocParty1.Resault + current.PerksLoc.LvlLocParty2.Resault + current.PerksLoc.LvlLocParty3.Resault + current.PerksLoc.LvlLocParty4.Resault);
        string rez = "";
        if (R == 0) rez = "Отсуствует";
        if (R == 1) rez = "Группа";
        if (R == 2) rez = "Движение";
        if (R == 3) rez = "Опозиция";
        if (R == 4) rez = "У власти";
        return rez;
    }
    public void MenuOnOff(int num)
    {
        if (current != null)
        {
            Lider.SetFullInfoLider();
            if (UiElems[num].activeSelf == false)
            {
                if (num == 2)
                {
                    UiElems[2].SetActive(true);
                    UiElems[1].SetActive(true);
                }
                else
                { UiElems[num].SetActive(true); }
            }
            else
            {
                if (num == 2)
                {
                    UiElems[2].SetActive(false);
                    UiElems[1].SetActive(false);
                }
                else
                {
                    if (UiElems[2].activeSelf == true) UiElems[2].SetActive(false);
                    else UiElems[1].SetActive(false);
                }
            }
        }
    }
    public void PerkLvlUp(int num, GameObject But)
    {
        switch (num)
        {
            case 1:
                if (Perks.LvlLid1.Resault == 0 & ((Money >= 1000) | Perks.LvlLid1.LearnCurrent > 0))
                {
                    if (Perks.LvlLid1.LearnCurrent == 0) { Money -= 1000; Perks.LvlLid1.LearnCurrent++; }
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                    Perks.CanvelLearn();
                    Perks.LvlLid1.LearnThis = true;
                }
                break;
            case 2:
                if ((Perks.LvlLid1.Resault == 1) & (Perks.LvlLid2.Resault == 0) & ((Money >= 50000) | Perks.LvlLid2.LearnCurrent1 > 0 | Perks.LvlLid2.LearnCurrent2 > 0))
                {
                    if (Perks.LvlLid2.LearnCurrent1 == 0) { Money -= 50000; Perks.LvlLid2.LearnCurrent1++; }
                    Perks.CanvelLearn();
                    Perks.LvlLid2.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 3:
                if ((Perks.LvlLid2.Resault == 1) & (Perks.LvlLid3.Resault == 0) & ((Money >= 100000) | Perks.LvlLid3.LearnCurrent1 > 0 | Perks.LvlLid3.LearnCurrent2 > 0))
                {
                    if (Perks.LvlLid3.LearnCurrent1 == 0) { Money -= 100000; Perks.LvlLid3.LearnCurrent1++; }
                    Perks.CanvelLearn();
                    Perks.LvlLid3.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 4:
                if ((Perks.LvlLid1.Resault == 1) & (Perks.LvlShadow1.Resault == 0) & ((Money >= 5000) | Perks.LvlShadow1.LearnCurrent > 0))
                {
                    if (Perks.LvlShadow1.LearnCurrent == 0) { Money -= 5000; Perks.LvlShadow1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlShadow1.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 5:
                if ((Perks.LvlLid2.Resault == 1) & (Perks.LvlShadow1.Resault == 1) & (Perks.LvlShadow2.Resault == 0) & ((Money >= 75000) | Perks.LvlShadow2.LearnCurrent > 0))
                {
                    if (Perks.LvlShadow2.LearnCurrent == 0) { Money -= 75000; Perks.LvlShadow2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlShadow2.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 6:
                if ((Perks.LvlLid3.Resault == 1) & (Perks.LvlShadow2.Resault == 1) & (Perks.LvlShadow3.Resault == 0) & ((Money >= 1000000) | Perks.LvlShadow3.LearnCurrent > 0))
                {
                    if (Perks.LvlShadow3.LearnCurrent == 0) { Money -= 1000000; Perks.LvlShadow3.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlShadow3.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 7:
                if ((Perks.LvlLid2.Resault == 1) & (Perks.LvlPassiveMoney1.Resault == 0) & ((Money >= 500000) | Perks.LvlPassiveMoney1.LearnCurrent > 0))
                {
                    if (Perks.LvlPassiveMoney1.LearnCurrent == 0) { Money -= 500000; Perks.LvlPassiveMoney1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlPassiveMoney1.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 8:
                if ((Perks.LvlLid3.Resault == 1) & (Perks.LvlPassiveMoney1.Resault == 1) & (Perks.LvlPassiveMoney2.Resault == 0) & ((Money >= 8000000) | Perks.LvlPassiveMoney1.LearnCurrent > 0))
                {
                    if (Perks.LvlPassiveMoney2.LearnCurrent == 0) { Money -= 8000000; Perks.LvlPassiveMoney2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlPassiveMoney2.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 9:
                if ((Perks.LvlLid1.Resault == 1) & (Perks.LvlProp1.Resault == 0) & ((Money >= 5000) | Perks.LvlProp1.LearnCurrent > 0))
                {
                    if (Perks.LvlProp1.LearnCurrent == 0) { Money -= 5000; Perks.LvlProp1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlProp1.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 10:
                if ((Perks.LvlLid2.Resault == 1) & (Perks.LvlProp1.Resault == 1) & (Perks.LvlProp2.Resault == 0) & ((Money >= 50000) | Perks.LvlProp2.LearnCurrent > 0))
                {
                    if (Perks.LvlProp2.LearnCurrent == 0) { Money -= 50000; Perks.LvlProp2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlProp2.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 11:
                if (Perks.LvlParty1.Resault == 0 & ((Money >= 500) | Perks.LvlParty1.LearnCurrent > 0))
                {
                    if (Perks.LvlParty1.LearnCurrent == 0) { Money -= 500; Perks.LvlParty1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlParty1.LearnThis = true;
                    if (LastIconsFrame != null) LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 12:
                if ((Perks.LvlParty2.Resault == 0) & (Perks.LvlParty1.Resault == 1) & ((Money >= 15000) | Perks.LvlParty2.LearnCurrent1 > 0 | Perks.LvlParty2.LearnCurrent2 > 0))
                {
                    if (Perks.LvlParty2.LearnCurrent1 == 0 & Perks.LvlParty2.LearnCurrent2 == 0) { Money -= 15000; Perks.LvlParty2.LearnCurrent1++; Perks.LvlParty2.LearnCurrent2++; }
                    Perks.CanvelLearn();
                    Perks.LvlParty2.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 13:
                if ((Perks.LvlParty3.Resault == 0) & (Perks.LvlParty2.Resault == 1) & ((Money >= 120000) | Perks.LvlParty3.LearnCurrent1 > 0 | Perks.LvlParty3.LearnCurrent2 > 0))
                {
                    if (Perks.LvlParty3.LearnCurrent1 == 0 & Perks.LvlParty3.LearnCurrent2 == 0) { Money -= 120000; Perks.LvlParty3.LearnCurrent1++; Perks.LvlParty3.LearnCurrent2++; }
                    Perks.CanvelLearn();
                    Perks.LvlParty3.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 14:
                if ((Perks.LvlParty4.Resault == 0) & (Perks.LvlParty3.Resault == 1) & ((Money >= 200000) | Perks.LvlParty4.LearnCurrent1 > 0 | Perks.LvlParty4.LearnCurrent2 > 0))
                {
                    if (Perks.LvlParty4.LearnCurrent1 == 0 & Perks.LvlParty4.LearnCurrent2 == 0) { Money -= 200000; Perks.LvlParty4.LearnCurrent1++; Perks.LvlParty4.LearnCurrent2++; }
                    Perks.CanvelLearn();
                    Perks.LvlParty4.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 15:
                if ((Perks.LvlUnity1.Resault == 0) & (Perks.LvlParty1.Resault == 1) & (Perks.LvlLid1.Resault == 1) & ((Money >= 5000) | Perks.LvlUnity1.LearnCurrent > 0))
                {
                    if (Perks.LvlUnity1.LearnCurrent == 0) { Money -= 5000; Perks.LvlUnity1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlUnity1.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 16:
                if ((Perks.LvlUnity2.Resault == 0) & (Perks.LvlUnity1.Resault == 1) & (Perks.LvlParty2.Resault == 1) & (Perks.LvlLid2.Resault == 1) & ((Money >= 100000) | Perks.LvlUnity2.LearnCurrent > 0))
                {
                    if (Perks.LvlUnity2.LearnCurrent == 0) { Money -= 100000; Perks.LvlUnity2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlUnity2.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 17:
                if ((Perks.LvlUnity3.Resault == 0) & (Perks.LvlUnity2.Resault == 1) & (Perks.LvlParty3.Resault == 1) & (Perks.LvlLid3.Resault == 1) & ((Money >= 300000) | Perks.LvlUnity3.LearnCurrent > 0))
                {
                    if (Perks.LvlUnity3.LearnCurrent == 0) { Money -= 300000; Perks.LvlUnity3.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlUnity3.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 18:
                if ((Perks.LvlLid1.Resault == 1) & (Perks.LvlLearn1.Resault == 0) & ((Money >= 5000) | Perks.LvlLearn1.LearnCurrent > 0))
                {
                    if (Perks.LvlLearn1.LearnCurrent == 0) { Money -= 5000; Perks.LvlLearn1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlLearn1.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 19:
                if ((Perks.LvlLid2.Resault == 1) & (Perks.LvlLearn2.Resault == 0) & (Perks.LvlLearn1.Resault == 1) & ((Money >= 100000) | Perks.LvlLearn2.LearnCurrent > 0))
                {
                    if (Perks.LvlLearn2.LearnCurrent == 0) { Money -= 100000; Perks.LvlLearn2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlLearn2.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 20:
                if ((Perks.LvlLid3.Resault == 1) & (Perks.LvlLearn3.Resault == 0) & (Perks.LvlLearn2.Resault == 1) & ((Money >= 150000) | Perks.LvlLearn3.LearnCurrent > 0))
                {
                    if (Perks.LvlLearn3.LearnCurrent == 0) { Money -= 150000; Perks.LvlLearn3.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlLearn3.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 21:
                if ((Perks.LvlParty1.Resault == 1) & (Perks.LvlAgit1.Resault == 0) & ((Money >= 5000) | Perks.LvlAgit1.LearnCurrent > 0))
                {
                    if (Perks.LvlAgit1.LearnCurrent == 0) { Money -= 5000; Perks.LvlAgit1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlAgit1.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 22:
                if ((Perks.LvlParty2.Resault == 1) & (Perks.LvlAgit2.Resault == 0) & (Perks.LvlAgit1.Resault == 1) & ((Money >= 50000) | Perks.LvlAgit2.LearnCurrent > 0))
                {
                    if (Perks.LvlAgit2.LearnCurrent == 0) { Money -= 50000; Perks.LvlAgit2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlAgit2.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 23:
                if ((Perks.LvlParty3.Resault == 1) & (Perks.LvlAgit3.Resault == 0) & (Perks.LvlAgit2.Resault == 1) & ((Money >= 150000) | Perks.LvlAgit3.LearnCurrent > 0))
                {
                    if (Perks.LvlAgit3.LearnCurrent == 0) { Money -= 1500000; Perks.LvlAgit3.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlAgit3.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 24:
                if ((Perks.LvlParty4.Resault == 1) & (Perks.LvlLid3.Resault == 1) & (Perks.RedArmy.Resault == 0) & ((Money >= 15000000) | Perks.RedArmy.LearnCurrent1 > 0 | Perks.RedArmy.LearnCurrent3 > 0 | Perks.RedArmy.LearnCurrent3 > 0))
                {
                    if (Perks.RedArmy.LearnCurrent1 == 0 & Perks.RedArmy.LearnCurrent2 == 0 & Perks.RedArmy.LearnCurrent3 == 0) { Money -= 15000000; Perks.RedArmy.LearnCurrent1++; Perks.RedArmy.LearnCurrent2++; Perks.RedArmy.LearnCurrent3++; }
                    Perks.CanvelLearn();
                    Perks.RedArmy.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 25:
                if ((Perks.LvlParty2.Resault == 1) & (Perks.LvlMoney1.Resault == 0) & ((Money >= 15000) | Perks.LvlMoney1.LearnCurrent > 0))
                {
                    if (Perks.LvlMoney1.LearnCurrent == 0) { Money -= 15000; Perks.LvlMoney1.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlMoney1.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 26:
                if ((Perks.LvlParty3.Resault == 1) & (Perks.LvlMoney2.Resault == 0) & (Perks.LvlMoney1.Resault == 1) & ((Money >= 300000) | Perks.LvlMoney2.LearnCurrent > 0))
                {
                    if (Perks.LvlMoney2.LearnCurrent == 0) { Money -= 300000; Perks.LvlMoney2.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlMoney2.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
            case 27:
                if ((Perks.LvlParty4.Resault == 1) & (Perks.LvlMoney3.Resault == 0) & (Perks.LvlMoney2.Resault == 1) & ((Money >= 450000) | Perks.LvlMoney3.LearnCurrent > 0))
                {
                    if (Perks.LvlMoney3.LearnCurrent == 0) { Money -= 450000; Perks.LvlMoney3.LearnCurrent++; }
                    Perks.CanvelLearn();
                    Perks.LvlMoney3.LearnThis = true;
                    LastIconsFrame.SetActive(false);
                    LastIconsFrame = But.transform.GetChild(0).gameObject;
                    LastIconsFrame.SetActive(true);
                }
                break;
        }
    }
    public void PerksAllRegions(int num, GameObject But)
    {
        foreach (SelectRegion _sel in Regions)
        {
            PerkLvlLocUp(num, But, _sel);
        }
    }
    public void PerkLvlLocUp(int num, GameObject But, SelectRegion SelReg)
    {
        if (SelReg == null) SelReg = current;
        SelReg.NumLocShi = num;
        switch (num)
        {
            case 1:
                if ((Perks.LvlParty1.Resault == 1) & (SelReg.PerksLoc.LvlLocParty1.Resault == 0) & (Money >= 1000))
                {
                    if (SelReg.CreateParty == true)
                    {
                        Money -= 1000;
                        SelReg.PerksLoc.CanvelLearn();
                        SelReg.PerksLoc.LvlLocParty1.LearnThis = true;
                        if (LastIconsFrameLoc != null) LastIconsFrameLoc.SetActive(false);
                        LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                        LastIconsFrameLoc.SetActive(true);
                        SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                        if (SelReg.LastIconsFrameLoc != null) SelReg.LastIconsFrameLoc.SetActive(true);
                    }
                    else
                    {
                        if (Party.Sovets >= 1 | Lider.GetRegionWithLider() == SelReg)
                        { if (Lider.GetRegionWithLider() != SelReg) Party.Sovets--; SelReg.CreateParty = true; goto case 1; }
                    }
                }
                break;
            case 2:
                if ((Perks.LvlParty2.Resault == 1) & (SelReg.PerksLoc.LvlLocParty2.Resault == 0) & (SelReg.PerksLoc.LvlLocParty1.Resault == 1) & ((float)SelReg.Adhs / (float)SelReg.Pop >= 0.2f) & ((Money >= 5000) | SelReg.PerksLoc.LvlLocParty2.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LvlLocParty2.LearnCurrent == 0) { Money -= 5000; SelReg.PerksLoc.LvlLocParty2.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocParty2.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 3:
                if ((Perks.LvlParty3.Resault == 1) & (SelReg.PerksLoc.LvlLocParty3.Resault == 0) & (SelReg.PerksLoc.LvlLocParty2.Resault == 1) & ((Money >= 10000) | SelReg.PerksLoc.LvlLocParty3.LearnCurrent1 > 0 | SelReg.PerksLoc.LvlLocParty3.LearnCurrent2 > 0))
                {
                    if (SelReg.PerksLoc.LvlLocParty3.LearnCurrent1 == 0 & SelReg.PerksLoc.LvlLocParty3.LearnCurrent2 == 0) { Money -= 10000; SelReg.PerksLoc.LvlLocParty3.LearnCurrent1++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocParty3.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 4:
                if ((Perks.LvlParty4.Resault == 1) & (SelReg.PerksLoc.LvlLocParty4.Resault == 0) & (SelReg.PerksLoc.LvlLocParty3.Resault == 1) & ((Money >= 15000) | SelReg.PerksLoc.LvlLocParty4.LearnCurrent1 > 0 | SelReg.PerksLoc.LvlLocParty4.LearnCurrent2 > 0))
                {
                    if (SelReg.PerksLoc.LvlLocParty4.LearnCurrent1 == 0 & SelReg.PerksLoc.LvlLocParty4.LearnCurrent2 == 0) { Money -= 15000; SelReg.PerksLoc.LvlLocParty4.LearnCurrent1++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocParty4.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 5:
                if ((Perks.RedArmy.Resault == 1) & (SelReg.PerksLoc.LocRedArmy.Resault == 0) & (SelReg.PerksLoc.LvlLocParty4.Resault == 1) & ((Money >= 20000) | SelReg.PerksLoc.LocRedArmy.LearnCurrent1 > 0 | SelReg.PerksLoc.LocRedArmy.LearnCurrent2 > 0))
                {
                    if (SelReg.PerksLoc.LocRedArmy.LearnCurrent1 == 0 & SelReg.PerksLoc.LocRedArmy.LearnCurrent2 == 0) { Money -= 20000; SelReg.PerksLoc.LocRedArmy.LearnCurrent1++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LocRedArmy.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 6:
                if ((Perks.LvlProp1.Resault == 1) & (SelReg.PerksLoc.LvlLocProp0.Resault == 0) & (SelReg.PerksLoc.LvlLocParty1.Resault == 1) & ((Money >= 1000) | SelReg.PerksLoc.LvlLocProp0.LearnCurrent1 > 0 | SelReg.PerksLoc.LvlLocProp0.LearnCurrent2 > 0))
                {
                    if (SelReg.PerksLoc.LvlLocProp0.LearnCurrent1 == 0 & SelReg.PerksLoc.LvlLocProp0.LearnCurrent2 == 0) { Money -= 1000; SelReg.PerksLoc.LvlLocProp0.LearnCurrent1++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocProp0.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 7:
                if ((Perks.LvlAgit1.Resault == 1) & (SelReg.PerksLoc.LvlLocProp1.Resault == 0) & (SelReg.PerksLoc.LvlLocParty1.Resault == 1) & (SelReg.PerksLoc.LvlLocProp0.Resault == 1) & ((Money >= 1500) | SelReg.PerksLoc.LvlLocProp1.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LvlLocProp1.LearnCurrent == 0) { Money -= 1500; SelReg.PerksLoc.LvlLocProp1.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocProp1.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 8:
                if ((Perks.LvlAgit2.Resault == 1) & (SelReg.PerksLoc.LvlLocProp2.Resault == 0) & (SelReg.PerksLoc.LvlLocParty1.Resault == 1) & (SelReg.PerksLoc.LvlLocProp1.Resault == 1) & ((Money >= 2000) | SelReg.PerksLoc.LvlLocProp2.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LvlLocProp2.LearnCurrent == 0) { Money -= 2000; SelReg.PerksLoc.LvlLocProp2.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocProp2.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 9:
                if ((Perks.LvlAgit3.Resault == 1) & (SelReg.PerksLoc.LvlLocProp3.Resault == 0) & (SelReg.PerksLoc.LvlLocParty1.Resault == 1) & (SelReg.PerksLoc.LvlLocProp2.Resault == 1) & ((Money >= 2500) | SelReg.PerksLoc.LvlLocProp3.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LvlLocProp3.LearnCurrent == 0) { Money -= 2500; SelReg.PerksLoc.LvlLocProp3.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocProp3.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 10:
                if ((Perks.LvlMoney1.Resault == 1) & (SelReg.PerksLoc.LvlLocMoney1.Resault == 0) & (SelReg.PerksLoc.LvlLocParty1.Resault == 1) & ((Money >= 1500) | SelReg.PerksLoc.LvlLocMoney1.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LvlLocMoney1.LearnCurrent == 0) { Money -= 1500; SelReg.PerksLoc.LvlLocMoney2.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocMoney1.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 11:
                if ((Perks.LvlMoney2.Resault == 1) & (SelReg.PerksLoc.LvlLocMoney2.Resault == 0) & (SelReg.PerksLoc.LvlLocParty2.Resault == 1) & (SelReg.PerksLoc.LvlLocMoney1.Resault == 1) & ((Money >= 2000) | SelReg.PerksLoc.LvlLocMoney2.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LvlLocMoney2.LearnCurrent == 0) { Money -= 2000; SelReg.PerksLoc.LvlLocMoney2.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LvlLocMoney2.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 12:
                if ((Perks.LvlShadow2.Resault == 1) & (SelReg.PerksLoc.LocShadow.Resault == 0) & (SelReg.PerksLoc.LvlLocParty3.Resault == 1) & ((Money >= 2000) | SelReg.PerksLoc.LocShadow.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LocShadow.LearnCurrent == 0) { Money -= 2000; SelReg.PerksLoc.LocShadow.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LocShadow.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
            case 13:
                if ((Perks.LvlUnity3.Resault == 1) & (SelReg.PerksLoc.LocUnity.Resault == 0) & (SelReg.PerksLoc.LvlLocParty4.Resault == 1) & ((Money >= 2000) | SelReg.PerksLoc.LocUnity.LearnCurrent > 0))
                {
                    if (SelReg.PerksLoc.LocUnity.LearnCurrent == 0) { Money -= 2000; SelReg.PerksLoc.LocUnity.LearnCurrent++; }
                    SelReg.PerksLoc.CanvelLearn();
                    SelReg.PerksLoc.LocUnity.LearnThis = true;
                    LastIconsFrameLoc.SetActive(false);
                    LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    LastIconsFrameLoc.SetActive(true);
                    SelReg.LastIconsFrameLoc = But.transform.GetChild(0).gameObject;
                    SelReg.LastIconsFrameLoc.SetActive(true);
                }
                break;
        }
    }
}