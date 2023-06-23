using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace GameMain
{
    /// <summary>
    /// ÓÎÏ·Èë¿Ú¡£
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static TeamController teamController
        {
            get;
            private set;
        }
        public static TeamManager TeamManager;
        public static DuQuanManager DuQuanManager;
        public static PlayController playController;
        public static PlayGame playGame;
        private static void InitCustomComponents()
        {
            teamController = UnityGameFramework.Runtime.GameEntry.GetComponent<TeamController>();
            TeamManager = UnityGameFramework.Runtime.GameEntry.GetComponent<TeamManager>();
            DuQuanManager = UnityGameFramework.Runtime.GameEntry.GetComponent<DuQuanManager>();
            playController = UnityGameFramework.Runtime.GameEntry.GetComponent<PlayController>();
        }
    }
}
