using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace FizX.Engine.Input
{
    internal class InputSnapshot
    {

        private readonly HashSet<Keyboard.Key> _downKeys = new HashSet<Keyboard.Key>();
        private readonly HashSet<Keyboard.Key> _upKeys = new HashSet<Keyboard.Key>();

        public bool WasPressedDown(Keyboard.Key key) => _downKeys.Any(k => k == key);

        public bool WasReleased(Keyboard.Key key) => _upKeys.Any(k => k == key);

        public void AddKeyDown(SFML.Window.Keyboard.Key key)
        {
            _downKeys.Add(key);
        }

        public void AddKeyUp(SFML.Window.Keyboard.Key key)
        {
            _upKeys.Add(key);
        }


    }
}
