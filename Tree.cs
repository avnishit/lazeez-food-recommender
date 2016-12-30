using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;

public class GenericTreeNode
{
    public string data;
    public LinkedList<GenericTreeNode> childList;
    public GenericTreeNode parent;
    public GenericTreeNode(string data)
    {
        this.data = data;
        this.childList = new LinkedList<GenericTreeNode>();
        this.parent = null;
    }

    public void addNodeToChildList(string childData)
    {
        GenericTreeNode temp = new GenericTreeNode(childData);
        temp.parent = this;
        this.childList.AddLast(temp);
    }
    public void removeNodeFromChildList(GenericTreeNode child)
    {
        foreach (GenericTreeNode current in this.childList)
        {
            if (current.data == child.data)
                this.childList.Remove(current);
        }
    }
    public GenericTreeNode searchForTreeNode(string data)
    {
        foreach (GenericTreeNode current in this.childList)
        {
            if (current.data == data)
               return  current;
        }
        return null;
    }
}

