using ARPG.Config;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ARPG
{
    /// <summary>
    /// 巡逻状态
    /// </summary>
    public class PatrolFSM : FSMBehaviour
    {
        /// <summary>
        /// 目标位置
        /// </summary>
        private Vector3 targetPos;
        private CharacterState EnemyState;


        public override void BehaviourStart(Enemy enemy)
        {
            EnemyState = enemy.data.State;
            SetRandomPoint();
        }
        
        public override void BehaviourUpdate(Enemy enemy)
        {
            //1.如果到达了目标点,重新生成随机坐标
            if (Vector2.Distance(enemy.transform.position, targetPos) < 0.15f)
            {
                SetRandomPoint();
            }
            //到达攻击范围
            if (Vector2.Distance(enemy.transform.position, 
                    GameManager.Instance.Player.transform.position) < enemy.data.Attackradius)
            {
                targetPos = GameManager.Instance.Player.transform.position;
            }
            else
            {
                enemy.transform.position = Vector2.Lerp(enemy.transform.position, 
                    targetPos, Random.value*EnemyState.MovSpeed * Time.deltaTime);
                Flip(enemy);
            }
        }

        /// <summary>
        /// 获取玩家附近随机偏移坐标点
        /// </summary>
        /// <returns></returns>
        private void SetRandomPoint()
        {
            Vector3 Player = GameManager.Instance.Player.transform.position;
            float randomX = Player.x+ Random.Range(-5, 5);
            float randomY = Player.y+Random.Range(-2, 2);
            targetPos = new Vector3(randomX, randomY,0);
        }

        public void Flip(Enemy enemy)
        {
            enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.position.x < targetPos.x ? 180 : 0, 0);
        }

        public override void BehaviourEnd(Enemy enemy)
        {
        }

        public override void OnColliderEnter2D(Collision2D other, Enemy enemy)
        {
            if (other.gameObject.CompareTag($"Ground"))
            {
                SetRandomPoint();
            }
        }

        public override void OnColliderExit2D(Collision2D other, Enemy enemy)
        {
            
        }
    }
}

