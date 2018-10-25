using UnityEngine;
using UnityEngine.UI;


public class SelectRegion : MonoBehaviour {

	public StateScript Top;
	public GameObject _Current;
	public string NameReg;
	public int Pop, Adhs, Netral, Enem, PartyMen, Warriors;
    public  PartyLocalClass PartyLoc;
    public PerkTreeLoc PerksLoc = new PerkTreeLoc();
    public GameObject LastIconsFrameLoc = null;
    public bool CreateParty = false;
    int DaysOfCivilWar = 0;
    public int NumLocShi = 0;
    public bool CommonPar =  true;
    public bool scboll, flagsc;
    private Vector3 StartScale, TargetScale;
    Color startColor;

    private void Awake()
    {
        Top = GameObject.FindGameObjectWithTag("State").GetComponent<StateScript>();
        _Current = gameObject;
        float Ran1 = Random.Range(5, 40);
        Enem = (int)(Pop * (Ran1 / 100));
        float Ran2 = Random.Range(5, 30);
        Adhs = (int)(Pop * (Ran2 / 100));
        Netral = Pop - (Enem + Adhs);
        PerksLoc.Ss = Top;
        PerksLoc.SetUI();
        PerksLoc.CanvelLearn();
        PerksLoc.LearnProsessLoc(PartyLoc.Agit, PartyLoc.Sovet, PartyLoc.Rec, PartyLoc.Money, PartyLoc.Hide, Top.Lider.PartyBuild);
        StartScale = gameObject.transform.localScale;
        TargetScale = new Vector3(StartScale.x * 1.01f, StartScale.y * 1.01f, StartScale.z * 1f);
        scboll = false;
    }

    void Start ()
    {
        startColor = GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        if (scboll == true)
        {
            switch (flagsc)
            {
                case true:
                    GetComponent<Renderer>().material.color += new Color(Time.deltaTime, Time.deltaTime, 0);
                    gameObject.transform.localScale -= new Vector3(Time.deltaTime/20, Time.deltaTime/20, 0);
                    break;
                case false:
                    GetComponent<Renderer>().material.color -= new Color(Time.deltaTime, Time.deltaTime, 0);
                    gameObject.transform.localScale += new Vector3(Time.deltaTime/20, Time.deltaTime/20, 0);
                    break;

            }
            if (gameObject.transform.localScale.x > TargetScale.x) flagsc = true;
            if (gameObject.transform.localScale.x <= StartScale.x)
            {
                flagsc = false;
                //scboll = false;
                GetComponent<Renderer>().material.color = Color.white;
                gameObject.transform.localScale = StartScale;
            }
        }
    }

    public void StopPuls()
    {
        scboll = false;
        GetComponent<Renderer>().material.color = startColor;
        gameObject.transform.localScale = StartScale;
    }

