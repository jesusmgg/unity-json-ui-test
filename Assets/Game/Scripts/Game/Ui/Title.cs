using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    /// <summary>
    /// Table title, synchronized with the <see cref="JsonUiDataReader"/>.
    /// </summary>
    public class Title : MonoBehaviour
    {
        Text text;

        JsonUiDataReader dataReader;

        void Awake()
        {
            text = GetComponent<Text>();
            dataReader = FindObjectOfType<JsonUiDataReader>();

            text.text = "";
        }

        void OnEnable()
        {
            dataReader.onFileChanged.AddListener(UpdateTitle);
        }

        void OnDisable()
        {
            dataReader.onFileChanged.RemoveListener(UpdateTitle);
        }

        void UpdateTitle()
        {
            text.text = dataReader.uiData.title;
        }
    }
}