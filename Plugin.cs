using BepInEx;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace iiMenu.AutoUpdater
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        void Awake()
        {
            Console.Title = "ii's Stupid Menu // Loading...";

            using (UnityWebRequest request = UnityWebRequest.Get("https://github.com/iiDk-the-actual/iis.Stupid.Menu/releases/latest/download/iis_Stupid_Menu.dll"))
            {
                UnityWebRequestAsyncOperation operation = request.SendWebRequest();

                while (!operation.isDone) { }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Assembly assembly = Assembly.Load(request.downloadHandler.data);
                    Type Plugin = assembly.GetType("iiMenu.Plugin");

                    if (typeof(MonoBehaviour).IsAssignableFrom(Plugin))
                        gameObject.AddComponent(Plugin);
                }
            }
        }
    }
}
