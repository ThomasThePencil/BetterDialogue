using BetterDialogue.UI;
using BetterDialogue.UI.Config;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterDialogue
{
	public class BetterDialogueConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[LabelKey("$Mods.BetterDialogue.Configs.BoxThiccCs.Label")]
		[TooltipKey("$Mods.BetterDialogue.Configs.BoxThiccCs.Description")]
		[Slider()]
		[Range(12, 30)]
		[DefaultValue(15)]
		public int DialogueBoxWidth { get; set; }

		[LabelKey("$Mods.BetterDialogue.Configs.BoxHeight.Label")]
		[TooltipKey("$Mods.BetterDialogue.Configs.BoxHeight.Description")]
		[Slider()]
		[Range(5, 15)]
		[DefaultValue(10)]
		public int DialogueBoxMaximumLines { get; set; }

		[Header("$Mods.BetterDialogue.Configs.SelectStyle.Header")]
		[LabelKey("$Mods.BetterDialogue.Configs.SelectStyle.Label")]
		[TooltipKey("$Mods.BetterDialogue.Configs.SelectStyle.Description")]
		[CustomModConfigItem(typeof(AvailableDialogueStyles))]
		[DefaultValue("Classic")]
		public string DialogueStyle { get; set; }
	}
}