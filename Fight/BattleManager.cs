﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Fight {
    public class BattleManager : Singleton<BattleManager> {
        static int BattleRound = 0;
        static int ActorInstanceId = 10000;

        public delegate void ActorHandler (Actor _actor);
        public event ActorHandler HeroActorGenerateEvent;
        public event ActorHandler EnemyActorGenerateEvent;
        public event ActorHandler EnemyActorDeadEvent;

        FriendlyActorModule fActorModule;
        HostileActorModule hActorModule;
        NeutralActorModule nActorModule;

        SkillManager skillManager;
        HitDataPool hitDataPool;


        public override void Init () {
            BattleRound++;
            skillManager = new SkillManager();
            hitDataPool = new HitDataPool();
            fActorModule = new FriendlyActorModule();
            hActorModule = new HostileActorModule();
            nActorModule = new NeutralActorModule();
        }

        public int AllocateActorInstanceId () {

            return ActorInstanceId++;
        }

        public int AllocateSkillInstanceId () {

            return skillManager.AllocateSkillInstanceId();
        }

        /// <summary>
        /// 查询指定范围内活着的敌人
        /// </summary>
        /// <param name="_center"></param>
        /// <param name="_radius"></param>
        /// <returns></returns>
        public List<MonsterActor> QueryEnemyActor (Vector2 _center, float _radius) {

            List<MonsterActor> retList = new List<MonsterActor>();
            List<int> enemyInstIdList = hActorModule.EnemyInstIdList;
            MonsterActor enemy = null;
            float distance = 0f;
            Vector3 v3;
            for (int i = 0; i < enemyInstIdList.Count; i++) {
                enemy = hActorModule.EnemyDict[enemyInstIdList[i]];
                v3 = enemy.aTransform.transform.position;
                distance = Vector2.Distance(_center, new Vector2(v3.x, v3.z));
                if (distance < _radius) {
                    retList.Add(enemy);
                }
            }

            return retList;
        }

        /// <summary>
        ///  查询指定范围内活着的敌人
        /// </summary>
        /// <param name="_category">种类</param>
        /// <returns></returns>
        public List<MonsterActor> QueryEnemyActor (EnemyCategory _category) {

            List<MonsterActor> retList = new List<MonsterActor>();
            List<int> enemyInstIdList = hActorModule.EnemyInstIdList;
            MonsterActor enemy = null;
            for (int i = 0; i < enemyInstIdList.Count; i++) {
                enemy = hActorModule.EnemyDict[enemyInstIdList[i]];
                if (enemy.Category == _category) {
                    retList.Add(enemy);
                }
            }

            return retList;
        }

        /// <summary>
        /// 查询某个等级区间的怪物
        /// </summary>
        /// <param name="_minLevel"></param>
        /// <param name="_maxLevel"></param>
        /// <returns></returns>
        public List<MonsterActor> QueryEnemyActor (int _minLevel, int _maxLevel) {

            List<MonsterActor> retList = new List<MonsterActor>();
            List<int> enemyInstIdList = hActorModule.EnemyInstIdList;
            MonsterActor enemy = null;
            for (int i = 0; i < enemyInstIdList.Count; i++) {
                enemy = hActorModule.EnemyDict[enemyInstIdList[i]];
                if (enemy.Level >= _minLevel && enemy.Level <= _maxLevel) {
                    retList.Add(enemy);
                }
            }

            return retList;
        }

        public HitData QueryHitData (int _hitId) {

            return hitDataPool.GetHitData(_hitId);
        }

        public void ProcessHitedActor (List<Actor> _actorList) {

        }
        protected class FriendlyActorModule {

            private HeroActor leader;
            public HeroActor Leader {
                get {
                    return leader;
                }
            }

            private HeroActor henchman;
            public HeroActor Henchman {
                get {
                    return henchman;
                }
            }

            public FriendlyActorModule () {

            }

            public void Creat (HeroActor _heroActor) {
                leader = _heroActor;
            }

        }

        protected class HostileActorModule {

            List<int> enemyInstIdList;
            public List<int> EnemyInstIdList {
                get {
                    return enemyInstIdList;
                }
            }

            Dictionary<int, MonsterActor> enemyDict;
            public Dictionary<int, MonsterActor> EnemyDict {
                get {
                    return enemyDict;
                }
            }

            public HostileActorModule () {

            }

            public void GenerateEnemy () {

            }

            public void KillEnemy () {

            }

        }

        protected class NeutralActorModule {

        }

    }
}

