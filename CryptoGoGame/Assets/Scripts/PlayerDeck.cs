using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


public class PlayerDeck : MonoBehaviourPunCallbacks
{
    /*deck is Deck of cards*/
    public static List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public int NumberOfPlayers;
    public static int DeckSize;

    /*StaticNumberOfDrawCard is the number of cards that each player will receive*/
    public static int StaticNumberOfDrawCard;
    private PlayerListingsMenu playerListingsMenu;

    public static List<Card> StaticDeck = new List<Card>();

    private List<string> enemyPanelNameList = new List<string>();
    public int enemyPanelNameListSize; 

    public GameObject Hand;
    public GameObject CardToHand;

    public GameObject CardBack;
    public GameObject Enemy;
    public static GameObject pickCard;
    public static GameObject pickCardZone;
    private GameObject playZone; 
    private List<Player> _players = new List<Player>();
    private  Dictionary<Player, List<Card>> playerDecks = new Dictionary<Player, List<Card>>();
    
    private int totalCardNumber = 0; 
    public int TotalCardNumber{
        get{ return totalCardNumber;}
        
    }
    private static bool isShuffle = false;
    private static bool isCardAdd = false;

    private static int turn = 1;


    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("PlayerDeck.Start() called");

        pickCard = GameObject.Find("PickCard");
        pickCardZone = GameObject.Find("PickCardZone");
        pickCard.SetActive(false);

        playerListingsMenu = GameObject.Find("PlayerListings").GetComponent<PlayerListingsMenu>();
        // playZone = GameObject.Find("PlayZone");
        // pickCard.SetActive(false);
    //     NumberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
    //     DeckSize = CardDatabase.StaticCardList.Count*NumberOfPlayers;
    //     enemyPanelNameListSize = PhotonNetwork.PlayerListOthers.Length;

    //  //   AddCardsToDeck(NumberOfPlayers);

