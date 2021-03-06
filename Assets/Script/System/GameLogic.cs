using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class GameLogic : MonoBehaviour
{

	[System.Serializable]
	public class AudioData {
		[SerializeField]
		public int m_numGhost = 0;
		[SerializeField]
		public AudioMixerSnapshot m_snapshot = null;
	}


	public class EnemyInfo {
		public GameObject Enemy;
		public NavMeshAgent Agent;
        public EnemyStatus Status;

		// temp
		public bool IsAround { set; get; } = false;
		public bool CanSee { set; get; } = false;
		public bool IsDead { set; get; } = false;

        public SpriteRenderer renderer { set; get; } = null;
		public Material material { set; get; } = null;

		public void ResetEveryFrame() {
			IsAround = false;
			CanSee = false;
		}
	}

	[SerializeField]
	AudioMixer m_mixer;
	[SerializeField]
	AudioData[] m_audios;

	[SerializeField]
	float m_aroundPlayerDistance = 10.0f;
	[SerializeField]
	float m_canSeeDistance = 3.0f;
	//[SerializeField]
	int m_currentAroundEnemy = 0;
	[SerializeField]
	float m_crossFadeTime = 1.0f;


	List<EnemyInfo> m_enemys = new List<EnemyInfo>();
	Player m_player = null;
	float [] m_weights;
	AudioMixerSnapshot[] m_snapshots = null;
	int m_lastMatchIdx = -1;

    [SerializeField] GameObject EnemyManager;
    private EnemyManager _manager;

    private GameOver _over;

    public void Start() {
		var ghosts = GameObject.FindGameObjectsWithTag("EnemyGhost");
		foreach(var ghost in ghosts) {
			var info = new EnemyInfo();
			info.Enemy = ghost;
			info.Agent = ghost.GetComponent<NavMeshAgent>();
            info.Status = ghost.GetComponent<EnemyStatus>();
			info.renderer = ghost.GetComponentInChildren<SpriteRenderer>();
			info.material = info.renderer.material;
		}

		var players = GameObject.FindObjectsOfType<Player>();
		foreach(var player in players) {
			if(player.m_isGhostObject == false) {
				m_player = player;
			}
		}

		m_weights = new float[m_audios.Length];
		var snaps = new List<AudioMixerSnapshot>();
		foreach(var audio in m_audios) {
			snaps.Add(audio.m_snapshot);
		}
		m_snapshots = snaps.ToArray();

        _manager = EnemyManager.GetComponent<EnemyManager>();

        _over = GetComponent<GameOver>();
    }

	public void Update() {
        if (m_player._life <= 0 || GameClear.gameclear)
        {
            Destroy(this.gameObject);
        }
		checkGhostAroundPlayer();
		//updateGhostVisibility();
		updateAudio();
	}

	public void checkGhostAroundPlayer() {
        /*
		m_currentAroundEnemy = 0;
		foreach(var enemy in m_enemys) {
			enemy.ResetEveryFrame();

			if(enemy.IsDead == false) {
				var dir = enemy.Enemy.transform.position - m_player.transform.position;
				if(dir.magnitude < m_aroundPlayerDistance) {
					// ???????????????
					if(enemy.Agent.isStopped == false) {
						m_currentAroundEnemy++;
						enemy.IsAround = true;
					}
					if(dir.magnitude < m_canSeeDistance) {
						// ?????????
						enemy.CanSee = true;
					}
				}
			}
		}*/
        
        m_currentAroundEnemy = 0;
        if (m_player.IsDead)
        {
            m_currentAroundEnemy = 100;
        } else if (_manager.IsRunAnyone)
        {
            m_currentAroundEnemy = 2;
        } else if (_manager.IsDeadGhost3)
        {
            m_currentAroundEnemy = 4;
        }
        else
        {
            m_currentAroundEnemy = -100;
        }
        Debug.Log(m_currentAroundEnemy);
	}

	/*
	public void updateGhostVisibility() {
		foreach(var ghost in m_enemys) {
			var color = ghost.material.color;
			if(ghost.CanSee == false) {
				color.a = 0.0f;
			} else {
				color.a = 1.0f;
			}
			ghost.material.color = color;
		}
	}
	*/

	

	/// <summary>
	/// ????????????????????????????????????????????????????????????????????????????????????????????????
	/// </summary>
	public void updateAudio() {

		int numGhost = m_currentAroundEnemy;
		int matchIdx = 0;
		{
			var idx = 0;
			foreach(var audioInfo in m_audios) {
				if(numGhost >= audioInfo.m_numGhost) {
					matchIdx = idx;
				}
				idx++;
			}
		}

		if(matchIdx != m_lastMatchIdx) {
			for(var i = 0; i < m_weights.Length; ++i) {
				m_weights[i] = (i == matchIdx) ? 1.0f : 0.0f;

				m_mixer.TransitionToSnapshots(m_snapshots, m_weights, m_crossFadeTime);
			}
		}

		m_lastMatchIdx = matchIdx;
	}
}


