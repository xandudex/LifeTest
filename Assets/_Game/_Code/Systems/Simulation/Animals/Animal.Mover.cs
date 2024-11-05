using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Life.Systems.Simulation
{
    internal partial class Animal
    {


        internal class Mover
        {
            bool isMoving;
            NavMeshAgent agent;

            private readonly CancellationToken token;
            public Mover(SimulationSettings settings, NavMeshAgent agent, CancellationToken token)
            {
                this.agent = agent;
                this.token = token;
                agent.speed = settings.AnimalsSpeed;
            }

            public async Awaitable Move(Vector3 position)
            {
                isMoving = true;

                agent.destination = position;
                while (isMoving && !Arrived())
                {
                    await Awaitable.NextFrameAsync(token);
                    if (token.IsCancellationRequested)
                        return;
                }
            }

            public void Stop()
            {
                isMoving = false;
            }


            bool Arrived()
            {
                if (agent.pathPending)
                    return false;

                if (agent.remainingDistance > agent.stoppingDistance)
                    return false;

                if (agent.hasPath && agent.velocity.sqrMagnitude != 0f)
                    return false;

                return true;
            }
        }
    }
}
