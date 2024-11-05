using Life.Factories;
using MessagePipe;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer.Unity;
using Xandudex.Utility.StateMachine;

namespace Life.Systems.Simulation
{
    internal record AnimalEat(Animal Animal);
    internal record AnimalStateChanged(Animal Animal);
    internal partial class Animal : IStateMachine<BaseAnimalState>, IStartable
    {
        Dictionary<Type, BaseAnimalState> states;
        BaseAnimalState currentState;
        Food food;
        Mover mover;

        private readonly GameObject animalObject;
        private readonly Transform animalTransform;
        private readonly IPublisher<AnimalStateChanged> animalStateChangedPub;

        public Animal(GameObject animalObject,
                      GameFactory gameFactory,
                      IPublisher<AnimalStateChanged> animalStateChangedPub)
        {
            this.animalObject = animalObject;
            this.animalTransform = animalObject.transform;
            this.animalStateChangedPub = animalStateChangedPub;
            this.mover = gameFactory.Create<Mover>(animalObject.GetComponent<NavMeshAgent>());

            states = new()
            {
                { typeof(SpawnAnimalState), gameFactory.Create<SpawnAnimalState>(this)},
                { typeof(SearchFoodAnimalState), gameFactory.Create<SearchFoodAnimalState>(this)},
                { typeof(MovingToFoodAnimalState), gameFactory.Create<MovingToFoodAnimalState>(this)},
                { typeof(EatingFoodAnimalState), gameFactory.Create<EatingFoodAnimalState>(this)},
            };
        }

        public BaseAnimalState CurrentState => currentState;
        public Food Food => food;
        public Vector3 Position => animalTransform.position;

        void IStartable.Start()
        {
            ChangeState<SpawnAnimalState>();
        }

        public void ChangeState<K>() where K : BaseAnimalState, IState
        {
            K state = GetState<K>();

            currentState?.Exit();

            currentState = state;
            state.Enter();
            animalStateChangedPub.Publish(new(this));
        }

        public void ChangeState<K, T>(T payload) where K : BaseAnimalState, IPayloadedState<T>
        {
            K state = GetState<K>();

            currentState?.Exit();

            currentState = state;
            state.Enter(payload);
            animalStateChangedPub.Publish(new(this));
        }

        K GetState<K>() where K : BaseAnimalState =>
            states[typeof(K)] as K;
    }
}
