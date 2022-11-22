using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private const string FileName = "save.txt";

    private static readonly string SaveDirectory = Application.dataPath + "/Saves/";

    public static void Init()
    {
        if (Directory.Exists(SaveDirectory) == false)
            Directory.CreateDirectory(SaveDirectory);
    }

    public static void Save(string saveData)
    {
        File.WriteAllText(SaveDirectory + FileName, saveData);
    }

    public static string Load()
    {
        if (File.Exists(SaveDirectory + FileName))
            return File.ReadAllText(SaveDirectory + FileName);
        else
            return null;
    }
}
