using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketLayer : MonoBehaviour
{

    public bool player;
    public int index = 0;

    public void drawLayer(List<int> displayInts, List<int> greys)
    {
        index = 0;
            BracketNode[] nodes = GetComponentsInChildren<BracketNode>();
            Dictionary<string, BracketNode> temp = new Dictionary<string, BracketNode>();
        if (greys[0] != -1)
        {

            foreach (BracketNode n in nodes)
            {
                if (n.tag != "playerablenode")
                {
                    temp.Add(n.gameObject.name.Split(' ')[1], n);
                }
                else
                {
                    n.drawNode(0, displayInts);
                }
            }

            foreach (KeyValuePair<string, BracketNode> node in temp)
            {
                Debug.Log("Node: " + displayInts[index]);
                if (!greys.Contains(displayInts[index]))
                {
                    node.Value.drawNode(index, displayInts, true);
                }
                else
                {
                    node.Value.drawNode(index, displayInts);

                }
                index++;

            }
        }
        else
        {
            foreach (BracketNode n in nodes)
            {
                if (n.tag != "playerablenode")
                {
                    temp.Add(n.gameObject.name.Split(' ')[1], n);
                }
                else
                {
                    n.drawNode(0, displayInts);
                }
            }

            foreach (KeyValuePair<string, BracketNode> node in temp)
            {
                node.Value.drawNode(index, displayInts);
                index++;
            }
        }
    }
}