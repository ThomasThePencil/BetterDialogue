using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class HousingRequestButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("UI.NPCHousing");

		public override double Priority => 100.0;

		public override string Description(NPC npc, Player player) => "How happy might " + npc.GivenName + " be with their current living arrangements? ...probably not very, as they don't have a house.";

		public override bool IsActive(NPC npc, Player player) => NPC.CanShowHomelessText(npc);

		public override void OnClick(NPC npc, Player player)
		{
			Main.npcChatCornerItem = -1;
			SoundEngine.PlaySound(SoundID.MenuTick);
			Main.DoNPCPortraitHop();
			string text = "TownNPCMood_" + NPCID.Search.GetName(npc.netID);
			if (npc.type == NPCID.BestiaryGirl && npc.altTexture == 2)
				text += "Transformed";

			if (npc.type == NPCID.TownDog)
			{
				text = "DogChatter";
			}
			else if (npc.type == NPCID.TownCat)
			{
				text = "CatChatter";
			}
			else if (npc.type == NPCID.TownBunny)
			{
				text = "BunnyChatter";
			}
			else if (NPCID.Sets.IsTownSlime[npc.type])
			{
				string slimeType = Lang.GetSlimeType(npc);
				text = "Slime" + slimeType + "Chatter";
			}

			if (npc.ModNPC is ModNPC modNPC)
				Main.npcChatText = Language.GetTextValue(modNPC.GetLocalizationKey("TownNPCMood") + ".NoHome");
			else
				Main.npcChatText = Language.GetTextValue(text + ".NoHome");
			Main.npcChatText += "\n\n";
			if (npc.type == NPCID.Truffle)
			{
				Main.npcChatText += Language.GetTextValueWith("HousingText.HousingRequirements_Truffle", new
				{
					NPCName = npc.FullName
				});
			}
			else if (npc.ModNPC is ModNPC modNPC2 && Language.Exists($"{modNPC2.GetLocalizationKey("HousingText")}.HousingRequirements"))
			{
				// If Mods.ModName.NPCs.NPCName.HousingText.HousingRequirements exists, use that as the text.
				Main.npcChatText += Language.GetTextValueWith($"{modNPC2.GetLocalizationKey("HousingText")}.HousingRequirements", new
				{
					NPCName = npc.FullName
				});
			}
			else
			{
				Main.npcChatText += Language.GetTextValue("HousingText.HousingRequirements");
			}
		}
	}
}
