using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public class UiData
    {
        public string title;
        public List<string> columnHeaders;
        public List<Dictionary<string, string>> data;
    }
}