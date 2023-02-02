using System.Collections.Generic;
using Mono.Cecil;
using BepInEx.Logging;

namespace ExamplePatcher
{
	public static class Patcher
	{
		// List of assemblies to patch
		public static IEnumerable<string> TargetDLLs { get; } = new[] {"Assembly-CSharp.dll"};

		// Patches the assemblies
		public static void Patch(AssemblyDefinition assembly)
		{
			// Patcher code here

			// BEWARE: patchers CANNOT be debugged using the unity tools as they load too early for that
			// You will need to use logging for debugging the patcher

			// Its highly reommended to make it a habit to include A LOT of debug logging using Logger.LogDebug
			// You will need to configure BepInEx to show debug log

			// Create logger
			ManualLogSource log = Logger.CreateLogSource("Patcher");
			log.LogInfo("Loaded the example patcher for assembly: " + assembly.FullName);
		}
	}
}
