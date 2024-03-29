using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int amountBlock;
    [SerializeField] TMP_Text  txtAmountBlock ;
    [SerializeField] GameObject prefab;

    Vector3 dir;
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(amountBlock>0){
                DropBlock();
            }
            
        }
         txtAmountBlock.text = amountBlock.ToString();
    }
    public void Move()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        transform.position += dir * speed * Time.deltaTime;
    }
    public void AddBlock(int amountBlock)
    {
        this.amountBlock += amountBlock;
        txtAmountBlock.text = amountBlock.ToString();
    }
    public void RemoveBlock(int amountBlock)
    {
        this.amountBlock -= amountBlock;
        txtAmountBlock.text = amountBlock.ToString();
    }
    public void SetAmountBlock(int val)
    {
        amountBlock  = val;
        txtAmountBlock.text = amountBlock.ToString();
    }
    public int GetAmountBolock() => amountBlock;
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.CompareTag("Block"))
            {
                pickUpBlock(other.gameObject);
                txtAmountBlock.text = amountBlock.ToString();
                AddBlock(1);
            }
        }
    }
    public void pickUpBlock(GameObject block)
    {
        AddBlock(1);
        Destroy(block);
    }

    public void DropBlock()
    {
        RemoveBlock(1);
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
