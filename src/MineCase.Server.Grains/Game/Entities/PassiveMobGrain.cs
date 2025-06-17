using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.World;
using MineCase.Server.World.EntitySpawner;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities
{
    [Reentrant]
    internal class PassiveMobGrain : EntityGrain, IPassiveMob
    {
        protected override void InitializeComponents()
        {
            base.InitializeComponents();

            // await SetComponent(new BlockPlacementComponent());  // 末影人

            // await SetComponent(new DiggingComponent()); // 末影人
            SetComponent(new EntityLifeTimeComponent());
            SetComponent(new EntityOnGroundComponent());
            SetComponent(new HealthComponent());
            SetComponent(new StandaloneHeldItemComponent());
            SetComponent(new NameComponent());
            SetComponent(new DiscoveryRegisterComponent());
            SetComponent(new EntityAiComponent());
        }

        public override Task OnActivateAsync(System.Threading.CancellationToken cancellationToken)
        {
            // ...existing activation logic if any...
            return base.OnActivateAsync(cancellationToken);
        }
    }
}
