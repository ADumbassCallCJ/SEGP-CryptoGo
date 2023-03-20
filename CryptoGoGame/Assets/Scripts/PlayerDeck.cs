 using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


public class PlayerDeck : MonoBehaviourPunCallbacks
{
    /*deck is Deck of cards*/
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public int NumberOfPlayers;
    public static int DeckSize;
    private AuxiliaryCardDatabase auxiliaryCardDatabase;

    /*StaticNumberOfDrawCard is the number of cards that each player will receive*/
    public static int StaticNumberOfDrawCard;
    private PlayerListingsMenu playerListingsMenu;


    private List<string> enemyPanelNameList = new List<string>();
    public int enemyPanelNameListSize; 

    public GameObject Hand;
    public GameObject CardToHand;

    public GameObject CardBack;
    [SerializeField]
    private GameObject auxiliaryCardPrefab;
    public GameObject Enemy;
    private GameObject playCardButton;
    public GameObject PlayCardButton{
        get{return playCardButton;}
    }
    public static GameObject pickCard;
    public static GameObject pickCardZone;
    private GameObject playZone; 
    private List<Player> _players = new List<Player>();
    private  Dictionary<Player, List<Card>> playerDecks = new Dictionary<Player, List<Card>>();
    
    private int totalCardNumber = 0; 
    public int TotalCardNumber{
        get{ return totalCardNumber;}
        
    }
    private bool isShuffle = false;
    private bool isCardAdd = false;

