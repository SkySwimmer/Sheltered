using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheltered.Patches
{
    /// <summary>
    /// Patch for the lizard AI
    /// </summary>
    [HarmonyPatch(typeof(LizardAI))]
    public static class LizardAIPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("DetermineBehavior")]
        public static void DetermineBehavior(ref LizardAI __instance, ref LizardAI.Behavior __result)
        {
            // Check state
            Lizard liz = (Lizard)__instance.creature.realizedCreature;
            if (liz.State.unrecognizedSaveStrings.ContainsKey("Sheltered::ShouldStay")
                && liz.State.unrecognizedSaveStrings["Sheltered::ShouldStay"].ToLower() == "true")
            {
                // On stay atm
                // Handle current behaviour and change state or behaviour if needed
                LizardAI.Behavior behavior = __result;
                if (behavior == LizardAI.Behavior.Idle ||
                        behavior == LizardAI.Behavior.InvestigateSound ||
                        behavior == LizardAI.Behavior.GoToSpitPos)
                {
                    // Override to follow so it doesnt wander off
                    __result = LizardAI.Behavior.FollowFriend;
                }
                else if (behavior == LizardAI.Behavior.EscapeRain ||
                      behavior == LizardAI.Behavior.Flee ||
                      behavior == LizardAI.Behavior.Injured ||
                      behavior == LizardAI.Behavior.ReturnPrey ||
                      behavior == LizardAI.Behavior.Hunt ||
                      behavior == LizardAI.Behavior.Frustrated ||
                      behavior == LizardAI.Behavior.Fighting)
                {
                    // Reset state
                    liz.State.unrecognizedSaveStrings["Sheltered::ShouldStay"] = "false";
                    if (liz.room.game.cameras.Length > 0)
                    {
                        if (behavior == LizardAI.Behavior.EscapeRain ||
                                behavior == LizardAI.Behavior.Flee ||
                                behavior == LizardAI.Behavior.Injured)
                            liz.room.game.cameras[0].hud.textPrompt.AddMessage("Lizard got frightened and fled, they will follow when they return", 0, 50, false, false);
                        else if (behavior == LizardAI.Behavior.Frustrated)
                            liz.room.game.cameras[0].hud.textPrompt.AddMessage("Lizard is frustrated and refused to stay where they are", 0, 50, false, false);
                        else
                            liz.room.game.cameras[0].hud.textPrompt.AddMessage("Lizard got distracted and will follow when they return", 0, 50, false, false);
                        liz.room.game.cameras[0].virtualMicrophone.PlaySound(SoundID.MENU_Continue_Game, 0f, 1f, 1f); ;
                    }
                }
            }
        }
    }
}
