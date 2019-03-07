using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
	public string Password
	{
		get { return this.password; }
		set { this.password = value; }
	}

	public int TerminalScore
	{
		get { return this.terminalScore; }
		set { this.terminalScore = value; }
	}
	
	private string password;
	
	private int terminalScore = 0;
	
	private enum TerminalType
	{
		common,
		uncommon,
		rare
	}

	[SerializeField]
	private TerminalType terminalType;

	private void Start()
	{
		switch (terminalType)
		{
				case TerminalType.common:
					terminalScore = 50000;
					password = Guid.NewGuid ().ToString ("N").Substring(0, 3);
					break;
				
				case TerminalType.uncommon:
					terminalScore = 90000;
					password = Guid.NewGuid ().ToString ("N").Substring(0, 5);
					break;
					
				case TerminalType.rare:
					terminalScore = 200000;
					password = Guid.NewGuid ().ToString ("N").Substring(0, 8);
					break;
		}
	}
}
