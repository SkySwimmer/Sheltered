using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Sheltered.Patches;

namespace Sheltered
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log;
        private void OnEnable()
        {
            Log = Logger;

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded! Applying patches...");
            Harmony.CreateAndPatchAll(typeof(FriendTrackerPatch));
            Harmony.CreateAndPatchAll(typeof(PlayerPatch));
            Harmony.CreateAndPatchAll(typeof(LizardAIPatch));
            Harmony.CreateAndPatchAll(typeof(SlugNPCAIPatch));
        }
    }
}
