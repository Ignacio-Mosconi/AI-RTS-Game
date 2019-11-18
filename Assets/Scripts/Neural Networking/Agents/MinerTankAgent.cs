using UnityEngine;

namespace GreenNacho.AI.NeuralNetworking
{
    public class MinerTankAgent : TankAgent
    {
        public GameObject NearestMine { get; set; }

        const float CloseMineSqrDistance = 2f;
        const float FitnessMultiplier = 2f;

        Vector3 GetDirectionToNearestMine()
        {
            return (NearestMine.transform.position - transform.position).normalized;
        }

        bool IsCloseToNearestMine()
        {
            return (transform.position - NearestMine.transform.position).sqrMagnitude <= CloseMineSqrDistance;
        }

        protected override void IncreaseFitness()
        {
            genome.Fitness *= FitnessMultiplier;
        }

        public override void Think()
        {
            Vector3 currentDirection = transform.forward;
            Vector3 targetDirection = GetDirectionToNearestMine();

            inputs[0] = targetDirection.x;
            inputs[1] = targetDirection.z;
            inputs[2] = currentDirection.x;
            inputs[3] = currentDirection.z;

            float[] outputs = NeuralNetwork.DoSynapsis(inputs);

            Move(outputs[0], outputs[1]);

            if (IsCloseToNearestMine())
            {
                IncreaseFitness();
                MinerTanksSimulationManager.Instance.RelocateMine(NearestMine);
            }
        }
    }
}