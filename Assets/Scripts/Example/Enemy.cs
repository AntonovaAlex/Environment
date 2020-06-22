using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatePattern
{

	public class Enemy : MonoBehaviour
	{
		protected Transform enemyObj;

		//состояния
		protected enum EnemyFSM
		{
			Attack,
			Flee,
			Stroll,
			MoveTowardsPlayer
		}

		//обновляет объект врага, присваивая ему новое состояние
		public virtual void UpdateEnemy(Transform playerObj)
		{

		}

		//Исполняет действие в зависимости от текущего состояния
		protected void DoAction(Transform playerObj, EnemyFSM enemyMode)
		{
			float fleeSpeed = 10f;
			float strollSpeed = 1f;
			float attackSpeed = 5f;

			switch (enemyMode)
			{
				case EnemyFSM.Attack:
					break;
				case EnemyFSM.Flee:
					//посмотреть в противоположную сторону
					enemyObj.rotation = Quaternion.LookRotation(enemyObj.position - playerObj.position);
					//Двигаться
					enemyObj.Translate(enemyObj.forward * fleeSpeed * Time.deltaTime);
					break;
				case EnemyFSM.Stroll:
					//посмотреть в любую сторону
					Vector3 randomPos = new Vector3(Random.Range(0f, 100f), 0f, Random.Range(0f, 100f));
					enemyObj.rotation = Quaternion.LookRotation(enemyObj.position - randomPos);
					//двигаться
					enemyObj.Translate(enemyObj.forward * strollSpeed * Time.deltaTime);
					break;
				case EnemyFSM.MoveTowardsPlayer:
					//посмотреть в сторону игрока
					enemyObj.rotation = Quaternion.LookRotation(playerObj.position - enemyObj.position);
					//двигаться
					enemyObj.Translate(enemyObj.forward * attackSpeed * Time.deltaTime);
					break;
			}
		}
	}
}
