using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    private MainMenu _mainMenu;
    private void Awake()
    {
        _mainMenu = GameObject.FindObjectOfType<MainMenu>();
        LoadLevel();
    }
    public void SaveLevel()
    {
        Debug.Log("Saving . . .");
        //ceate or open file
        FileStream File = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);
        //binary formater
        BinaryFormatter formatter = new BinaryFormatter();
        //serialization to write
        formatter.Serialize(File, _mainMenu.data);

        File.Close();
    }
    public void LoadLevel()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/player.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        _mainMenu.data = (Data) formatter.Deserialize(file) as Data;
        file.Close();
    }


}
