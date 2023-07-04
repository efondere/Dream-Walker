using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EditingController : MonoBehaviour
{
    [HideInInspector] public GameObject currentBlock;
    public playerBlocksManager playerBlocksManager;
    private InputManager inputManager;

    private int[,] ints = new int[2,3];
    int currentBlockIndex = 0;
    public float blockMoveStep;
    [Space]
    public Color normalColor;
    private float currentBlockAngle;
    public Color wrongSpawnPosColor;
    public float animateToRedSpeed;
    private bool animateToRed;
    public RectTransform blockSpawnBound;


    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        currentBlock = null;

    }

    public void EditBlock()
    {

        // Change selection
        if (currentBlockIndex != playerBlocksManager.m_currentlySelectedTile)
        {
            if (currentBlock != null)
            {
                currentBlock.SetActive(false);
            }
            currentBlockIndex = playerBlocksManager.m_currentlySelectedTile;
        }



        if (playerBlocksManager.blockList[currentBlockIndex].Count != 0)
        {
            //Get and activate block
            currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
            currentBlock.SetActive(true);

            //Get block components
            Collider2D currentBlockCollider = currentBlock.GetComponent<Collider2D>();
            currentBlockCollider.isTrigger = true;
            SpriteRenderer currentBlockSpriteRenderer = currentBlock.GetComponent<SpriteRenderer>();

            //Set block transparency for editing
            currentBlockSpriteRenderer.color = new Color(currentBlockSpriteRenderer.color.r, currentBlockSpriteRenderer.color.g, currentBlockSpriteRenderer.color.b, 0.2f);


            //currentBlock.transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * blockMoveStep;

            // Block control

            if (!PauseMenu.isPaused)
            {
                currentBlock.transform.position = Camera.main.ScreenToWorldPoint(inputManager.MousePosition()) + new Vector3(0f, 0f, 10f);
            }

            if (inputManager.RotatedBlock() &&  inputManager.BlockRotationDirection() > 0f)
            {
                currentBlockAngle += 90f;
                currentBlock.transform.rotation = Quaternion.Euler(0f, 0f, currentBlockAngle);
            }
            else if (inputManager.RotatedBlock() && inputManager.BlockRotationDirection() < 0f)
            {

                currentBlockAngle -= 90f;
                currentBlock.transform.rotation = Quaternion.Euler(0f, 0f, currentBlockAngle);
            }


            // Drop block
            if (inputManager.MouseClick() && !PauseMenu.isPaused)
            {
                //Collider2D otherCollider = currentBlockCollider.OverlapBox(currentBlock.transform.position, new Vector2(currentBlockCollider.size.x * currentBlock.transform.lossyScale.x, currentBlockCollider.size.y * currentBlock.transform.lossyScale.y), 0f);
                if (!currentBlockCollider.IsTouchingLayers(-1))
                {
                    currentBlockSpriteRenderer.color = normalColor;
                    currentBlockCollider.isTrigger = false;
                    playerBlocksManager.blockList[currentBlockIndex].Remove(currentBlock);
                    playerBlocksManager.useBlock(currentBlockIndex);

                    if (playerBlocksManager.blockList[currentBlockIndex].Count != 0)
                    {
                        currentBlock = playerBlocksManager.blockList[currentBlockIndex][0];
                    }
                    else
                    {
                        currentBlock = null;
                    }
                    // droppedBlock = true;
                }
                else
                {
                    animateToRed = true;
                }



            }


            // Indicating wrong spawn pos
        //    if (animateToRed)
        //    {
        //        IndicateWrongSpawnPos();
        //    }


            // Setting bounds for spawning
            if (currentBlock != null)
            {
                if (currentBlock.transform.position.x - currentBlock.transform.lossyScale.x * 0.5f < blockSpawnBound.position.x - blockSpawnBound.rect.width * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.x - blockSpawnBound.rect.width * 0.5f + currentBlock.transform.lossyScale.x * 0.5f, 0f, 0f);
                }
                if (currentBlock.transform.position.x + currentBlock.transform.lossyScale.x * 0.5f > blockSpawnBound.position.x + blockSpawnBound.rect.width * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.x + blockSpawnBound.rect.width * 0.5f - currentBlock.transform.lossyScale.x * 0.5f, 0f, 0f);

                }
                if (currentBlock.transform.position.y - currentBlock.transform.lossyScale.y * 0.5f < blockSpawnBound.position.y - blockSpawnBound.rect.height * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.y - blockSpawnBound.rect.height * 0.5f + currentBlock.transform.lossyScale.x * 0.5f, 0f, 0f);
                }
                if (currentBlock.transform.position.y + currentBlock.transform.lossyScale.y * 0.5f > blockSpawnBound.position.y + blockSpawnBound.rect.height * 0.5f)
                {
                    currentBlock.transform.position = new Vector3(blockSpawnBound.position.y + blockSpawnBound.rect.height * 0.5f - currentBlock.transform.lossyScale.y * 0.5f, 0f, 0f);
                }
            }
        }
        else
        {
            currentBlock = null;
        }
    }
}
