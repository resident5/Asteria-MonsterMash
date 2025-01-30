using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldEater
{
    public class WorldEaterManagers
    {
        public readonly string GameManager = "GAME MANAGERS";
        public readonly string CameraManager = "Camera Manager";
        public readonly string InputManager = "Input Manager";
        public readonly string DialogueManager = "Dialogue Manager";
        public readonly string VariableManager = "Variable Manager";
        public readonly string InventoryManager = "Inventory Manager";
        public readonly string SceneManager = "Scene Manager";
        public readonly string HUDManager = "UI Canvas";
        public readonly string BattleManager = "Battle Manager";
        public readonly string BattleHUDManager = "Battle HUD";
    }

    public class WorldEaterCameras
    {
        public readonly string MainCamera = "Main Camera";
        public readonly string VirtualCamera = "Virtual Camera";
    }

    public class WorldEaterObjects
    {
        public readonly string PlayerTag = "Player";
    }

}
