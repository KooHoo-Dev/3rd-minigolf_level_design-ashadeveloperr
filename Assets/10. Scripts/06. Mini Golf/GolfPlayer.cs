using System;
using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Study3D.MiniGolf
{
    public class GolfPlayer : MonoBehaviour
    {
        [SerializeField] private float fastTimeScale = 3f;
        
        [Header("Settings")]
        public float minPower;
        public float maxPower;
        public float cycleTime;

        [Header("Ref")] 
        public GolfBall ball;
        public GolfAimIndicator aimIndicator;
        
        private float chargeTime;
        private Camera mainCam;
        private float power01; // 0~1 까지로 치환한 세기

        private void Awake()
        {
            mainCam = Camera.main;
        }
        
        private void Update()
        {
            Vector3 aimDirection = mainCam.transform.forward;
            aimDirection.y = 0.0f;
            aimDirection.Normalize();

            bool canShoot = ball.IsStopped;

            // 시간 배속은 발사 가능 여부와 무관하게 처리
            if (SimpleInput.GetKey(Key.Space))
            {
                Time.timeScale = fastTimeScale;
            }
            else if (SimpleInput.GetKeyUp(Key.Space))
            {
                Time.timeScale = 1f;
            }


// 발사 입력은 공이 멈춰 있을 때만 처리
            if (canShoot)
            {
                if (SimpleInput.GetKey(Key.Space))
                {
                    chargeTime += Time.unscaledDeltaTime;
                    power01 = Mathf.PingPong(chargeTime / cycleTime, 1);
                }
                else if (SimpleInput.GetKeyUp(Key.Space))
                {
                    float power = Mathf.Lerp(minPower, maxPower, power01);
                    ball.Shoot(aimDirection, power);

                    chargeTime = 0f;
                    power01 = 0f;
                }
            }
            
            UpdateFeedback(aimDirection, canShoot);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
        
        // 보통 플레이어의 입력정보들에 관련된 UI나 피드백들은
        // 하나의 함수로 모아두는게 좋습니다. (찢지 마세여)
        // (생각외로 엄청 많이 바뀜)
        private void UpdateFeedback(Vector3 direction, bool canShoot)
        {
            if (canShoot)
                aimIndicator.Show(ball.transform.position, direction, power01);
            else
                aimIndicator.Hide();
        }
    }
}