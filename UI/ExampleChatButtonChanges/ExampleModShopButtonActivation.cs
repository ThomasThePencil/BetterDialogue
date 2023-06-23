using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Gamepad;

namespace BetterDialogue.UI.ExampleChatButtonChanges
{
	public class ExampleModShopButtonActivation : GlobalNPC
	{
		// This will only load if ExampleMod is active.
		public override bool IsLoadingEnabled(Mod mod) => ModLoader.HasMod("ExampleMod");

		public override void SetStaticDefaults()
		{
			// To add an NPC as a shoppable NPC, all you need to do is call BetterDialogue.RegisterShoppableNPC with their type, like so.
			BetterDialogue.RegisterShoppableNPC(ModContent.Find<ModNPC>("ExampleMod", "ExamplePerson").Type);
			BetterDialogue.RegisterShoppableNPC(ModContent.Find<ModNPC>("ExampleMod", "ExampleTravelingMerchant").Type);
			BetterDialogue.RegisterShoppableNPC(ModContent.Find<ModNPC>("ExampleMod", "ExampleBoneMerchant").Type);
		}
	}
}
