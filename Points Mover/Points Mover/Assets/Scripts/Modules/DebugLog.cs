using UnityEngine;

namespace Modules
{
    public static class DebugLog 
    {
        public static void LogRed(string message) =>
                Debug.Log($"<color=red>{message}</color>");
    }
}
