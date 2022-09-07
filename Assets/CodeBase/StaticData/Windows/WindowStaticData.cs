using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Window Static Data")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> configs;
    }
}