    private int turn;
    private int round;


    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("PlayerDeck.Start() called");
        auxiliaryCardDatabase = this.transform.gameObject.GetComponent<AuxiliaryCardDatabase>();
        pickCard = GameObject.Find("PickCard");
        pickCardZone = GameObject.Find("PickCardZone");
        playCardButton = GameObject.Find("PlayCardButton");
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
        // foreach(string name in enemyPanelNameList){
        //     Debug.Log(name);                                                                                                                                                                                   
        // }
        turn = 0;
        round = 1;

    }
    [PunRPC]
    public void ResetDeck(){
        Debug.Log("ResetDeck()");
        deck = new List<Card>();
        playerListingsMenu.PlayerDecks = new Dictionary<Player, List<Card>>();
        playerListingsMenu.TempPlayerDecks = new Dictionary<Player, List<Card>>();
        playerListingsMenu.PlayerCardsPlay = new Dictionary<Player, List<Card>>();
        ExitGames.Client.Photon.Hashtable setCardsValue = new ExitGames.Client.Photon.Hashtable();
        int[] myPlayCards = new int[0];
        setCardsValue["PlayerCardsPlay"] = myPlayCards ;
        Debug.Log(PhotonNetwork.LocalPlayer.SetCustomProperties(setCardsValue));
        isShuffle = false;
        isCardAdd = false;
        totalCardNumber = 0;
        round = 1;
        turn = 0;

        PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public Player GetCurrentPlayerTurn(){
        return playerTurn(turn);
    }    

    [PunRPC]
    public Player RPC_NextTurn(int cardID){  
        if(turn != -1){
            Debug.Log("It's " + playerTurn(turn) + " turn");
            if(GetCurrentPlayerTurn() == PhotonNetwork.LocalPlayer){
                RPC_PlayCards(GetCurrentPlayerTurn(), cardID); 
                this.photonView.RPC("DestroyOneEnemyCard", RpcTarget.All, GetCurrentPlayerTurn());
                turn++;
                ExitGames.Client.Photon.Hashtable setTurnValue = new ExitGames.Client.Photon.Hashtable();
                setTurnValue.Add("turn", turn);
                Debug.Log("Set turn value: " + PhotonNetwork.CurrentRoom.SetCustomProperties(setTurnValue));
            }
            //  if(round == 5){
                    
            //         int numberOfDrawCard = 4;
            //         enemyPanelNameListSize = PhotonNetwork.PlayerListOthers.Length;
            //   //      StartCoroutine(CreateCardObjectsToHand(6));
              
            //     //    this.photonView.RPC("specialTurn", RpcTarget.All, player.Value, numberOfDrawCard);
            //         specialTurn(PhotonNetwork.LocalPlayer, numberOfDrawCard);
            //         destroyAllCards(Hand);
            //         InstantiateCards(numberOfDrawCard,0,PhotonNetwork.LocalPlayer);
            //          turn++;
            //         ExitGames.Client.Photon.Hashtable setTurnValue = new ExitGames.Client.Photon.Hashtable();
            //         setTurnValue.Add("turn", turn);
            //         Debug.Log("Set turn value: " + PhotonNetwork.CurrentRoom.SetCustomProperties(setTurnValue));

            // }
               
            //        specialTurn(PhotonNetwork.LocalPlayer,numberOfDrawCard);
    
            //        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " do the thing: ");
             //       destroyAllCards(Hand);
               //     InstantiateCards(numberOfDrawCard,0,PhotonNetwork.LocalPlayer);

              //      turn++;
                    // ExitGames.Client.Photon.Hashtable setTurnValue = new ExitGames.Client.Photon.Hashtable();
                    // setTurnValue.Add("turn", turn);
                    // Debug.Log("Set turn value: " + PhotonNetwork.CurrentRoom.SetCustomProperties(setTurnValue));

                
          //  }
            // turn++; 
            if(turn > _players.Count){
                Debug.Log("turn larger");
                // round++;
                // ExitGames.Client.Photon.Hashtable setRoundValue = new ExitGames.Client.Photon.Hashtable();
                // setRoundValue.Add("round", round);
                // Debug.Log("Set round value: " + PhotonNetwork.CurrentRoom.SetCustomProperties(setRoundValue));
                // Debug.Log("Finish round " + round);
  

             //   InstantiatePlayCardsToEnemies();
                this.photonView.RPC("InstantiatePlayCardsToEnemies", RpcTarget.All);
                if(round == 10){
                    turn = -1;
                    
                    Debug.Log("Finish the game");
                    this.photonView.RPC("ResetDeck", RpcTarget.AllViaServer);
                }
                if(round == 5){
                    
                    int numberOfDrawCard = 4;
                    // enemyPanelNameListSize = PhotonNetwork.PlayerListOthers.Length;
              //      StartCoroutine(CreateCardObjectsToHand(6));
              
                //    this.photonView.RPC("specialTurn", RpcTarget.All, player.Value, numberOfDrawCard);
                    foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players){
                        specialTurn(player.Value, numberOfDrawCard);
                    }
                    this.photonView.RPC("InstantiateCardsSpecialTurn",RpcTarget.All,numberOfDrawCard);
                    // destroyAllCards(Hand);
                    // InstantiateCards(numberOfDrawCard,0,PhotonNetwork.LocalPlayer);
                     

                }
                else if(round != 5){
                    finishRound();
                }
            //      else if(round == 5){
                    
            //         int numberOfDrawCard = 4;
            //         enemyPanelNameListSize = PhotonNetwork.PlayerListOthers.Length;
            // //   //      StartCoroutine(CreateCardObjectsToHand(6));
              
            // //     if(PhotonNetwork.IsMasterClient){
            //   //           foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players){
            //     //             specialTurn(player.Value,numberOfDrawCard);
            //      //        }
            //        this.photonView.RPC("specialTurn", RpcTarget.All, PhotonNetwork.LocalPlayer, numberOfDrawCard); 

            //   //       Debug.Log(PhotonNetwork.LocalPlayer.NickName + " do the thing: ");
                   

            //      }

                Debug.Log("Finish round " + round);
                round++;
         

                turn = 1;
                ExitGames.Client.Photon.Hashtable setTurnValue = new ExitGames.Client.Photon.Hashtable();
                setTurnValue.Add("turn", turn);
                Debug.Log("Set turn value: " + PhotonNetwork.CurrentRoom.SetCustomProperties(setTurnValue));

            }
            
            return playerTurn(turn);
        }    
        return null;
        
    }

    [PunRPC]
    private void InstantiateCardsSpecialTurn(int numberOfDrawCard){
        enemyPanelNameListSize = PhotonNetwork.PlayerListOthers.Length;
        destroyAllCards(Hand);
        InstantiateCards(numberOfDrawCard,0,PhotonNetwork.LocalPlayer);
    }
    IEnumerator CreateCardObjectsToHand(int numberOfDrawCard){

         // Give Main Player Card
        for(int i = 0; i < numberOfDrawCard; i++){
            yield return new WaitForSeconds(1);
            Hand = GameObject.Find("Hand");
            GameObject _CardToHand = Instantiate(CardToHand, transform.position, transform.rotation,Hand.transform);
            ThisCard thisCard = _CardToHand.GetComponent<ThisCard>();
    
            Debug.Log("Instantiate a card" + " to " + PhotonNetwork.LocalPlayer);
            _CardToHand.tag ="Clone";
        }

        if(enemyPanelNameListSize != 0){
            StartCoroutine(GiveEnemyCards(numberOfDrawCard,1,enemyPanelNameList[enemyPanelNameListSize-1]));

        }
        else{
            turn = 1;
        }
        
    }

    [PunRPC]
    private void specialTurn(Player player,int numberOfDrawCard){
        Debug.Log(player.NickName + " special turn called");

        // Add 4 cards to each player
        int indexPlayer = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player);
        List<Card> mainPlayerCardsList = playerListingsMenu.PlayerDecks.Values.ElementAt(indexPlayer);
        List<Card> addCards = GiveCardsToPlayer(player, numberOfDrawCard, deck.Count);
        foreach(Card card in addCards){
            mainPlayerCardsList.Add(card);
        }
        Debug.Log("playerDecks size = " + playerDecks.Count);
        int[] myCards = new int[mainPlayerCardsList.Count];
        int i = 0;
        foreach(Card card in mainPlayerCardsList){
        myCards[i++] = card.Id;
        }
        ExitGames.Client.Photon.Hashtable setCardsValue = new ExitGames.Client.Photon.Hashtable();

        setCardsValue["PlayerCards"] = myCards;
        Debug.Log(player.SetCustomProperties(setCardsValue));



        // destroyAllCards(Hand);
        // InstantiateCards(numberOfDrawCard,0,player);
        
    }
    private void destroyAllCards(GameObject hand){
        for(int i = 0; i < hand.transform.childCount;i++){
            GameObject cardObject = hand.transform.GetChild(i).gameObject;
            Destroy(cardObject);
        }

    }

    private void finishRound(){
        Debug.Log("Finish round");
        List<Player> players = new List<Player>();

            for(int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count; i++){
                players.Add(PhotonNetwork.LocalPlayer);
            }
            foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
                players[playerInfo.Key-1] = playerInfo.Value;
            }
            for(int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count; i++){
                Debug.Log(i + " " + players[i]);
            }
            if(players.Count == 2){
                transferCards(players[0], players[1]);

            }
            else if(players.Count == 3){
                
                    transferCards(players[0], players[2]);
                    transferCards(players[1], players[2]);
            }
            /* 4 players */
            else if(players.Count == 4){
                transferCards(players[0], players[3]);
                transferCards(players[2], players[3]);
                transferCards(players[1], players[2]);
            }

          //  updateCardObjects(PhotonNetwork.LocalPlayer);
    
        
    }
    private void transferCards(Player player1, Player player2){
        Debug.Log("Cards Transfer");
        int indexPlayer1 = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player1);
        List<Card> mainPlayerCardsList1 = playerListingsMenu.PlayerDecks.Values.ElementAt(indexPlayer1);

        int indexPlayer2 = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player2);
        List<Card> mainPlayerCardsList2 = playerListingsMenu.PlayerDecks.Values.ElementAt(indexPlayer2);

        List<Card> tempCardsList = new List<Card>();
        tempCardsList = mainPlayerCardsList1;
        
        // 1 <- 2
        mainPlayerCardsList1 = mainPlayerCardsList2;
       playerListingsMenu.PlayerDecks[player1]= mainPlayerCardsList1;
        int[] player1Cards = new int[mainPlayerCardsList1.Count];
        int a = 0;
        foreach(Card card in mainPlayerCardsList1){
            player1Cards[a++] = card.Id;
        }
        ExitGames.Client.Photon.Hashtable setCardsValue = new ExitGames.Client.Photon.Hashtable();
    
        setCardsValue["PlayerCards"] = player1Cards;
        Debug.Log(player1.SetCustomProperties(setCardsValue));
 
        // 2 <- 1
        mainPlayerCardsList2 = tempCardsList;
       playerListingsMenu.PlayerDecks[player2]= mainPlayerCardsList2;
        int[] player2Cards = new int[mainPlayerCardsList2.Count];
        int b = 0;
        foreach(Card card in mainPlayerCardsList2){
            player2Cards[b++] = card.Id;
        }
        setCardsValue["PlayerCards"] = player2Cards;
        Debug.Log(player2.SetCustomProperties(setCardsValue));
    }
  
    public void updateCardObjects(Player player){
        Debug.Log("update card objects called");
        Hand = GameObject.Find("Hand");
        int indexPlayer = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player);
        List<Card> mainPlayerCardsList = playerListingsMenu.PlayerDecks.Values.ElementAt(indexPlayer);
        for(int i = 0; i < Hand.transform.childCount; i++){
            GameObject cardObjects = Hand.transform.GetChild(i).gameObject;
            ThisCard thisCard = cardObjects.GetComponent<ThisCard>();
            thisCard.thisId = mainPlayerCardsList[i].Id;
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object turnObject;
        if(propertiesThatChanged.TryGetValue("turn",out turnObject)){
            int t = (int) turnObject;
            turn = t;
        }
        object deckObject;
        if(propertiesThatChanged.TryGetValue("Deck", out deckObject)){
            Debug.Log("Deck updated");
            int[] deckArray = (int[])deckObject;
            List<Card> deckShuffle = new List<Card>();
            for(int i =0; i < deckArray.Length; i++){
                deckShuffle.Add(CardDatabase.StaticCardList[deckArray[i]]);
            }
            deck = deckShuffle;
            Debug.Log("Deck updated size = " + deck.Count);
            foreach(Card card in deck){
                Debug.Log("id = " + card.Id + " , name = " + card.Name);
            }

        }
    }

   // [PunRPC]
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
            ExitGames.Client.Photon.Hashtable setCardsPlayValue1 = new ExitGames.Client.Photon.Hashtable();
            setCardsPlayValue1["PlayerCardsPlay"] = myCardsPlay;
            Debug.Log(player.SetCustomProperties(setCardsPlayValue1));

            mainPlayerCards.RemoveAt(cardIndexInMainPlayerCards);
            // update server player list
            int[] myCards = new int[mainPlayerCards.Count];
            int a = 0;
            foreach(Card card in mainPlayerCards){
                myCards[a++] = card.Id;
            }
            ExitGames.Client.Photon.Hashtable setCardsValue = new ExitGames.Client.Photon.Hashtable();
     
            // setCardsValue["PlayerCardsPlay"] = myCardsPlay;
            setCardsValue["PlayerCards"] = myCards;
            Debug.Log(player.SetCustomProperties(setCardsValue));


            // if(player != PhotonNetwork.LocalPlayer){
            //     int enemyPanelIndex = -1; 
            //     for(int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++){
            //         if(player == PhotonNetwork.PlayerListOthers[i]){
            //             Debug.Log("Enemy Player : " + player + " found");
            //             enemyPanelIndex = i;
            //             Debug.Log("Enemy index = " + enemyPanelIndex);
            //         }
            //     }
            
            //     Debug.Log("Enemy Panel name = " + enemyPanelNameList[enemyPanelIndex]);
            //     if(enemyPanelIndex != -1){
            //         GameObject EnemyHand = GameObject.Find(enemyPanelNameList[enemyPanelIndex]);
            //         GameObject cardBack = EnemyHand.transform.GetChild(0).gameObject;
            //         Debug.Log(cardBack);
            //         Destroy(cardBack);
            //     }
            // }
        }
       
    }
    [PunRPC]
    public void DestroyOneEnemyCard(Player player){
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
    public void PrintPlayCardsOfPlayer(){
        foreach(Player player in _players){
            int indexPlayer = playerDecks.Keys.ToList().IndexOf(player);
            List<Card> mainPlayerCardsPlay = playerListingsMenu.PlayerCardsPlay.Values.ElementAt(indexPlayer);
            foreach(Card card in mainPlayerCardsPlay){
                Debug.Log(player + " have played " + card.Name);
            }
            
        } 
    }

    
    public void InstantiateCards(int numberOfDrawCard, int numberOfAuxCard,Player player){
        Debug.Log("InstantiateCards called");
        StartCoroutine(StartGame(numberOfDrawCard,numberOfAuxCard ,player));
    }

    /*Give the Main player cards*/
    IEnumerator StartGame(int numberOfDrawCard, int numberOfAuxCard ,Player player){
        Debug.Log("PlayerDeck.StartGame() called");
        int indexMainPlayer = playerListingsMenu.PlayerDecks.Keys.ToList().IndexOf(player);
        List<Card> mainPlayerCards = playerListingsMenu.PlayerDecks.Values.ElementAt(indexMainPlayer);
        Debug.Log("index main player position = " + indexMainPlayer);
        if(numberOfAuxCard > 0){
            List<string> auxiliaryCards = pickAuxiliaryCardsRandom(numberOfAuxCard);
            foreach(string auxiliaryCardString in auxiliaryCards){
                GameObject auxiliaryCardsObject = GameObject.Find("AuxiliaryCards");
                GameObject _auxiliaryCardToHand = Instantiate(auxiliaryCardPrefab, transform.position, transform.rotation, auxiliaryCardsObject.transform);
                _auxiliaryCardToHand.tag = "AuxiliaryCard";
                GameObject cardImage = _auxiliaryCardToHand.transform.Find("Card Image").gameObject;
                Sprite auxiliaryCardImage = Resources.Load<Sprite>(auxiliaryCardString);
                cardImage.GetComponent<Image>().sprite = auxiliaryCardImage;
                Debug.Log("auxiliary card instantiated");
                yield return new WaitForSeconds(1);

            }
        }
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
            StartCoroutine(GiveEnemyCards(numberOfDrawCard,1,enemyPanelNameList[enemyPanelNameListSize-1]));

        }
        else{
            turn = 1;
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
            StartCoroutine(GiveEnemyCards(numberOfDrawCard,1,enemyPanelNameList[enemyPanelNameListSize-1]));
        }
        else{
            turn = 1;
        }

     }
     [PunRPC]
     private void InstantiatePlayCardsToEnemies(){
        Debug.Log("Instantiate play cards to enemy");
        List<string> enemyPlayZone = new List<string>();
        enemyPlayZone.Add("EnemyPlayZone (1)");
        enemyPlayZone.Add("EnemyPlayZone (2)");
        enemyPlayZone.Add("EnemyPlayZone (3)");
        for(int i = 0; i < PhotonNetwork.PlayerListOthers.Length; i++){
            StartCoroutine(InstantiatePlayCards(0,PhotonNetwork.PlayerListOthers[i], enemyPlayZone[i]));
        }

     }
     //EnemyPlayZone (1)
     IEnumerator InstantiatePlayCards(float timeToWait, Player player ,string enemyPlayZoneName){
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("Instantiate play cards");
        GameObject EnemyPlayZone = GameObject.Find(enemyPlayZoneName);
        int indexPlayer = playerListingsMenu.PlayerCardsPlay.Keys.ToList().IndexOf(player);
        List<Card> playerCardsPlay = playerListingsMenu.PlayerCardsPlay.Values.ElementAt(indexPlayer);
        GameObject _CardToHand = Instantiate(CardToHand, transform.position, transform.rotation, EnemyPlayZone.transform);
        ThisCard thisCard = _CardToHand.GetComponent<ThisCard>();
        thisCard.thisCardId = playerCardsPlay[playerCardsPlay.Count-1].Id;


     }

    // Update is called once per frame


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
        GameObject PlayerListings = GameObject.Find("PlayerListings");
    //    PlayerListings.SetActive(false);
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


    /*Shuffle the deck of cards randomly*/
    public void ShuffleCard(){
       
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
            
            // isShuffle = true;
            Debug.Log("Deck alr shuffled: ");
            for(int i = 0 ; i < deck.Count; i++){
                Debug.Log("i = " + i + " , card name = " + deck[i].Name);
            }
            int[] myShuffleDeck= new int[deck.Count];
            int b = 0;
            foreach(Card card in deck){
                myShuffleDeck[b++] = card.Id;
            }
            ExitGames.Client.Photon.Hashtable setShuffleDeck = new ExitGames.Client.Photon.Hashtable();
            setShuffleDeck["Deck"] = myShuffleDeck;
            Debug.Log(PhotonNetwork.CurrentRoom.SetCustomProperties(setShuffleDeck));


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
        
       // bool existedPlayer = playerListingsMenu.PlayerDecks.ContainsKey(player);
      //  if(!existedPlayer){
            Debug.Log("RPC_GiveCardsToPlayer called");
            List<Card> playerCards = new List<Card>();
            Debug.Log(player.NickName + " : ");
            for(int i = 0; i < numberOfCardsGivenToPlayer; i++){
                Debug.Log("Card Number = " + (cardNumber-1) + " is added to player " + player);
                
                playerCards.Add(deck[cardNumber-1]);
                Debug.Log(player + " added " + playerCards[i].Name);
                deck.RemoveAt(cardNumber-1);
                Debug.Log("deck size = " + deck.Count);
                cardNumber--;
                
            }
            int[] myRemainingDeck= new int[deck.Count];
            int b = 0;
            foreach(Card card in deck){
                myRemainingDeck[b++] = card.Id;
            }
            ExitGames.Client.Photon.Hashtable setRemainingDeck = new ExitGames.Client.Photon.Hashtable();
            setRemainingDeck["Deck"] = myRemainingDeck;
            Debug.Log(PhotonNetwork.CurrentRoom.SetCustomProperties(setRemainingDeck));

            return playerCards;
          //  playerDecks.Add(player, playerCards);
           // Debug.Log("playerDecks size = " + playerDecks.Count);
           // playerCardsPlay.Add(player, new List<Card>());
      //  } 
      //  return null;
    }
    private Player playerTurn(int turn){
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
            if(playerInfo.Key == turn){
                return playerInfo.Value;
            }
        }
        return null;
       
    }

    private List<string> pickAuxiliaryCardsRandom(int numberOfAuxiliaryCard){
        List<string> auxiliaryCards = new List<string>();
        int rand; 
        int i = 0; 

        do{
            rand = Random.Range(0, auxiliaryCardDatabase.StaticAuxiliaryCardList.Count-1);
            auxiliaryCards.Add(auxiliaryCardDatabase.StaticAuxiliaryCardList[rand]);
            i++;
            // int indexOfCard = auxiliaryCards.FindIndex(x => x == auxiliaryCardDatabase.StaticAuxiliaryCardList[rand]);

            // if(indexOfCard == -1){
            //     auxiliaryCards.Add(auxiliaryCardDatabase.StaticAuxiliaryCardList[i]);
            //     i++;
            // }
            
        }while(i < numberOfAuxiliaryCard);
        
        return auxiliaryCards;
    }

}