    //     StaticNumberOfDrawCard = 6;
        enemyPanelNameList.Add("EnemyHand (1)");
        enemyPanelNameList.Add("EnemyHand (2)");
        enemyPanelNameList.Add("EnemyHand (3)");
        foreach(string name in enemyPanelNameList){
            Debug.Log(name);
        }

    }
    public void ResetDeck(){
        deck = new List<Card>();
        isShuffle = false;
        isCardAdd = false;
        totalCardNumber = 0;
    }

    public Player GetCurrentPlayerTurn(){
        return playerTurn(turn);
    }    

    [PunRPC]
    public Player RPC_NextTurn(){        
        turn++; 
        if(turn > _players.Count){
            turn = 1;
        }
        Debug.Log("It's " + playerTurn(turn) + " turn");
        return playerTurn(turn);
        
    }

    [PunRPC]
    public void RPC_PlayCards(Player player, int cardId){
          Debug.Log("PlayCards() called");
        // // remove playerDecks
        int indexPlayer = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player);
        Debug.Log(playerListingsMenu.PlayerDecks.Count);
        List<Card> mainPlayerCards = playerListingsMenu.PlayerDecks.Values.ElementAt(indexPlayer);
        foreach(Card card in mainPlayerCards){
            Debug.Log(player + " has: " + card.Name);
        }
 
        int cardIndexInMainPlayerCards = mainPlayerCards.FindIndex(x => x.Id == cardId);
        Debug.Log("Card found at " +cardIndexInMainPlayerCards);
        // // add playerCardsPlay
        if(cardIndexInMainPlayerCards != -1){
            
            // mainPlayerCardsPlay.Add(mainPlayerCards[cardIndexInMainPlayerCards]);
       //     Debug.Log(mainPlayerCardsPlay.Count);
       //     Debug.Log(player + " played " + mainPlayerCardsPlay[mainPlayerCardsPlay.Count-1].Name + " card");

            // add new play card to play card list to server
            int indexPlayer1 = playerListingsMenu.PlayerCardsPlay.Keys.ToList().IndexOf(player);
            List<Card> mainPlayerCardsPlay = playerListingsMenu.PlayerCardsPlay.Values.ElementAt(indexPlayer1);
            
            mainPlayerCardsPlay.Add(mainPlayerCards[cardIndexInMainPlayerCards]);
            Debug.Log("MainPlayerCardsPlay size = " +mainPlayerCardsPlay.Count);
            int[] myCardsPlay= new int[mainPlayerCardsPlay.Count];
            int b = 0;
            foreach(Card card in mainPlayerCardsPlay){
                myCardsPlay[b++] = card.Id;
            }
            // ExitGames.Client.Photon.Hashtable setCardsPlayValue = new ExitGames.Client.Photon.Hashtable();
            // setCardsPlayValue["PlayerCardsPlay"] = myCardsPlay;
            // Debug.Log(player.SetCustomProperties(setCardsPlayValue));

            mainPlayerCards.RemoveAt(cardIndexInMainPlayerCards);
            // update server player list
            int[] myCards = new int[mainPlayerCards.Count];
            int a = 0;
            foreach(Card card in mainPlayerCards){
                myCards[a++] = card.Id;
            }
            ExitGames.Client.Photon.Hashtable setCardsValue = new ExitGames.Client.Photon.Hashtable();
            setCardsValue["PlayerCardsPlay"] = myCardsPlay;
            setCardsValue["PlayerCards"] = myCards;
            Debug.Log(player.SetCustomProperties(setCardsValue));


            if(player != PhotonNetwork.LocalPlayer){
                int enemyPanelIndex = -1; 
                for(int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++){
                    if(player == PhotonNetwork.PlayerListOthers[i]){
                        Debug.Log("Enemy Player : " + player + " found");
                        enemyPanelIndex = i;
                        Debug.Log("Enemy index = " + enemyPanelIndex);
                    }
                }
            
                Debug.Log("Enemy Panel name = " + enemyPanelNameList[enemyPanelIndex]);
                if(enemyPanelIndex != -1){
                    GameObject EnemyHand = GameObject.Find(enemyPanelNameList[enemyPanelIndex]);
                    GameObject cardBack = EnemyHand.transform.GetChild(0).gameObject;
                    Debug.Log(cardBack);
                    Destroy(cardBack);
                }
            }
        }

      
        
        

    }
    public void PrintPlayCardsOfPlayer(){
        foreach(Player player in _players){
            int indexPlayer = playerDecks.Keys.ToList().IndexOf(player);
            List<Card> mainPlayerCardsPlay = playerListingsMenu.PlayerCardsPlay.Values.ElementAt(indexPlayer);
            foreach(Card card in mainPlayerCardsPlay){
                Debug.Log(player + " have played " + card.Name);
            }
            
        } 
    }

    
    public void InstantiateCards(int numberOfDrawCard, Player player){
        Debug.Log("InstantiateCards called");
        StartCoroutine(StartGame(numberOfDrawCard, player));
    }

    /*Give the Main player cards*/
    IEnumerator StartGame(int numberOfDrawCard, Player player){
        Debug.Log("PlayerDeck.StartGame() called");
        int indexMainPlayer = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player);
        List<Card> mainPlayerCards = playerListingsMenu.PlayerDecks.Values.ElementAt(indexMainPlayer);
        Debug.Log("index main player position = " + indexMainPlayer);
        // Give Main Player Card
        foreach(Card card in mainPlayerCards){
            yield return new WaitForSeconds(1);

            Hand = GameObject.Find("Hand");
            GameObject _CardToHand = Instantiate(CardToHand, transform.position, transform.rotation,Hand.transform);
            ThisCard thisCard = _CardToHand.GetComponent<ThisCard>();
            thisCard.thisCardId = card.Id;
            Debug.Log("Instantiate " + card.Name + " to " + PhotonNetwork.LocalPlayer);
            _CardToHand.tag ="Clone";
        }
        if(enemyPanelNameListSize != 0){
            StartCoroutine(GiveEnemyCards(numberOfDrawCard,2,enemyPanelNameList[enemyPanelNameListSize-1]));

        }
        
    }
    /*Give the Enemy cards*/
    IEnumerator GiveEnemyCards(int numberOfDrawCard,float timeToWait, string enemyPanelName){
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("PlayerDeck.GiveEnemyCards() called"); 
        Debug.Log(enemyPanelName);
        Enemy = GameObject.Find(enemyPanelName);
        Debug.Log(Enemy.gameObject.name);
        for(int i = 0; i < numberOfDrawCard; i++){
            yield return new WaitForSeconds(1);
            Enemy = GameObject.Find(enemyPanelName);
            
            GameObject _cardBack = Instantiate(CardBack, transform.position, transform.rotation, Enemy.transform);
            _cardBack.tag ="Clone";

        }
        enemyPanelNameListSize--;
        if(enemyPanelNameListSize != 0){
            StartCoroutine(GiveEnemyCards(numberOfDrawCard,2,enemyPanelNameList[enemyPanelNameListSize-1]));
        }

     }

    // Update is called once per frame
    void Update()
    {
        StaticDeck = deck;
    }

    /*
        1. The game start (start button click)
        2. The deck has the number of cards
        3. Shuffle the deck of cards randomly
        4. Give 6 cards to each player (from the top of the deck, deck[size]--) : add the cards to the list -> Dictionary<name, cards list>
        5. Instantiate the card objects to the player's hand objects
    */

    [PunRPC]
    public void RPC_playerDeckStart(){
        Debug.Log("RPC_Start called");
        NumberOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        DeckSize = CardDatabase.StaticCardList.Count*NumberOfPlayers;
        enemyPanelNameListSize = PhotonNetwork.PlayerListOthers.Length;
        /*2. Add cards to the deck base on the total number of players in the room */
        AddCardsToDeck(NumberOfPlayers);
        addPlayers();
        enemyPanelNameListSize = _players.Count-1;
        // for(int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count; i ++){
        //     playerCardsPlay.Add(PhotonNetwork.LocalPlayer, new List<Card>());
        // }
   //     StartCoroutine(StartGame(6));

            
    }
    
    [PunRPC]
    /*Add the cards into the card deck base on the number of players (can be modified later)*/
    private void AddCardsToDeck(int numberOfPlayers){
        Debug.Log("AddCardsToDeck() called");
        if(!isCardAdd){
            for(int i = 0; i < numberOfPlayers; i++){
                foreach(Card card in CardDatabase.StaticCardList){
                    deck.Add(card);
                    
                }
            }
            totalCardNumber = deck.Count;
            isCardAdd = true;


        }
    }

    [PunRPC]
    /*Shuffle the deck of cards randomly*/
    private void RPC_ShuffleCard(){
        if(!isShuffle){
            Debug.Log("RPC_ShuffleCard call");
            int lowerBoundShuffleTimes = Random.Range(3,9);
            int upperBoundShuffleTimes = Random.Range(10,20);
            int shuffleTimes = Random.Range(lowerBoundShuffleTimes,upperBoundShuffleTimes);
            for(int i = 0; i < shuffleTimes; i++){
                for(int j  = 0; j < DeckSize; j++){
                    Card tempCard = deck[j];
                    int randomPosition = Random.Range(j,DeckSize-1);
                    deck[j] = deck[randomPosition];
                    deck[randomPosition] = tempCard;
                }
            }
            }
            isShuffle = true;
            Debug.Log("Deck alr shuffled: ");
            for(int i = 0 ; i < deck.Count; i++){
                Debug.Log("i = " + i + " , card name = " + deck[i].Name);
            }
            Debug.Log("Over");
    }
    private void addPlayers(){
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
         //   GiveCardsToPlayer(playerInfo.Value, numberOfCardsGiven);
            int index = _players.FindIndex(x => x == playerInfo.Value);
            if(index == -1){
                _players.Add(playerInfo.Value);
                Debug.Log("Add : " + playerInfo.Value);
            }
        }        
        Debug.Log(_players.Count);
    }
    [PunRPC]
    private void RPC_GiveCards(int numberOfCardsGiven){
        Debug.Log("RPC_GiveCards called");
        this.photonView.RPC("RPC_GiveCardsToPlayer", RpcTarget.AllViaServer,PhotonNetwork.LocalPlayer,numberOfCardsGiven); 
   //     this.photonView.RPC("RPC_InstantiateCards", RpcTarget.AllViaServer, PhotonNetwork.LocalPlayer);
    }



    /*Give cards to each player (after shuffling)*/
    public List<Card> GiveCardsToPlayer(Player player,int numberOfCardsGivenToPlayer, int cardNumber){
        
        bool existedPlayer = playerListingsMenu.PlayerDecks.ContainsKey(player);
        if(!existedPlayer){
            Debug.Log("RPC_GiveCardsToPlayer called");
            List<Card> playerCards = new List<Card>();
            Debug.Log(player.NickName + " : ");
            for(int i = 0; i < numberOfCardsGivenToPlayer; i++){
                Debug.Log("Card Number = " + (cardNumber-1) + " is added to player " + player);
                
                playerCards.Add(deck[cardNumber-1]);
                Debug.Log(player + " added " + playerCards[i].Name);
                cardNumber--;
                
            }
            return playerCards;
          //  playerDecks.Add(player, playerCards);
            Debug.Log("playerDecks size = " + playerDecks.Count);
           // playerCardsPlay.Add(player, new List<Card>());
        } 
        return null;
    }
    private Player playerTurn(int turn){
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
            if(playerInfo.Key == turn){
                return playerInfo.Value;
            }
        }
        return null;
       
    }

}
