using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public List<string> cryptoKits = new List<string> {"1","2a","2b","2c","2d"};
    public static List<Card> StaticCardList = new List<Card>();

    void Awake(){
        Debug.Log("CardDatabase.Awake() called");
        AddCards();
    }

    public void AddCards(){
        Debug.Log("CardDatabase.AddCards() called");
        StaticCardList.Add(new Card(0,"SC","A5/1","Originally designed to be used in the GSM protocol. Its design was made public in 1994", "Stream cipher", new List<string>{"2a","2b"}));
        StaticCardList.Add(new Card(1,"BC","AES","Standarized in 2001 -- proposal known as Rijndael --, it has a block length of 128 bits and supports key lenghts of 128, 192 and 256 bits", "Block cipher", new List<string>{"1","2a","2c","2d"}));
        StaticCardList.Add(new Card(2,"H","BLAKE","Hash function proposed in 2008. There are four variants, with 224, 256, 384 and 512-bit output", "Hash function", new List<string>{"2b","2d"}));
        StaticCardList.Add(new Card(3,"OM","CFB","Cipher Feedback mode. Standarized in 2001.", "Operation mode",new List<string>{"2c","2d"}));
        StaticCardList.Add(new Card(4,"AE","CCM","Standarized by NIST in 2004. It combines CTR mode with CBC-MAC.", "Authenticated encryption", new List<string>{"1"}));
        StaticCardList.Add(new Card(5,"MAC","AMAC","Known as ANSI Retail MAC, it is used in combination with DES. ", "Authentication code", new List<string>{"2a","2b","2c","2d"}));
    }



}