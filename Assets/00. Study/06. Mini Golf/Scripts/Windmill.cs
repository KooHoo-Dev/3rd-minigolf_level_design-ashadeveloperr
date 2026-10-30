using UnityEngine;

namespace Study3D.MiniGolf
{
    [RequireComponent(typeof(Rigidbody))]
    public class WindmillRotor : MonoBehaviour
    {
        [Header("Rotation Settings")]
        [SerializeField] private Vector3 rotationAxis = Vector3.forward;
        [SerializeField] private float degreesPerSecond = 60f;

        private Rigidbody rBody;

        private void Awake()
        {
            rBody = GetComponent<Rigidbody>();

            rBody.isKinematic = true;
            rBody.useGravity = false;
            rBody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void FixedUpdate()
        {
            RotateRotor();
        }

        private void RotateRotor()
        {
            float angle =
                degreesPerSecond * Time.fixedDeltaTime;

            Quaternion deltaRotation =
                Quaternion.AngleAxis( angle, rotationAxis.normalized
                );

            Quaternion targetRotation =
                rBody.rotation * deltaRotation;

            rBody.MoveRotation(targetRotation);
        }
    }
}