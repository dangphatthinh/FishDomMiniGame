using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Data;
using Athena.MiniGame.Fishdom2.GamePlay;

namespace Athena.MiniGame.Fishdom2.InputManagers
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        private bool _inputEneble = true;

        public bool InputEneble
        {
            get => _inputEneble;
            set => _inputEneble = value;
        }

        public void Execute()
        {
            if(InputEneble)
            {
                UpdateInput();
            }                    
        }
        public void UpdateInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider != null && hitInfo.collider.GetComponent<BoxCollider>() != null)
                    {
                         _gameController.ProcessingInput(hitInfo.collider.gameObject.GetComponent<TileStatus>().Index);
                    }
                }
            }
        }
    }
}

