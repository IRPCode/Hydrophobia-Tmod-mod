using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace TestMod.Content.Buffs
{
	public class Soggy : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
		{
			tip = "My boots! My poor boots!";
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<SoggyPlayer>().soggy = true;
		}
	}
	public class SoggyPlayer : ModPlayer
	{
		public bool soggy;
		public override void ResetEffects()
		{
			soggy = false;
		}

		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			if (soggy)
			{
				damageSource = PlayerDeathReason.ByCustomReason($"{Player.name} got their socks wet");
			}
			base.Kill(damage, hitDirection, pvp, damageSource);
		}
		public override void UpdateBadLifeRegen()
		{
			if (soggy)
			{
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;

				Player.lifeRegen -= 5;
			}

		}
	}
}
