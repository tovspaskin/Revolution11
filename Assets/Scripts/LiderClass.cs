
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LiderClass
{
    string Namel = "Ленин";
    int ActionPoints = 10;
    SelectRegion RegionWithLider;
    public int Agit, Money, Hide, PartyBuild, Learn = 0;
    public float ExpOfAgit,ExpMoney, ExpImp, ExpLearn, ExpHide = 0;
    public int LvlOfPerkAgit, LvlOfPerkMoney, LvlOfPerkHide, LvlOfPerkSpeak;
    [SerializeField]
    public GameObject[] UiInfo;
    public StateScript Ss;
public string GetName()
    { return Namel; }
    public void SetName()
    {
       Namel = PlayerPrefs.GetString("NameLider");
    }

    public SelectRegion GetRegionWithLider()
    {
        return RegionWithLider;
    }

    public void UpdExp()
    {
        ExpLearn += Mathf.Pow(Learn*(1+Ss.Perks.GetLvlLearn()), 0.5f)/ ((ExpLearn / (1+3*Ss.Perks.GetLvlLearn())) + 1);
        if ((ExpMoney + ExpOfAgit + ExpImp + ExpHide) <= ExpLearn)
        {
            if ((Agit > Money) & (Agit > Hide) & (Agit > PartyBuild)) ExpOfAgit += Agit / ((ExpOfAgit / 10) + 1);
            if ((PartyBuild > Money) & (PartyBuild > Hide) & (PartyBuild > Agit)) ExpImp += PartyBuild / ((ExpImp / 10) + 1);
            if ((Money > PartyBuild) & (Money > Hide) & (Money > Agit)) ExpMoney += Money / ((ExpMoney / 10) + 1);
            if ((Hide > PartyBuild) & (Hide > Money) & (Hide > Agit)) ExpHide += Hide / ((ExpHide / 10) + 1);
             
        }
    }

    public int GetAgitLiderFactor(SelectRegion _sel)
    {   
       return ((LvlOfPerkSpeak + LvlOfPerkAgit)*Agit+Mathf.RoundToInt(Mathf.Log(ExpOfAgit+1, 2)))*GetMnj(_sel);
    }

    public int GetMnj(SelectRegion _sel)
    {
        int val = 0;
        if (RegionWithLider == _sel) val = 1;
        return val;
    }


    public void SetRegionLider(SelectRegion value)
    {
        RegionWithLider = value;
    }

    public int GetActionLider() { return ActionPoints; }
    public void SetActionLider(int value) { ActionPoints += value; }

    public void SetFullInfoLider()
    {
        UiInfo[0].GetComponent<Text>().text = Namel;
        UiInfo[1].GetComponent<Text>().text = "очки действия: " + ActionPoints.ToString();
        if (RegionWithLider != null) UiInfo[2].GetComponent<Text>().text = RegionWithLider.GetComponent<SelectRegion>().NameReg.ToString();
        else UiInfo[2].GetComponent<Text>().text = "Нет";
        UiInfo[3].GetComponent<Text>().text = "Пропаганда: "+ Agit;
        UiInfo[4].GetComponent<Text>().text = "Заработок: " + Money;
        UiInfo[5].GetComponent<Text>().text = "Конспирация: " + Hide;
        UiInfo[6].GetComponent<Text>().text = "Партистрой: " + PartyBuild;
        UiInfo[7].GetComponent<Text>().text = "Учение: " + Learn;
        UiInfo[8].GetComponent<Text>().text = "Ораторские навыки: " + Mathf.RoundToInt(ExpOfAgit);
        UiInfo[9].GetComponent<Text>().text = "Рабочие навыки: " + Mathf.RoundToInt(ExpMoney);
        UiInfo[10].GetComponent<Text>().text = "Навыки организации: " + Mathf.RoundToInt(ExpImp);
        UiInfo[11].GetComponent<Text>().text = "Навыки учения: " + Mathf.RoundToInt(ExpLearn);
        UiInfo[12].GetComponent<Text>().text = "Навыки конспирации: " + Mathf.RoundToInt(ExpHide);
    }


}
