using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Lab10_XML
{
    public partial class Form1 : Form
    {
        string document;
        XDocument xmlDocument;

        public Form1()
        {
            InitializeComponent();
            document = fileBox.Text;

            loadButton_Click(null, null);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            document = fileBox.Text;            
            try
            {
                xmlDocument = XDocument.Load(document);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            treeView1.Nodes.Clear();
            XElement root = xmlDocument.Root;

            treeView1.Nodes.Add(root.Name.ToString());
            TreeNode tRoot = treeView1.Nodes[0];
            readXMLTree(root, tRoot);
        }

        public void readXMLTree(XElement root, TreeNode tNode)
        {
            if (!root.HasElements) return;

            IEnumerable<XElement> children = root.Elements();

            foreach (XElement child in children)
            {
                string childName = child.Name.ToString();

                if (child.HasAttributes)
                {
                    childName += " (";
                    for (int i = 0; i < child.Attributes().Count(); i++)
                    {
                        XAttribute att = child.Attributes().ToArray()[i];

                        childName += att.Name.ToString();
                        childName += "=";
                        childName += att.Value.ToString();
                        if (i < child.Attributes().Count() - 1)
                            childName += ", ";

                    }
                    childName += ")";
                }

                var firstTextNode = child.Nodes().OfType<XText>().FirstOrDefault();
                if (firstTextNode != null)
                {
                    var textValue = firstTextNode.Value;


                    childName += ": " + firstTextNode.Value.ToString();
                }

                TreeNode tNewNode = new TreeNode(childName);
                tNode.Nodes.Add(tNewNode);
                readXMLTree(child, tNewNode);
            }
        }

        private XmlElement createXMLNode(XmlDocument doc, TreeNode child)
        {
            string nodeName = "";
            List<string> nodeAttrNames = new List<string>();
            List<string> nodeAttrValues = new List<string>();
            string nodeText = "";

            string tNodeValue = child.Text.ToString();
            string tNodeBeforeText = tNodeValue;
            if (tNodeValue.Contains(":"))
            {
                string[] split = tNodeValue.Split(':');
                nodeText = split[1];
                tNodeBeforeText = split[0];
            }
            else
            {
                nodeName = tNodeValue;
            }

            if (tNodeBeforeText.Contains("("))
            {
                string[] split = tNodeBeforeText.Split(')')[0].Split('(');
                nodeName = split[0];

                string[] split2 = split[1].Split(',');
                foreach (string attr in split2)
                {
                    string[] split3 = attr.Split('=');
                    nodeAttrNames.Add(split3[0]);
                    nodeAttrValues.Add(split3[1]);
                }
            }
            else
            {
                nodeName = tNodeBeforeText;
            }

            nodeName = nodeName.Trim();
            XmlElement newXElem = doc.CreateElement(nodeName);

            for (int i = 0; i < nodeAttrNames.Count; i++)
            {
                newXElem.SetAttribute(nodeAttrNames[i], nodeAttrValues[i]);
            }
            nodeText = nodeText.Trim();
            newXElem.InnerText = nodeText;

            return newXElem;
        }

        private void createXMLTree(XmlDocument doc, XmlElement xmlNode, TreeNode tNode)
        {
            if (xmlNode == null)
            {
                XmlElement newXElem = createXMLNode(doc, tNode);
                doc.AppendChild(newXElem);
                xmlNode = newXElem;
            }

            foreach (TreeNode child in tNode.Nodes)
            {
                XmlElement newXElem = createXMLNode(doc, child);
                xmlNode.AppendChild(newXElem);

                createXMLTree(doc, newXElem, child);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            createXMLTree(doc, null, treeView1.Nodes[0]);

            try
            {
                doc.Save(fileBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        static XElement CreateFileSystemXmlTree(string source)
        {
            DirectoryInfo di = new DirectoryInfo(source);
            return new XElement("Dir",
                new XAttribute("Name", di.Name),
                from d in Directory.GetDirectories(source)
                select CreateFileSystemXmlTree(d),
                from fi in di.GetFiles()
                select new XElement("File",
                    new XElement("Name", fi.Name),
                    new XElement("Size", fi.Length)
                )
            );
        }

        private void directoryButton_Click(object sender, EventArgs e)
        {
            string directoryPath = fileBox.Text;
            XElement directory = null;

            try
            {
                directory = CreateFileSystemXmlTree(directoryPath);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            treeView1.Nodes.Clear();

            treeView1.Nodes.Add(directory.FirstAttribute.Value.ToString());
            TreeNode tRoot = treeView1.Nodes[0];
            readXMLTree(directory, tRoot);

        }
    }
}
