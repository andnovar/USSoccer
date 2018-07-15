using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour {

	public Animator CameraObject;

	[Header("Panels")]
	public string sceneName; 

	[Header("Panels")]
	public GameObject PanelControls;
	public GameObject PanelVideo;
	public GameObject PanelGame;
	public GameObject PanelKeyBindings;
	public GameObject PanelMovement;
	public GameObject PanelCombat;
	public GameObject PanelGeneral;
	public GameObject PanelareYouSure;

	[Header("SFX")]
	public GameObject hoverSound;
	public GameObject sfxhoversound;
	public GameObject clickSound;

	// campaign button sub menu
	[Header("Sub Menu Buttons")]
	public GameObject continueBtn;
	public GameObject newGameBtn;
	public GameObject loadGameBtn;

	// highlights
	[Header("Highlight Effects")]
	public GameObject lineGame;
	public GameObject lineVideo;
	public GameObject lineControls;
	public GameObject lineKeyBindings;
	public GameObject lineMovement;
	public GameObject lineCombat;
	public GameObject lineGeneral;

	public void  PlayCampaign (){
		PanelareYouSure.gameObject.active = false;
		continueBtn.gameObject.active = true;
		newGameBtn.gameObject.active = true;
		loadGameBtn.gameObject.active = true;
	}

	public void NewGame(){
		SceneManager.LoadScene(sceneName);
	}

	public void  DisablePlayCampaign (){
		continueBtn.gameObject.active = false;
		newGameBtn.gameObject.active = false;
		loadGameBtn.gameObject.active = false;
	}

	public void  Position2 (){
		DisablePlayCampaign();
		CameraObject.SetFloat("Animate",1);
	}

	public void  Position1 (){
		CameraObject.SetFloat("Animate",0);
	}

	public void  GamePanel (){
		PanelControls.gameObject.active = false;
		PanelVideo.gameObject.active = false;
		PanelGame.gameObject.active = true;
		//PanelKeyBindings.gameObject.active = false;

		lineGame.gameObject.active = true;
		lineControls.gameObject.active = false;
		lineVideo.gameObject.active = false;
		// lineKeyBindings.gameObject.active = false;
	}

	public void  VideoPanel (){
		PanelControls.gameObject.active = false;
		PanelVideo.gameObject.active = true;
		PanelGame.gameObject.active = false;
		// PanelKeyBindings.gameObject.active = false;

		lineGame.gameObject.active = false;
		lineControls.gameObject.active = false;
		lineVideo.gameObject.active = true;
		// lineKeyBindings.gameObject.active = false;
	}

	public void  ControlsPanel (){
		PanelControls.gameObject.active = true;
		PanelVideo.gameObject.active = false;
		PanelGame.gameObject.active = false;
		// PanelKeyBindings.gameObject.active = false;

		lineGame.gameObject.active = false;
		lineControls.gameObject.active = true;
		lineVideo.gameObject.active = false;
		// lineKeyBindings.gameObject.active = false;
	}

	public void  KeyBindingsPanel (){
		PanelControls.gameObject.active = false;
		PanelVideo.gameObject.active = false;
		PanelGame.gameObject.active = false;
		// PanelKeyBindings.gameObject.active = true;

		lineGame.gameObject.active = false;
		lineControls.gameObject.active = false;
		lineVideo.gameObject.active = true;
		// lineKeyBindings.gameObject.active = true;
	}

	public void  MovementPanel (){
		PanelMovement.gameObject.active = true;
		PanelCombat.gameObject.active = false;
		PanelGeneral.gameObject.active = false;

		lineMovement.gameObject.active = true;
		lineCombat.gameObject.active = false;
		lineGeneral.gameObject.active = false;
	}

	public void  CombatPanel (){
		PanelMovement.gameObject.active = false;
		PanelCombat.gameObject.active = true;
		PanelGeneral.gameObject.active = false;

		lineMovement.gameObject.active = false;
		lineCombat.gameObject.active = true;
		lineGeneral.gameObject.active = false;
	}

	public void  GeneralPanel (){
		PanelMovement.gameObject.active = false;
		PanelCombat.gameObject.active = false;
		PanelGeneral.gameObject.active = true;

		lineMovement.gameObject.active = false;
		lineCombat.gameObject.active = false;
		lineGeneral.gameObject.active = true;
	}

	public void  PlayHover (){
		hoverSound.GetComponent<AudioSource>().Play();
	}

	public void  PlaySFXHover (){
		sfxhoversound.GetComponent<AudioSource>().Play();
	}

	public void  PlayClick (){
		clickSound.GetComponent<AudioSource>().Play();
	}

	// Are You Sure - Quit Panel Pop Up
	public void  AreYouSure (){
		PanelareYouSure.gameObject.active = true;
		DisablePlayCampaign();
	}

	public void  No (){
		PanelareYouSure.gameObject.active = false;
	}

	public void  Yes (){
		Application.Quit();
	}
}