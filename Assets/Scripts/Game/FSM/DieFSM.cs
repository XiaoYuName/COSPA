using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public class DieFSM : FSMBehaviour
    {
        public override void BehaviourStart(Enemy enemy)
        {
            if (enemy.data.Type == EnemyType.BOSS && enemy.stateUI != null)
            {
                UISystem.Instance.CloseUI("BossStateUI");
            }
            
            WaitUtils.WaitTimeDo(1, () =>
            {
                if(enemy== null || enemy.gameObject ==null)return;
                enemy.gameObject.SetActive(false);//释放线程池资源
                EnemyManager.Instance.DieCurrentEnemy(enemy);
            });
        }

        public override void BehaviourUpdate(Enemy enemy)
        {
            
        }

        public override void BehaviourEnd(Enemy enemy)
        {
            
        }

        public override void OnColliderEnter2D(Collision2D other, Enemy enemy)
        {
            
        }

        public override void OnColliderExit2D(Collision2D other, Enemy enemy)
        {
            
        }
    }

}
