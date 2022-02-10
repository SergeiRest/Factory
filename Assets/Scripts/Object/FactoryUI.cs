using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FactoryUI : MonoBehaviour
{
	[SerializeField] private FactoryEntity _factory;
	[SerializeField] private TMP_Text _text;

	private void Start()
	{
		_factory.OnFactoryStopped += Show;
		_factory.OnFactoryReboot += Hide;
	}

	private void Show(string text)
	{
		_text.text = text;
		_text.gameObject.SetActive(true);
	}

	private void Hide(string text)
	{
		_text.gameObject.SetActive(false);
	}
}
