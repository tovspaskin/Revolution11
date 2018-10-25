using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PerkTree
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
    Image[] Icons = new Image[27];
    public StateScript Ss;
    public PerkStrct LvlLid1 = new PerkStrct(false, 0, 0, 60);
    public PerkStrctHard LvlLid2 = new PerkStrctHard(false, 0, 0, 60, 0, 60);
    public PerkStrctHard LvlLid3 = new PerkStrctHard(false, 0, 0, 60, 0, 80);
    public PerkStrct LvlShadow1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlShadow2 = new PerkStrct(false, 0, 0, 55);
    public PerkStrct LvlShadow3 = new PerkStrct(false, 0, 0, 80);
    public PerkStrct LvlPassiveMoney1 = new PerkStrct(false, 0, 0, 180);
    public PerkStrct LvlPassiveMoney2 = new PerkStrct(false, 0, 0, 360);
    public PerkStrct LvlMoney1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlMoney2 = new PerkStrct(false, 0, 0, 60);
    public PerkStrct LvlMoney3 = new PerkStrct(false, 0, 0, 90);
    public PerkStrct LvlProp1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlProp2 = new PerkStrct(false, 0, 0, 60);
    public PerkStrct LvlProp3 = new PerkStrct(false, 0, 0, 90);
    public PerkStrct LvlAgit1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlAgit2 = new PerkStrct(false, 0, 0, 60);
    public PerkStrct LvlAgit3 = new PerkStrct(false, 0, 0, 90);
    public PerkStrct LvlParty1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrctHard LvlParty2 = new PerkStrctHard(false, 0, 0, 60, 0, 30);
    public PerkStrctHard LvlParty3 = new PerkStrctHard(false, 0, 0, 60, 0, 60);
    public PerkStrctHard LvlParty4 = new PerkStrctHard(false, 0, 0, 90, 0, 90);
    public PerkStrct LvlUnity1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlUnity2 = new PerkStrct(false, 0, 0, 60);
    public PerkStrct LvlUnity3 = new PerkStrct(false, 0, 0, 90);
    public PerkStrct LvlLearn1 = new PerkStrct(false, 0, 0, 30);
    public PerkStrct LvlLearn2 = new PerkStrct(false, 0, 0, 50);
    public PerkStrct LvlLearn3 = new PerkStrct(false, 0, 0, 70);
    public PerkStrctVeryHard RedArmy = new PerkStrctVeryHard(false, 0, 0, 90, 0, 90, 0, 90);

    public void SetUI()
    {
        for ( int i = 0; i <= 26; i++)
        {
          //  Debug.Log(i.ToString() + Ss.icons[i].name);
            Icons[i] = Ss.icons[i]; }
        /*Icons[0] = GameObject.Find("BtnLvlLid1").GetComponent<Image>();
        Icons[1] = GameObject.Find("BtnLvlLid2").GetComponent<Image>();
        Icons[2] = GameObject.Find("BtnLvlLid3").GetComponent<Image>();
        Icons[3] = GameObject.Find("BtnShadowLvl1").GetComponent<Image>();
        Icons[4] = GameObject.Find("BtnShadowLvl2").GetComponent<Image>();
        Icons[5] = GameObject.Find("BtnShadowLvl3").GetComponent<Image>();
        Icons[6] = GameObject.Find("BtnPassiveMoneyLvl1").GetComponent<Image>();
        Icons[7] = GameObject.Find("BtnPassiveMoneyLvl2").GetComponent<Image>();
        Icons[8] = GameObject.Find("BtnPropLvl1").GetComponent<Image>();
        Icons[9] = GameObject.Find("BtnPropLvl2").GetComponent<Image>();
        Icons[10] = GameObject.Find("BtnLvlParty1").GetComponent<Image>();
        Icons[11] = GameObject.Find("BtnLvlParty2").GetComponent<Image>();
        Icons[12] = GameObject.Find("BtnLvlParty3").GetComponent<Image>();
        Icons[13] = GameObject.Find("BtnLvlParty4").GetComponent<Image>();
        Icons[14] = GameObject.Find("BtnUnityLvl1").GetComponent<Image>();
        Icons[15] = GameObject.Find("BtnUnityLvl2").GetComponent<Image>();
        Icons[16] = GameObject.Find("BtnUnityLvl3").GetComponent<Image>();
        Icons[17] = GameObject.Find("BtnLearnLvl1").GetComponent<Image>();
        Icons[18] = GameObject.Find("BtnLearnLvl2").GetComponent<Image>();
        Icons[19] = GameObject.Find("BtnLearnLvl3").GetComponent<Image>();
        Icons[20] = GameObject.Find("BtnAgitLvl1").GetComponent<Image>();
        Icons[21] = GameObject.Find("BtnAgitLvl2").GetComponent<Image>();
        Icons[22] = GameObject.Find("BtnAgitLvl3").GetComponent<Image>();
        Icons[23] = GameObject.Find("BtnRedArmy").GetComponent<Image>();
        Icons[24] = GameObject.Find("BtnMoneyLvl1").GetComponent<Image>();
        Icons[25] = GameObject.Find("BtnMoneyLvl2").GetComponent<Image>();
        Icons[26] = GameObject.Find("BtnMoneyLvl3").GetComponent<Image>();*/

    }

    public void CanvelLearn()
    {
        LvlLid1.LearnThis = false; 
        LvlLid2.LearnThis = false;
        LvlLid3.LearnThis = false;
        LvlShadow1.LearnThis = false;
        LvlShadow2.LearnThis = false;
        LvlShadow3.LearnThis = false;
        LvlPassiveMoney1.LearnThis = false;
        LvlPassiveMoney2.LearnThis = false;
        LvlProp1.LearnThis = false;
        LvlProp2.LearnThis = false;
        LvlProp3.LearnThis = false;
        LvlAgit1.LearnThis = false;
        LvlAgit2.LearnThis = false;
        LvlParty1.LearnThis = false;
        LvlParty2.LearnThis = false;
        LvlParty3.LearnThis = false;
        LvlParty4.LearnThis = false;
        LvlUnity1.LearnThis = false;
        LvlUnity2.LearnThis = false;
        LvlUnity3.LearnThis = false;
        LvlLearn1.LearnThis = false;
        LvlLearn2.LearnThis = false;
        LvlLearn3.LearnThis = false;
        LvlAgit1.LearnThis = false;
        RedArmy.LearnThis = false;
        LvlMoney1.LearnThis = false;
        LvlMoney2.LearnThis = false;
        LvlMoney3.LearnThis = false;
    }

    public void LearnProsess(float Pprop, float PBiz, float PPartyBuild, float PLearn, float PShadow)
    {
        if (LvlLid1.LearnThis == true) { LvlLid1.LearnCurrent = Mathf.Clamp(LvlLid1.LearnCurrent + PLearn, 0, LvlLid1.LearnMax); Icons[0].fillAmount = LvlLid1.LearnCurrent / LvlLid1.LearnMax; if (LvlLid1.LearnCurrent >= LvlLid1.LearnMax) LvlLid1.Resault = 1; }
        if (LvlLid2.LearnThis == true) { LvlLid2.LearnCurrent1 = Mathf.Clamp(LvlLid2.LearnCurrent1 + PLearn, 0, LvlLid2.LearnMax1); LvlLid2.LearnCurrent2 = Mathf.Clamp(LvlLid2.LearnCurrent2 + Pprop, 0, LvlLid2.LearnMax2); Icons[1].fillAmount = (LvlLid2.LearnCurrent1+ LvlLid2.LearnCurrent2) / (LvlLid2.LearnMax1+ LvlLid2.LearnMax2); if ((LvlLid2.LearnCurrent1 + LvlLid2.LearnCurrent2) >= (LvlLid2.LearnMax1 + LvlLid2.LearnMax2)) LvlLid2.Resault = 1; }
        if (LvlLid3.LearnThis == true) { LvlLid3.LearnCurrent1 = Mathf.Clamp(LvlLid3.LearnCurrent1 + PLearn, 0, LvlLid3.LearnMax1); LvlLid3.LearnCurrent2 = Mathf.Clamp(LvlLid3.LearnCurrent2 + PShadow, 0, LvlLid3.LearnMax2); Icons[2].fillAmount = (LvlLid3.LearnCurrent1 + LvlLid3.LearnCurrent2) / (LvlLid3.LearnMax1 + LvlLid3.LearnMax2); if ((LvlLid3.LearnCurrent1 + LvlLid3.LearnCurrent2) >= (LvlLid3.LearnMax1 + LvlLid3.LearnMax2)) LvlLid3.Resault = 1; }
        if (LvlShadow1.LearnThis == true) { LvlShadow1.LearnCurrent = Mathf.Clamp(LvlShadow1.LearnCurrent + PShadow, 0, LvlShadow1.LearnMax); Icons[3].fillAmount = LvlShadow1.LearnCurrent / LvlShadow1.LearnMax; if (LvlShadow1.LearnCurrent >= LvlShadow1.LearnMax) LvlShadow1.Resault = 1; }
        if (LvlShadow2.LearnThis == true) { LvlShadow2.LearnCurrent = Mathf.Clamp(LvlShadow2.LearnCurrent + PShadow, 0, LvlShadow2.LearnMax); Icons[4].fillAmount = LvlShadow2.LearnCurrent / LvlShadow2.LearnMax; if (LvlShadow2.LearnCurrent >= LvlShadow2.LearnMax) LvlShadow2.Resault = 1; }
        if (LvlShadow3.LearnThis == true) { LvlShadow3.LearnCurrent = Mathf.Clamp(LvlShadow3.LearnCurrent + PShadow, 0, LvlShadow3.LearnMax); Icons[5].fillAmount = LvlShadow3.LearnCurrent / LvlShadow3.LearnMax; if (LvlShadow3.LearnCurrent >= LvlShadow3.LearnMax) LvlShadow3.Resault = 1; }
        if (LvlPassiveMoney1.LearnThis == true) { LvlPassiveMoney1.LearnCurrent = Mathf.Clamp(LvlPassiveMoney1.LearnCurrent + PBiz, 0, LvlPassiveMoney1.LearnMax); Icons[6].fillAmount = LvlPassiveMoney1.LearnCurrent / LvlPassiveMoney1.LearnMax; if (LvlPassiveMoney1.LearnCurrent >= LvlPassiveMoney1.LearnMax) LvlPassiveMoney1.Resault = 1; }
        if (LvlPassiveMoney2.LearnThis == true) { LvlPassiveMoney2.LearnCurrent = Mathf.Clamp(LvlPassiveMoney2.LearnCurrent + PBiz, 0, LvlPassiveMoney2.LearnMax); Icons[7].fillAmount = LvlPassiveMoney2.LearnCurrent / LvlPassiveMoney2.LearnMax; if (LvlPassiveMoney2.LearnCurrent >= LvlPassiveMoney2.LearnMax) LvlPassiveMoney2.Resault = 2; }
        if (LvlProp1.LearnThis == true) { LvlProp1.LearnCurrent = Mathf.Clamp(LvlProp1.LearnCurrent + Pprop, 0, LvlProp1.LearnMax); Icons[8].fillAmount = LvlProp1.LearnCurrent / LvlProp1.LearnMax; if (LvlProp1.LearnCurrent >= LvlProp1.LearnMax) LvlProp1.Resault = 1; }
        if (LvlProp2.LearnThis == true) { LvlProp2.LearnCurrent = Mathf.Clamp(LvlProp2.LearnCurrent + Pprop, 0, LvlProp2.LearnMax); Icons[9].fillAmount = LvlProp2.LearnCurrent / LvlProp2.LearnMax; if (LvlProp2.LearnCurrent >= LvlProp1.LearnMax) LvlProp2.Resault = 1; }
        if (LvlParty1.LearnThis == true) { LvlParty1.LearnCurrent = Mathf.Clamp(LvlParty1.LearnCurrent + PPartyBuild, 0, LvlParty1.LearnMax); Icons[10].fillAmount = LvlParty1.LearnCurrent / LvlParty1.LearnMax; if (LvlParty1.LearnCurrent >= LvlParty1.LearnMax) LvlParty1.Resault = 1; }
        if (LvlParty2.LearnThis == true) { LvlParty2.LearnCurrent1 = Mathf.Clamp(LvlParty2.LearnCurrent1 + PPartyBuild, 0, LvlParty2.LearnMax1); LvlParty2.LearnCurrent2 = Mathf.Clamp(LvlParty2.LearnCurrent2 + PLearn, 0, LvlParty2.LearnMax2); Icons[11].fillAmount = (LvlParty2.LearnCurrent1 + LvlParty2.LearnCurrent2) / (LvlParty2.LearnMax1 + LvlParty2.LearnMax2); if ((LvlParty2.LearnCurrent1 + LvlParty2.LearnCurrent2) >= (LvlParty2.LearnMax1 + LvlParty2.LearnMax2)) LvlParty2.Resault = 1; }
        if (LvlParty3.LearnThis == true) { LvlParty3.LearnCurrent1 = Mathf.Clamp(LvlParty3.LearnCurrent1 + PPartyBuild, 0, LvlParty3.LearnMax1); LvlParty3.LearnCurrent2 = Mathf.Clamp(LvlParty3.LearnCurrent2 + PShadow, 0, LvlParty3.LearnMax2); Icons[12].fillAmount = (LvlParty3.LearnCurrent1 + LvlParty3.LearnCurrent2) / (LvlParty3.LearnMax1 + LvlParty3.LearnMax2); if ((LvlParty3.LearnCurrent1 + LvlParty3.LearnCurrent2) >= (LvlParty3.LearnMax1 + LvlParty3.LearnMax2)) LvlParty3.Resault = 1; }
        if (LvlParty4.LearnThis == true) { LvlParty4.LearnCurrent1 = Mathf.Clamp(LvlParty4.LearnCurrent1 + PPartyBuild, 0, LvlParty4.LearnMax1); LvlParty4.LearnCurrent2 = Mathf.Clamp(LvlParty4.LearnCurrent2 + Pprop, 0, LvlParty4.LearnMax2); Icons[13].fillAmount = (LvlParty4.LearnCurrent1 + LvlParty4.LearnCurrent2) / (LvlParty4.LearnMax1 + LvlParty4.LearnMax2); if ((LvlParty4.LearnCurrent1 + LvlParty4.LearnCurrent2) >= (LvlParty4.LearnMax1 + LvlParty4.LearnMax2)) LvlParty4.Resault = 1; }
        if (LvlUnity1.LearnThis == true) { LvlUnity1.LearnCurrent = Mathf.Clamp(LvlUnity1.LearnCurrent + PPartyBuild, 0, LvlUnity1.LearnMax); Icons[14].fillAmount = LvlUnity1.LearnCurrent / LvlUnity1.LearnMax; if (LvlUnity1.LearnCurrent >= LvlUnity1.LearnMax) LvlUnity1.Resault = 1; }
        if (LvlUnity2.LearnThis == true) { LvlUnity2.LearnCurrent = Mathf.Clamp(LvlUnity2.LearnCurrent + PPartyBuild, 0, LvlUnity2.LearnMax); Icons[15].fillAmount = LvlUnity2.LearnCurrent / LvlUnity2.LearnMax; if (LvlUnity2.LearnCurrent >= LvlUnity2.LearnMax) LvlUnity2.Resault = 1; }
        if (LvlUnity3.LearnThis == true) { LvlUnity3.LearnCurrent = Mathf.Clamp(LvlUnity3.LearnCurrent + PPartyBuild, 0, LvlUnity3.LearnMax); Icons[16].fillAmount = LvlUnity3.LearnCurrent / LvlUnity3.LearnMax; if (LvlUnity3.LearnCurrent >= LvlUnity3.LearnMax) LvlUnity3.Resault = 1; }
        if (LvlLearn1.LearnThis == true) { LvlLearn1.LearnCurrent = Mathf.Clamp(LvlLearn1.LearnCurrent + PLearn, 0, LvlLearn1.LearnMax); Icons[17].fillAmount = LvlLearn1.LearnCurrent / LvlLearn1.LearnMax; if (LvlLearn1.LearnCurrent >= LvlLearn1.LearnMax) LvlLearn1.Resault = 1; }
        if (LvlLearn2.LearnThis == true) { LvlLearn2.LearnCurrent = Mathf.Clamp(LvlLearn2.LearnCurrent + PLearn, 0, LvlLearn2.LearnMax); Icons[18].fillAmount = LvlLearn2.LearnCurrent / LvlLearn2.LearnMax; if (LvlLearn2.LearnCurrent >= LvlLearn2.LearnMax) LvlLearn2.Resault = 1; }
        if (LvlLearn3.LearnThis == true) { LvlLearn3.LearnCurrent = Mathf.Clamp(LvlLearn3.LearnCurrent + PLearn, 0, LvlLearn3.LearnMax); Icons[19].fillAmount = LvlLearn3.LearnCurrent / LvlLearn3.LearnMax; if (LvlLearn3.LearnCurrent >= LvlLearn3.LearnMax) LvlLearn3.Resault = 1; }
        if (RedArmy.LearnThis == true)
        {
            RedArmy.LearnCurrent1 = Mathf.Clamp(RedArmy.LearnCurrent1 + PPartyBuild, 0, RedArmy.LearnMax1);
            RedArmy.LearnCurrent2 = Mathf.Clamp(RedArmy.LearnCurrent2 + PBiz, 0, RedArmy.LearnMax2);
            RedArmy.LearnCurrent3 = Mathf.Clamp(RedArmy.LearnCurrent3 + Pprop, 0, RedArmy.LearnMax3);
            Icons[23].fillAmount = (RedArmy.LearnCurrent1 + RedArmy.LearnCurrent2 + RedArmy.LearnCurrent3) / (RedArmy.LearnMax1 + RedArmy.LearnMax2 + RedArmy.LearnMax3);
            if ((RedArmy.LearnCurrent1 + RedArmy.LearnCurrent2 + RedArmy.LearnCurrent3) >= (RedArmy.LearnMax1 + RedArmy.LearnMax2 + RedArmy.LearnMax3)) RedArmy.Resault = 1;
        }
        if (LvlAgit1.LearnThis == true) { LvlAgit1.LearnCurrent = Mathf.Clamp(LvlAgit1.LearnCurrent + Pprop, 0, LvlAgit1.LearnMax); Icons[20].fillAmount = LvlAgit1.LearnCurrent / LvlAgit1.LearnMax; if (LvlAgit1.LearnCurrent >= LvlAgit1.LearnMax) LvlAgit1.Resault = 1; }
        if (LvlAgit2.LearnThis == true) { LvlAgit2.LearnCurrent = Mathf.Clamp(LvlAgit2.LearnCurrent + Pprop, 0, LvlAgit2.LearnMax); Icons[21].fillAmount = LvlAgit2.LearnCurrent / LvlAgit2.LearnMax; if (LvlAgit2.LearnCurrent >= LvlAgit2.LearnMax) LvlAgit2.Resault = 1; }
        if (LvlAgit3.LearnThis == true) { LvlAgit3.LearnCurrent = Mathf.Clamp(LvlAgit3.LearnCurrent + Pprop, 0, LvlAgit3.LearnMax); Icons[22].fillAmount = LvlAgit3.LearnCurrent / LvlAgit3.LearnMax; if (LvlAgit3.LearnCurrent >= LvlAgit3.LearnMax) LvlAgit3.Resault = 1; }
        if (LvlMoney1.LearnThis == true) { LvlMoney1.LearnCurrent = Mathf.Clamp(LvlMoney1.LearnCurrent + PBiz, 0, LvlMoney1.LearnMax); Icons[24].fillAmount = LvlMoney1.LearnCurrent / LvlMoney1.LearnMax; if (LvlMoney1.LearnCurrent >= LvlMoney1.LearnMax) LvlMoney1.Resault = 1; }
        if (LvlMoney2.LearnThis == true) { LvlMoney2.LearnCurrent = Mathf.Clamp(LvlMoney2.LearnCurrent + PBiz, 0, LvlMoney2.LearnMax); Icons[25].fillAmount = LvlMoney2.LearnCurrent / LvlMoney2.LearnMax; if (LvlMoney2.LearnCurrent >= LvlMoney2.LearnMax) LvlMoney2.Resault = 1; }
        if (LvlMoney3.LearnThis == true) { LvlMoney3.LearnCurrent = Mathf.Clamp(LvlMoney3.LearnCurrent + PBiz, 0, LvlMoney3.LearnMax); Icons[26].fillAmount = LvlMoney3.LearnCurrent / LvlMoney3.LearnMax; if (LvlMoney3.LearnCurrent >= LvlMoney1.LearnMax) LvlMoney3.Resault = 1; }
    }

    public int SumResPerks()
    {
       return LvlLid1.Resault + LvlLid2.Resault + LvlLid3.Resault + LvlShadow1.Resault + LvlShadow2.Resault + LvlShadow3.Resault + LvlPassiveMoney1.Resault + LvlPassiveMoney2.Resault + LvlProp1.Resault + LvlProp2.Resault + LvlProp3.Resault + LvlAgit1.Resault + LvlAgit2.Resault + LvlParty1.Resault + LvlParty2.Resault + LvlParty3.Resault + LvlParty4.Resault + LvlUnity1.Resault + LvlUnity2.Resault + LvlUnity3.Resault + LvlLearn1.Resault + LvlLearn2.Resault + LvlLearn3.Resault + LvlAgit1.Resault + RedArmy.Resault + LvlMoney1.Resault + LvlMoney2.Resault + LvlMoney3.Resault;
    }

    public int GetLvlAgit()
    {
        return LvlAgit1.Resault + LvlAgit2.Resault + LvlAgit3.Resault;
    }

    public int GetLvlUnity()
    {
        return LvlUnity1.Resault+LvlUnity2.Resault+LvlUnity3.Resault;
    }

    public int GetLvlLearn()
    {
        return LvlLearn1.Resault + LvlLearn2.Resault + LvlLearn3.Resault;
    }

    public int GetLvlMoney()
    {
        return LvlMoney1.Resault + LvlMoney2.Resault + LvlMoney3.Resault;
    }

}