 using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class PlayedCards : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject Cards;

    [SerializeField]
    private GameObject Card;

    [SerializeField]
    private Text PlayerScoreText;
    [SerializeField]
    private GameObject PlayerListings;
    [SerializeField]
    private GameObject Forming;


    private PlayerListingsMenu playerListingsMenu;

    private FormingCryptoKits formingCryptoKits;

    private int playerScore;
    public double PlayerScore{
        get{return playerScore;}
    }
    private List<Card> playerCards;
    public List<Card> PlayerCards{
        get{return playerCards;}
        set{
            playerCards = value;
        }
    }

    private Dictionary<int, List<Card>> formPlayedCards;
    public Dictionary<int, List<Card>> FormPlayedCards{
        get{return formPlayedCards;}
        set{
            formPlayedCards = value;
        }
    }

    void Start(){
        playerCards = new List<Card>();
        playerScore = 0;
        formPlayedCards = new Dictionary<int, List<Card>>();
        formingCryptoKits = Forming.GetComponent<FormingCryptoKits>();
        
        // playerCards.Add(CardDatabase.StaticCardList[1]);
        // playerCards.Add(CardDatabase.StaticCardList[6]);
        // playerCards.Add(CardDatabase.StaticCardList[0]);
        // playerCards.Add(CardDatabase.StaticCardList[5]);
        // playerCards.Add(CardDatabase.StaticCardList[9]);
        // playerCards.Add(CardDatabase.StaticCardList[0]);
        // playerCards.Add(CardDatabase.StaticCardList[5]);
        // playerCards.Add(CardDatabase.StaticCardList[2]);
        // playerCards.Add(CardDatabase.StaticCardList[14]);
        // playerCards.Add(CardDatabase.StaticCardList[14]);
   //     InstantiateCards();
    }
    
    void Awake(){
        Debug.Log("PlayedCards Awake() called");
        playerListingsMenu = PlayerListings.GetComponent<PlayerListingsMenu>();
        // Debug.Log(playerListingsMenu);
        // int indexPlayer = playerListingsMenu.PlayerCardsPlay.Keys.ToList().IndexOf(PhotonNetwork.LocalPlayer);
        // List<Card> mainPlayerCardsPlay = playerListingsMenu.PlayerCardsPlay.Values.ElementAt(indexPlayer);
        // playerCards = mainPlayerCardsPlay;
    }
    public void OpenFormKit(){
    //    this.playerCards = mainPlayerPlayCards;
      //  formingCryptoKits.PlayerCards = mainPlayerPlayCards;
        // int indexPlayer = playerListingsMenu.PlayerCardsPlay.Keys.ToList().IndexOf(PhotonNetwork.LocalPlayer);
        // List<Card> mainPlayerCardsPlay = playerListingsMenu.PlayerCardsPlay.Values.ElementAt(indexPlayer);
        playerCards = GameManager.Instance.playerCardsPlay;
        Debug.Log("Played card size = " + GameManager.Instance.playerCardsPlay.Count);
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
    // private void DestroyCards(){
    //     for(int i = 0; i < Cards.transform.childCount; i++){
    //         GameObject card = Cards.transform.GetChild(i).gameObject;
    //         Destroy(card);
    //     }
        
    // }
    // public void RemoveCardsInPlayedCards(List<Card> kitForm){
    //     foreach(Card card in kitForm){
    //         int index = playerCards.FindIndex(x => x == card);
    //         Debug.Log(index);
    //         if(index != -1){
    //             playerCards.RemoveAt(index);
    //         }
    //     }
    //     GameManager.Instance.playerCardsPlay = playerCards;
    //     Debug.Log("Remove cards size now = " + GameManager.Instance.playerCardsPlay.Count);
    //     DestroyCards();
    //     InstantiateCards();
    // }

    public void CalculateScoreKits(List<Card> kitForm){
        this.playerScore += 16;
        if(CheckPenalty(kitForm)){
            foreach(Card card in kitForm){
                if(card.SecurityLevel == "low"){
                    this.playerScore -= 4;
                }
                else if(card.SecurityLevel == "medium"){
                    this.playerScore -= 2;
                }
            }
        }
        else{
            this.playerScore += 4;
        }
        GameManager.Instance.playerTotalScore = this.playerScore;
        string playerScoreString = this.playerScore.ToString();
        PlayerScoreText.text = playerScoreString;
    }

    private bool CheckPenalty(List<Card> kitForm){
        bool isPenalty = false; 
        foreach(Card card in kitForm){
            if(card.SecurityLevel == "low" || card.SecurityLevel == "medium"){
                isPenalty = true;
                return isPenalty;
            }
        }
        return isPenalty;
    }
    public bool CheckStillHaveCardsToForm(){
        List<string> kit1 = new List<string>();
        List<string> kit2_a = new List<string>();
        List<string> kit2_b = new List<string>();
        List<string> kit2_c = new List<string>();
        List<string> kit2_d = new List<string>();
        foreach(Card card in GameManager.Instance.playerCardsPlay){
            List<string> cardCombinedKits = card.CombinedKits;
            bool kit1Index = cardCombinedKits.Contains("1");

            int numberOfBCCardInKit2_c = 0;
            // check if 2_c already contain BC 
            foreach(string type in kit2_c){
                if(type == "BC"){
                    numberOfBCCardInKit2_c++;
                }    
            }

            if(cardCombinedKits.Contains("1") && !kit1.Contains(card.TypeCard)){
                kit1.Add(card.TypeCard);
            }
            if(cardCombinedKits.Contains("2a") && !kit2_a.Contains(card.TypeCard)){
                kit2_a.Add(card.TypeCard);
            }
            if(cardCombinedKits.Contains("2b") && !kit2_b.Contains(card.TypeCard)){
                kit2_b.Add(card.TypeCard);
            }
            if(cardCombinedKits.Contains("2c") && !kit2_c.Contains(card.TypeCard) && card.TypeCard != "BC" || card.TypeCard == "BC" && cardCombinedKits.Contains("2c") && numberOfBCCardInKit2_c < 2 ){
                kit2_c.Add(card.TypeCard);
            }
            if(cardCombinedKits.Contains("2d") && !kit2_d.Contains(card.TypeCard)){
                kit2_d.Add(card.TypeCard);
            }


        }

        if(kit1.Count == 2 || kit2_a.Count == 3 || kit2_b.Count == 3 || kit2_c.Count == 4 || kit2_d.Count == 4){
            Debug.Log("Kit1 count = " + kit1.Count);
            Debug.Log("Kit2a count = " + kit2_a.Count);
            Debug.Log("Kit2b count = " + kit2_b.Count);
            Debug.Log("Kit2c count = " + kit2_c.Count);
            Debug.Log("Kit2d count = " + kit2_d.Count);

            return true;
        }
        return false;
    }

    public void FormClicked(){
        this.transform.gameObject.SetActive(false);
    }

    


}
