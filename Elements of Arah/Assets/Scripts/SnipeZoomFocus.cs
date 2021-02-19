using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace CreatingCharacters.Abilities
{
    public class SnipeZoomFocus : Ability
    {

        public Transform mc;
        public CinemachineVirtualCamera cv;

        // Start is called before the first frame update
        void Start()
        {
        
        }


        protected override void Update()
        {
            Debug.Log(mc.rotation.x);
            //runt ook base update
            base.Update();
        }

        public override void Cast()
        {
            StartCoroutine(Zoom());
        }

        public virtual IEnumerator Zoom()
        {
            cv.m_Priority = 12;
           
            yield return new WaitForSeconds(2.5f);
      

            cv.m_Priority = 8;
            cv.m_Transitions.m_InheritPosition = false;
            yield return null;
        }
    }
}