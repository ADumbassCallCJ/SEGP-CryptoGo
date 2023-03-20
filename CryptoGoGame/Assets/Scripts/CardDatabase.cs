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
        AddCryptoKitsToCards();
    }

    private void AddCards(){
        Debug.Log("CardDatabase.AddCards() called");
        StaticCardList.Add(new Card(0,"SC","A5/1","Originally designed to be used in the GSM protocol. Its design was made public in 1994", "Stream cipher", new List<string>{"2a","2b"}, "low"));
        StaticCardList.Add(new Card(1,"BC","AES","Standarized in 2001 -- proposal known as Rijndael --, it has a block length of 128 bits and supports key lenghts of 128, 192 and 256 bits", "Block cipher", new List<string>{"1","2a","2c","2d"}, "high"));
        StaticCardList.Add(new Card(2,"H","BLAKE","Hash function proposed in 2008. There are four variants, with 224, 256, 384 and 512-bit output", "Hash function", new List<string>{"2b","2d"}, "high"));
        StaticCardList.Add(new Card(3,"OM","CFB","Cipher Feedback mode. Standarized in 2001.", "Operation mode",new List<string>{"2c","2d"}, "medium"));
        StaticCardList.Add(new Card(4,"AE","CCM","Standarized by NIST in 2004. It combines CTR mode with CBC-MAC.", "Authenticated encryption", new List<string>{"1"},"medium"));
        StaticCardList.Add(new Card(5,"MAC","AMAC","Known as ANSI Retail MAC, it is used in combination with DES. ", "Authentication code", new List<string>{"2a","2b","2c","2d"}, "high"));
        StaticCardList.Add(new Card(6,"AE", "EAX", "Introduced by Bellare et al. in 2004, it is very similar to CCM mode. Both encryption and decryption can be performed online.", "Authenticated encryption", "high"));
        StaticCardList.Add(new Card(7,"AE", "GCM", "Galois Counter Mode was designed by McGrew and Viega in 2004, it combines a CTR mode with a Carter-Wegman MAC.", "Authenticated encryption", "high"));
        StaticCardList.Add(new Card(8,"AE", "CWC", "Designed by Kohno et al. in 2004, it combines a Carter-Wegman MAC with CTR mode encryption.", "Authenticated encryption", "medium"));
        StaticCardList.Add(new Card(9,"BC", "Camellia", "Proposed by Matsui et al. in 2004, with Feistel cipher design. It has a block length of 128 bits and supports key lengths of 128, 192 and 256 bits", "Block cipher", "high"));
        StaticCardList.Add(new Card(10,"BC", "Serpent", "Designed by Biham et al, It is one of the ciphers standarized for SSH. It has 128-bit block size and supports 128, 192 and 256-bit key lengths.", "Block cipher", "high"));
        StaticCardList.Add(new Card(11,"BC", "Kasumi", "Proposed in 2011 as a variant of MISTY-1. It has a 128-bit key and 64-bit block size", "Block cipher", "medium"));
        StaticCardList.Add(new Card(12,"BC", "Three-Key 3DES", "Triple DES variant, 64-bit block size and 168-bit ket length", "Block cipher", "medium" ));
        StaticCardList.Add(new Card(13,"BC", "Blowfish >= 80b keys", "Proposed by Bruce Schneier in 1993. It has a 64-bit block size and various key sizes (up to 448 bits)", "Block cipher", "medium"));
        StaticCardList.Add(new Card(14,"BC", "DES", "Symmetric encryption standard until 2001. It has a 64-bit block and 56-bit key size", "Block cipher", "low" ));
    }
    private void AddCryptoKitsToCards(){
        foreach(Card card in StaticCardList){
            if(card.TypeCard == "SC"){
                card.CombinedKits = new List<string>{"2a","2b"};
            }
            else if(card.TypeCard == "BC"){
                card.CombinedKits =  new List<string>{"1","2a","2c","2d"};
            }
            else if(card.TypeCard == "H"){
                card.CombinedKits = new List<string>{"2b","2d"};
            }
            else if(card.TypeCard == "OM"){
                card.CombinedKits = new List<string>{"2c","2d"};
            }
            else if(card.TypeCard == "AE"){
                card.CombinedKits = new List<string>{"1"};
            }
            else if(card.TypeCard == "MAC"){
                card.CombinedKits = new List<string>{"2a","2b","2c","2d"};
            }


        }
    }



}