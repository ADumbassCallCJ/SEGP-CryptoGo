using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.Events;

public class FormingCryptoKits : MonoBehaviour
{

    [SerializeField]
    private GameObject CryptoKitPhase;

    [SerializeField]
    private GameObject Kit1;

    [SerializeField]
    private GameObject Kit2;

    [SerializeField]
    private GameObject Kit2_1;
    [SerializeField]
    private GameObject Kit2_2;

    [SerializeField]
    private GameObject ChooseCardPanel;

    [SerializeField]
    private GameObject Card;

    [SerializeField]
    private GameObject PlayedCards;
    [SerializeField]
    private GameObject Cards;
    [SerializeField]
    private GameObject FormKitNotification;
    [SerializeField]
    private GameObject FormKitButton;
    [SerializeField]
    private GameObject EndKitPhaseButton;
    [SerializeField]
    private GameObject EndKitPhase;
    private PlayedCards playedCards;

    private GameObject warning1;
    private GameObject warning2;
    private GameObject warning3;

    private CardDatabase cardDatabase;

    private List<Card> playerCards;
    public List<Card> PlayerCards{
        get{return playerCards;}
        set{
            playerCards = value;
        }
    }


    private Dictionary<int, List<Card>> kitsForm; 
    


    // Start is called before the first frame update
    void Start()
    {
       
        warning1 = Kit1.transform.Find("Warning").gameObject;
        warning1.SetActive(false);
        warning2 = Kit2_1.transform.Find("Warning").gameObject;
        warning2.SetActive(false);
        warning3 = Kit2_2.transform.Find("Warning").gameObject;
        warning3.SetActive(false);
        Kit1.SetActive(false);
        Kit2.SetActive(false);
        Kit2_1.SetActive(false);
        Kit2_2.SetActive(false);
        FormKitNotification.SetActive(false);
        EndKitPhaseButton.SetActive(false);
        EndKitPhase.SetActive(false);

        kitsForm = new Dictionary<int, List<Card>>();
   //     ChooseCardPanel.SetActive(false);

        cardDatabase = GameObject.Find("Background Image").GetComponent<CardDatabase>();
    }
    void Awake(){

        playedCards = PlayedCards.GetComponent<PlayedCards>();
        playerCards = GameManager.Instance.playerCardsPlay;
        
    }

