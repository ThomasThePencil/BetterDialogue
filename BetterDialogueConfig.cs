using BetterDialogue.UI;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace BetterDialogue
{
	public class BetterDialogueConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[LabelKey("$Mods.BetterDialogue.Config.BoxThiccCs.Label")]
		[TooltipKey("$Mods.BetterDialogue.Config.BoxThiccCs.Description")]
		[Slider()]
		[Range(12, 30)]
		[DefaultValue(15)]
		public int DialogueBoxWidth { get; set; }

		[LabelKey("$Mods.BetterDialogue.Config.BoxHeight.Label")]
		[TooltipKey("$Mods.BetterDialogue.Config.BoxHeight.Description")]
		[Slider()]
		[Range(5, 15)]
		[DefaultValue(10)]
		public int DialogueBoxMaximumLines { get; set; }

		[Header("$Mods.BetterDialogue.Config.SelectStyle.Header")]
		[LabelKey("$Mods.BetterDialogue.Config.SelectStyle.Label")]
		[TooltipKey("$Mods.BetterDialogue.Config.SelectStyle.Description")]
		[OptionStrings(new string[]{"Classic", "Classic Refurbished"})]
		[DefaultValue("Classic")]
		public string DialogueStyle { get; set; }
	}
}