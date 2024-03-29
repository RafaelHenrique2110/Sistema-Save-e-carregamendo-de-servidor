using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Numerics;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class SaveController : MonoBehaviour
{
    public GameObject prefab;
    public Player player;
    string path;
    string level;
    private void Start()
    {
        path = Application.persistentDataPath + "/save.json";// local do arquivo;
        Debug.Log( path);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Clear();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            level="level01";
           StartCoroutine(LoadOnline()) ;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Space))
        {
            level="level02";
           StartCoroutine(LoadOnline()) ;
        }
        
    }
    void Save()
    {
        Block[] blocks = FindObjectsByType<Block>(FindObjectsSortMode.None);
        PlayerData playerData = AdapterData.GetPlayerData(player);
        List<BlockData> blockDataList = new List<BlockData>();
        foreach (Block block in blocks)
        {
            blockDataList.Add(AdapterData.GetBlockData(block));
        }
        SceneData sceneData = new SceneData();
        sceneData.player = playerData;
        sceneData.blocks = blockDataList.ToArray();// adicionando a lista no array e convetendo a lista em array;
        string json = JsonUtility.ToJson(sceneData);// convertendo para Json;
        File.WriteAllText(path, json); // criando um arquivo com os dados;


    }
    void Load()
    {
        Clear();
        string json = File.ReadAllText(path); // pegamdo arquivo
        SceneData sceneData = JsonUtility.FromJson<SceneData>(json);// convertendo e adicionando o arquivo em SceneData;
        AdapterData.GetPlayer(sceneData.player, ref player); // adicionando a propiadade do arquivo para o player
        foreach (BlockData block in sceneData.blocks)
        {
            Instantiate(prefab, block.position, transform.rotation);
        }
    }
    void Clear()
    {
        // procura por todos os objetos com componente Block
        Block[] blocks = FindObjectsByType<Block>(FindObjectsSortMode.None);
        foreach (Block block in blocks)
        {
            Destroy(block.gameObject);
        }
        Debug.Log("Clear");
    }
    IEnumerator LoadOnline()
    {
        string url = "https://webhistorim.000webhostapp.com/" + level + ".json";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Clear();
            SceneData sceneData = JsonUtility.FromJson<SceneData>(json);// convertendo e adicionando o arquivo em SceneData;
            AdapterData.GetPlayer(sceneData.player, ref player); // adicionando a propiadade do arquivo para o player
            foreach (BlockData block in sceneData.blocks)
            {
                Instantiate(prefab, block.position, transform.rotation);
            }
        }

    }
}
