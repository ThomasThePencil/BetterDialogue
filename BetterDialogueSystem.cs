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
				int DPRPanelIndex = layers.IndexOf(DPRPanel);
				if (Main.LocalPlayer.sign != -1)
					layers[DPRPanelIndex].Active = false;
				else if (BetterDialogue.SupportedNPCs.Contains(Main.LocalPlayer.TalkNPC.type))
					layers[DPRPanelIndex].Active = false;
			}
		}
	}
}
