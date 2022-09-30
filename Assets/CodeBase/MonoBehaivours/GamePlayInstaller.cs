using codeBase.components;
using codeBase.factories;
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

        private EcsWorld _ecsWorld;
        private GameObject _player;

        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;

        public override void InstallBindings()
        {
            _ecsWorld = new EcsWorld();
            _player = new ObjectFactory().getPlayer(_playerPrefab, _initPoint.position);
            initAllModels();
            InitAllSystems();

            Container
                .Bind<IEcsSystems>()
                .FromInstance(_updateSystems)
                .AsSingle()
                .NonLazy();
        }

        private void initAllModels()
        {
            _initSystems = new EcsSystems(_ecsWorld, _playerSettings)
                .Add(new PlayerInitSystem(_player.GetComponentInChildren<Animator>(), _player.transform))
                .Add(new InitMainCameraSystem(_cameraSettings, _player.transform))
                .Add(new DoorInitSystem())
                .Add(new ButtonInitSystem());
            _initSystems.Init();
        }

        private void InitAllSystems()
        {
            _updateSystems = new EcsSystems(_ecsWorld, _gameDate)
                .Add(new UnityInputSystam())
                .Add(new RaycasrSystam())
                .Add(new PlayerMovementListenerSystem())
                .Add(new PlayerMovementSystem())
                .Add(new CheckMovementSystem())
                .Add(new AnimationSystem())
                .Add(new CameraFollowSystem())
                .Add(new CheckButtonPressedSystem())
                .Add(new CheckButtonReleaseSystem())
                .Add(new DoorStopOpeningSystem())
                .Add(new DoorStartOpeningSystem())
                .Add(new DoorOpeningSystem())
                .DelHere<EventOnCollisionEnterComponent>()
                .DelHere<EventOnCollisionExitComponent>()
                .DelHere<EventMouseInputComponent>()
                .DelHere<EventButtonPressedComponent>()
                .DelHere<EventButtonReleaseComponent>()
                .DelHere<RaycastEventComponent>();
            _updateSystems.Init();
        }

        private void OnDestroy()
        {
            _initSystems.Destroy();
            _updateSystems.Destroy();
            _ecsWorld.Destroy();
        }
    }
}