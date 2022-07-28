using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFS.Script
{
    public class BFSGenerator : MonoBehaviour
    {
        private bool _isRunning = false;
        private Block _startBlock;
        private Block _finishBlock;
        private Block _currentBlock;
        private List<Block> _waitingList = new List<Block>();
        private void Start()
        {
            BaseMgr.Instance().BlockSelected = block =>
            {
                if (_isRunning)
                {
                    return;
                }

                if (_startBlock == null)
                {
                    _startBlock = block;
                    _startBlock.MarkSelected();
                }
                else if (_finishBlock == null)
                {
                    _finishBlock = block;
                    _finishBlock.MarkSelected();
                    _isRunning = true;
                    StartCoroutine(DoBFSGnerate());
                }
            };
        }

        IEnumerator DoBFSGnerate()
        {
            _currentBlock = null;
            _waitingList.Add(_startBlock);
            yield return null;

            while (_currentBlock != _finishBlock)
            {
                if (_currentBlock != null)
                {
                    _currentBlock.MarkVisited();
                }

                _currentBlock = _waitingList[0];
                _waitingList.RemoveAt(0);
                _currentBlock.MarkVisiting();
                for (int directionIndex = 0; directionIndex < _currentBlock.GetDirectionCount(); directionIndex++)
                {
                    var direction = _currentBlock.GetDirection(directionIndex);
                    if (_currentBlock.Neighbors[direction] != null && !_currentBlock.Neighbors[direction].IsVisited && !_waitingList.Contains(_currentBlock.Neighbors[direction]))
                    {
                        _currentBlock.Neighbors[direction].MarkWillVisit();
                        _waitingList.Add(_currentBlock.Neighbors[direction]);
                    }
                }

                yield return null;
            }

            _isRunning = false;
            _startBlock = null;
            _finishBlock = null;
        }
    }
}