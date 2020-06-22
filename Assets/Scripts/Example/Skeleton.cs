using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{
	public class Skeleton : Enemy
	{
		EnemyFSM skeletonMode = EnemyFSM.Stroll;

		float health = 100f;

		public Skeleton(Transform skeletonObj)
		{
			base.enemyObj = skeletonObj;
		}

		public override void UpdateEnemy(Transform playerObj)
		{
			//расстояние между скелетом и игроком
			float distance = (base.enemyObj.position - playerObj.position).magnitude;

			switch (skeletonMode)
			{
				case EnemyFSM.Attack:
					if (health < 20f)
					{
						skeletonMode = EnemyFSM.Flee;
					}
					else if (distance > 6f)
					{
						skeletonMode = EnemyFSM.MoveTowardsPlayer;
					}
					break;
				case EnemyFSM.Flee:
					if (health > 60f)
					{
						skeletonMode = EnemyFSM.Stroll;
					}
					break;
				case EnemyFSM.Stroll:
					if (distance < 10f)
					{
						skeletonMode = EnemyFSM.MoveTowardsPlayer;
					}
					break;
				case EnemyFSM.MoveTowardsPlayer:
					//The skeleton has bow and arrow so can attack from distance
					if (distance < 5f)
					{
						skeletonMode = EnemyFSM.Attack;
					}
					else if (distance > 15f)
					{
						skeletonMode = EnemyFSM.Stroll;
					}
					break;
			}

			//Move the enemy based on a state
			DoAction(playerObj, skeletonMode);
		}

	}
}

