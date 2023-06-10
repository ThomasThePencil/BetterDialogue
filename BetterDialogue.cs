using BetterDialogue.UI;
using BetterDialogue.UI.VanillaChatButtons;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace BetterDialogue
{
	public class BetterDialogue : Mod
	{
		internal static BetterDialogue Instance;

		public BetterDialogue()
		{
			Instance = this;
		}

		public static void RegisterNPCToShopIndex(int npcType, int shopIndex) => ShopButton.NPCToShopIndex.Add(npcType, shopIndex);

		public static DialogueStyle CurrentActiveStyle
		{
			get
			{
				if (DialogueStyleLoader.DialogueStyleDisplayNames.Contains(ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle))
					return DialogueStyleLoader.DialogueStyles.FirstOrDefault(x => x.DisplayName == ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle);
				else
				{
					ModContent.GetInstance<BetterDialogueConfig>().DialogueStyle = "Classic";
					return DialogueStyle.Classic;
				}
			}
		}

		public override void Load()
		{
			DialogueStyleLoader.Load();
			ChatButtonLoader.Load();
			ShopButton.ResetShopIndexDictionary();

			On_Main.GUIChatDrawInner += (orig, self) => CurrentActiveStyle.DrawDialogueStyle();
		}
	}
}