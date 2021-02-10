using System;
using System.Collections.Generic;
using System.Text;

namespace FizX.Core
{
    public class GameScheduler
    {
        public Game Game { get; }
        public decimal TargetTickRate { get; set; } = 60;
        public decimal TargetFrameRate { get; set; } = 60;
        public bool IsRunning { get; set; } = false;

        public GameScheduler(Game game)
        {
            Game = game;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
