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

		public static void RegisterShoppableNPC(int npcType) => ShopButton.ShoppableNPCs.Add(npcType);

		public static void UnregisterShoppableNPC(int npcType) => ShopButton.ShoppableNPCs.Remove(npcType);

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
			ShopButton.ResetShoppableNPCList();

			On_Main.GUIChatDrawInner += (orig, self) => CurrentActiveStyle.DrawDialogueStyle();
		}

		public override void Unload()
		{
			DialogueStyleLoader.Unload();
			ChatButtonLoader.Unload();
			ShopButton.UnloadShoppableNPCList();
		}
	}
}