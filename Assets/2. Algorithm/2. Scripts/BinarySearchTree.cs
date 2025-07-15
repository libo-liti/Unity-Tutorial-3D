using System;
using UnityEngine;

public class BinarySearchTree : MonoBehaviour
{
    public class TreeNode
    {
        public TreeNode left;
        public TreeNode right;
        public int value;

        public TreeNode(int value)
        {
            this.value = value;
        }
    }
    private TreeNode _root;
    private int[] _array = { 8, 3, 10, 1, 6, 14, 4, 7, 13 };

    private string _result;

    private void Start()
    {
        foreach (var v in _array)
            _root = Insert(_root, v);
        
        PreOrder(_root);
        Debug.Log($"Preoder : {_result.TrimEnd(',')}");
        
        _result = String.Empty;
        InOrder(_root);
        Debug.Log($"InOder : {_result.TrimEnd(',')}");

        _result = String.Empty;
        PostOrder(_root);
        Debug.Log($"PostOrder : {_result.TrimEnd(',')}");
    }

    private TreeNode Insert(TreeNode node, int value)
    {
        if (node == null)
            return new TreeNode(value);

        if (value < node.value)
            node.left = Insert(node.left, value);
        else
            node.right = Insert(node.right, value);

        return node;
    }

    private void PreOrder(TreeNode node)
    {
        if (node == null)
            return;
        
        _result += $"{node.value} ,";
        PreOrder(node.left);
        PreOrder(node.right);
    }

    private void InOrder(TreeNode node)
    {
        if (node == null)
            return;
        
        InOrder(node.left);
        _result += $"{node.value} ,";
        InOrder(node.right);
    }

    private void PostOrder(TreeNode node)
    {
        if (node == null)
            return;
        
        PostOrder(node.left);
        PostOrder(node.right);
        _result += $"{node.value} ,";
    }
}
