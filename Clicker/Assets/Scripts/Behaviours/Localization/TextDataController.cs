using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lokalization
{
    public static class TextLoader
    {
        private static JSONSaveSystem<TextSaveStorage> saveSystem = new JSONSaveSystem<TextSaveStorage>();
        private static Dictionary<Languages, string> languagesFiles = new Dictionary<Languages, string>()
        {
            { Languages.English, "en-US" },
            { Languages.Ukrainian, "uk-UA" }
        };

        //Used to get Language data and convert it to Dictionary with button name - text element type
        public static Dictionary<string, string> GetLanguageData(Languages languageType)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (!languagesFiles.ContainsKey(languageType))
                return null;

            string pathToData = ResourcesPath.Languages + $"/{languagesFiles[languageType]}";


            var dataStorage = saveSystem.Load(pathToData);

            if (dataStorage == null || dataStorage.data == null)
                return null;

            foreach (var item in dataStorage.data)
            {
                if (!result.ContainsKey(item.Name) && item.IsDataLoaded)
                    result.Add(item.Name, item.Text);
            }

            return result;
        }

    }

    [Serializable]
    public class TextDataToSave:ICorrectlyLoaded
    {
        public string Name;    
        public string Text;

        public bool IsDataLoaded => Name != "";
    }

    [Serializable]
    public class TextSaveStorage
    {
        public List<TextDataToSave> data = new List<TextDataToSave>();

        public TextSaveStorage(List<TextDataToSave> data)
        {
            this.data = data;
        }
    }
}
