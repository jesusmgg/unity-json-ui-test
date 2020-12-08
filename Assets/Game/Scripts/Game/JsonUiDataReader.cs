using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;

namespace Game
{
    /// <summary>
    /// Reads a JSON file from the streaming assets folder, loads the contents into a <see cref="UiData"/> instance
    /// and keeps track of file changes.
    /// </summary>
    public class JsonUiDataReader : MonoBehaviour
    {
        public string fileName = "JsonChallenge.json";
        public UiData uiData;

        string path;
        bool hasPendingChanges;

        FileSystemWatcher fileWatcher;

        public UnityEvent onFileChanged;

        void Start()
        {
            path = $"{Application.streamingAssetsPath}/{fileName}";

            // Setup file watcher
            fileWatcher = new FileSystemWatcher
            {
                Path = Application.streamingAssetsPath,
                Filter = fileName,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Size
            };
            fileWatcher.Changed += ReadJson;
            fileWatcher.EnableRaisingEvents = true;

            // Make the initial file read
            ReadJson();
        }

        void Update()
        {
            // This is needed because the file watcher uses its own thread. The Unity UI won't automatically update if
            // the change isn't coming from the main thread.
            if (hasPendingChanges)
            {
                onFileChanged.Invoke();
                hasPendingChanges = false;
            }
        }

        /// <summary>
        /// Reads the json file at <see cref="path"/>. If the file doesn't exist or contains invalid JSON, does nothing. 
        /// </summary>
        void ReadJson()
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                
                try
                {
                    uiData = JsonConvert.DeserializeObject<UiData>(jsonString);
                    Debug.Log("Data source changes detected.");
                    hasPendingChanges = true;
                }
                catch (JsonSerializationException e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }

        void ReadJson(object sender, FileSystemEventArgs e)
        {
            ReadJson();
        }
    }
}