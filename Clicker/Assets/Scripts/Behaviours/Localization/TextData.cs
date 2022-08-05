using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lokalization
{
    //Must to be on every GameoObject with Text in elements
    public class TextData : MonoBehaviour
    {
        public string Name;
        private TextMeshProUGUI text;
        private Languages currentLanguage;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            Lokalizator.Instance.TextSetter += SetData;
            SetData();
        }

        private void SetData()
        {
            if (text != null)
                text.text = Lokalizator.Instance.GetTextData(Name);
        }
    }
}
