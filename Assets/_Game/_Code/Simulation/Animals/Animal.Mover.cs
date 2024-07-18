using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace Xandudex.LifeGame
{
    internal partial class Animal
    {
        internal class Mover
        {
            NavMeshAgent agent;
            CancellationTokenSource cts;
            public Mover(NavMeshAgent agent)
            {
                this.agent = agent;
            }

            public async Awaitable Move(Vector3 position)
            {
                cts = new();
                CancellationToken token = cts.Token;

                agent.destination = position;
                while (!Arrived())
                    await Awaitable.NextFrameAsync(token);
            }

            public void Stop()
            {
                cts?.Cancel();
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
