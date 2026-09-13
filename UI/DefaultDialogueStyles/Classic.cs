using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace BetterDialogue.UI.DefaultDialogueStyles
{
	public class Classic : DialogueStyle
	{
		public override string DisplayName => "Classic";

		public override string Description => "The vanilla dialogue style. Everyone knows it, though not everyone loves it.\n[c/FFFF00:Selected by default.]";

		public override Texture2D DialogueBoxTileSheet => ModContent.Request<Texture2D>("BetterDialogue/UI/DefaultDialogueStyles/Classic_MainBox", AssetRequestMode.ImmediateLoad).Value;
	}
}
