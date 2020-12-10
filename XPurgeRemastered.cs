using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using Logger = Rocket.Core.Logging.Logger;

namespace XPurgeRemastered
{
    public class XPurgeRemastered : RocketPlugin
    {
        protected override void Load()
        {
            Logger.Log(" Plugin loaded correctly!");
            Logger.Log(" More Plugins: www.dvtserver.xyz");

        }

        void FixedUpdate()
        {
            bool isFullMoon = LightingManager.isFullMoon;
            if (isFullMoon && PurgeOn)
            {
                return;
            }
            else if (isFullMoon && !PurgeOn)
            {
                PurgeOn = true;
                ChatManager.serverSendMessage(Translate("purge_start"), UnityEngine.Color.red);
                EffectManager.sendUIEffect(16160, 160, true);
            }
            else if (!isFullMoon && PurgeOn)
            {
                PurgeOn = false;
                ChatManager.serverSendMessage(Translate("purge_end"), UnityEngine.Color.red);
            }
        }

        [RocketCommand("purgeactive", "Command to check is the purge are active")]
        [RocketCommandPermission("xpurge.check")]
        public void PurgeActiveCommand(IRocketPlayer caller, string[] command)
        {
            if (PurgeOn)
            {
                UnturnedChat.Say(caller, Translate("purge_active"), UnityEngine.Color.green);
            }
            else
            {
                UnturnedChat.Say(caller, Translate("purge_not_active"), UnityEngine.Color.green);
            }
        }

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    { "purge_start", "The purge is starting!" },
                    { "purge_end", "The purge are finished." },
                    { "purge_active", "The purge is active." },
                    { "purge_not_active", "The purge is not active." }
                };
            }
        }

        protected override void Unload()
        {
            PurgeOn = false;
            Logger.Log("[XPurgeRemastered] Plugin unloaded correctly!");
        }

        internal bool PurgeOn = false;
    }
}