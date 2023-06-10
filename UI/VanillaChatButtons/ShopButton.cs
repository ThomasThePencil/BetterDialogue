using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class ShopButton : ChatButton
	{
		internal static Dictionary<int, int> NPCToShopIndex;

		internal static void ResetShopIndexDictionary()
		{
			NPCToShopIndex = new Dictionary<int, int>()
			{
				{ NPCID.Merchant, 1 },
				{ NPCID.ArmsDealer, 2 },
				{ NPCID.Dryad, 3 },
				{ NPCID.Demolitionist, 4 },
				{ NPCID.Clothier, 5 },
				{ NPCID.GoblinTinkerer, 6 },
				{ NPCID.Wizard, -7 },
				{ NPCID.Mechanic, 8 },
				{ NPCID.SantaClaus, 9 },
				{ NPCID.Truffle, 10 },
				{ NPCID.Steampunker, 11 },
				{ NPCID.DyeTrader, 12 },
				{ NPCID.PartyGirl, 13 },
				{ NPCID.Cyborg, 14 },
				{ NPCID.Painter, 15 },
				{ NPCID.WitchDoctor, 16 },
				{ NPCID.Pirate, 17 },
				{ NPCID.Stylist, 18 },
				{ NPCID.TravellingMerchant, 19 },
				{ NPCID.SkeletonMerchant, 20 },
				{ NPCID.DD2Bartender, 21 },
				{ NPCID.Golfer, 22 },
				{ NPCID.BestiaryGirl, 23 },
				{ NPCID.Princess, 24 },
			};
		}

		internal static void UnloadShopIndexDictionary() => NPCToShopIndex = null;

		public override string Text(NPC npc, Player player) => Lang.inter[28].Value;

		public override string Description(NPC npc, Player player) => "See what " + npc.GivenName + " has for sale.";

		public override bool IsActive(NPC npc, Player player) => NPCToShopIndex.ContainsKey(npc.type);

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.playerInventory = true;
			Main.stackSplit = 9999;
			Main.npcChatText = "";
			Main.SetNPCShopIndex(1);
			Main.instance.shop[Main.npcShop].SetupShop(NPCToShopIndex[npc.type]);
		}
	}
}
