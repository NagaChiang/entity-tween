using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Entities;

namespace Timespawn.EntityTween.Tests.Common
{
    [TestFixture]
    public abstract class ECSTestFixture
    {
        protected World World { get; private set; }
        protected EntityManager EntityManager { get; private set; }

        [SetUp]
        protected virtual void SetUp()
        {
            World = new World("TestWorld");
            EntityManager = World.EntityManager;

            IReadOnlyList<Type> defaultSystems = DefaultWorldInitialization.GetAllSystems(WorldSystemFilterFlags.Default);
            DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(World, defaultSystems);
            DefaultWorldInitialization.AddSystemsToRootLevelSystemGroups(World, typeof(OverrideTimeSystem));
        }

        [TearDown]
        protected virtual void TearDown()
        {
            World.Dispose();
        }

        protected void OverrideNextDeltaTime(float deltaTime)
        {
            World.GetOrCreateSystem<OverrideTimeSystem>().OverrideNextDeltaTime(deltaTime);
        }
    }
}