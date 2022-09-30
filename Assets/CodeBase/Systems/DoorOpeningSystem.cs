using codeBase.components;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.systems
{
    public class DoorOpeningSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world.Filter<DoorComponent>().Inc<DoorActivatedComponent>().End();

            var doorPool = world.GetPool<DoorComponent>();

            foreach (var entity in filter)
            {
                ref var doorComponent = ref doorPool.Get(entity);

                float moveSpeel = doorComponent.moveSpeed * Time.deltaTime;
                doorComponent.door.position = Vector3.MoveTowards(doorComponent.door.position, doorComponent.open.position, moveSpeel);
            }
        }
    }
}