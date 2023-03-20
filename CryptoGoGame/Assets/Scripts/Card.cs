using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{
    private int id;
    public int Id{
        get{return id;}
    }
    private string typeCard;
    public string TypeCard{
        get{return typeCard;}
    }

    private string name;
    public string Name{
        get{return name;}
    }

    private string description;
    public string Description{
        get{return description;}
    }

    private string toolName;
    public string ToolName{
        get{return toolName;}
    }

    private List<string> combinedKits;
    public List<string> CombinedKits{
        get{return combinedKits;}
        set{
            combinedKits = value;
        }
    }

    private string securityLevel;
    public string SecurityLevel{
        get{return securityLevel;}
    }

    public Card(){

    }
    public Card(int Id, string TypeCard, string Name, string Description, string ToolName,
                 List<string> CombinedKits, string securityLevel)
    {
        this.id = Id;
        this.typeCard = TypeCard;
        this.name = Name;
        this.description = Description;
        this.toolName = ToolName;
        this.combinedKits = CombinedKits;
        this.securityLevel = securityLevel;
    }
    public Card(int Id, string TypeCard, string Name, string Description, string ToolName, string securityLevel)
    {
        this.id = Id;
        this.typeCard = TypeCard;
        this.name = Name;
        this.description = Description;
        this.securityLevel = securityLevel;
    }


}
