using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class EndKitPhase : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text PlayersScoreText; 
    [SerializeField] 
    private GameObject EndGameNotification;

    [SerializeField]
    private GameObject PlayerDeck; 

    [SerializeField]
    private Text RemainingTimeText;
    [SerializeField]
    private double timeResetDeck; 

    private int counter; 

    private bool startTimer;
    private double timerIncrementValue; 
    private double startTime;


    void Awake(){
        counter = 0; 
        startTimer = false;
        Debug.Log("startTimer status: " + startTimer);
        // Debug.Log("EndKitPhase Awake() called");
        // if(GameManager.Instance.playerCardsPlay.Count != PhotonNetwork.CurrentRoom.PlayerCount){
        //     EndGameNotification.SetActive(true);
        // }
        // else{
        //     EndGameNotification.SetActive(false);
        //     PlayersScoreText.text = DisplayPlayersScore();

        // }
    }
    [PunRPC]
    public void EndKitPhaseStatus(){
        Debug.Log("EndKitPhase() called");
        Debug.Log("GameManager.Instance.playersScore.Count = " + GameManager.Instance.playersScore.Count);
        foreach(var player in GameManager.Instance.playersScore){
            Debug.Log(player.Key.NickName + " : " + player.Value.ToString());
        }
        if(GameManager.Instance.playersScore.Count != PhotonNetwork.CurrentRoom.PlayerCount){
           Debug.Log("Waiting");
            EndGameNotification.SetActive(true);
        }
        else{
            EndGameNotification.SetActive(false);
            PlayersScoreText.text = DisplayPlayersScore();
            CounterResetRoom();

        }
    }

    private void CounterResetRoom(){
        if(PhotonNetwork.IsMasterClient){
            startTime = PhotonNetwork.Time;
            startTimer = true;
            ExitGames.Client.Photon.Hashtable setTimeResetRoomValue = new ExitGames.Client.Photon.Hashtable();
            setTimeResetRoomValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(setTimeResetRoomValue);
        }
        else{
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }
    }
    void Update(){
        if(!startTimer) return; 
        else{
            timerIncrementValue = PhotonNetwork.Time - startTime;
           Debug.Log("Start time = " + startTime);
           Debug.Log(timerIncrementValue);
            RemainingTimeText.text = Mathf.RoundToInt((float)(timeResetDeck-timerIncrementValue)).ToString();
            if(timerIncrementValue >= timeResetDeck){
                Debug.Log("Completed");
                    PlayerDeck.GetComponent<PhotonView>().RPC("ResetDeck", RpcTarget.AllBuffered);
            }

        }
        
    }

    public string DisplayPlayersScore(){
        string playersScoreString = "";
        var sortedPlayersScore = from entry in GameManager.Instance.playersScore orderby entry.Value descending select entry;
        foreach(var player in sortedPlayersScore){
            string playerScore = player.Key.NickName + " : " + player.Value.ToString() + "\n";
            playersScoreString += playerScore;

        }
        return playersScoreString;
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
//         object startTimeObject;
//         if(propertiesThatChanged.TryGetValue("StartTime", out startTimeObject)){
//             startTime = (double) startTimeObject;
//             timerIncrementValue = PhotonNetwork.Time - startTime;
//             Debug.Log("Start time = " + startTime);
//             Debug.Log(timerIncrementValue);
//             RemainingTimeText.text += timerIncrementValue;
//             if(timerIncrementValue >= timeResetDeck){
//  //               PlayerDeck.GetComponent<PhotonView>().RPC("ResetDeck", RpcTarget.AllBuffered);
//             }
//         }
    }


}
