using System.Runtime.InteropServices;
using UnityEngine;

#nullable enable

namespace OpenRCT2.Bindings
{
    public class Game
    {
        // Hardcoded paths for Xbox UWP Dev Mode
        readonly string _openRCT2DataPath = "E:/OpenRCT2/data";
        readonly string _rct2Path = "E:/Games/Rollercoaster Tycoon 2";
        readonly string? _rct1Path = "E:/Games/Rollercoaster Tycoon 1";

        /// <summary>
        /// Starts the game based on the settings set in the editor.
        /// </summary>
        public Game()
        {
            Debug.Log("Xbox UWP Dev Mode: Using hardcoded paths on E:/ drive.");
        }

        /// <summary>
        /// Starts the game by opening a park.
        /// </summary>
        public void OpenPark(string parkPath)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            StartGame(_openRCT2DataPath, _rct2Path, _rct1Path);
            LoadPark(parkPath);

            string parkname = Park.GetName();
            Debug.Log($"OpenRCT2 started on park: '{parkname}' (startup time: {watch.Elapsed})");
        }

        /// <summary>
        /// Shuts down the game.
        /// </summary>
        public void ClosePark()
        {
            StopGame();
        }

        /// <summary>
        /// Performs a single game update.
        /// </summary>
        public void Update()
        {
            PerformGameUpdate();
        }

        /// <summary>
        /// Starts the game with the specified folder paths.
        /// </summary>
        /// <param name="datapath">The data folder of OpenRCT2.</param>
        /// <param name="rct2path">The absolute path to the RCT2 directory.</param>
        /// <param name="rct1path">The absolute path to the RCT1 directory (optional).</param>
        [DllImport(Plugin.FileName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static extern void StartGame([MarshalAs(UnmanagedType.LPStr)] string datapath, [MarshalAs(UnmanagedType.LPStr)] string rct2path, [MarshalAs(UnmanagedType.LPStr)] string? rct1path = default);

        /// <summary>
        /// Performs a single game update.
        /// </summary>
        [DllImport(Plugin.FileName, CallingConvention = CallingConvention.Cdecl)]
        static extern void PerformGameUpdate();

        /// <summary>
        /// Shuts down the game.
        /// </summary>
        [DllImport(Plugin.FileName, CallingConvention = CallingConvention.Cdecl)]
        static extern void StopGame();

        /// <summary>
        /// Loads a park from the specified path.
        /// </summary>
        [DllImport(Plugin.FileName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        static extern void LoadPark([MarshalAs(UnmanagedType.LPStr)] string path);
    }
}
