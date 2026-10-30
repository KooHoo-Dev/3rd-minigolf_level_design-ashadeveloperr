using UnityEngine;

namespace Study3D.MiniGolf
{
    public class BoostPad : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float acceleration = 8f;

        private void OnTriggerStay(Collider other)
        {
            GolfBall ball = other.GetComponentInParent<GolfBall>();
            if (ball == null)return;

            Rigidbody rBody = ball.GetComponent<Rigidbody>();
            if (rBody == null) return;

            Vector3 boostDirection = transform.forward;

            rBody.AddForce(
                boostDirection * acceleration,
                ForceMode.Acceleration
            );
        }
    }
}