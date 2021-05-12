using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArrayLayout 
{

	

	[System.Serializable]
	public struct rowData
	{
		public Yesses[] row;
	}

	public enum Yesses
	{
		here,
		there,
		everywhere,
		yee,
	}

	public rowData[] rows = new rowData[7]; //Grid of 7x7
}


