﻿using FizX.Core.World;

namespace FizX.Core.ContentLoading;

public interface IWorldLoader
{
    IWorld LoadWorld();
}