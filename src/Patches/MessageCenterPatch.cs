using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Harmony;

using BattleTech;

namespace DarkCrew {
  [HarmonyPatch(typeof(MessageCenter), "PublishMessage")]
  public class MessageCenterPatch {
    static void Postfix(MessageCenter __instance, MessageCenterMessage message) {
      if (message is LevelLoadCompleteMessage) {
        LevelLoadCompleteMessage levelLoadCompleteMessage = (LevelLoadCompleteMessage)message;
        if (levelLoadCompleteMessage.Level == "SimGame") {
          SwapCrewWithDarkSelves();
        }
      }
    }

    private static void SwapCrewWithDarkSelves() {
      SwapCrewMember("chrMdlCrew_sumire_clothing", "chrMdlCrew_sumire_cornea");
      SwapCrewMember("chrMdlCrew_darius_body", "chrMdlCrew_darius_transparent");
      SwapCrewMember("chrMdlCrew_alexander_clothing", "chrMdlCrew_alexander_transparentCornea");
      SwapCrewMember("chrMdlCrew_kamea_clothing", "chrMdlCrew_kamea_cornea");
      SwapCrewMember("chrMdlCrew_farah_clothing", "ChrMdlCrew_farah_cornea");
      SwapCrewMember("chrMdlCrew_yang_body", "chrMdlCrew_yang_transparent");
    }

    private static void SwapCrewMember(string clothingObjectName, string eyeObjectName) {
      GameObject sumireClothing = GameObject.Find(clothingObjectName);
      SkinnedMeshRenderer clothingSkinnedMeshRenderer = sumireClothing.GetComponent<SkinnedMeshRenderer>();
      Material clothingSharedMat = clothingSkinnedMeshRenderer.sharedMaterial;
      clothingSharedMat.color = Color.black;

      GameObject sumireEyes = GameObject.Find(eyeObjectName);
      SkinnedMeshRenderer eyeSkinnedMeshRenderer = sumireEyes.GetComponent<SkinnedMeshRenderer>();
      Material eyeSharedMat = eyeSkinnedMeshRenderer.sharedMaterial;
      eyeSharedMat.shader = Shader.Find("UI/DefaultBackground");
      eyeSharedMat.color = Color.red;
    }
  }
}