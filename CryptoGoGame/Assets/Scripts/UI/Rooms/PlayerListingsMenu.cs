using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
   [SerializeField]
    private PlayerListing _playerListing;

    [SerializeField]
    private MainPlayer _player;

    [SerializeField]
    private MainPlayer _enemy;

    [SerializeField]
    private GameObject _CardTohandPrefab;

    [SerializeField]
    private Transform _content;
    [SerializeField]
    private Transform _mainPlayer;
    [SerializeField]
    private MainPlayer _enemyPlayer1;
    [SerializeField]
    private MainPlayer _enemyPlayer2;
    [SerializeField]
    private MainPlayer _enemyPlayer3;

    [SerializeField]
    private Text _readyUpText;

    [SerializeField]
    private Button _readyButton;

    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private PlayerDeck playerDeck;

    private bool _ready = false;
    private List<PlayerListing> _listings = new List<PlayerListing>(); 
    public List<PlayerListing> PlayerListings{get {return _listings;}}
    private List<MainPlayer> _mainPlayersListTemp = new List<MainPlayer>();
    private List<string> _transformListings = new List<string>{"MainPlayer", "EnemyPlayer (1)", "EnemyPlayer (2)","EnemyPlayer (3)"};
    private Dictionary<int, MainPlayer> _enemyPlayerObjectList = new Dictionary<int, MainPlayer>();

    private  Dictionary<Player, List<Card>> playerDecks = new Dictionary<Player, List<Card>>();
    public Dictionary<Player,List<Card>> PlayerDecks{
        get {return playerDecks;}
        set{
            playerDecks = value;
        }
    }
    public Dictionary<Player, List<Card>> playerCardsPlay = new Dictionary<Player, List<Card>>();
        public Dictionary<Player,List<Card>> PlayerCardsPlay{
        get {return playerCardsPlay;}
        set{
            playerCardsPlay = value;
        }
    }

 //   private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = ExitGames.Client.Photon.Hashtable();
    private int cardNumber;

    private void Awake(){
        Debug.Log("Call");
        playerDeck = GameObject.Find("Background Image").GetComponent<PlayerDeck>();
        GetCurrentRoomPlayers();
        ButtonsHide();
      //  AddPlayerObject();
        AddingEnemyPlayerObjectList();
        CreatePlayerObjectOfCurrentRoomPlayers();
       
    }
    private void ButtonsHide(){
        Debug.Log("ButtonsHide() call");
        Debug.Log(PhotonNetwork.IsMasterClient);
        if(!PhotonNetwork.IsMasterClient){
            _startButton.gameObject.SetActive(false);
        }
        else{
            _readyButton.gameObject.SetActive(false);
            int index = _listings.FindIndex(x => x.Player == PhotonNetwork.MasterClient);
            if(index != -1){
                _listings[index].SetReady(false);

            }
            _readyUpText.text = "Waiting others ready";
        }
    }
    private void AddingEnemyPlayerObjectList(){
        _enemyPlayerObjectList.Add(1, _enemyPlayer1);
        _enemyPlayerObjectList.Add(2, _enemyPlayer2);
        _enemyPlayerObjectList.Add(3, _enemyPlayer3);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
    }
    private void SetReadyUp(bool state){
        _ready = state;
        if(!PhotonNetwork.IsMasterClient){
            if(_ready){
                _readyUpText.text = "Ready";
            }
            else{
                _readyUpText.text = "Unready";
            }
      
        }
    }
    public void OnClick_ReadyUp(){
        Debug.Log("Ready button clicked");
        if(!PhotonNetwork.IsMasterClient){
            SetReadyUp(!_ready);
        //    base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, !_ready);
            this.photonView.RPC("RPC_ChangeReadyState", RpcTarget.All, PhotonNetwork.LocalPlayer, _ready);
        }
    }
    public void OnClick_StartGame(){
        if(PhotonNetwork.IsMasterClient){
            foreach(PlayerListing player in _listings){
                if(!player.Ready && player.Player != PhotonNetwork.MasterClient){
                    return;
                }
            }
          //  playerDeck.StartGame();
            this.photonView.RPC("RPC_StartingGame", RpcTarget.AllViaServer);
//            _readyUpText.gameObject.SetActive(false);
            _readyButton.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(false);
        }
    }
    private void GetCurrentRoomPlayers(){
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
            Debug.Log("Player : " + playerInfo.Value.NickName + " , Key: " + playerInfo.Key);
            AddPlayerListing(playerInfo.Value);
        }

    }
    private void CreatePlayerObjectOfCurrentRoomPlayers(){
   //     _playersListTemp.Add(PhotonNetwork.LocalPlayer);
        AddPlayerObject(PhotonNetwork.LocalPlayer, 0);
        // foreach(Player playerInfo in PhotonNetwork.PlayerListOthers){
        //     _playersListTemp.Add(playerInfo);
        // }

        for(int i = 0; i < PhotonNetwork.PlayerListOthers.Length;i++){
            AddPlayerObject(PhotonNetwork.PlayerListOthers[i], i+1);
        }
    
    }
    private void AddPlayerObject(Player newPlayer, int position){
        Debug.Log("Instantiate " + newPlayer.NickName);
        // int index = _playersListTemp.FindIndex(x => x == newPlayer);
        // if(index == -1){
        //     _playersListTemp.Add(newPlayer);
        // }  
        MainPlayer playerObject;
        GameObject playerPosition = GameObject.Find(_transformListings[position]);
        if(newPlayer == PhotonNetwork.LocalPlayer){
            playerObject = Instantiate(_player, playerPosition.transform);
        }
        else{
            playerObject = Instantiate(_enemyPlayerObjectList[position], playerPosition.transform);
        }
        if(playerObject != null){
            playerObject.SetPlayerInfo(newPlayer);
            _mainPlayersListTemp.Add(playerObject);
        }
        // GameObject playerPosition = GameObject.Find(_transformListings[0]);
        // MainPlayer playerObject = Instantiate(_player, playerPosition.transform);
            //         GameObject playerPosition = GameObject.Find(_transformListings[playerInfo.Key-1]);
            // MainPlayer playerObject;
            // if(playerInfo.Value == PhotonNetwork.LocalPlayer){
            //     Debug.Log("Local Player: " + playerInfo.Value.NickName);
            //     playerObject = Instantiate(_player, playerPosition.transform);
            // }
            // else{
            //     playerObject = Instantiate(_enemy, playerPosition.transform);
            // }
            
            // playerObject.SetPlayerInfo(playerInfo.Value);
    }
    public void AddPlayerListing(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " just joined the room");
        PlayerListing listing = Instantiate(_playerListing,_content);
        if(listing != null){
            listing.SetPlayerInfo(newPlayer);
            listing.SetReady(false);
            _listings.Add(listing);
        
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        AddPlayerObject(newPlayer, PhotonNetwork.PlayerListOthers.Length);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer){
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        int indexMainPlayer = _mainPlayersListTemp.FindIndex(x => x.Player == otherPlayer);
        if(index != -1){
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
        if(indexMainPlayer != -1){
            Destroy(_mainPlayersListTemp[indexMainPlayer].gameObject);
            _mainPlayersListTemp.RemoveAt(indexMainPlayer);
        }
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
       // base.OnMasterClientSwitched(newMasterClient);
        PhotonNetwork.CurrentRoom.IsOpen = false;
        playerDeck.ResetDeck();
        PhotonNetwork.LeaveRoom();
        
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready){
        Debug.Log("RPC_ChangeReadyState() called");
        Debug.Log(player.NickName + " , Ready: " + ready);
        int index = _listings.FindIndex(x => x.Player == player);
        if(index != -1){
            _listings[index].SetReady(ready);
        }
    }

    [PunRPC]
    private void RPC_StartingGame(){
        _readyUpText.gameObject.SetActive(false);
        _readyButton.gameObject.SetActive(false);

        GameObject Hand = GameObject.Find("Hand");
      //  MasterManager.NetworkInstantiate(_CardTohandPrefab, Hand.transform.position, Quaternion.identity, Hand);
        
        GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_playerDeckStart", RpcTarget.All);
        GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_ShuffleCard", RpcTarget.All);
        // foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players){
          //  GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_GiveCardsToPlayer", RpcTarget.All, playerInfo.Value, 6);
       // }
    //    GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_GiveCardsToPlayer", RpcTarget.All, th, 6);
    //    GameObject.Find("Background Image").GetComponent<PhotonView>().RPC("RPC_GiveCardsToPlayer", RpcTarget.All, PhotonNetwork.LocalPlayer,6);


         List<Card> currentPlayerCards = playerDeck.GiveCardsToPlayer(PhotonNetwork.LocalPlayer, 6, playerDeck.TotalCardNumber);
         
         playerDecks.Add(PhotonNetwork.LocalPlayer, currentPlayerCards);
            playerCardsPlay.Add(PhotonNetwork.LocalPlayer, new List<Card>());
         

         Debug.Log("playerDecks size = " + playerDecks.Count);
         int[] myCards = new int[currentPlayerCards.Count];
         int i = 0;
         foreach(Card card in currentPlayerCards){
            myCards[i++] = card.Id;
            
         }
         
    //     Debug.Log(myCards.Length);
    //     // playerDecks.Add(PhotonNetwork.LocalPlayer, currentPlayerCards);
        ExitGames.Client.Photon.Hashtable setCardsValue = new ExitGames.Client.Photon.Hashtable();
    //   //  setCardsValue.Add("playerCards",myCards);
         setCardsValue["PlayerCards"] = myCards;
         Debug.Log(PhotonNetwork.LocalPlayer.SetCustomProperties(setCardsValue));

        
        
        playerDeck.InstantiateCards(6, PhotonNetwork.LocalPlayer);
       // Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerCards"));
        
       // Debug.Log("playerDecks size = " + playerDecks.Count);
    

    
        // playerDeck.StartGame();
    }

    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == null || changedProps == null) {
            return;
        }
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        Debug.Log(targetPlayer.NickName + " properties changed");
        
        object arrayObject;
        if (targetPlayer.CustomProperties.TryGetValue("PlayerCards", out arrayObject)) {
            int[] cardIdArray = (int[])arrayObject;
            Debug.Log("The length of the array is: " + cardIdArray.Length);
            List<Card> playerCards = new List<Card>();
            for(int i = 0; i < cardIdArray.Length; i++){
                playerCards.Add(CardDatabase.StaticCardList[cardIdArray[i]]);
               
            }
            bool existedPlayer = playerDecks.ContainsKey(targetPlayer);
            if(!existedPlayer){
                playerDecks.Add(targetPlayer, playerCards);
                playerCardsPlay.Add(targetPlayer, new List<Card>());

            }
            else{
             //   int indexMainPlayer = playerDecks.Keys.ToList().IndexOf(targetPlayer);
                playerDecks[targetPlayer] = playerCards;
                
         
                
          //      Debug.Log("playerDecks updated");
            }
            int index = playerDecks.Keys.ToList().IndexOf(targetPlayer);
            List<Card> targetPlayerCards = playerDecks.Values.ElementAt(index);
            foreach(Card card in targetPlayerCards){
                Debug.Log(targetPlayer.NickName + " updated " + card.Name);
            }
           // Debug.Log("playerDecks size = " + playerDecks.Count);   
        }



        object arrayObject1;
        if (targetPlayer.CustomProperties.TryGetValue("PlayerCardsPlay", out arrayObject1)) {
            int[] playCardIdArray = (int[])arrayObject1;
            Debug.Log("The length of the array is: " + playCardIdArray.Length);
            List<Card> playerCardsPlayList = new List<Card>();
            for(int i = 0; i < playCardIdArray.Length; i++){
                playerCardsPlayList.Add(CardDatabase.StaticCardList[playCardIdArray[i]]);
               
            }
            bool existedPlayer = PlayerCardsPlay.ContainsKey(targetPlayer);
            if(!existedPlayer){
                playerCardsPlay.Add(targetPlayer, playerCardsPlayList);

            }
            else{
             //   int indexMainPlayer = playerDecks.Keys.ToList().IndexOf(targetPlayer);
                playerCardsPlay[targetPlayer] = playerCardsPlayList;
                
         
                
                Debug.Log("playerCardsPlay updated, size = " + playerCardsPlay.Count);
            }
            int index = playerDecks.Keys.ToList().IndexOf(targetPlayer);
            List<Card> targetPlayerCardsPlay = playerCardsPlay.Values.ElementAt(index);
            foreach(Card card in targetPlayerCardsPlay){
                Debug.Log(targetPlayer.NickName + " played " + card.Name);
            }
            Debug.Log("playerCardsPlay size = " + playerCardsPlayList.Count);   
        }

    }

}