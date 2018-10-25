using UnityEngine;
[System.Serializable]
public class PartyClass {
    int ActionPoints;
    int NumbersAllRegions;
    public int Warriors;
    public  float AdhsNum, Sovets = 0;
    public float Extrim = 0, Unity = 1, ChangeExtrim = 0, AccelerationExtrim = 0; 
    public int Status = 0;
   public int LvlOfPerkAgit, LvlOfPerkUnity, LvlOfPerkBiz, LvlOfPerkRec = 0;
    
    public void SetNumAllReg(int value)
    {
        NumbersAllRegions = value;
    }
    
    public int GetNumAllReg()
    {
        return NumbersAllRegions;
    }
    

}
