using System;
using System.Collections.Generic;
using System.Text;
using FizX.Engine.Input;
using SFML.Graphics;
using SFML.Window;

namespace FizX.Engine.Input
{
    internal class InputSystem
    {
        //private RenderWindow _window;
        private InputSnapshot _inputSnapshotA, _inputSnapshotB;

        public InputSystem(Window window)
        {
            _inputSnapshotA = new InputSnapshot();
            _inputSnapshotB = new InputSnapshot();

            window.KeyPressed += OnKeyPressed;
            window.KeyReleased += OnKeyReleased;

            window.SetKeyRepeatEnabled(false);
        }

        public InputSnapshot GetInput()
        {
            _inputSnapshotB = _inputSnapshotA;
            _inputSnapshotA = new InputSnapshot();
            return _inputSnapshotB;
        }

        public void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
            _inputSnapshotA.AddKeyDown(e.Code);
        }

        public void OnKeyReleased(object sender, SFML.Window.KeyEventArgs e)
        {
            _inputSnapshotA.AddKeyUp(e.Code);
        }
    }
}
