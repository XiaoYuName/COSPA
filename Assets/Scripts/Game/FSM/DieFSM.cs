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
                TaskManager.Instance.TriggerTask(TaskTrigger.击杀BOSS,1);
                AudioManager.Instance.PlayActiveSceneBGM(); //切换成默认场景BGM
                UISystem.Instance.CloseUI("BossStateUI");
                enemy.DamageCollider2D.enabled = false;
            }
            TaskManager.Instance.TriggerTask(TaskTrigger.击杀怪物,1);
            foreach (var skill in enemy.SkillDic)
            {
                enemy.SkillDic[skill.Key].UHandle();
            }
            
            WaitUtils.WaitTimeDo(1, () =>
            {
                if(enemy== null || enemy.gameObject ==null)return;
                enemy.gameObject.SetActive(false);//释放线程池资源
                enemy.DamageCollider2D.enabled = true;
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
