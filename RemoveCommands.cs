using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using System.Reflection;
using System.IO;

namespace RemoveCommands
{
    [ApiVersion(2, 1)]
    public class RemoveCommands : TerrariaPlugin
    {
        #region Plugin Info
        public override string Name => "RemoveCommands";
        public override string Author => "BMS";
        public override string Description => "Remove unnecessary  commands";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        #endregion

        public ConfigFile Config = new ConfigFile();

        public RemoveCommands(Main game) : base(game)
        {
        }

        private void LoadConfig()
        {
            string path = Path.Combine(TShock.SavePath, "RemoveCommands.json");
            Config = ConfigFile.Read(path);
        }

        public override void Initialize()
        {
            LoadConfig();
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(disposing);
        }
        private void OnInitialize(EventArgs args)
        {
            foreach (string temp in Config.Commands)
            {
                for (int i = 0; i < Commands.ChatCommands.Count; i++)
                {
                    if (Commands.ChatCommands[i].Names.Contains(temp))
                    {
                        Commands.ChatCommands.Remove(Commands.ChatCommands[i]);
                        continue;
                    }
                }
            }
        }
    }
}
