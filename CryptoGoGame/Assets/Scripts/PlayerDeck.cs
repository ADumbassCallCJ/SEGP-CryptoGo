using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public int NumberOfPlayers;
    public static int DeckSize;

    public static int StaticNumberOfDrawCard;

    public static List<Card> StaticDeck = new List<Card>();

    public List<string> enemyPanelNameList = new List<string>{"Enemy (1)", "Enemy (2)", "Enemy (3)"};
    public int enemyPanelNameListSize; 

    public GameObject Hand;
    public GameObject CardToHand;

    public GameObject CardBack;
    public GameObject Enemy;
    public static GameObject pickCard;
    public static GameObject pickCardZone;
     private GameObject playZone; 

    // Start is called before the first frame update
    public void Start()
    {
        Debug.Log("PlayerDeck.Start() called");

        pickCard = GameObject.Find("PickCard");
        pickCardZone = GameObject.Find("PickCardZone");
        pickCard.SetActive(false);
        // playZone = GameObject.Find("PlayZone");
        // pickCard.SetActive(false);


        NumberOfPlayers = 4;
        DeckSize = CardDatabase.StaticCardList.Count*NumberOfPlayers;
        enemyPanelNameListSize = enemyPanelNameList.Count;

        for(int i = 0; i < NumberOfPlayers; i++){
            foreach(Card card in CardDatabase.StaticCardList){
                deck.Add(card);           
            }
        }
        StaticNumberOfDrawCard = 6;

        foreach(string name in enemyPanelNameList){
            Debug.Log(name);
        }

    }

    IEnumerator StartGame(int numberOfDrawCard){
        Debug.Log("PlayerDeck.StartGame() called");
        // Give Main Player Card
        for(int i = 0; i < numberOfDrawCard; i++){
            yield return new WaitForSeconds(1);
            Hand = GameObject.Find("Hand");
            GameObject _CardToHand = Instantiate(CardToHand, transform.position, transform.rotation,Hand.transform);
            _CardToHand.tag ="Clone";
        }
        StartCoroutine(GiveEnemyCards(numberOfDrawCard,2,enemyPanelNameList[enemyPanelNameListSize-1]));
        
        
    }
    IEnumerator GiveEnemyCards(int numberOfDrawCard,float timeToWait, string enemyPanelName){
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("PlayerDeck.GiveEnemyCards() called");
        Debug.Log(enemyPanelName);
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
    public void StartGame(){
        Shuffle();
        StartCoroutine(StartGame(StaticNumberOfDrawCard));
    }
    
    public void Shuffle(){
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
}
