using System.IO;
using UnityEngine;

public class JSONSaveSystem <T>
{
    public void Save(T data, string path)
    {
        if (!File.Exists(path))
        {
            using (var file = new StreamWriter(path))
            {
                file.WriteLine(JsonUtility.ToJson(data));
            }
        }
    }

    public T Load(string path)
    {
        TextAsset loadedData = Resources.Load<TextAsset>(path);
        if (loadedData == null) 
            return default(T);

        var data = JsonUtility.FromJson<T>(loadedData.ToString());
        if(data is ICorrectlyLoaded && !(data as ICorrectlyLoaded).IsDataLoaded)
            return default(T);
      
        return data;
    }
}