    void OnMouseDown()
    {
        startColor = GetComponent<Renderer>().material.color;
        Top.Select(_Current, NameReg, Pop.ToString(), Netral.ToString(), Enem.ToString(), Adhs.ToString());
        Top.UpdateInfo(this);
        Top.UpdateDiagram();
        if (Top.LastIconsFrameLoc != null & NumLocShi == 0) Top.LastIconsFrameLoc.SetActive(false);
        if (LastIconsFrameLoc != null)LastIconsFrameLoc.SetActive(true);
        if (Top.Party.Status == 0)
        Top.UiElems[5].GetComponent<Text>().text = "Создать партию";
        else Top.UiElems[5].GetComponent<Text>().text = "Развитие партии";
        PerksLoc.IconsProsessLoc();
        if (LastIconsFrameLoc != null)
        {
            Top.LastIconsFrameLoc.SetActive(false);
            LastIconsFrameLoc.SetActive(true);
            Top.LastIconsFrameLoc = LastIconsFrameLoc;
        }
    } 
    public void LocalUpdate()
    {
        //Прибавка Сторонников
        //float x = (PartyLoc.Agit + Top.Lider.Agit * Mathf.Clamp(PerksLoc.LvlLocProp0.Resault+Top.Lider.GetMnj(this), 0, 1)) * (1 + PerksLoc.LvlLocProp1.Resault*2 + PerksLoc.LvlLocProp2.Resault*2 + PerksLoc.LvlLocProp3.Resault*2);
        //AdhsPlus((float)(Mathf.Pow(2, (0.18f*(x)))+ ((Top.Perks.LvlAgit1.Resault + Top.Perks.LvlAgit2.Resault + Top.Perks.LvlAgit3.Resault) * 0.00001 * Pop)));
        AdhsPlus((
            100*(1-Mathf.Exp(-(Top.Lider.Agit*(1+Top.Perks.GetLvlAgit())*Top.Lider.ExpOfAgit)*Top.Party.Unity*Top.Lider.GetMnj(this))/5))
            +(PartyLoc.Agit*100*(1+PerksLoc.GetAgitLoc()))*(1-Mathf.Exp(-Top.Party.GetNumAllReg()/10))
            +PartyMen*PartyLoc.Agit/10*Top.Party.Unity);
        float fUnity = ((Mathf.Pow(0.5f, (0.3f * (Top.Party.Unity*100 - 20))) + Mathf.Pow(0.5f, (0.07f * (Top.Party.Unity*100 - 35)))) / (1 + PerksLoc.LvlLocParty1.Resault + PerksLoc.LvlLocParty2.Resault + PerksLoc.LvlLocParty3.Resault + PerksLoc.LvlLocParty4.Resault));
        NetralPlus(Top.Lider.Agit * Top.Lider.ExpOfAgit*Top.Perks.LvlAgit2.Resault);
        PartyMen = (int)Mathf.Clamp(PartyMen + PartyLoc.Rec * (1 + PerksLoc.GetPartyLoc())*Top.Party.Unity+ Mathf.Pow(PartyMen * Top.Party.Unity, 0.5f)-fUnity, 0, Adhs);
        if (PerksLoc.LocRedArmy.Resault == 1) Warriors = Mathf.Clamp((int)(Warriors + PartyLoc.Rec+Top.Lider.PartyBuild)*25, 0, Adhs);
        if (Top.CivilWar == true) BettleInWar();
        PerksLoc.LearnProsessLoc(PartyLoc.Agit, PartyLoc.Sovet, PartyLoc.Rec, PartyLoc.Money, PartyLoc.Hide, Top.Lider.PartyBuild);
        if (Top.current == this) PerksLoc.IconsProsessLoc();
    }

    public float GetLocalExtrimFactor()
    {
        return PartyLoc.Agit + PartyLoc.Sovet + PartyLoc.Rec;
    }

    void AdhsPlus( float value)
    {
        if (value + Adhs <= Pop-Enem)
        {
            Adhs += (int)value;
            //int Min1 = Random.Range(0, (int)value);
            //int Min2 = (int)value - Min1;
            Netral -= (int)value;
            //Enem -= Min2;
        }
    }
    void NetralPlus(float value)
    {
        if  (Enem >= value)
        {
          if (value + Netral <= Pop)
            if (Top.Perks.LvlAgit3.Resault == 0)
            {
                Netral += (int)value;
                float Min1 = value * 2 / 3 + Random.Range(0, value / 3);
                int Min2 = (int)(value - Min1);
                Enem -= (int)Min1;
                Adhs -= Min2;
            }
            else
            {
                Netral += (int)value;
                Enem -= (int)value;
            }
        }
        else { Netral += Enem; Enem = 0; }
    }
    void EnemPlus(float value)
    {
        if (value + Enem <= Pop)
        {
            Enem += (int)value;
            int Min1 = Random.Range(0, (int)value);
            int Min2 = (int)value - Min1;
            Netral -= Min1;
            Adhs -= Min2;
        }
    }
    void BettleInWar()
    {
        float Zn = (Mathf.Pow(2, (0.03f*DaysOfCivilWar)));
        Enem -= (int)Zn + Warriors;
        Warriors -= (int)Zn;
        Adhs -= (int)Zn;
        Pop = Adhs+Enem+Netral;
        DaysOfCivilWar++;
    }
}
