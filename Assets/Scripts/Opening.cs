using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Opening : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private GameObject creditObject;
    private VideoPlayer _player;
    
    private void Awake()
    {
        _player = GetComponent<VideoPlayer>();
        StartCoroutine(PrepareVideo());
    }

    private IEnumerator PrepareVideo()
    {
        _player.Prepare();
        while (!_player.isPrepared)
        {
            yield return null;
        }

        rawImage.texture = _player.texture;
        _player.Play();
        yield return new WaitForSeconds(30f);
        if (_player.isPlaying)
        {
            yield return null;
        }

        rawImage.DOFade(0f, .5f).OnComplete(() => Destroy(rawImage.gameObject));
    }

    public void OnClickCredit()
    {
        creditObject.SetActive(true);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(1);
    }
}
