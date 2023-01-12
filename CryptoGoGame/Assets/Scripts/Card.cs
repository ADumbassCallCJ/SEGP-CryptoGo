using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{
    public int Id;
    public string TypeCard;
    public string Name;
    public string Description;
    public string ToolName;
    public List<string> CombinedKits;

    public Card(){

    }
    public Card(int Id, string TypeCard, string Name, string Description, string ToolName,
                 List<string> CombinedKits)
    {
        this.Id = Id;
        this.TypeCard = TypeCard;
        this.Name = Name;
        this.Description = Description;
        this.ToolName = ToolName;
        this.CombinedKits = CombinedKits;
    }
    public Card(int Id, string TypeCard, string Name, string Description, string ToolName)
    {
        this.Id = Id;
        this.TypeCard = TypeCard;
        this.Name = Name;
        this.Description = Description;
    }


}
