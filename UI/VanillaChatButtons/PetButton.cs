using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class PetButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("UI.PetTheAnimal");

		public override double Priority => 1.0;

		public override string Description(NPC npc, Player player) => "Pet pat pat pet pet pat pot pat pit pot pat pet pet. Yes. :)";

		public override bool IsActive(NPC npc, Player player) => NPCID.Sets.IsTownPet[npc.type];

		public override void OnClick(NPC npc, Player player) => player.PetAnimal(player.talkNPC);
	}
}
