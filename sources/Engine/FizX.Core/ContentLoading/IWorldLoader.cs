﻿using FizX.Core.Worlds;

namespace FizX.Core.ContentLoading;

public interface IWorldLoader
{
    World LoadWorld();
}