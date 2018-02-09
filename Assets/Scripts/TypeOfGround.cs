using UnityEngine;
using System.Collections;

public enum TypesOfGrounds {
	Grass,
	Wood,
	Cave
};

public class TypeOfGround : MonoBehaviour {
	public TypesOfGrounds type;

	public TypesOfGrounds getType(){
		return type;
	}
}
