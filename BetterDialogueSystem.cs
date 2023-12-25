using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace BetterDialogue
{
	public class BetterDialogueSystem : ModSystem
	{
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			if (Main.LocalPlayer.TalkNPC is null && Main.LocalPlayer.sign == -1)
				return;

			GameInterfaceLayer DPRPanel = layers.FirstOrDefault(x => x.Name == "DialogueTweak: Reworked Dialog Panel");
			if (DPRPanel is not null)
			{
				if (Main.LocalPlayer.sign != -1 || BetterDialogue.SupportedNPCs.Contains(Main.LocalPlayer.TalkNPC.type))
					layers.Remove(DPRPanel);
			}

			GameInterfaceLayer DPRGearPanel = layers.FirstOrDefault(x => x.Name == "DialogueTweak: Panel Style Toggle Button");
			if (DPRGearPanel is not null)
			{
				if (Main.LocalPlayer.sign != -1 || BetterDialogue.SupportedNPCs.Contains(Main.LocalPlayer.TalkNPC.type))
					layers.Remove(DPRGearPanel);
			}
		}
	}
}
