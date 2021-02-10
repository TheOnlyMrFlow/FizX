using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace FizX.Engine.Input
{
    public static class Input
    {
        private static InputSnapshot _snapshot;

        internal static void SetSnapshot(InputSnapshot snapshot)
        {
            _snapshot = snapshot;
        }

        /// <summary>
        /// Indicates whether the specified key switched from up to down during the frame
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool WasPressedDown(Keyboard.Key key)
        {
            return _snapshot.WasPressedDown(key);
        }

        /// <summary>
        /// Indicates whether the specified key switched from down to up during the frame
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool WasReleased(Keyboard.Key key)
        {
            return _snapshot.WasReleased(key);
        }
    }
}
