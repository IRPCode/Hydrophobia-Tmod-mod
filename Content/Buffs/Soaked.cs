using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace TestMod.Content.Buffs
{
	public class Soaked : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
		{
			tip = "The water has saturated your clothes!";
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<soakedPlayer>().soaked = true;
		}
	}
	public class soakedPlayer : ModPlayer
	{
		public bool soaked;
		public override void ResetEffects()
		{
			soaked = false;
		}

		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			// Check if the damage source was from the "soaked" debuff
			if (soaked)
			{
				// Set a custom death message when the player dies from the debuff
				damageSource = PlayerDeathReason.ByCustomReason($"{Player.name} discovered their shower was too hot.");
			}
			base.Kill(damage, hitDirection, pvp, damageSource); // Call the base method to handle actual death
		}

		public override void UpdateBadLifeRegen()
		{


			if (soaked)
			{
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;

				Player.lifeRegen -= 15;
			}

		}
	}
}
