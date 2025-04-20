using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEater
{
    public static class WorldEaterManagers
    {
        public readonly static string GameManager = "GAME MANAGERS";
        public readonly static string CameraManager = "Camera Manager";
        public readonly static string InputManager = "Input Manager";
        public readonly static string DialogueManager = "Dialogue Manager";
        public readonly static string VariableManager = "Variable Manager";
        public readonly static string InventoryManager = "Inventory Manager";
        public readonly static string SceneManager = "Scene Manager";
        public readonly static string HUDManager = "UI Canvas";
        public readonly static string BattleManager = "Battle Manager";
        public readonly static string BattleHUDManager = "Battle HUD";
    }

    public static class WorldEaterCameras
    {
        public readonly static string WorldCameraHolder = "===== CAMERAS =====";
        public readonly static string MainCamera = "Main Camera";
        public readonly static string VirtualCamera = "Virtual Camera";
    }

    public static class WorldEaterObjects
    {
        public readonly static string PlayerTag = "Player";
    }

}
