using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class TableRowText : MonoBehaviour
    {
        Text text;
        
        void Awake()
        {
            text = GetComponent<Text>();

            text.text = "";
        }

        public static TableRowText Instantiate(GameObject original, Transform parent, string text, bool isHeader = false)
        {
            GameObject go = GameObject.Instantiate(original, parent);
            TableRowText instance = go.GetComponent<TableRowText>();
            instance.text.text = text;
            instance.text.fontStyle = isHeader ? FontStyle.Bold : FontStyle.Normal;

            return instance;
        }
    }
}