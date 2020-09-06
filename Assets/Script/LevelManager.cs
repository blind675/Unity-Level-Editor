using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public ColorToPrefab [] colorMappings;

	static private Texture2D [] levels;
	static private int levelIndex = 0;


	void Awake ()
	{
		LoadLevels ();
		GenerateLevel ();
	}

	private void LoadLevels ()
	{
		levels = Resources.LoadAll<Texture2D> ("Levels");
		levelIndex = 0;
	}

	private void GenerateLevel ()
	{
		for (int x = 0; x < GetLevelImageMap ().width; x++) {
			for (int y = 0; y < GetLevelImageMap ().height; y++) {
				GenerateTile (x, y);
			}
		}
	}

	private void GenerateTile (int x, int y)
	{
		Color pixelColor = GetLevelImageMap ().GetPixel (x, y);

		if (pixelColor.a == 0) {
			return;
		}

		foreach (ColorToPrefab colorMapping in colorMappings) {

			if (colorMapping.color.Equals (pixelColor)) {

				Vector3 position = GetTilePositionFromCoordonates (x, y);

				int prefabIndex = 0;

				if (colorMapping.prefabs.Length > 0) prefabIndex = Random.Range (0, colorMapping.prefabs.Length);

				GameObject prefab = colorMapping.prefabs [prefabIndex];

				if (colorMapping.setupType == ColorToPrefab.PrefabSetupType.Spawn) {
					SpawnPrefabAtPosition (prefab, position);
				} else {
					MovePrefabToPosition (prefab, position);
				}

			}
		}
	}

	public void GoToNextLevel ()
	{
		levelIndex++;
		levelIndex = Mathf.Clamp (levelIndex, 0, levels.Length - 1);


		// remove all the previously spawn game objects
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

		GenerateLevel ();
	}

	private Texture2D GetLevelImageMap () => levels [levelIndex];
	// same as
	//private Texture2D GetLevelImageMap () {
	//	return levels [levelIndex];
	//}


	// photo origin is top left, map origin is center -- corect for that
	private Vector3 GetTilePositionFromCoordonates (int x, int y) => new Vector3 (x - GetLevelImageMap ().width / 2, 0, y - GetLevelImageMap ().height / 2);

	private GameObject SpawnPrefabAtPosition (GameObject prefab, Vector3 position) => Instantiate (prefab, position, Quaternion.identity, transform);

	private void MovePrefabToPosition (GameObject prefab, Vector3 position) => prefab.transform.position = position;
}
