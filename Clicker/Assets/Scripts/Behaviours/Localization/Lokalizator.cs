using System.Collections.Generic;
using UnityEngine;

namespace Lokalization
{
    public delegate void TextDataSetter();

    //Must to be on Game Object with Dont Destroy On Load property
    //Used to change language on every Text in project, that have TextData script
    public class Lokalizator : MonoBehaviour
    {
        public static Lokalizator Instance { get; private set; }
        private Languages language = Languages.English;
        public event TextDataSetter TextSetter;

        public Languages Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    data = TextLoader.GetLanguageData(language);
                    TextSetter?.Invoke();
                }
            }
        }

        private Dictionary<string, string> data = new Dictionary<string, string>();

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            

            data = TextLoader.GetLanguageData(Language);
        }

        public void ChangeLanguage(int id)
        {
            Language = (Languages)id;
        }

        public string GetTextData(string name)
        {
            if(data == null)
                return "error data loading";

            if (!data.ContainsKey(name))
                return "";
            return data[name];
        }
    }

    public enum Languages
    {
        English,
        Ukrainian
    }
}
