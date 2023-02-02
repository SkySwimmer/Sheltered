using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Sheltered.Patches
{
    /// <summary>
    /// Patch for the Lizard AI to control if they will follow the player or not
    /// </summary>
    [HarmonyPatch(typeof(FriendTracker))]
    public static class FriendTrackerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        public static bool Update(ref FriendTracker __instance)
        {
            // Check keys
            if (__instance.creature.State.unrecognizedSaveStrings.ContainsKey("Sheltered::ShouldStay")
                && __instance.creature.State.unrecognizedSaveStrings["Sheltered::ShouldStay"].ToLower() == "true")
            {
                // Make the lizard stay
                __instance.friendDest = __instance.creature.coord;

                // Prevent original code from running
                return false;
            }
            return true; // Allow pathfinding
        }
    }
}
