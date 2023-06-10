using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;

namespace BetterDialogue.UI.VanillaChatButtons
{
	public class PartyGirlMusicButton : ChatButton
	{
		public override string Text(NPC npc, Player player) => Language.GetTextValue("GameUI.Music");

		public override string Description(NPC npc, Player player) => npc.GivenName + " loves to party, but even the best parties get stale if you don't mix up the beats every once in a while, yeah?";

		public override bool IsActive(NPC npc, Player player)
		{
			if (npc.type != NPCID.PartyGirl)
				return false;

			FieldInfo TOWMusicUnlockedInfo = typeof(Main).GetField("TOWMusicUnlocked", BindingFlags.NonPublic | BindingFlags.Static);
			return (bool)TOWMusicUnlockedInfo.GetValue(null);
		}

		public override void OnClick(NPC npc, Player player)
		{
			SoundEngine.PlaySound(SoundID.MenuTick);
			int num28 = Main.rand.Next(1, 4);
			Main.npcChatText = Language.GetTextValue("PartyGirlSpecialText.Music" + num28);
			FieldInfo swapMusicInfo = typeof(Main).GetField("swapMusic", BindingFlags.NonPublic | BindingFlags.Static)!;
			swapMusicInfo.SetValue(null, !(bool)swapMusicInfo.GetValue(null));
		}
	}
}
