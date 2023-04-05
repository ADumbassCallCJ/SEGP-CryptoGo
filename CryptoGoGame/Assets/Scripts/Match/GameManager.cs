using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;



public class GameManager : MonoBehaviourPunCallbacks
{

    public static GameManager Instance { get; private set; }
    [HideInInspector]
    public List<Card> playerCardsPlay;

    [HideInInspector]
    public int playerTotalScore;

    [HideInInspector]
    public Dictionary<Player, int> playersScore;

    [HideInInspector]
    public int DeckSize;






    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        playerCardsPlay = new List<Card>();
        playerTotalScore = 0;
        DeckSize = 0;
        playersScore = new Dictionary<Player, int>();
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion 

    #region  Public Methods

   

    public void LeaveRoom(){
        PhotonNetwork.LeaveRoom();
    }
    #endregion
}
