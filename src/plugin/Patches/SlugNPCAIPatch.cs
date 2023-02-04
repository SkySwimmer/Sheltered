using HarmonyLib;
using MoreSlugcats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheltered.Patches
{
    /// <summary>
    /// Patch for the slugpup AI
    /// </summary>
    [HarmonyPatch(typeof(SlugNPCAI))]
    public static class SlugNPCAIPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("DecideBehavior")]
        public static void DecideBehavior(ref SlugNPCAI __instance)
        {
            // Check state
            Player cat = __instance.cat;
            if (cat.State.unrecognizedSaveStrings.ContainsKey("Sheltered::ShouldStay")
                && cat.State.unrecognizedSaveStrings["Sheltered::ShouldStay"].ToLower() == "true")
            {
                // On stay atm
                // Handle current behaviour and change state or behaviour if needed
                var f = Traverse.Create(__instance).Field("behaviorType");
                SlugNPCAI.BehaviorType behavior = f.GetValue<SlugNPCAI.BehaviorType>();
                if (behavior == SlugNPCAI.BehaviorType.Idle)
                {
                    // Override to follow so it doesnt wander off
                    f.SetValue(SlugNPCAI.BehaviorType.Following);
                }
                else if (behavior == SlugNPCAI.BehaviorType.Fleeing ||
                        behavior == SlugNPCAI.BehaviorType.Attacking ||
                        behavior == SlugNPCAI.BehaviorType.GrabItem)
                {
                    // Reset state
                    cat.State.unrecognizedSaveStrings["Sheltered::ShouldStay"] = "false";
                    if (cat.room.game.cameras.Length > 0)
                    {
                        if (behavior == SlugNPCAI.BehaviorType.Fleeing)
                            cat.room.game.cameras[0].hud.textPrompt.AddMessage("Slugpup got frightened and fled, they will follow when they return", 0, 50, false, false);
                        else
                            cat.room.game.cameras[0].hud.textPrompt.AddMessage("Slugpup got distracted and will follow when they return", 0, 50, false, false);
                        cat.room.game.cameras[0].virtualMicrophone.PlaySound(SoundID.MENU_Continue_Game, 0f, 1f, 1f); ;
                    }
                }
            }
        }
    }
}
