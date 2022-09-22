using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

[Serializable]
public class GameData
{
    public string sceneName;
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance = null; //static = accessible de n'importe où sans besoin d'être instancié
    public static string dataPath 
    {
        get{return Application.persistentDataPath + "/gameInfo.dat";} //on a qu'un get ==> en read only

    }
    public static bool saveFileExists
    {
        get {return File.Exists(dataPath);}
    }
    private void Awake()
    {
        if(instance == null) //si première fois datamanager est vide, on instancie lui-même
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }
    public static void Save() //si on n'utilise que des attributs static de la class, on peut utiliser une méthode static
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameInfo.dat");
        GameData data = new GameData();
        data.sceneName = SceneManager.GetActiveScene().name;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log($"Save file {Application.persistentDataPath + "/gameInfo.dat"} has been created.");
    }

    public static void Load()
    {
        if(!saveFileExists) return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        GameData data = bf.Deserialize(file) as GameData;
        SceneManager.LoadScene(data.sceneName);
    }
}
