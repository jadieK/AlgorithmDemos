using System;
using TMPro;
using UnityEngine;

namespace BlockBase
{
    public class BlockBaseGenerator : MonoBehaviour
    {
        public GameObject CubePrefab;
        public GameObject WallPrefab;
        public TMP_InputField WidthInput;
        public TMP_InputField HeightInput;

        public bool generateWalls = true;
        private void Start()
        {
            WidthInput.text = "10";
            HeightInput.text = "10";
        }

        public void OnButtonRegenerate()
        {
            int width = int.Parse(WidthInput.text);
            int height = int.Parse(HeightInput.text);
            BaseMgr.Instance().DestroyBaseCube();
            BaseMgr.Instance().SetBaseSize(width, height);
            BaseMgr.Instance().GenerateBaseCube(CubePrefab, WallPrefab, generateWalls);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null)
                {
                    RaycastHit raycastHit;
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out raycastHit, 100f))
                    {
                        if (raycastHit.transform != null && raycastHit.transform.gameObject.CompareTag("Block"))
                        {
                            var info = raycastHit.transform.gameObject.GetComponent<BlockInfo>();
                            BaseMgr.Instance().SetSelectedBlock(info.IndexX, info.IndexY);
                        }
                    }
                }
            }
        }
    }
}