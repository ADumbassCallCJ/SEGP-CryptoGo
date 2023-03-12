using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Photon.Pun;
using Photon.Realtime;
[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : ScriptableObjectSingleton<MasterManager>
{
    [SerializeField]
    private GameSettings _gameSettings;
    public static GameSettings GameSettings { get{ return Instance._gameSettings; }}

    [SerializeField]
    private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();

    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation, GameObject parent){

        foreach (NetworkedPrefab networkedPrefab in Instance._networkedPrefabs){
            if(networkedPrefab.Prefab == obj){
                if(networkedPrefab.Path != string.Empty){
                    GameObject result = PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                    result.transform.SetParent(parent.transform);
                    return result;
                    
                }
                else{
                    Debug.LogError("Path is empty for gameobect name " + networkedPrefab.Prefab);
                    return null;
                }

            }
        }
        return null;
    }
#if UNITY_EDITOR 
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void PopulateNetworkedPrefabs(){

        Instance._networkedPrefabs.Clear();

        GameObject[] results = Resources.LoadAll<GameObject>("");
        for(int i = 0; i < results.Length; i++){
            if(results[i].GetComponent<PhotonView>() != null){
                string path = AssetDatabase.GetAssetPath(results[i]);
                Instance._networkedPrefabs.Add(new NetworkedPrefab(results[i], path));
            }
        }

        for(int i = 0; i < Instance._networkedPrefabs.Count; i++){
            UnityEngine.Debug.Log(Instance._networkedPrefabs[i].Prefab.name + ", " + Instance._networkedPrefabs[i].Path);
        }
    }
#endif
}
