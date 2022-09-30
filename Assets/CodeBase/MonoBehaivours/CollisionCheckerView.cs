using codeBase.components;
using Leopotam.EcsLite;
using UnityEngine;

namespace codeBase.monoBehaivours
{
    public class CollisionCheckerView : MonoBehaviour
    {
        public EcsWorld world;

        private void OnCollisionEnter(Collision collision)
        {
            var hit = world.NewEntity();

            var collisionEnter = world.GetPool<EventOnCollisionEnterComponent>();
            collisionEnter.Add(hit);
            ref var collisionEnterComponent = ref collisionEnter.Get(hit);

            collisionEnterComponent.collisionObject = collision.gameObject;
        }

        private void OnCollisionExit(Collision collision)
        {
            var hit = world.NewEntity();

            var collisionExit = world.GetPool<EventOnCollisionExitComponent>();
            collisionExit.Add(hit);
            ref var collisionExitComponent = ref collisionExit.Get(hit);

            collisionExitComponent.collisionObject = collision.gameObject;
        }
    }
}