    public void BC_Card_Clicked(){
    
        GameObject _BCPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit1.transform);
        GameObject CardNumberObject = _BCPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        GameObject _BackButton = _BCPanel.transform.Find("Back button").gameObject; 
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_BCPanel); });

        GameObject cardsObject = GameObject.Find("ChooseCardPanel(Clone)/Cards");
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "BC"){
                cardList.Add(card);
                // Debug.Log("add " + card.Name);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _BCPanel);

    }
    private void DestroyCards(){
        for(int i = 0; i < Cards.transform.childCount; i++){
            GameObject card = Cards.transform.GetChild(i).gameObject;
            Destroy(card);
        }
        
    }
    public void RemoveCardsInPlayedCards(List<Card> kitForm){
        foreach(Card card in kitForm){
            int index = playerCards.FindIndex(x => x == card);
            Debug.Log(index);
            if(index != -1){
                playerCards.RemoveAt(index);
            }
        }
        GameManager.Instance.playerCardsPlay = playerCards;
        Debug.Log("Remove cards size now = " + GameManager.Instance.playerCardsPlay.Count);
        DestroyCards();
        InstantiateCards();
    }

    private void InstantiateCards(){
         foreach(Card card in GameManager.Instance.playerCardsPlay){
            GameObject _Card = Instantiate(Card, transform.position, transform.rotation,Cards.transform);
            ThisCard thisCard = _Card.GetComponent<ThisCard>();
            thisCard.thisCardId = card.Id;
            _Card.tag ="Clone";
        }
    }
        public void BC_Card_Clicked2(){
    
        GameObject _BCPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_2.transform);
        GameObject CardNumberObject = _BCPanel.transform.Find("Card Number").gameObject;
        
   //     Debug.Log(CardNumberObject);
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
    //    Debug.Log(CardNumberText);
        GameObject _BackButton = _BCPanel.transform.Find("Back button").gameObject;
     //   Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_BCPanel); });

        GameObject cardsObject = GameObject.Find("ChooseCardPanel(Clone)/Cards");
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "BC"){
                cardList.Add(card);
                // Debug.Log("add " + card.Name);
            }
        }
        Debug.Log(cardList.Count);
        
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _BCPanel);

    }
    public void AE_Card_Clicked(){
    
        GameObject _AEPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit1.transform);
        GameObject _BackButton = _AEPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _AEPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_AEPanel); });

        GameObject cardsObject = GameObject.Find("ChooseCardPanel(Clone)/Cards");
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "AE"){
                cardList.Add(card);
            }
        }
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _AEPanel);
    }

      public void SC_Card_Clicked(){
    
        GameObject _SCPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_1.transform);
        GameObject _BackButton = _SCPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _SCPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_SCPanel); });

       
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "SC"){
                cardList.Add(card);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _SCPanel);

    }

    public void MAC_Card_Clicked1(){
    
        GameObject _MACPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_1.transform);
        GameObject _BackButton = _MACPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _MACPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_MACPanel); });

       
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "MAC"){
                cardList.Add(card);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _MACPanel);

    }
      public void MAC_Card_Clicked2(){
    
        GameObject _MACPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_2.transform);
        GameObject _BackButton = _MACPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _MACPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_MACPanel); });

       
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "MAC"){
                cardList.Add(card);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _MACPanel);

    }
        public void OM_Card_Clicked(){
        
        GameObject _OMPanel  = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_2.transform);
        GameObject _BackButton = _OMPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _OMPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_OMPanel); });

       
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "OM"){
                cardList.Add(card);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _OMPanel);

    }
        public void H_BC_Card_Clicked1(){
    
        GameObject _HBCPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_1.transform);
        GameObject _BackButton = _HBCPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _HBCPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_HBCPanel); });


       
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "H" || card.TypeCard == "BC"){
                cardList.Add(card);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _HBCPanel);

    }
    public void H_BC_Card_Clicked2(){
    
        GameObject _HBCPanel = Instantiate(ChooseCardPanel, transform.position, transform.rotation, Kit2_2.transform);
        GameObject _BackButton = _HBCPanel.transform.Find("Back button").gameObject;
        GameObject CardNumberObject = _HBCPanel.transform.Find("Card Number").gameObject;
        Text CardNumberText = CardNumberObject.GetComponent<Text>();
        Debug.Log(_BackButton);
        Button backButton = _BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(delegate {CancelChoosingPanel(_HBCPanel); });
   
       
        List<Card> cardList = new List<Card>();
        Debug.Log(CardDatabase.StaticCardList.Count);
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            if(card.TypeCard == "H" || card.TypeCard == "BC"){
                cardList.Add(card);
            }
        }
        Debug.Log(cardList.Count);
        CardNumberText.text = cardList.Count.ToString();
        InstantiateCards(cardList, _HBCPanel);

    }
    private void InstantiateCards(List<Card> cardList, GameObject panel){
         GameObject cardsObject = GameObject.Find("ChooseCardPanel(Clone)/Cards");
        foreach(Card card in cardList){
            GameObject _Card = Instantiate(Card, transform.position, transform.rotation,cardsObject.transform);
            ThisCard thisCard = _Card.GetComponent<ThisCard>();
            thisCard.thisId = card.Id;
            Button cardClick = _Card.GetComponent<Button>();
            cardClick.onClick.AddListener(delegate {PickCard(card, panel); });
        }
    }
    public void PickCard(Card card, GameObject panel){
        Debug.Log("You clicked card " + card.Name);
        GameObject parent = panel.transform.parent.gameObject;
        Destroy(panel);
      
        Debug.Log(parent.ToString());
        string path = parent.name + "/" + card.TypeCard;
        Debug.Log(path);
        GameObject transformObject = GameObject.Find(parent.name + "/" + card.TypeCard);
        if(transformObject.transform.childCount > 0 && card.TypeCard == "BC"){
             transformObject = GameObject.Find(parent.name + "/BC(1)");
        }
    
        // Debug.Log("Child count = " + transformObject.transform.childCount);
        // if(transformObject.transform.childCount >= 1){
        //     GameObject child = transformObject.transform.GetChild(0).gameObject;
        //     Debug.Log(child);
        //     Destroy(transformObject.transform.GetChild(0).gameObject);
        // }
        GameObject _Card = Instantiate(Card, transformObject.transform.position, transformObject.transform.rotation, transformObject.transform);
        ThisCard thisCard = _Card.GetComponent<ThisCard>();
        thisCard.thisId = card.Id;
 //       Debug.Log(thisCard.thisId);
        _Card.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        
        Debug.Log(transformObject);
    }

    public void CancelChoosingPanel(GameObject panel){
        Debug.Log("Cancel Button clicked");
        Destroy(panel);
    }
    public void ChooseKit1(){
        CryptoKitPhase.SetActive(false);
        Kit1.SetActive(true);
    }

    
    public void ChooseKit2(){
        CryptoKitPhase.SetActive(false);
        Kit2.SetActive(true);
    }
    
    public void ChooseKit2_1(){
        Kit2.SetActive(false);
        Kit2_1.SetActive(true);
    }
    public void ChooseKit2_2(){
        Kit2.SetActive(false);
        Kit2_2.SetActive(true);
    }

    public void ContinueKit1(){
        GameObject BC_Object = GameObject.Find("Kit1/BC");
        GameObject AE_Object = GameObject.Find("Kit1/AE");
        List<Card> cards = new List<Card>();

        
        if(BC_Object.transform.childCount > 0){
            ThisCard BCCard = BC_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[BCCard.thisId]);
        }
        if(AE_Object.transform.childCount > 0){
            ThisCard AECard = AE_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[AECard.thisId]);
        }
        if(cards.Count < 2){
            Debug.Log("Not enough to form a kit");
            warning1.SetActive(true);
        }
        else if(cards.Count == 2){
            playedCards.CalculateScoreKits(cards);
            RemoveCardsInPlayedCards(cards);
            kitsForm.Add(kitsForm.Count, cards);
            Debug.Log("Still have cards to form : " + playedCards.CheckStillHaveCardsToForm());
            warning1.SetActive(false);
            ClearCardChooseKit1();
            Kit1.SetActive(false);
            CryptoKitPhase.SetActive(true);
            PlayedCards.SetActive(true);
            if(!playedCards.CheckStillHaveCardsToForm()){
                FormKitNotification.SetActive(true);
                FormKitButton.SetActive(false);
                EndKitPhaseButton.SetActive(true);
            }
        }

    }
    public void ClearCardChooseKit1(){
        GameObject BC_Object = GameObject.Find("Kit1/BC");
        GameObject AE_Object = GameObject.Find("Kit1/AE");

        
        if(BC_Object.transform.childCount > 0){
            Destroy(BC_Object.transform.GetChild(0).gameObject);
        }
        if(AE_Object.transform.childCount > 0){
            Destroy(AE_Object.transform.GetChild(0).gameObject);
        }
    }

    public void ContinueKit2_1(){
        List<Card> cards = new List<Card>();
        GameObject SC_Object = GameObject.Find("Kit2_1/SC");
        GameObject MAC_Object = GameObject.Find("Kit2_1/MAC"); 
        GameObject H_Object = GameObject.Find("Kit2_1/H");
        GameObject BC_Object = GameObject.Find("Kit2_1/BC");  

        if(SC_Object.transform.childCount > 0){
            ThisCard SCCard = SC_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[SCCard.thisId]);
        }
        if(MAC_Object.transform.childCount > 0){
            ThisCard MACCard = MAC_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[MACCard.thisId]);
        }
        if(H_Object.transform.childCount > 0){
            ThisCard HCard = H_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[HCard.thisId]);
        }
        if(BC_Object.transform.childCount > 0){
            ThisCard BCCard = BC_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[BCCard.thisId]);
        }
        if(cards.Count < 3){
            Debug.Log("Not enough to form a kit");
            warning2.SetActive(true);
        }
        else if(cards.Count == 3){
            playedCards.CalculateScoreKits(cards);
            RemoveCardsInPlayedCards(cards);
            kitsForm.Add(kitsForm.Count, cards);
            Debug.Log("Still have cards to form : " + playedCards.CheckStillHaveCardsToForm());
            warning2.SetActive(false);
            ClearCardChooseKit2_1();
            Kit2_1.SetActive(false);
            CryptoKitPhase.SetActive(true);
            PlayedCards.SetActive(true);
            if(!playedCards.CheckStillHaveCardsToForm()){
                FormKitNotification.SetActive(true);
                FormKitButton.SetActive(false);
                EndKitPhaseButton.SetActive(true);
            }
        }

    }
    public void ClearCardChooseKit2_1(){
        GameObject SC_Object = GameObject.Find("Kit2_1/SC");
        GameObject MAC_Object = GameObject.Find("Kit2_1/MAC"); 
        GameObject H_Object = GameObject.Find("Kit2_1/H");
        GameObject BC_Object = GameObject.Find("Kit2_1/BC");  

        if(SC_Object.transform.childCount > 0){
            Destroy(SC_Object.transform.GetChild(0).gameObject);
        }
        if(MAC_Object.transform.childCount > 0){
            Destroy(MAC_Object.transform.GetChild(0).gameObject);
        }
        if(H_Object.transform.childCount > 0){
            Destroy(H_Object.transform.GetChild(0).gameObject);
        }
        if(BC_Object.transform.childCount > 0){
            Destroy(BC_Object.transform.GetChild(0).gameObject);
        }
        
    }

    public void ContinueKit2_2(){
        List<Card> cards = new List<Card>();
        GameObject OM_Object = GameObject.Find("Kit2_2/OM");
        GameObject BC_Object = GameObject.Find("Kit2_2/BC"); 
        GameObject MAC_Object = GameObject.Find("Kit2_2/MAC");
        GameObject BC1_Object = GameObject.Find("Kit2_2/BC(1)");  
        GameObject H_Object = GameObject.Find("Kit2_2/H");

        if(OM_Object.transform.childCount > 0){
            ThisCard OMCard = OM_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[OMCard.thisId]);
        }
        if(BC_Object.transform.childCount > 0){
            ThisCard BCCard = BC_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[BCCard.thisId]);
        }
        if(MAC_Object.transform.childCount > 0){
            ThisCard MACCard = MAC_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[MACCard.thisId]);
        }
        if(BC1_Object.transform.childCount > 0){
            ThisCard BC1Card = BC1_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[BC1Card.thisId]);
        }
        if(H_Object.transform.childCount > 0){
            ThisCard HCard = H_Object.transform.GetChild(0).gameObject.GetComponent<ThisCard>();
            cards.Add(CardDatabase.StaticCardList[HCard.thisId]);
        }
        if(cards.Count < 4){
            Debug.Log("Not enough to form a kit");
            warning3.SetActive(true);
        }
        else if(cards.Count == 4){
            playedCards.CalculateScoreKits(cards);
            RemoveCardsInPlayedCards(cards);
            kitsForm.Add(kitsForm.Count, cards);
            Debug.Log("Still have cards to form : " + playedCards.CheckStillHaveCardsToForm());
            warning3.SetActive(false);
            ClearCardChooseKit2_2();
            Kit2_2.SetActive(false);
            CryptoKitPhase.SetActive(true);
            PlayedCards.SetActive(true);
            if(!playedCards.CheckStillHaveCardsToForm()){
                FormKitNotification.SetActive(true);
                FormKitButton.SetActive(false);
                EndKitPhaseButton.SetActive(true);
            }
        }

    }

    public void ClearCardChooseKit2_2(){
        GameObject OM_Object = GameObject.Find("Kit2_2/OM");
        GameObject BC_Object = GameObject.Find("Kit2_2/BC"); 
        GameObject MAC_Object = GameObject.Find("Kit2_2/MAC");
        GameObject BC1_Object = GameObject.Find("Kit2_2/BC(1)");  
        GameObject H_Object = GameObject.Find("Kit2_2/H");  
        if(OM_Object.transform.childCount > 0){
            Destroy(OM_Object.transform.GetChild(0).gameObject);
        }
        if(BC_Object.transform.childCount > 0){
            Destroy(BC_Object.transform.GetChild(0).gameObject);
        }
        if(MAC_Object.transform.childCount > 0){
            Destroy(MAC_Object.transform.GetChild(0).gameObject);
        }
        if(BC1_Object.transform.childCount > 0){
            Destroy(BC1_Object.transform.GetChild(0).gameObject);
        }
        if(H_Object.transform.childCount > 0){
            Destroy(H_Object.transform.GetChild(0).gameObject);
        }
    }

    public void EndKitPhaseClicked(){
        EndKitPhase.SetActive(true);
    }

    public void BackKit2(){
        Kit2.SetActive(true);
        Kit2_1.SetActive(false);
        Kit2_2.SetActive(false);
    }

    public void BackFormKit(){
        CryptoKitPhase.SetActive(true);
        Kit1.SetActive(false);
        Kit2.SetActive(false);
    }


}
