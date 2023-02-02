using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace Sheltered.Patches
{
    /// <summary>
    /// Patch for the Player object to hook into the grabbing system
    /// </summary>
    [HarmonyPatch(typeof(Player))]
    public static class PlayerPatch
    {
        private static Dictionary<Player, bool> _buttonDownStates = new Dictionary<Player, bool>();

        [HarmonyPrefix]
        [HarmonyPatch("Grabability")]
        public static bool Grabability(ref Player __instance, PhysicalObject obj, ref object __result)
        {
            if (!_buttonDownStates.ContainsKey(__instance) || _buttonDownStates[__instance])
                return true; // Cancel
            if (obj is Lizard)
            {
                // Target is a lizard
                Lizard liz = (Lizard)obj;
                if (liz.AI.LikeOfPlayer(liz.AI.tracker.RepresentationForCreature(__instance.abstractCreature, false)) > 0.5f)
                {
                    // Lizard likes the player, prob friend
                    // Check input
                    if (__instance.input.Length >= 1 && __instance.input[0].y < 0 && !__instance.grasps.Any(t => t?.grabbed is Lizard))
                    {
                        // Down is pressed and player is not grabbing the lizard
                        // The grab check is to make sure eg. rideablelizards does not bug

                        // Set result
                        __result = Enum.Parse(typeof(Player).Assembly.GetType("Player+ObjectGrabability"), "OneHand");
                        return false; // Prevent original code from running
                    }
                }
            }
            return true; // Run default code
        }

        [HarmonyPrefix]
        [HarmonyPatch("SlugcatGrab")]
        public static bool SlugcatGrab(ref Player __instance, PhysicalObject obj)
        {
            if (!_buttonDownStates.ContainsKey(__instance) || _buttonDownStates[__instance])
                return true; // Cancel
            if (obj is Lizard)
            {
                // Target is a lizard
                Lizard liz = (Lizard)obj;
                if (liz.AI.LikeOfPlayer(liz.AI.tracker.RepresentationForCreature(__instance.abstractCreature, false)) > 0.5f)
                {
                    // Lizard likes the player, prob friend
                    // Check input
                    if (__instance.input.Length >= 1 && __instance.input[0].y < 0 && !__instance.grasps.Any(t => t?.grabbed is Lizard))
                    {
                        // Down is pressed and player is not grabbing the lizard
                        // The grab check is to make sure eg. rideablelizards does not bug

                        // Prevent duplicate calls
                        _buttonDownStates[__instance] = true;

                        // Check state
                        if (liz.State.unrecognizedSaveStrings.ContainsKey("Sheltered::ShouldStay")
                            && liz.State.unrecognizedSaveStrings["Sheltered::ShouldStay"].ToLower() == "true")
                        {
                            // Staying, make them follow
                            liz.State.unrecognizedSaveStrings["Sheltered::ShouldStay"] = "false";

                            // UI and sound
                            if (__instance.room.game.cameras.Length > 0)
                                __instance.room.game.cameras[0].hud.textPrompt.AddMessage("Lizard is now following", 0, 50, false, false);
                            __instance.PlayHUDSound(SoundID.MENU_Checkbox_Uncheck);
                        }
                        else
                        {
                            // Following, make them stay
                            liz.State.unrecognizedSaveStrings["Sheltered::ShouldStay"] = "true";

                            // UI and sound
                            if (__instance.room.game.cameras.Length > 0)
                                __instance.room.game.cameras[0].hud.textPrompt.AddMessage("Lizard is now staying", 0, 50, false, false);
                            __instance.PlayHUDSound(SoundID.MENU_Checkbox_Check);
                        }

                        return false; // Prevent original code from running
                    }
                }
            }
            return true; // Run default code
        }

        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        public static void Update(ref Player __instance)
        {
            // Called on every frame, used to reset the down state to prevent spam-calling the stay/follow code
            if (__instance.input.Length >= 1 && __instance.input[0].y >= 0)
                _buttonDownStates[__instance] = false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Destroy")]
        public static void Destroy(ref Player __instance)
        {
            // Clean up
            if (_buttonDownStates.ContainsKey(__instance))
                _buttonDownStates.Remove(__instance);
        }
    }
}
