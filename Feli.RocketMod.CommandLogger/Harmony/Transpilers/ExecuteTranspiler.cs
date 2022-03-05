using HarmonyLib;
using Rocket.API;
using Rocket.Core.Commands;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Feli.RocketMod.CommandLogger.Harmony.Transpilers
{
    [HarmonyPatch(typeof(RocketCommandManager)), HarmonyPatch(nameof(RocketCommandManager.Execute))]
    public class ExecuteTranspiler
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Execute(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                CodeInstruction instruction = codes[i];

                if (instruction.opcode == OpCodes.Callvirt && instruction.Calls(AccessTools.Method(typeof(IRocketCommand), nameof(IRocketCommand.Execute))))
                {
                    instruction.opcode = OpCodes.Call;
                    instruction.operand = AccessTools.Method(typeof(ExecuteTranspiler), nameof(OnCommandExecute));
                }
            }

            return codes;
        }

        public static void OnCommandExecute(IRocketCommand command, IRocketPlayer player, string[] args)
        {
            // just to be sure
            if (Plugin.Instance != null)
            {
                Plugin.Instance.LogCommand(player, command, args);
            }

            command.Execute(player, args);
        }
    }
}
