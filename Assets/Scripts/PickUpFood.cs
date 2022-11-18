using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(XRGrabInteractable))]
[DisallowMultipleComponent]
public class PickUpFood : MonoBehaviour
{    private XRGrabInteractable XRGrab;
        private Collider[] Colliders;
     
        private void Start()
        {
            XRGrab = GetComponent<XRGrabInteractable>();
            Colliders = GetComponents<Collider>();
     
            XRGrab.selectEntered.AddListener(Grab);
            XRGrab.selectExited.AddListener(Drop);
        }
     
        public void Grab(SelectEnterEventArgs args)
        {
            foreach (Collider collider in Colliders)
                collider.isTrigger = true;
        }
     
        public void Drop(SelectExitEventArgs args)
        {
            foreach (Collider collider in Colliders)
                collider.isTrigger = false;
     
        }
}
