using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public int thisId;
    public int Id;
    public string CardType;
    public string CardName;
    public string CardDescription;
    public string CardToolName;
    public List<string> CardCombinedKits = new List<string>();


    
    
    public Text CardTypeText;
    public Text CardNameText;
    public Text CardDescriptionText;
    public Text CardToolNameText;
    public Text CardCombinedKitsText;

    public Image CardColor;

    public Transform DecorFrame;

    public bool CardBack; 
    public static bool staticCardBack;

    public GameObject Hand;
    public int NumberOfCardsInDeck;

    private Transform thisTF;
    private Vector3 cardInitialPosition;
    private bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        thisTF = GetComponent<Transform>();
        cards.Add(new Card());
 //       cards[0] = CardDatabase.StaticCardList[thisId];
        CardBack = true;
        NumberOfCardsInDeck = PlayerDeck.DeckSize;
        cardInitialPosition = transform.position;
        Debug.Log(NumberOfCardsInDeck);
    }

    // Update is called once per frame
    void Update()
    {
        //assign data to cards
        if(this.tag == "Clone"){
            Debug.Log("triggered");
            cards[0] = PlayerDeck.StaticDeck[NumberOfCardsInDeck-1];
            NumberOfCardsInDeck--;
            PlayerDeck.DeckSize--;
            this.tag = "Untagged";
        }

        //open cards if it is the Hand Panel(Main playuer)
        Hand = GameObject.Find("Hand");
 //       Debug.Log("this.transform.parent: " + this.transform.parent);
 //       Debug.Log("Hand.transform: " + Hand.transform);
        if(this.transform.parent == Hand.transform){
 //           Debug.Log("Hand Clone called");
            CardBack = false;
        }
        

        cards[0] = CardDatabase.StaticCardList[thisId];
        Id = cards[0].Id;
        CardType = cards[0].TypeCard;
        CardName = cards[0].Name;
        CardDescription = cards[0].Description;
        CardToolName = cards[0].ToolName;
        CardCombinedKits = cards[0].CombinedKits;

;


        staticCardBack = CardBack;

        CardTypeText.text = "" + CardType;
        CardNameText.text = "" + CardName;
        CardDescriptionText.text = "" + CardDescription;
        CardToolNameText.text = "" + CardToolName;

        string CardCombinedKitsString = "";
        foreach(string s in CardCombinedKits){
            CardCombinedKitsString += "\t" + s;
            
        }
        CardCombinedKitsText.text = CardCombinedKitsString;

        DecorFrame = this.transform.GetChild(1);

        

        // Color the Card
        ColorTheCard();




    }

    public void ColorTheCard(){
        if(CardType == "SC"){
            CardColor.GetComponent<Image>().color = new Color32(198,54,64,255);
            ChangeColorTextCard(new Color32(53,45,54,255));
            ChangeDecorTexts(new Color32(214,123,129,255));
        }
        else if(CardType == "BC"){
            CardColor.GetComponent<Image>().color = new Color32(255,166,203,255);
            ChangeColorTextCard(new Color32(61,45,54,255));
            ChangeDecorTexts(new Color32(249,193,216,255));

        }
        else if(CardType == "H"){
            CardColor.GetComponent<Image>().color = new Color32(241,155,69,255);
            ChangeColorTextCard(new Color32(64,51,45,255));
            ChangeDecorTexts(new Color32(240,182,126,255));
        }
        else if(CardType == "OM"){
            CardColor.GetComponent<Image>().color = new Color32(250,239,147,255);
            ChangeColorTextCard(new Color32(60,58,43,255));
            ChangeDecorTexts(new Color32(253,247,196,255));
        }
        else if(CardType == "AE"){
            CardColor.GetComponent<Image>().color = new Color32(117,185,117,255);
            ChangeColorTextCard(new Color32(37,49,40,255));
            ChangeDecorTexts(new Color32(166,209,166,255));
        }
        else if(CardType == "MAC"){
            CardColor.GetComponent<Image>().color = new Color32(97,169,218,255);
            ChangeColorTextCard(new Color32(33,46,57,255));
            ChangeDecorTexts(new Color32(149,197,229,255));
        }
    }

    public void ChangeColorTextCard(Color32 colorText){
        foreach(Transform child in this.transform){
            if(child.GetComponent<Text>() != null){
                child.GetComponent<Text>().color = colorText;
            }
        }
    }
    public void ChangeDecorTexts(Color32 colorTextDecor){
        foreach(RectTransform child in DecorFrame){
            if(child.GetComponent<Text>() != null){
                child.GetComponent<Text>().color = colorTextDecor;
            }
        }
            
     }

    // public void OnPointerClick(PointerEventData eventData)
    // {
    //     Debug.Log("You choose the card " + CardName);
    //     Debug.Log(transform.position);
    //     if(!isClicked){
    //         GameObject ClickedCard = GameObject.Find("ClickedCard");
    //         // Debug.Log(transform.parent);
    //         transform.SetParent(ClickedCard.transform,false);
    //         transform.localScale = new Vector3(0.7f,0.7f,1);
    //     //  //   transform.parent = ClickedCard.transform;
    //         // Debug.Log(transform.parent);
    //         transform.position = ClickedCard.transform.position;
    //         // Debug.Log(transform.position);
    //     }
    //     else{
    //         isClicked = false;
    //         GameObject Hand = GameObject.Find("Hand");
    //         transform.SetParent(Hand.transform, false);
    //         transform.localScale = new Vector3(1f,1f,1);
    //         transform.position = cardInitialPosition;
    //         Debug.Log(transform.position);
    //     }

    
    // }
}



