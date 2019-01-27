using UnityEngine;

namespace Ruffz.UI {

	//Base class for all modals
	public class Modal : MonoBehaviour {

		[SerializeField]
		protected CanvasGroup canvasGroup;

		protected virtual void Awake () {
			if (canvasGroup == null) {
				canvasGroup = GetComponent<CanvasGroup> ();
			}
		}

		public virtual void CloseModal () {
			gameObject.SetActive (false);
			if (canvasGroup != null) {
				canvasGroup.alpha = 0;
			}
			DisableInteractivity ();
		}

		public virtual void Show () {
			gameObject.SetActive (true);
			if (canvasGroup != null) {
				canvasGroup.alpha = 1;
			}
			EnableInteractivity ();
		}

		public bool IsShown() {
			bool opaque = true;
			if (canvasGroup != null) {
				opaque = canvasGroup.alpha > 0;
			}
			return gameObject.activeSelf && opaque;
		}

		protected virtual void EnableInteractivity () {
			if (canvasGroup != null) {
				canvasGroup.interactable = true;
				canvasGroup.blocksRaycasts = true;
			}
		}

		protected virtual void DisableInteractivity () {
			if (canvasGroup != null) {
				canvasGroup.interactable = false;
				canvasGroup.blocksRaycasts = false;
			}
		}
	}
}