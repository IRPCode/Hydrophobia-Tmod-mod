using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TestMod.Content.Buffs;

namespace TestMod
{
    public class MyPlayer : ModPlayer
    {

        public override void PreUpdate()
        {
            int xLoc = Player.Center.ToTileCoordinates().X;
            int yLoc = Player.Center.ToTileCoordinates().Y + 2;

            if (Player.wet)
            {
                Player.AddBuff(ModContent.BuffType<Drenched>(), 100, quiet: false);
            }

            else if (Player.ZoneRain && Main.tile[xLoc, yLoc].WallType == 0)
            {
                if ((Player.HeldItem.type != ItemID.Umbrella) && (Player.HeldItem.type != ItemID.TragicUmbrella) && (Player.armor[0].type != ItemID.UmbrellaHat)){
                    Player.AddBuff(ModContent.BuffType<Soaked>(), 100, quiet: false);
                }
            }

            if (Player.HasBuff(BuffID.Wet))
            {
                Player.AddBuff(ModContent.BuffType<Soaked>(), 100, quiet: false);
            }

            if (Player.HasBuff(BuffID.WaterCandle) || wetBlockChecker(xLoc, yLoc))
            {
                Player.AddBuff(ModContent.BuffType<Soggy>(), 100, quiet: false);
            }

            if (Player.HeldItem.type == ItemID.BottledWater && Player.itemTime > 0) {
                Player.AddBuff(ModContent.BuffType<Drenched>(), 1000, quiet: false);
            }

            base.PreUpdate();
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            Random randomX = new Random();
            Random randomY = new Random();
            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, new Vector2(randomX.Next(-10,10), randomY.Next(-10,10)), ProjectileID.WetBomb, 0, 0f, Player.whoAmI);
           return true;
        }

        public static Boolean wetBlockChecker(int x, int y)
        {
            if (Main.tile[x, y].TileType == TileID.SnowBlock ||
            Main.tile[x, y].TileType == TileID.IceBlock ||
             Main.tile[x, y].TileType == TileID.CorruptIce ||
              Main.tile[x, y].TileType == TileID.HallowedIce ||
               Main.tile[x, y].TileType == TileID.BreakableIce ||
                Main.tile[x, y].TileType == TileID.MagicalIceBlock)
            {
                return true;
            }
            else{
                return false;
            } 
        }
    }
}