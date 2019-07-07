using Harmony;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using VRC.Core;
using VRCModLoader;
using VRCTools;
using VRCTools.utils;

namespace Mod
{
	[VRCModInfo("ShowLockInSocial", "1.0", "Bluscream")]
    public class Mod : VRCMod
    {
        public static VRCModInfoAttribute ModInfo = Attribute.GetCustomAttribute(typeof(Mod), typeof(VRCModInfoAttribute)) as VRCModInfoAttribute;
        public static readonly string LOCK_ICON = "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";
        Sprite LOCK_SPRITE; Texture2D LOCK_TEXTURE;
        private static bool patched = false;HarmonyInstance harmonyInstance;
        internal static HarmonyMethod GetPatch(string name, Type type) => new HarmonyMethod(type.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
		private void OnApplicationStart()
		{
            Utils.Log("OnApplicationStart");
            LOCK_TEXTURE = new Texture2D(2, 2);
            Texture2DUtils.LoadImage(LOCK_TEXTURE, Convert.FromBase64String(LOCK_ICON));
            LOCK_SPRITE = Sprite.Create(LOCK_TEXTURE, new Rect(0, 0, LOCK_TEXTURE.width, LOCK_TEXTURE.height), new Vector2(0.5f, 0.5f));
            harmonyInstance = HarmonyInstance.Create(ModInfo.Name);
		}
        void OnLevelWasLoaded(int level) {
            Utils.Log("OnLevelWasLoaded", level);
            if (!patched) {
                Patch();
            }
        }
        void Patch() {
            Utils.Log("Init", ModInfo.ToJson());
            var originalMethod = typeof(UiUserList).GetMethod("LPEPONDFHLA", (BindingFlags)62);
            if (originalMethod is null) return;
            var patchMethod = GetPatch("GenerateButtonFromUser", typeof(Patches));
            harmonyInstance.Patch(originalMethod, transpiler: patchMethod);
            patched = true;
        }
    }
    public class Patches : UiUserList
    {
        public void GenerateButtonFromUser(VRCUiContentButton Button, object ButtonObject)
        {
            Utils.Log("LPEPONDFHLA called!");
            // MethodBase.GetCurrentMethod().Invoke(null, new object[] { Button, ButtonObject });
            if (ButtonObject is APIUser apiUser)
            {
                var location = apiUser.location;
                Utils.Log("Location of", apiUser.displayName, "is", location);
                if (location == "private")
                {
                    Button.nameText.text += " (Private)";
                    Utils.Log("Set", Button.nameText.text.Quote(), "for user", apiUser.displayName.Quote());
                }
            }
        }
    }
}
