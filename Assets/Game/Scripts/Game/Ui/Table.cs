using System.Collections.Generic;
using UnityEngine;

namespace Game.Ui
{
    /// <summary>
    /// Data table. Has a template <see cref="TableRow"/>, each of which have a template <see cref="TableRowText"/>.
    ///
    /// The data table listens to changes in the data source (<see cref="JsonUiDataReader"/>) and updates the cells
    /// accordingly.
    /// </summary>
    public class Table : MonoBehaviour
    {
        GameObject tableRowTemplate;
        List<TableRow> tableRows = new List<TableRow>();

        JsonUiDataReader dataReader;

        void Awake()
        {
            tableRowTemplate = GetComponentInChildren<TableRow>().gameObject;
            dataReader = FindObjectOfType<JsonUiDataReader>();
        }

        void OnEnable()
        {
            dataReader.onFileChanged.AddListener(UpdateRows);
        }

        void OnDisable()
        {
            dataReader.onFileChanged.RemoveListener(UpdateRows);
        }

        void UpdateRows()
        {
            tableRowTemplate.SetActive(true);
            
            // Delete old rows
            tableRows.ForEach(row => Destroy(row.gameObject));
            tableRows.Clear();

            // Instantiate header
            tableRows.Add(TableRow.Instantiate(tableRowTemplate, transform, dataReader.uiData.columnHeaders, true));

            // Instantiate data rows
            foreach (Dictionary<string, string> dict in dataReader.uiData.data)
            {
                if (dict.Count <= 0)
                {
                    continue;
                }
                
                List<string> texts = new List<string>();
                foreach (string headerKey in dataReader.uiData.columnHeaders)
                {
                    string text = "";
                    if (dict.ContainsKey(headerKey))
                    {
                        text = dict[headerKey];
                    }

                    texts.Add(text);
                }

                tableRows.Add(TableRow.Instantiate(tableRowTemplate, transform, texts));
            }

            // Hide template
            tableRowTemplate.SetActive(false);
        }
    }
}