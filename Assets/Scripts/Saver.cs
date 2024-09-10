using System.IO;
using System;
using UnityEngine;


[Serializable]
public class Saver<T>
{
    public T data;

        
    public static void TryLoad(string filename, ref T data)
    {
        string path = FileHandler.Path(filename);
        //Debug.Log(path);
        if(File.Exists(path))
        {
            string dataString = File.ReadAllText(path);
            Saver<T> saver = JsonUtility.FromJson<Saver<T>>(dataString);
            data = saver.data;
        }
            
    }
    public static void Save(string filename, T data)
    {
        Saver<T> wrapper = new Saver<T>();
        wrapper.data = data;
        string dataString = JsonUtility.ToJson(wrapper);
        File.WriteAllText(FileHandler.Path(filename), dataString);            
    }

        
}

public static class FileHandler
{
    public static string Path(string filename)
    {
        //Debug.Log(Application.persistentDataPath + "/");
        return Application.persistentDataPath + "/" + filename;
    }
    public static void Reset(string filename)
    {
        string path = Path(filename);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public static bool HasFile(string filename)
    {
        return File.Exists(Path(filename));
    }
}
