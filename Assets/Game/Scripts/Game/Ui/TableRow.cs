using System.Collections.Generic;
using UnityEngine;

namespace Game.Ui
{
    public class TableRow : MonoBehaviour
    { 
        TableRowText textTemplate;

        void Awake()
        {
            textTemplate = GetComponentInChildren<TableRowText>();
        }

        void InstantiateTexts(List<string> data, bool isHeader = false)
        {
            foreach (string text in data)
            {
                TableRowText.Instantiate(textTemplate.gameObject, transform, text, isHeader);
            } 
            
            // Delete template
            Destroy(textTemplate.gameObject);
        }

        public static TableRow Instantiate(GameObject original, Transform parent, List<string> data, bool isHeader = false)
        {
            GameObject go = GameObject.Instantiate(original, parent);
            TableRow instance = go.GetComponent<TableRow>();
            instance.InstantiateTexts(data, isHeader);

            return instance;           
        }
    }
}