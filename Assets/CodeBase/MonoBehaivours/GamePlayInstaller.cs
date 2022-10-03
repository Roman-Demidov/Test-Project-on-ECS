using codeBase.components;
using codeBase.configs;
using codeBase.scriptableObjects;
using codeBase.systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Zenject;

namespace codeBase.monoBehaivours
{
    public class GamePlayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _initPoint;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private GameDate _gameDate;
        [SerializeField] private UpdateECSRun _updateECSRun;

        private EcsWorld _ecsWorld;
        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private GameObject _player;

        public override void InstallBindings()
        {
            _ecsWorld = new EcsWorld();
            _player = createPlayer();
            
            initAllModels();
            InitAllSystems();

            _updateECSRun.init(_updateSystems, _fixedUpdateSystems);
        }

        private void initAllModels()
        {
            _initSystems = new EcsSystems(_ecsWorld)
                .Add(new InitPlayerSystem(_player, _playerSettings))
                .Add(new InitMainCameraSystem(_cameraSettings, _player.transform))
                .Add(new InitDoorSystem())
                .Add(new InitButtonSystem());
            _initSystems.Init();
        }

        private void InitAllSystems()
        {
            _updateSystems = new EcsSystems(_ecsWorld, _gameDate)
                .Add(new UnityInputSystam())
                .Add(new RaycastSystam())
                .Add(new PlayerStartMoveListenerSystem())
                .Add(new PlayerCollisionEnterSystem())
                .Add(new PlayerCollisionExitSystem())
                .Add(new AnimationSystem())
                .Add(new ButtonPressedListenerSystem())
                .Add(new ButtonReleaseListenerSystem())
                .Add(new DoorStartOpeningSystem())
                .Add(new DoorStopOpeningSystem())
                .Add(new RemoveMovementSystem())
                .DelHere<EventPlayerCollisionEnterComponent>()
                .DelHere<EventPlayerCollisionExitComponent>()
                .DelHere<EventButtonPressedComponent>()
                .DelHere<EventButtonReleaseComponent>()
                .DelHere<EventMouseInputComponent>()
                .DelHere<EventRaycastHitGroundComponent>();
            _updateSystems.Init();

            _fixedUpdateSystems = new EcsSystems(_ecsWorld, _gameDate)
                .Add(new CameraFollowSystem())
                .Add(new RotateSystem())
                .Add(new MovementSystem())
                .Add(new UpdateTransformPositionSystem())
                .Add(new UpdateTransformRotationSystem());
            _fixedUpdateSystems.Init();
        }

        private void OnDestroy()
        {
            _initSystems.Destroy();
            _updateSystems.Destroy();
            _ecsWorld.Destroy();
        }

        private GameObject createPlayer()
        {
            return Container.InstantiatePrefab(_playerPrefab, _initPoint.position, Quaternion.identity, null);
        }
    }
}