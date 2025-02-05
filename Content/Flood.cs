using Steamworks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TestMod
{
    public class WaterFloodSystem : ModSystem
    {
        private int tick = 0;
        private int dryTimer = 0;
        private int waterLevel = 41; //one block below sky limit

        public override void PostUpdateWorld()
        {
            tick++;

            if (tick <= 10000)
            {
                dryTimer++;
            }

            if (dryTimer == 3000) //sets message and plays sounds //7500
            {
                
                SoundEngine.PlaySound(SoundID.Thunder with { Volume = 1f, Pitch = -1f }); //fix this from not properly playing sounds in multiplayer, use the netmode if statement
                SoundEngine.PlaySound(SoundID.BlizzardStrongLoop with { Volume = 1f, Pitch = -10000f });
                
                if (Main.netMode > NetmodeID.SinglePlayer){
                     NetMessage.SendData(MessageID.ChatText, -1, -1, NetworkText.FromLiteral("Something wicked this way comes..."), 255, 170, 0, 0);
                } else {
                    Main.NewText("Something wicked this way comes...", 170, 0, 0);
                }
                
                Main.windSpeedTarget = .75f;
            }
        
            if (dryTimer == 2000){ // 5000
                SoundEngine.PlaySound(SoundID.Thunder with { Volume = .5f, Pitch = -.75f });
                Main.windSpeedTarget = .5f;
            }

            if (dryTimer == 1000){ //2500
                SoundEngine.PlaySound(SoundID.Thunder with { Volume = .25f, Pitch = -.5f });
                Main.windSpeedTarget = .25f;
            }


            if (tick % 10000 == 0) //every 10k ticks, water is spawned and weather is reset
            {

                if (Main.rainTime < 5000)
                {
                    Main.rainTime = 100000;
                }

                Main.raining = true;
                Main.maxRaining = 1f;
                Main.windSpeedTarget = 1f;

                for (int x = 50; x < Main.maxTilesX - 50; x++)
                {
                    for (int y = waterLevel; y < waterLevel + 1; y++)
                    {
                        Tile tile = Main.tile[x, y];
                        tile.LiquidAmount = 255;
                        tile.LiquidType = LiquidID.Water;
                    }

                }
                Liquid.UpdateLiquid(); //sets liquids
            }
        }
    }
}
