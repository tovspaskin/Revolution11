using UnityEngine;

[System.Serializable]
public class PartyLocalClass
{
    int ActionPoints = 10;
    public float Agit, Sovet, Rec, Money, Hide = 0;
    public int Status = 0;
public int GetActionParty() { return ActionPoints; }
    public void SetActionParty(int value)
    { ActionPoints = value; }
    public void AddActionParty(int value)
    { ActionPoints += value; }
}

