using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace TestMod.Content.Buffs
{
	public class Drenched : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = false;
			BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

		public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
		{
			tip = "The water, it burns!";
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ExampleLifeRegenDebuffPlayer>().drenched = true;
		}
	}
	public class ExampleLifeRegenDebuffPlayer : ModPlayer
	{
		public bool drenched;
		public override void ResetEffects()
		{
			drenched = false;
		}
		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			// Check if the damage source was from the "Drenched" debuff
			if (drenched)
			{
				// Set a custom death message when the player dies from the debuff
				damageSource = PlayerDeathReason.ByCustomReason($"{Player.name} finally took a bath.");
			}
			base.Kill(damage, hitDirection, pvp, damageSource); // Call the base method to handle actual death
		}
		public override void UpdateBadLifeRegen()
		{
			if (drenched)
			{
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				Player.lifeRegenTime = 0;

				Player.lifeRegen -= 50;
			}


		}
	}
}
