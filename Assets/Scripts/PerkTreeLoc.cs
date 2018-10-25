using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PerkTreeLoc
{
    public struct PerkStrct
    {
        public bool LearnThis;
        public int Resault;
        public float LearnCurrent;
        public float LearnMax;
        public PerkStrct(bool P1, int P2, float P3, float P4)
        {
            LearnThis = P1;
            Resault = P2;
            LearnCurrent = P3;
            LearnMax = P4;
        }
    }
    public struct PerkStrctHard
    {
        public bool LearnThis;
        public int Resault;
        public float LearnCurrent1;
        public float LearnMax1;
        public float LearnCurrent2;
        public float LearnMax2;

        public PerkStrctHard(bool P1, int P2, float P3, float P4, float P5, float P6)
        {
            LearnThis = P1;
            Resault = P2;
            LearnCurrent1 = P3;
            LearnMax1 = P4;
            LearnCurrent2 = P5;
            LearnMax2 = P6;
        }
    }
    public struct PerkStrctVeryHard
    {
        public bool LearnThis;
        public int Resault;
        public float LearnCurrent1;
        public float LearnMax1;
        public float LearnCurrent2;
        public float LearnMax2;
        public float LearnCurrent3;
        public float LearnMax3;

        public PerkStrctVeryHard(bool P1, int P2, float P3, float P4, float P5, float P6, float P7, float P8)
        {
            LearnThis = P1;
            Resault = P2;
            LearnCurrent1 = P3;
            LearnMax1 = P4;
            LearnCurrent2 = P5;
            LearnMax2 = P6;
            LearnCurrent3 = P7;
            LearnMax3 = P8;
        }
    }
    public StateScript Ss;
    Image[] IconsLoc = new Image[13];
    public PerkStrct LvlLocParty1 = new PerkStrct(false, 0, 0, 15);
    public PerkStrct LvlLocParty2 = new PerkStrct(false, 0, 0, 30);
    public PerkStrctHard LvlLocParty3 = new PerkStrctHard(false, 0, 0, 40, 0, 40);
    public PerkStrctHard LvlLocParty4 = new PerkStrctHard(false, 0, 0, 75, 0, 75);
    public PerkStrct LvlLocMoney1 = new PerkStrct(false, 0, 0, 20);
    public PerkStrct LvlLocMoney2 = new PerkStrct(false, 0, 0, 30);
    public PerkStrctHard LvlLocProp0 = new PerkStrctHard(false, 0, 0, 20, 0, 20);
    public PerkStrct LvlLocProp1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlLocProp2 = new PerkStrct(false, 0, 0, 40);
    public PerkStrct LvlLocProp3 = new PerkStrct(false, 0, 0, 50);
    public PerkStrct LocShadow = new PerkStrct(false, 0, 0, 50);
    public PerkStrct LocUnity = new PerkStrct(false, 0, 0, 50);
    public PerkStrctHard LocRedArmy = new PerkStrctHard(false, 0, 0, 30, 0, 30);

    public void SetUI()
    {
        for (int i = 0; i <= 12; i++)
        { IconsLoc[i] = Ss.icons_loc[i]; }
        /*IconsLoc[0] = GameObject.Find("BtnPartyLocLvl1").GetComponent<Image>();
        IconsLoc[1] = GameObject.Find("BtnPartyLocLvl2").GetComponent<Image>();
        IconsLoc[2] = GameObject.Find("BtnPartyLocLvl3").GetComponent<Image>();
        IconsLoc[3] = GameObject.Find("BtnPartyLocLvl4").GetComponent<Image>();
        IconsLoc[4] = GameObject.Find("BtnRedArmyLoc").GetComponent<Image>();
        IconsLoc[5] = GameObject.Find("BtnPropLocLvl0").GetComponent<Image>();
        IconsLoc[6] = GameObject.Find("BtnPropLocLvl1").GetComponent<Image>();
        IconsLoc[7] = GameObject.Find("BtnPropLocLvl2").GetComponent<Image>();
        IconsLoc[8] = GameObject.Find("BtnPropLocLvl3").GetComponent<Image>();
        IconsLoc[9] = GameObject.Find("BtnMoneyLocLvl1").GetComponent<Image>();
        IconsLoc[10] = GameObject.Find("BtnMoneyLocLvl2").GetComponent<Image>();
        IconsLoc[11] = GameObject.Find("BtnShadowLoc").GetComponent<Image>();
        IconsLoc[12] = GameObject.Find("BtnUnityLoc").GetComponent<Image>();*/
    }

    public void CanvelLearn()
    {
        LvlLocParty1.LearnThis = false;
        LvlLocParty2.LearnThis = false;
        LvlLocParty3.LearnThis = false;
        LvlLocParty4.LearnThis = false;
        LvlLocMoney1.LearnThis = false;
        LvlLocMoney2.LearnThis = false;
        LvlLocProp0.LearnThis = false;
        LvlLocProp1.LearnThis = false;
        LvlLocProp2.LearnThis = false;
        LvlLocProp3.LearnThis = false;
        LocShadow.LearnThis = false;
        LocUnity.LearnThis = false;
        LocRedArmy.LearnThis = false;
    }

    public void LearnProsessLoc(float PAgit, float PSov, float PRec, float PBiz, float PShadow, float PPartyBuild)
    {
        if (LvlLocParty1.LearnThis == true) { LvlLocParty1.LearnCurrent = Mathf.Clamp(LvlLocParty1.LearnCurrent + PPartyBuild, 0, LvlLocParty1.LearnMax); if (LvlLocParty1.LearnCurrent >= LvlLocParty1.LearnMax) LvlLocParty1.Resault = 1; }
        if (LvlLocParty2.LearnThis == true) { LvlLocParty2.LearnCurrent = Mathf.Clamp(LvlLocParty2.LearnCurrent + PSov, 0, LvlLocParty2.LearnMax); if (LvlLocParty2.LearnCurrent >= LvlLocParty2.LearnMax) LvlLocParty2.Resault = 1; }
        if (LvlLocParty3.LearnThis == true) { LvlLocParty3.LearnCurrent1 = Mathf.Clamp(LvlLocParty3.LearnCurrent1 + PSov, 0, LvlLocParty3.LearnMax1); LvlLocParty3.LearnCurrent2 = Mathf.Clamp(LvlLocParty3.LearnCurrent2 + PRec, 0, LvlLocParty3.LearnMax2); if ((LvlLocParty3.LearnCurrent1 + LvlLocParty3.LearnCurrent2) >= (LvlLocParty3.LearnMax1+ LvlLocParty3.LearnMax2)) LvlLocParty3.Resault = 1; }
        if (LvlLocParty4.LearnThis == true) { LvlLocParty4.LearnCurrent1 = Mathf.Clamp(LvlLocParty4.LearnCurrent1 + PSov, 0, LvlLocParty4.LearnMax1); LvlLocParty4.LearnCurrent2 = Mathf.Clamp(LvlLocParty4.LearnCurrent2 + PShadow, 0, LvlLocParty4.LearnMax2); if ((LvlLocParty4.LearnCurrent1 + LvlLocParty4.LearnCurrent2) >= (LvlLocParty4.LearnMax1 + LvlLocParty4.LearnMax2)) LvlLocParty4.Resault = 1; }
        if (LvlLocMoney1.LearnThis == true) { LvlLocMoney1.LearnCurrent = Mathf.Clamp(LvlLocMoney1.LearnCurrent + PBiz, 0, LvlLocMoney1.LearnMax); if (LvlLocMoney1.LearnCurrent >= LvlLocMoney1.LearnMax) LvlLocMoney1.Resault = 1; }
        if (LvlLocMoney2.LearnThis == true) { LvlLocMoney2.LearnCurrent = Mathf.Clamp(LvlLocMoney2.LearnCurrent + PBiz, 0, LvlLocMoney2.LearnMax); if (LvlLocMoney2.LearnCurrent >= LvlLocMoney2.LearnMax) LvlLocMoney2.Resault = 1; }
        if (LvlLocProp0.LearnThis == true) { LvlLocProp0.LearnCurrent1 = Mathf.Clamp(LvlLocProp0.LearnCurrent1 + PAgit, 0, LvlLocProp0.LearnMax1); LvlLocProp0.LearnCurrent2 = Mathf.Clamp(LvlLocProp0.LearnCurrent1 + PRec, 0, LvlLocProp0.LearnMax2); if ((LvlLocProp0.LearnCurrent1 + LvlLocProp0.LearnCurrent2) >= (LvlLocProp0.LearnMax1 + LvlLocProp0.LearnMax2)) LvlLocProp0.Resault = 1; }
        if (LvlLocProp1.LearnThis == true) { LvlLocProp1.LearnCurrent = Mathf.Clamp(LvlLocProp1.LearnCurrent + PAgit, 0, LvlLocProp1.LearnMax); if (LvlLocProp1.LearnCurrent >= LvlLocProp1.LearnMax) LvlLocProp1.Resault = 1; }
        if (LvlLocProp2.LearnThis == true) { LvlLocProp2.LearnCurrent = Mathf.Clamp(LvlLocProp2.LearnCurrent + PAgit, 0, LvlLocProp2.LearnMax); if (LvlLocProp2.LearnCurrent >= LvlLocProp2.LearnMax) LvlLocProp2.Resault = 1; }
        if (LvlLocProp3.LearnThis == true) { LvlLocProp3.LearnCurrent = Mathf.Clamp(LvlLocProp3.LearnCurrent + PAgit, 0, LvlLocProp3.LearnMax); if (LvlLocProp3.LearnCurrent >= LvlLocProp3.LearnMax) LvlLocProp3.Resault = 1; }
        if (LocShadow.LearnThis == true) { LocShadow.LearnCurrent = Mathf.Clamp(LocShadow.LearnCurrent + PShadow, 0, LocShadow.LearnMax); if (LocShadow.LearnCurrent >= LocShadow.LearnMax) LocShadow.Resault = 1; }
        if (LocUnity.LearnThis == true) { LocUnity.LearnCurrent = Mathf.Clamp(LocUnity.LearnCurrent + PSov, 0, LocUnity.LearnMax); if (LocUnity.LearnCurrent >= LocUnity.LearnMax) LocUnity.Resault = 1; }
        if (LocRedArmy.LearnThis == true) { LocRedArmy.LearnCurrent1 = Mathf.Clamp(LocRedArmy.LearnCurrent1 + PRec, 0, LocRedArmy.LearnMax1); LocRedArmy.LearnCurrent2 = Mathf.Clamp(LocRedArmy.LearnCurrent2 + PSov, 0, LocRedArmy.LearnMax2); if ((LocRedArmy.LearnCurrent1 + LocRedArmy.LearnCurrent2) >= (LocRedArmy.LearnMax1 + LocRedArmy.LearnMax2)) LocRedArmy.Resault = 1; }
    }

    public void IconsProsessLoc()
    {
         IconsLoc[0].fillAmount = LvlLocParty1.LearnCurrent / LvlLocParty1.LearnMax;
         IconsLoc[1].fillAmount = LvlLocParty2.LearnCurrent / LvlLocParty2.LearnMax;
         IconsLoc[2].fillAmount = (LvlLocParty3.LearnCurrent1 + LvlLocParty3.LearnCurrent2) / (LvlLocParty3.LearnMax1 + LvlLocParty3.LearnMax2);
         IconsLoc[3].fillAmount = (LvlLocParty4.LearnCurrent1 + LvlLocParty4.LearnCurrent2) / (LvlLocParty4.LearnMax1 + LvlLocParty4.LearnMax2); 
         IconsLoc[9].fillAmount = LvlLocMoney1.LearnCurrent / LvlLocMoney1.LearnMax;
         IconsLoc[10].fillAmount = LvlLocMoney2.LearnCurrent / LvlLocMoney2.LearnMax;
         IconsLoc[5].fillAmount = (LvlLocProp0.LearnCurrent1 + LvlLocProp0.LearnCurrent2) / (LvlLocProp0.LearnMax1 + LvlLocProp0.LearnMax2);
         IconsLoc[6].fillAmount = LvlLocProp1.LearnCurrent / LvlLocProp1.LearnMax; 
         IconsLoc[7].fillAmount = LvlLocProp2.LearnCurrent / LvlLocProp2.LearnMax; 
         IconsLoc[8].fillAmount = LvlLocProp3.LearnCurrent / LvlLocProp3.LearnMax; 
         IconsLoc[11].fillAmount = LocShadow.LearnCurrent / LocShadow.LearnMax; 
         IconsLoc[12].fillAmount = LocUnity.LearnCurrent / LocUnity.LearnMax; 
         IconsLoc[4].fillAmount = (LocRedArmy.LearnCurrent1 + LocRedArmy.LearnCurrent2) / (LocRedArmy.LearnMax1 + LocRedArmy.LearnMax2);
    }
public float PerksLocSum()
    {
      return LvlLocParty1.Resault+ LvlLocParty2.Resault + LvlLocParty3.Resault + LvlLocParty4.Resault + LvlLocMoney1.Resault + LvlLocMoney2.Resault + LvlLocProp0.Resault + LvlLocProp1.Resault + LvlLocProp2.Resault + LvlLocProp3.Resault + LocShadow.Resault + LocUnity.Resault + LocRedArmy.Resault;
    }
    public int GetAgitLoc()
    {
        return LvlLocProp0.Resault + LvlLocProp1.Resault + LvlLocProp2.Resault + LvlLocProp3.Resault;
    }

    public int GetPartyLoc()
    {
        return LvlLocParty1.Resault+LvlLocParty2.Resault+LvlLocParty3.Resault+LvlLocParty4.Resault;
    }

}